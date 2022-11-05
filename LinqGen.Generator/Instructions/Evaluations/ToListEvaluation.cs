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

    public sealed class ToListEvaluation : Evaluation
    {
        public ToListEvaluation(in LinqGenExpression expression) : base(expression)
        {
        }

        public override TypeSyntax ReturnType =>
            GenericName(Identifier("List"), TypeArgumentList(Upstream!.OutputElementType));

        public override BlockSyntax RenderMethodBody()
        {
            return Block(
                LocalDeclarationStatement(ListVar.Identifier,
                    ObjectCreationExpression(ReturnType, ArgumentList(Upstream!.IsCountable
                            ? MemberAccessExpression(SourceVar, CountProperty)
                            : LiteralExpression(0)),
                        default)),
                UsingLocalDeclarationStatement(IteratorVar.Identifier,
                    InvocationExpression(SourceVar, GetEnumeratorMethod)),
                WhileStatement(InvocationExpression(MemberAccessExpression(IteratorVar, MoveNextMethod)),
                    ExpressionStatement(InvocationExpression(
                        MemberAccessExpression(ListVar, AddMethod),
                        ArgumentList(MemberAccessExpression(IteratorVar, CurrentProperty))))),
                ReturnStatement(ListVar));
        }
    }
}
