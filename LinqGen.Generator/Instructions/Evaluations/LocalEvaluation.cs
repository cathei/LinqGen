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

    public abstract class LocalEvaluation : Evaluation
    {
        private static readonly SyntaxTree VisitorTemplate = CSharpSyntaxTree.ParseText(@"
        private struct _Visitor_ : IVisitor<_Input_>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Init()
            {
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Visit(_Input_ element)
            {
            }

            public _Output_ Result
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Dispose()
            {
            }
        }
");

        private class VisitorRewriter : CSharpSyntaxRewriter
        {
            private readonly LocalEvaluation _instruction;

            public VisitorRewriter(LocalEvaluation instruction)
            {
                _instruction = instruction;
            }

            public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
            {
                var members = _instruction.GetParameterInfos()
                    .Select(x => (MemberDeclarationSyntax)FieldDeclaration(
                        PrivateTokenList, x.Type, x.Name.Identifier));

                members = members.Concat(_instruction.RenderVisitorFields());

                node = node
                    .WithIdentifier(_instruction.VisitorStructName.Identifier)
                    .WithTypeParameterList(_instruction.GetTypeParameters(_instruction.Arity))
                    .WithConstraintClauses(_instruction.GetGenericConstraints(_instruction.Arity))
                    .AddMembers(members.ToArray());

                return base.VisitStructDeclaration(node);
            }

            public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "Init":
                        node = RewriteInitMethod(node);
                        break;

                    case "Visit":
                        node = RewriteVisitMethod(node);
                        break;

                    case "Dispose":
                        node = RewriteDisposeMethod(node);
                        break;
                }

                return base.VisitMethodDeclaration(node);
            }

            public override SyntaxNode? VisitAccessorDeclaration(AccessorDeclarationSyntax node)
            {
                node = node.WithBody(Block(_instruction.RenderReturn()));

                return base.VisitAccessorDeclaration(node);
            }

            public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "_Input_":
                        return _instruction.InputElementType;

                    case "_Output_":
                        return _instruction.ReturnType;
                }

                return base.VisitIdentifierName(node);
            }

            private MethodDeclarationSyntax RewriteInitMethod(MethodDeclarationSyntax node)
            {
                var statements = _instruction.GetParameterInfos()
                    .Select(x => (StatementSyntax)ExpressionStatement(SimpleAssignmentExpression(
                        MemberAccessExpression(ThisExpression(), x.Name), x.Name)));

                statements = statements.Concat(_instruction.RenderInitialization());

                return node
                    .WithParameterList(_instruction.GetParameters(false))
                    .WithBody(Block(statements));
            }

            private MethodDeclarationSyntax RewriteVisitMethod(MethodDeclarationSyntax node)
            {
                return node.WithBody(Block(_instruction.RenderAccumulation()));
            }

            private MethodDeclarationSyntax RewriteDisposeMethod(MethodDeclarationSyntax node)
            {
                return node.WithBody(Block(_instruction.RenderDispose()));
            }
        }

        private readonly VisitorRewriter _rewriter;

        public LocalEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            _rewriter = new VisitorRewriter(this);
        }

        private IdentifierNameSyntax VisitorStructName =>
            IdentifierName($"Visitor_{MethodName.Identifier.ValueText}_{Id}");

        protected static readonly IdentifierNameSyntax ElementVar = IdentifierName("element");

        protected abstract IEnumerable<MemberDeclarationSyntax> RenderVisitorFields();
        protected abstract IEnumerable<StatementSyntax> RenderAccumulation();
        protected abstract IEnumerable<StatementSyntax> RenderReturn();

        protected virtual IEnumerable<StatementSyntax> RenderInitialization()
        {
            yield break;
        }

        protected virtual IEnumerable<StatementSyntax> RenderDispose()
        {
            yield break;
        }

        protected abstract TypeSyntax ReturnType { get; }

        protected virtual IEnumerable<ParameterInfo> GetParameterInfos()
        {
            yield break;
        }

        protected ParameterListSyntax GetParameters(bool extensionMethod)
        {
            var parameters = GetParameterInfos()
                .Select(x => x.AsParameter(extensionMethod));

            var sourceParameter =
                Parameter(UpstreamResolvedClassName, Identifier("source"));

            if (extensionMethod)
                sourceParameter = sourceParameter.WithModifiers(ThisTokenList);

            return ParameterList(parameters.Prepend(sourceParameter));
        }

        private ArgumentListSyntax GetArguments()
        {
            var arguments = GetParameterInfos().Select(x => x.AsArgument());

            return ArgumentList(arguments.Prepend(Argument(IdentifierName("source"))));
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderUpstreamMembers()
        {
            if (!Upstream.HasLocalVisitMethod)
            {
                yield return Upstream.RenderLocalVisitMethod();
            }
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderExtensionMembers()
        {
            var structSyntax = VisitorTemplate.GetRoot()
                .DescendantNodesAndSelf()
                .OfType<StructDeclarationSyntax>()
                .First();

            yield return (StructDeclarationSyntax)_rewriter.Visit(structSyntax);

            yield return MethodDeclaration(
                SingletonList(AggressiveInliningAttributeList), PublicStaticTokenList,
                ReturnType, null, MethodName.Identifier, GetTypeParameters(Arity), GetParameters(true),
                GetGenericConstraints(Arity), RenderBody(), null, default);
        }

        private BlockSyntax RenderBody()
        {
            var visitorName = IdentifierName("visitor");

            var body = Block(
                ExpressionStatement(InvocationExpression(
                    MemberAccessExpression(visitorName, IdentifierName("Init")), GetArguments())),
                ExpressionStatement(InvocationExpression(
                    MemberAccessExpression(IdentifierName("source"), VisitMethod),
                    ArgumentList(SingletonSeparatedList(Argument(null, Token(SyntaxKind.RefKeyword),
                        visitorName))))),
                ReturnStatement(MemberAccessExpression(visitorName, IdentifierName("Result"))));

            var visitorResolvedName = MakeGenericName(VisitorStructName, GetTypeArguments(Arity));

            return Block(
                LocalDeclarationStatement(
                    visitorName.Identifier, ObjectCreationExpression(visitorResolvedName, EmptyArgumentList, null)),
                TryStatement(body, default,
                    FinallyClause(Block(ExpressionStatement(InvocationExpression(visitorName, DisposeMethod))))));
        }
    }
}
