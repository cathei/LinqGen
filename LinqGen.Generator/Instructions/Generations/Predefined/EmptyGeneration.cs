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

    public sealed class EmptyGeneration : PredefinedGeneration
    {
        public EmptyGeneration(in LinqGenExpression expression, int id) : base(expression, id)
        {
        }

        public override TypeSyntax OutputElementType => TypeName("Element");

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            yield return new TypeParameterInfo(TypeName("Element"));
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield break;
        }

        public override ExpressionSyntax RenderCount()
        {
            return LiteralExpression(0);
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(
            bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            yield break;
        }

        public override BlockSyntax RenderIteration(bool isLocal,
            SyntaxList<StatementSyntax> statements)
        {
            return Block();
        }
    }
}