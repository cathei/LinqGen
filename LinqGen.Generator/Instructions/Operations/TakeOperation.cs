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

    public sealed class TakeOperation : Operation
    {
        public TakeOperation(in LinqGenExpression expression, int id) : base(expression, id) { }

        public override bool IsCountable => Upstream!.IsCountable;
        public override bool IsPartition => Upstream!.IsPartition;

        protected override IEnumerable<MemberInfo> GetMemberInfos()
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
                    ArgumentList(LiteralExpression(0), ValueVar)),
                ValueVar), null)));
        }

        public override BlockSyntax RenderGetSliceEnumeratorBody()
        {
            // var newTake = take.HasValue ? Math.Min(take.Value, value - skip) : value;
            // return new Enumerator(source.GetSliceEnumerator(skip, newTake), newTake);
            return Block(
                LocalDeclarationStatement(Identifier("newTake"),
                    ConditionalExpression(MemberAccessExpression(TakeVar, HasValueProperty),
                        MathMin(MemberAccessExpression(TakeVar, ValueProperty),
                            SubtractExpression(ValueVar, SkipVar)),
                        ValueVar)),
                ReturnStatement(ObjectCreationExpression(EnumeratorType, ArgumentList(
                    InvocationExpression(
                        MemberAccessExpression(SourceVar, GetSliceEnumeratorMethod),
                        ArgumentList(SkipVar, IdentifierName("newTake"))),
                    IdentifierName("newTake")), null)));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(
                IfStatement(
                    GreaterOrEqualExpression(PreDecrementExpression(ValueVar), LiteralExpression(0)),
                    ReturnStatement(InvocationExpression(SourceVar, MoveNextMethod))),
                ReturnStatement(FalseExpression()));
        }

        public override BlockSyntax RenderCountGetBody()
        {
            return Block(ReturnStatement(MathMin(
                MemberAccessExpression(SourceVar, CountProperty), ValueVar)));
        }
    }
}