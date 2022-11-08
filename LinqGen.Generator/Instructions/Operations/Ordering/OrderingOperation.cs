// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public abstract class OrderingOperation : Operation
    {
        protected TypeSyntax SelectorTypeName { get; }
        protected bool WithStruct { get; }

        public OrderingOperation(in LinqGenExpression expression, int id,
            INamedTypeSymbol selectorType, bool withStruct) : base(expression, id)
        {
            WithStruct = withStruct;
            SelectorTypeName = ParseTypeName(selectorType);
        }

        public override bool IsCountable => Upstream!.IsCountable;
        public override bool IsPartition => true;

        protected TypeSyntax ComparerTypeName =>
            GenericName(Identifier("IComparer"), TypeArgumentList(Upstream!.OutputElementType));

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
            {
                yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}1"),
                    TypeConstraint(SelectorTypeName));

                yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}2"),
                    ClassOrStructConstraint(SyntaxKind.StructConstraint), TypeConstraint(ComparerTypeName));
            }
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            return Block(ReturnStatement(
                InvocationExpression(GetSliceEnumeratorMethod,
                    ArgumentList(LiteralExpression(0), LiteralExpression(-1)))));
        }
    }
}