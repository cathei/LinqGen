// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    /// <summary>
    /// Common base class for CompilingGeneration and CompiledGeneration.
    /// So they can provide metadata with same interfaces.
    /// </summary>
    public abstract class Generation : Instruction
    {
        protected Generation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            ClassName = IdentifierName($"{expression.SignatureSymbol!.Name}_{id}");
        }

        public abstract TypeSyntax OutputElementType { get; }

        /// <summary>
        /// The qualified class name cached for rendering
        /// </summary>
        public NameSyntax ClassName { get; }

        // /// <summary>
        // /// If True, method will be embedded as member method
        // /// If false, method will be extension method
        // /// </summary>
        // public virtual bool ShouldBeMemberMethod => false;

        public List<Operation>? Downstream { get; private set; }
        public List<Evaluation>? Evaluations { get; private set; }

        public virtual void SetUpstream(Generation upstream)
        {
            // only operation can have upstream
            throw new NotSupportedException();
        }

        public void AddDownstream(Operation downstream)
        {
            Downstream ??= new List<Operation>();
            Downstream.Add(downstream);
        }

        public void AddEvaluation(Evaluation downstream)
        {
            Evaluations ??= new List<Evaluation>();
            Evaluations.Add(downstream);
        }

        public SourceText Render()
        {
            return GenerationTemplate.Render(this);
        }

        public virtual IEnumerable<MemberDeclarationSyntax> RenderEnumerableMembers()
        {
            if (Evaluations != null)
            {
                foreach (var evaluation in Evaluations)
                {
                    foreach (var member in evaluation.RenderMembers())
                        yield return member;
                }
            }
        }

        protected abstract IEnumerable<MemberInfo> GetMemberInfos();

        public abstract IEnumerable<StatementSyntax> RenderInitialization(RenderOption option);

        /// <summary>
        /// Return value is the return value of MoveNext().
        /// Returning null means the operation will not have breaking condition.
        /// </summary>
        public abstract StatementSyntax? RenderMoveNext(RenderOption option);

        /// <summary>
        /// Return value is the value of Current.
        /// Returning Null means the operation will not change the value of Upstream Current.
        /// </summary>
        public abstract ExpressionSyntax? RenderCurrent(RenderOption option);

        public abstract IEnumerable<StatementSyntax> RenderDispose(RenderOption option);

        /// <summary>
        /// Can be overriden to true for fresh iterations that requires upstream to be ran to the end.
        /// For example, OrderBy or GroupBy.
        /// </summary>
        public virtual bool IsFreshIteration => false;

        /// <summary>
        /// Writes full body of iteration.
        /// </summary>
        public BlockSyntax RenderIteration(RenderOption option)
        {
            var upstreams = new Stack<Generation>();
            {
                var upstream = this;

                while (upstream != null)
                {
                    upstreams.Push(upstream);

                    if (upstream.IsFreshIteration)
                        break;

                    upstream = upstream.Upstream;
                }
            }

            var statements = new List<StatementSyntax>();

            int currentIndex = 0;
            IdentifierNameSyntax currentName = IdentifierName("_invalid_");

            foreach (var upstream in upstreams)
            {
                var moveNext = upstream.RenderMoveNext(option);

                if (moveNext != null)
                {
                    moveNext = moveNext.ReplaceNode(CurrentVar, currentName);
                    statements.Add(moveNext);
                }

                var getCurrent = upstream.RenderCurrent(option);

                if (getCurrent != null)
                {
                    getCurrent = getCurrent.ReplaceNode(CurrentVar, currentName);

                    currentIndex++;
                    currentName = IdentifierName($"current{currentIndex}");

                    statements.Add(LocalDeclarationStatement(currentName.Identifier, getCurrent));
                }
            }

            statements.Add(ReturnStatement(TrueExpression()));

            return Block(WhileStatement(TrueExpression(), Block(statements)), ReturnStatement(FalseExpression()));
        }

        public IEnumerable<ParameterSyntax> GetParameters(
            MemberKind kind, bool includeUpstream, bool defaultValue = false)
        {
            if (includeUpstream && Upstream != null)
            {
                foreach (var param in Upstream.GetParameters(kind, includeUpstream))
                    yield return param;
            }

            foreach (var member in GetMemberInfos())
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return member.AsParameter(defaultValue);
            }
        }

        public IEnumerable<ArgumentSyntax> GetArguments(MemberKind kind, bool includeUpstream)
        {
            if (includeUpstream && Upstream != null)
            {
                foreach (var arg in Upstream.GetArguments(kind, includeUpstream))
                    yield return arg;
            }

            foreach (var member in GetMemberInfos())
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return member.AsArgument();
            }
        }

        public IEnumerable<MemberDeclarationSyntax> GetFieldDeclarations(MemberKind kind, bool isReadOnly)
        {
            if (Upstream != null)
            {
                foreach (var field in Upstream.GetFieldDeclarations(kind, isReadOnly))
                    yield return field;
            }

            foreach (var member in GetMemberInfos())
            {
                if ((member.Kind & kind) != kind)
                    continue;

                var tokenList = isReadOnly ? PrivateReadOnlyTokenList : PrivateTokenList;
                yield return FieldDeclaration(default, tokenList, VariableDeclaration(
                    member.Type, SingletonSeparatedList(VariableDeclarator(member.Name.Identifier))));
            }
        }

        public IEnumerable<StatementSyntax> GetAssignments(MemberKind kind, IdentifierNameSyntax? source = null)
        {
            if (Upstream != null)
            {
                foreach (var assignment in Upstream.GetAssignments(kind, source))
                    yield return assignment;
            }

            foreach (var member in GetMemberInfos())
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return ExpressionStatement(SimpleAssignmentExpression(
                    MemberAccessExpression(ThisExpression(), member.Name),
                    source == null ? member.Name : MemberAccessExpression(source, member.Name)));
            }
        }
    }
}