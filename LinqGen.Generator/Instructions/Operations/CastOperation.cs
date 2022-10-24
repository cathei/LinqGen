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

    public class CastOperation : Operation
    {
        private readonly bool SkipIfMismatch;

        public CastOperation(in LinqGenExpression expression, bool skipIfMismatch)
            : base(expression, IdentifierName("T1"))
        {
            SkipIfMismatch = skipIfMismatch;
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            yield return new TypeParameterInfo(null);
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            if (SkipIfMismatch)
            {
                return Block(WhileStatement(InvocationExpression(SourceName, MoveNextName),
                        Block(IfStatement(
                            IsExpression(MemberAccessExpression(SourceName, CurrentName), IdentifierName("T1")),
                            ReturnStatement(TrueExpression())))),
                    ReturnStatement(FalseExpression()));
            }

            return base.RenderMoveNextBody();
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            // assuming this is faster even for OfType than storing variable
            // because assigning reference variable to instance requires Write Barrier call
            return Block(ReturnStatement(CastExpression(IdentifierName("T1"),
                MemberAccessExpression(SourceName, CurrentName))));
        }
    }
}