// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public sealed class SkipOperation : Operation
    {
        public SkipOperation(in LinqGenExpression expression, int id) : base(expression, id) { }

        public override bool IsCountable => Upstream!.IsCountable;
        public override bool IsPartition => Upstream!.IsPartition;

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Both, IntType, ValueVar);
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            if (!Upstream!.IsPartition)
                return base.RenderGetEnumeratorBody();

            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType, ArgumentList(
                InvocationExpression(
                    MemberAccessExpression(SourceVar, GetSliceEnumeratorMethod),
                    ArgumentList(ValueVar, NullLiteral)),
                LiteralExpression(0)), null)));
        }

        public override BlockSyntax RenderGetSliceEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType, ArgumentList(
                InvocationExpression(
                    MemberAccessExpression(SourceVar, GetSliceEnumeratorMethod),
                    ArgumentList(
                        AddExpression(SkipVar, ValueVar),
                        SubtractExpression(TakeVar, ValueVar))),
                LiteralExpression(0)), null)));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            if (Upstream!.IsPartition)
                return base.RenderMoveNextBody();

            return Block(WhileStatement(
                    GreaterOrEqualExpression(PreDecrementExpression(ValueVar), LiteralExpression(0)),
                    IfStatement(
                        LogicalNotExpression(InvocationExpression(SourceVar, MoveNextMethod)),
                        ReturnStatement(FalseExpression()))),
                ReturnStatement(InvocationExpression(SourceVar, MoveNextMethod)));
        }

        public override BlockSyntax RenderCountGetBody()
        {
            return Block(ReturnStatement(MathMax(
                SubtractExpression(MemberAccessExpression(SourceVar, CountProperty), ValueVar),
                LiteralExpression(0))));
        }
    }
}