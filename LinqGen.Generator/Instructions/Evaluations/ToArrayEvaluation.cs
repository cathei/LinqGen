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

    public sealed class ToArrayEvaluation : LocalEvaluation
    {
        public ToArrayEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            ReturnType = ParseTypeName(expression.MethodSymbol.ReturnType);
        }

        private TypeSyntax ReturnType { get; }

        public override IEnumerable<MemberDeclarationSyntax> RenderUpstreamMembers()
        {
            yield return MethodDeclaration(
                SingletonList(AggressiveInliningAttributeList), PublicTokenList,
                ReturnType, null, MethodName.Identifier, null,
                ParameterList(), default, RenderBody(), null, default);
        }

        protected override IEnumerable<StatementSyntax> RenderInitialization()
        {
            yield return UsingLocalDeclarationStatement(VarName("list").Identifier,
                ObjectCreationExpression(GenericName(Identifier("PooledList"),
                        TypeArgumentList(Upstream.OutputElementType)),
                    ArgumentList(LiteralExpression(0)), null));
        }

        protected override IEnumerable<StatementSyntax> RenderAccumulation()
        {
            yield return ExpressionStatement(InvocationExpression(
                MemberAccessExpression(VarName("list"), AddMethod),
                ArgumentList(CurrentPlaceholder)));
        }

        protected override IEnumerable<StatementSyntax> RenderReturn()
        {
            yield return ReturnStatement(InvocationExpression(VarName("list"), IdentifierName("ToArray")));
        }
    }
}
