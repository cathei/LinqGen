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

    public sealed class LastEvaluation : Evaluation
    {
        private bool OrDefault { get; }

        public LastEvaluation(in LinqGenExpression expression, bool orDefault) : base(expression)
        {
            OrDefault = orDefault;
        }

        public override TypeSyntax ReturnType => Upstream!.OutputElementType;

        public override BlockSyntax RenderMethodBody()
        {
            var lastValue = IdentifierName("lastValue");

            return Block(UsingLocalDeclarationStatement(
                    IteratorField.Identifier, InvocationExpression(SourceField, GetEnumeratorMethod)),
                IfStatement(LogicalNotExpression(InvocationExpression(IteratorField, MoveNextMethod)),
                    OrDefault ? ReturnDefaultStatement() : ThrowInvalidOperationStatement()),
                LocalDeclarationStatement(lastValue.Identifier, MemberAccessExpression(IteratorField, CurrentProperty)),
                WhileStatement(InvocationExpression(IteratorField, MoveNextMethod),
                    ExpressionStatement(SimpleAssignmentExpression(
                        lastValue, MemberAccessExpression(IteratorField, CurrentProperty)))),
                ReturnStatement(lastValue));
        }
    }
}