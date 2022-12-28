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

    public class TakeOperation : Operation
    {
        public TakeOperation(in LinqGenExpression expression, int id) : base(expression, id)
        {
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Both, IntType, LocalName("take"));
            yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("index"));
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal,
            ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            ExpressionSyntax newTakeVar = Member("take");

            if (skipVar != null)
                newTakeVar = SubtractExpression(newTakeVar, skipVar);

            if (takeVar != null)
                newTakeVar = MathMin(newTakeVar, takeVar);

            if (SupportPartition && skipVar != null)
            {
                yield return ExpressionStatement(SimpleAssignmentExpression(
                    Iterator("index"), SubtractExpression(skipVar, LiteralExpression(1))));
            }
            else
            {
                yield return ExpressionStatement(SimpleAssignmentExpression(
                    Iterator("index"), LiteralExpression(-1)));
            }

            foreach (var statement in base.RenderInitialization(isLocal, skipVar, newTakeVar))
                yield return statement;
        }

        public override ExpressionSyntax? RenderCount()
        {
            var upstreamCount = Upstream.RenderCount();

            if (upstreamCount == null)
                return null;

            return MathMin(ParenthesizedExpression(upstreamCount), Member("take"));
        }

        protected override StatementSyntax? RenderMoveNext()
        {
            return IfStatement(
                GreaterOrEqualExpression(PreIncrementExpression(Iterator("index")), Member("take")),
                BreakStatement());
        }
    }
}