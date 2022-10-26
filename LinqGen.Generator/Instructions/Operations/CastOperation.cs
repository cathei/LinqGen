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

        // /// <summary>
        // /// Cast forces output to use generic element
        // /// </summary>
        // public override bool SupportGenericElementOutput => true;
        //
        // public override bool PreserveElementType => false;

        public override bool ShouldBeMemberMethod => true;

        public override TypeSyntax OutputElementType { get; }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            yield return new TypeParameterInfo(
                IdentifierName($"{TypeParameterPrefix}1"), null);
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            if (SkipIfMismatch)
            {
                return Block(WhileStatement(InvocationExpression(SourceName, MoveNextName),
                        Block(IfStatement(
                            IsExpression(MemberAccessExpression(SourceName, CurrentName), OutputElementType),
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
                        MemberAccessExpression(SourceName, CurrentName)))));
        }
    }
}
