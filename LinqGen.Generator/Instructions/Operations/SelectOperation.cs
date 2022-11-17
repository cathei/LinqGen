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

    public class SelectOperation : Operation
    {
        private TypeSyntax SelectorType { get; }
        private bool WithIndex { get; }
        private bool WithStruct { get; }

        public SelectOperation(in LinqGenExpression expression, int id,
            ITypeSymbol parameterType, bool withIndex, bool withStruct) : base(expression, id)
        {
            SelectorType = ParseTypeName(parameterType);
            WithIndex = withIndex;
            WithStruct = withStruct;
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
            {
                yield return new TypeParameterInfo(TypeName("1"), SelectorType);
            }
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Both,
                WithStruct ? TypeName("1") : SelectorType, VarName("selector"));

            if (WithIndex)
                yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"), LiteralExpression(-1));
        }

        public override ExpressionSyntax RenderCurrent(RenderOption option)
        {
            return InvocationExpression(
                MemberAccessExpression(VarName("selector"), InvokeMethod),
                ArgumentList(WithIndex
                    ? new ExpressionSyntax[] { CurrentPlaceholder, PreIncrementExpression(VarName("index")) }
                    : new ExpressionSyntax[] { CurrentPlaceholder }));
        }
    }
}