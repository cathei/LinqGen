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

        protected override TypeSyntax ReturnType { get; }

        protected override IEnumerable<MemberDeclarationSyntax> RenderVisitorFields()
        {
            yield return FieldDeclaration(
                PrivateTokenList, PooledListType(InputElementType), VarName("list").Identifier);
        }

        protected override IEnumerable<StatementSyntax> RenderInitialization()
        {
            ExpressionSyntax countExpression = Upstream.SupportCount
                ? MemberAccessExpression(IdentifierName("source"), CountProperty)
                : LiteralExpression(0);

            yield return ExpressionStatement(SimpleAssignmentExpression(VarName("list"),
                ObjectCreationExpression(PooledListType(InputElementType),
                    ArgumentList(countExpression), null)));
        }

        protected override IEnumerable<StatementSyntax> RenderAccumulation()
        {
            yield return ExpressionStatement(InvocationExpression(
                MemberAccessExpression(VarName("list"), AddMethod), ArgumentList(ElementVar)));
            yield return ReturnStatement(TrueExpression());
        }

        protected override IEnumerable<StatementSyntax> RenderDispose()
        {
            yield return ExpressionStatement(InvocationExpression(VarName("list"), DisposeMethod));
        }

        protected override IEnumerable<StatementSyntax> RenderReturn()
        {
            yield return ReturnStatement(InvocationExpression(VarName("list"), IdentifierName("ToArray")));
        }
    }
}
