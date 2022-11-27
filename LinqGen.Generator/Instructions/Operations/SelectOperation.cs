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
            INamedTypeSymbol parameterType, bool withIndex, bool withStruct) : base(expression, id)
        {
            SelectorType = ParseTypeName(parameterType);

            // Func<TIn, TOut> or IStructFunction<TIn, TOut>
            // Func<TIn, int, TOut> or IStructFunction<TIn, int, TOut>
            var elementSymbol = parameterType.TypeArguments[withIndex ? 2 : 1];
            OutputElementSymbol = elementSymbol;
            OutputElementType = ParseTypeName(elementSymbol);

            WithIndex = withIndex;
            WithStruct = withStruct;
        }

        public override ITypeSymbol OutputElementSymbol { get; }
        public override TypeSyntax OutputElementType { get; }

        public override TypeSyntax? DummyParameterType => WithStruct ? OutputElementType : null;

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
            {
                yield return new TypeParameterInfo(TypeName("Selector"), SelectorType);
            }
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Both,
                WithStruct ? TypeName("Selector") : SelectorType, VarName("selector"));

            if (WithIndex)
                yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"), LiteralExpression(-1));
        }

        public override ExpressionSyntax? RenderCount()
        {
            return Upstream.RenderCount();
        }

        protected override ExpressionSyntax RenderCurrent()
        {
            return InvocationExpression(
                MemberAccessExpression(VarName("selector"), InvokeMethod),
                ArgumentList(WithIndex
                    ? new ExpressionSyntax[] { CurrentPlaceholder, PreIncrementExpression(VarName("index")) }
                    : new ExpressionSyntax[] { CurrentPlaceholder }));
        }
    }
}