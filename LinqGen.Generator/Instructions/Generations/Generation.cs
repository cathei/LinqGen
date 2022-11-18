// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
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
        public IdentifierNameSyntax MethodName { get; }

        protected Generation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            MethodName = IdentifierName(expression.MethodSymbol.Name);
            ClassName = IdentifierName($"{expression.SignatureSymbol!.Name}_{id}");
        }

        public abstract TypeSyntax OutputElementType { get; }

        /// <summary>
        /// The qualified class name cached for rendering
        /// </summary>
        public IdentifierNameSyntax ClassName { get; }

        public TypeSyntax ResolvedClassName
        {
            get
            {
                if (Arity == 0)
                    return ClassName;
                return GenericName(ClassName.Identifier, GetTypeArguments()!);
            }
        }

        /// <summary>
        /// Non-operation generations has to be exposed as extension method.
        /// </summary>
        public override MethodKind MethodKind => MethodKind.Extension;

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
            if (Downstream != null)
            {
                foreach (var operation in Downstream)
                {
                    foreach (var member in operation.RenderUpstreamMembers())
                        yield return member;
                }
            }

            if (Evaluations != null)
            {
                foreach (var evaluation in Evaluations)
                {
                    foreach (var member in evaluation.RenderUpstreamMembers())
                        yield return member;
                }
            }
        }

        public virtual IEnumerable<MemberDeclarationSyntax> RenderExtensionMembers()
        {
            if (MethodKind == MethodKind.Extension)
            {
                var parameters = GetParameters(MemberKind.Enumerable, false, true).ToList();
                parameters[0] = parameters[0].WithModifiers(ThisTokenList);

                var expression = ObjectCreationExpression(ResolvedClassName,
                    ArgumentList(GetArguments(MemberKind.Enumerable, false)), null);

                yield return MethodDeclaration(new(AggressiveInliningAttributeList), PublicStaticTokenList,
                    ResolvedClassName, null, MethodName.Identifier, GetTypeParameters(), ParameterList(parameters),
                    GetGenericConstraints(), null, ArrowExpressionClause(expression), SemicolonToken);
            }
        }

        protected abstract IEnumerable<MemberInfo> GetMemberInfos(bool isLocal);

        /// <summary>
        /// Additional initialization statements.
        /// </summary>
        public virtual IEnumerable<StatementSyntax> RenderInitialization(RenderOption option)
        {
            yield break;
        }

        /// <summary>
        /// Dispose statements if needed.
        /// </summary>
        public virtual IEnumerable<StatementSyntax> RenderDispose(RenderOption option)
        {
            yield break;
        }

        /// <summary>
        /// Writes full body of iteration. Can be overriden to change behaviour.
        /// </summary>
        public abstract BlockSyntax RenderIteration(
            RenderOption option, SyntaxList<StatementSyntax> statements);

        public IEnumerable<ParameterSyntax> GetParameters(
            MemberKind kind, bool includeUpstream, bool defaultValue = false)
        {
            if (includeUpstream && Upstream != null)
            {
                foreach (var param in Upstream.GetParameters(kind, includeUpstream))
                    yield return param;
            }

            foreach (var member in GetMemberInfos(false))
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

            foreach (var member in GetMemberInfos(false))
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return member.AsArgument();
            }
        }

        public IEnumerable<MemberDeclarationSyntax> GetFieldDeclarations(MemberKind kind)
        {
            if (Upstream != null)
            {
                foreach (var field in Upstream.GetFieldDeclarations(kind))
                    yield return field;
            }

            foreach (var member in GetMemberInfos(false))
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return FieldDeclaration(default, PrivateTokenList, VariableDeclaration(
                    member.Type, SingletonSeparatedList(VariableDeclarator(member.Name.Identifier))));
            }
        }

        public IEnumerable<StatementSyntax> GetFieldAssignments(MemberKind kind, IdentifierNameSyntax? source = null)
        {
            if (Upstream != null)
            {
                foreach (var assignment in Upstream.GetFieldAssignments(kind, source))
                    yield return assignment;
            }

            foreach (var member in GetMemberInfos(false))
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return ExpressionStatement(SimpleAssignmentExpression(
                    MemberAccessExpression(ThisExpression(), member.Name),
                    source == null ? member.Name : MemberAccessExpression(source, member.Name)));
            }
        }

        public IEnumerable<LocalDeclarationStatementSyntax> GetLocalDeclarations(MemberKind kind)
        {
            if (Upstream != null)
            {
                foreach (var local in Upstream.GetLocalDeclarations(kind))
                    yield return local;
            }

            foreach (var member in GetMemberInfos(true))
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return LocalDeclarationStatement(default, VariableDeclaration(
                    member.Type, SingletonSeparatedList(VariableDeclarator(
                        member.Name.Identifier, null,
                        member.DefaultValue != null ? EqualsValueClause(member.DefaultValue) : null))));
            }
        }

        public IEnumerable<StatementSyntax> GetLocalAssignments(MemberKind kind, ExpressionSyntax? source = null)
        {
            source ??= ThisExpression();

            if (Upstream != null)
            {
                foreach (var assignment in Upstream.GetLocalAssignments(kind, source))
                    yield return assignment;
            }

            foreach (var member in GetMemberInfos(true))
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return ExpressionStatement(SimpleAssignmentExpression(
                    member.Name, MemberAccessExpression(source, member.Name)));
            }
        }
    }
}
