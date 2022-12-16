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
        }

        protected override TypeSyntax ReturnType => ArrayType(
            Upstream.OutputElementType, SingletonList(ArrayRankSpecifier()));

        protected override IEnumerable<StatementSyntax> RenderInitialization()
        {
            ExpressionSyntax countExpression = Upstream.SupportCount
                ? CountProperty
                : LiteralExpression(0);

            yield return UsingLocalDeclarationStatement(VarName("list").Identifier, ObjectCreationExpression(
                PooledListType(Upstream.OutputElementType, Upstream.OutputElementSymbol.IsUnmanagedType),
                ArgumentList(countExpression), null));
        }

        protected override IEnumerable<StatementSyntax> RenderAccumulation()
        {
            yield return ExpressionStatement(InvocationExpression(
                MemberAccessExpression(VarName("list"), AddMethod), ArgumentList(CurrentPlaceholder)));
        }

        protected override IEnumerable<StatementSyntax> RenderReturn()
        {
            yield return ReturnStatement(InvocationExpression(VarName("list"), IdentifierName("ToArray")));
        }
    }
}
