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

    public class CastOperation : Operation
    {
        private bool SkipIfMismatch { get; }

        public CastOperation(in LinqGenExpression expression, int id, bool skipIfMismatch) : base(expression, id)
        {
            SkipIfMismatch = skipIfMismatch;
            OutputElementSymbol = expression.MethodSymbol.ConstructedFrom.TypeParameters[0];
            OutputElementType = TypeName("Out");
        }

        public override ITypeSymbol OutputElementSymbol { get; }
        public override TypeSyntax OutputElementType { get; }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            yield return new TypeParameterInfo(TypeName("Out"));
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield break;
        }

        public override ExpressionSyntax? RenderCount()
        {
            return Upstream.RenderCount();
        }

        protected override StatementSyntax? RenderMoveNext()
        {
            if (SkipIfMismatch)
            {
                return IfStatement(LogicalNotExpression(
                        ParenthesizedExpression(IsExpression(CurrentPlaceholder, OutputElementType))),
                    ContinueStatement());
            }

            return null;
        }

        protected override ExpressionSyntax RenderCurrent()
        {
            return CastExpression(OutputElementType, CastExpression(ObjectType, CurrentPlaceholder));
        }
    }
}