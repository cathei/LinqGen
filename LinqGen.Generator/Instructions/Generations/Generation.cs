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
            // File name has to use integer id
            FileName = $"LinqGen_{expression.SignatureSymbol!.Name}_{id}.g.cs";

            MethodName = IdentifierName(expression.MethodSymbol.Name);
            ClassName = IdentifierName($"{expression.SignatureSymbol!.Name}_{Id}");
        }

        public abstract ITypeSymbol OutputElementSymbol { get; }
        public abstract TypeSyntax OutputElementType { get; }

        public string FileName { get; }

        /// <summary>
        /// The qualified class name cached for rendering
        /// </summary>
        public IdentifierNameSyntax ClassName { get; }

        public NameSyntax ResolvedClassName
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

        public virtual void AddUpstream(Generation upstream)
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
            var countExpression = RenderCount();

            if (countExpression != null)
            {
                yield return MethodDeclaration(SingletonList(AggressiveInliningAttributeList), PublicTokenList,
                    IntType, null, CountMethod.Identifier, null, EmptyParameterList, default, null,
                    ArrowExpressionClause(countExpression), SemicolonToken);
            }

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

        public virtual ParameterListSyntax GetExtensionMethodParameters()
        {
            var parameters = GetParameters(MemberKind.Enumerable, false, true).ToList();
            parameters[0] = parameters[0].WithModifiers(ThisTokenList);

            return ParameterList(parameters);
        }

        public IEnumerable<MemberDeclarationSyntax> RenderExtensionClassMembers()
        {
            if (MethodKind == MethodKind.Extension)
            {
                var expression = ObjectCreationExpression(ResolvedClassName,
                    ArgumentList(GetArguments(MemberKind.Enumerable, false)), null);

                yield return MethodDeclaration(new(AggressiveInliningAttributeList), PublicStaticTokenList,
                    ResolvedClassName, null, MethodName.Identifier, GetTypeParameters(), GetExtensionMethodParameters(),
                    GetGenericConstraints(), null, ArrowExpressionClause(expression), SemicolonToken);
            }

            if (Evaluations != null)
            {
                foreach (var evaluation in Evaluations)
                {
                    foreach (var member in evaluation.RenderExtensionMembers())
                        yield return member;
                }
            }
        }

        public virtual TypeSyntax EnumerableInterfaceType =>
            GenericName(Identifier("IInternalStub"), TypeArgumentList(OutputElementType));

        protected abstract IEnumerable<MemberInfo> GetMemberInfos(bool isLocal);

        public abstract bool SupportPartition { get; }

        public bool SupportCount => RenderCount() != null;

        /// <summary>
        /// This means the operation will locally run to the end on timing of initialization.
        /// For example, OrderBy needs to run to end first to sort the upstream.
        /// </summary>
        protected virtual bool ClearsUpstreamEnumerator => false;

        private bool ShouldIgnoreUpstream(MemberKind kind)
        {
            if ((kind & MemberKind.Enumerator) == 0)
                return false;
            return ClearsUpstreamEnumerator;
        }

        /// <summary>
        /// Returns null if cannot get count without iteration.
        /// </summary>
        public abstract ExpressionSyntax? RenderCount();

        /// <summary>
        /// Additional initialization statements.
        /// </summary>
        public virtual IEnumerable<StatementSyntax> RenderInitialization(bool isLocal, ExpressionSyntax source,
            ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            yield break;
        }

        /// <summary>
        /// Dispose statements if needed.
        /// </summary>
        public virtual IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
        {
            yield break;
        }

        /// <summary>
        /// Writes full body of iteration. Can be overriden to change behaviour.
        /// </summary>
        public abstract BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements);

        public IEnumerable<ParameterSyntax> GetParameters(
            MemberKind kind, bool includeUpstream, bool defaultValue = false)
        {
            if (includeUpstream && Upstream != null && !ShouldIgnoreUpstream(kind))
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
            if (includeUpstream && Upstream != null && !ShouldIgnoreUpstream(kind))
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
            if (Upstream != null && !ShouldIgnoreUpstream(kind))
            {
                foreach (var field in Upstream.GetFieldDeclarations(kind))
                    yield return field;
            }

            foreach (var member in GetMemberInfos(false))
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return FieldDeclaration(PrivateTokenList, member.Type, member.Name.Identifier);
            }
        }

        public IEnumerable<StatementSyntax> GetFieldAssignments(MemberKind kind, IdentifierNameSyntax? source = null)
        {
            if (Upstream != null && !ShouldIgnoreUpstream(kind))
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

        public IEnumerable<StatementSyntax> GetFieldDefaultAssignments(MemberKind kind)
        {
            if (Upstream != null && !ShouldIgnoreUpstream(kind))
            {
                foreach (var assignment in Upstream.GetFieldDefaultAssignments(kind))
                    yield return assignment;
            }

            foreach (var member in GetMemberInfos(false))
            {
                if ((member.Kind & kind) != kind)
                    continue;

                if (member.DefaultValue == null)
                    continue;

                yield return ExpressionStatement(SimpleAssignmentExpression(
                    MemberAccessExpression(ThisExpression(), member.Name), member.DefaultValue));
            }
        }

        public IEnumerable<LocalDeclarationStatementSyntax> GetLocalDeclarations(MemberKind kind)
        {
            if (Upstream != null && !ShouldIgnoreUpstream(kind))
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

            if (Upstream != null && !ShouldIgnoreUpstream(kind))
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

        public bool HasGetEnumerator { get; private set; }

        public IEnumerable<MemberDeclarationSyntax> RenderGetEnumerator()
        {
            HasGetEnumerator = true;

            yield return EnumeratorTemplate.Render(this);

            yield return MethodDeclaration(SingletonList(AggressiveInliningAttributeList), PublicTokenList,
                IdentifierName("Enumerator"), null, GetEnumeratorMethod.Identifier, null, ParameterList(),
                default, null, ArrowExpressionClause(ObjectCreationExpression(
                    IdentifierName("Enumerator"), ArgumentList(ThisExpression()), null)), SemicolonToken);
        }

        /// <summary>
        /// Used for local evaluation
        /// </summary>
        public bool HasLocalVisitMethod { get; private set; }

        public MethodDeclarationSyntax RenderLocalVisitMethod()
        {
            HasLocalVisitMethod = true;

            var visitorInterface = GenericName(Identifier("IVisitor"), TypeArgumentList(OutputElementType));
            var visitorType = new TypeParameterInfo(IdentifierName("TVisitor"), visitorInterface);
            var visitorName = IdentifierName("visitor");

            var initialDeclarations = GetLocalDeclarations(MemberKind.Enumerator)
                .Concat(GetLocalAssignments(MemberKind.Both))
                .Concat(RenderInitialization(true, ThisExpression(), null, null));

            StatementSyntax accumulationStatement = IfStatement(LogicalNotExpression(InvocationExpression(
                    MemberAccessExpression(visitorName, VisitMethod), ArgumentList(CurrentPlaceholder))),
                ReturnStatement());

            var iterationBlock = RenderIteration(true, SingletonList(accumulationStatement));

            var disposeStatements = RenderDispose(true);

            var iterationStatements = iterationBlock.Statements;

            StatementSyntax tryStatement = TryStatement(
                Block(iterationStatements), default, FinallyClause(Block(disposeStatements)));

            var body = Block(initialDeclarations.Append(tryStatement));

            return MethodDeclaration(SingletonList(AggressiveInliningAttributeList), PublicTokenList, VoidType, null,
                VisitMethod.Identifier, TypeParameterList(SingletonSeparatedList(visitorType.AsTypeParameter())),
                ParameterList(Parameter(visitorType.Name, visitorName.Identifier).WithModifiers(RefTokenList)),
                SingletonList(visitorType.AsGenericConstraint()!), body, null);
        }
    }
}
