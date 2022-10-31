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

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Both, IntType, ValueField);
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            if (!Upstream!.IsPartition)
                return base.RenderGetEnumeratorBody();

            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType, ArgumentList(
                InvocationExpression(
                    MemberAccessExpression(SourceField, GetSliceEnumeratorMethod),
                    ArgumentList(ValueField, NullLiteral)),
                LiteralExpression(0)), null)));
        }

        public override BlockSyntax RenderGetSliceEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType, ArgumentList(
                InvocationExpression(
                    MemberAccessExpression(SourceField, GetSliceEnumeratorMethod),
                    ArgumentList(
                        AddExpression(SkipField, ValueField),
                        SubtractExpression(TakeField, ValueField))),
                LiteralExpression(0)), null)));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            if (Upstream!.IsPartition)
                return base.RenderMoveNextBody();

            return Block(WhileStatement(
                    GreaterOrEqualExpression(PreDecrementExpression(ValueField), LiteralExpression(0)),
                    IfStatement(
                        LogicalNotExpression(InvocationExpression(SourceField, MoveNextMethod)),
                        ReturnStatement(FalseExpression()))),
                ReturnStatement(InvocationExpression(SourceField, MoveNextMethod)));
        }

        public override BlockSyntax RenderCountGetBody()
        {
            return Block(ReturnStatement(MathMax(
                SubtractExpression(MemberAccessExpression(SourceField, CountProperty), ValueField),
                LiteralExpression(0))));
        }
    }
}