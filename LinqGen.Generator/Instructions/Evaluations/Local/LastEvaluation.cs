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

    public sealed class LastEvaluation : LocalEvaluation
    {
        private bool OrDefault { get; }

        public LastEvaluation(in LinqGenExpression expression, int id, bool orDefault) : base(expression, id)
        {
            OrDefault = orDefault;
        }

        protected override ExpressionSyntax? SkipExpression
            => Upstream.SupportCount
                ? SubtractExpression(InvocationExpression(CountMethod), LiteralExpression(1))
                : null;

        protected override ExpressionSyntax? TakeExpression
            => Upstream.SupportCount ? LiteralExpression(1) : null;

        protected override TypeSyntax ReturnType => Upstream.OutputElementType;

        protected override IEnumerable<StatementSyntax> RenderInitialization()
        {
            if (!OrDefault)
                yield return LocalDeclarationStatement(BoolType, LocalName("isSet").Identifier, FalseExpression());

            yield return LocalDeclarationStatement(
                Upstream.OutputElementType, LocalName("result").Identifier, DefaultLiteral);
        }

        protected override IEnumerable<StatementSyntax> RenderAccumulation()
        {
            if (!OrDefault)
                yield return ExpressionStatement(SimpleAssignmentExpression(LocalName("isSet"), TrueExpression()));

            yield return ExpressionStatement(SimpleAssignmentExpression(LocalName("result"), CurrentPlaceholder));
        }

        protected override IEnumerable<StatementSyntax> RenderReturn()
        {
            if (!OrDefault)
                yield return IfStatement(LogicalNotExpression(LocalName("isSet")), ThrowInvalidOperationStatement());

            yield return ReturnStatement(LocalName("result"));
        }
    }
}
