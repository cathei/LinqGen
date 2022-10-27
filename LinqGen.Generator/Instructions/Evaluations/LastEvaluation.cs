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
                    IteratorName.Identifier, InvocationExpression(SourceName, GetEnumeratorName)),
                IfStatement(LogicalNotExpression(InvocationExpression(IteratorName, MoveNextName)),
                    OrDefault ? ReturnDefaultStatement() : ThrowInvalidOperationStatement()),
                LocalDeclarationStatement(lastValue.Identifier, MemberAccessExpression(IteratorName, CurrentName)),
                WhileStatement(InvocationExpression(IteratorName, MoveNextName),
                    ExpressionStatement(SimpleAssignmentExpression(
                        lastValue, MemberAccessExpression(IteratorName, CurrentName)))),
                ReturnStatement(lastValue));
        }
    }
}