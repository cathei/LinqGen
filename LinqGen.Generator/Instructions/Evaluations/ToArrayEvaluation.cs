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

    public sealed class ToArrayEvaluation : Evaluation
    {
        public ToArrayEvaluation(in LinqGenExpression expression) : base(expression)
        {
        }

        public override TypeSyntax ReturnType =>
            ArrayType(Upstream!.OutputElementType, SingletonList(ArrayRankSpecifier()));

        public override BlockSyntax RenderMethodBody()
        {
            return Block(
                UsingLocalDeclarationStatement(ListVar.Identifier, ObjectCreationExpression(
                    GenericName(Identifier("PooledList"), TypeArgumentList(Upstream!.OutputElementType)),
                    ArgumentList(Upstream!.IsCountable
                        ? MemberAccessExpression(SourceVar, CountProperty)
                        : LiteralExpression(0)),
                    default)),
                UsingLocalDeclarationStatement(IteratorVar.Identifier,
                    InvocationExpression(SourceVar, GetEnumeratorMethod)),

                ExpressionStatement(InvocationExpression(
                    MemberAccessExpression(ListVar, AddRangeMethod), ArgumentList(IteratorVar))),
                // WhileStatement(InvocationExpression(MemberAccessExpression(IteratorVar, MoveNextMethod)),
                //     ExpressionStatement(InvocationExpression(
                //         MemberAccessExpression(ListVar, AddMethod),
                //         ArgumentList(MemberAccessExpression(IteratorVar, CurrentProperty))))),

                ReturnStatement(InvocationExpression(MemberAccessExpression(ListVar, IdentifierName("ToArray")))));
        }
    }
}
