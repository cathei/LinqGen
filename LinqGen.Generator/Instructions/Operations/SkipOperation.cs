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

    public class SkipOperation : Operation
    {
        public SkipOperation(in LinqGenExpression expression, int id) : base(expression, id)
        {
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Both, IntType, LocalName("skip"));

            if (!Upstream.SupportPartition)
            {
                yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("index"), LiteralExpression(-1));
            }
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal,
            ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            ExpressionSyntax newSkipVar = MemberName("skip");

            if (skipVar != null)
                newSkipVar = AddExpression(newSkipVar, skipVar);

            return base.RenderInitialization(isLocal, newSkipVar, takeVar);
        }

        public override ExpressionSyntax? RenderCount()
        {
            var upstreamCount = Upstream.RenderCount();

            if (upstreamCount == null)
                return null;

            return MathMax(SubtractExpression(
                ParenthesizedExpression(upstreamCount), MemberName("skip")), LiteralExpression(0));
        }

        protected override StatementSyntax? RenderMoveNext()
        {
            if (Upstream.SupportPartition)
                return null;

            return IfStatement(
                LessThanExpression(PreIncrementExpression(LocalName("index")), MemberName("skip")),
                ContinueStatement());
        }
    }
}