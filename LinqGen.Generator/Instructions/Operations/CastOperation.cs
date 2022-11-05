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

    public sealed class CastOperation : Operation
    {
        private bool SkipIfMismatch { get; }

        public CastOperation(in LinqGenExpression expression, int id, bool skipIfMismatch) : base(expression, id)
        {
            SkipIfMismatch = skipIfMismatch;
            OutputElementType = IdentifierName($"{TypeParameterPrefix}1");
        }

        public override bool ShouldBeMemberMethod => true;

        public override TypeSyntax OutputElementType { get; }

        public override bool IsCountable => Upstream!.IsCountable;
        public override bool IsPartition => Upstream!.IsPartition;

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}1"));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            if (SkipIfMismatch)
            {
                return Block(WhileStatement(InvocationExpression(SourceField, MoveNextMethod),
                        Block(IfStatement(
                            IsExpression(MemberAccessExpression(SourceField, CurrentProperty), OutputElementType),
                            ReturnStatement(TrueExpression())))),
                    ReturnStatement(FalseExpression()));
            }

            return base.RenderMoveNextBody();
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            // assuming this is faster even for OfType than storing variable
            // because assigning reference variable to instance requires Write Barrier call
            // object conversion is required
            return Block(ReturnStatement(
                CastExpression(OutputElementType,
                    CastExpression(ObjectType,
                        MemberAccessExpression(SourceField, CurrentProperty)))));
        }
    }
}
