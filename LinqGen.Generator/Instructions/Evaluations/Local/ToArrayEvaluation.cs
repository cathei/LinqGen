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
            if (Upstream.SupportCount)
            {
                yield return LocalDeclarationStatement(VarName("array").Identifier,
                    ObjectCreationExpression(ArrayType(Upstream.OutputElementType,
                        SingletonList(ArrayRankSpecifier(SingletonSeparatedList(
                            (ExpressionSyntax)InvocationExpression(CountMethod)))))));

                yield return LocalDeclarationStatement(VarName("index").Identifier, LiteralExpression(-1));
            }
            else
            {
                yield return UsingLocalDeclarationStatement(VarName("list").Identifier, ObjectCreationExpression(
                    PooledListType(Upstream.OutputElementType, Upstream.OutputElementSymbol.IsUnmanagedType),
                    ArgumentList(LiteralExpression(0)), null));
            }
        }

        protected override IEnumerable<StatementSyntax> RenderAccumulation()
        {
            if (Upstream.SupportCount)
            {
                yield return ExpressionStatement(SimpleAssignmentExpression(
                    ElementAccessExpression(VarName("array"), PreIncrementExpression(VarName("index"))),
                    CurrentPlaceholder));
            }
            else
            {
                yield return ExpressionStatement(InvocationExpression(
                    MemberAccessExpression(VarName("list"), AddMethod), ArgumentList(CurrentPlaceholder)));
            }
        }

        protected override IEnumerable<StatementSyntax> RenderReturn()
        {
            if (Upstream.SupportCount)
            {
                yield return ReturnStatement(VarName("array"));
            }
            else
            {
                yield return ReturnStatement(InvocationExpression(VarName("list"), IdentifierName("ToArray")));
            }
        }
    }
}
