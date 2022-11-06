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

    public sealed class OrderByOperation : Operation
    {
        private TypeSyntax SelectorTypeName { get; }
        private bool WithStruct { get; }

        public OrderByOperation(in LinqGenExpression expression, int id,
            INamedTypeSymbol selectorType, bool withStruct) : base(expression, id)
        {
            WithStruct = withStruct;
            SelectorTypeName = ParseTypeName(selectorType);
        }

        public override bool IsCountable => Upstream!.IsCountable;
        public override bool IsPartition => true;

        private TypeSyntax ComparerTypeName =>
            GenericName(Identifier("IComparer"), TypeArgumentList(Upstream!.OutputElementType));

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Enumerable,
                WithStruct ? IdentifierName($"{TypeParameterPrefix}1") : SelectorTypeName, SelectorVar);

            yield return new MemberInfo(MemberKind.Enumerable,
                WithStruct ? IdentifierName($"{TypeParameterPrefix}2") : ComparerTypeName, ComparerVar,
                WithStruct ? null : NullLiteral);

            // yield return new MemberInfo(MemberKind.Enumerator, HashSetType, HashSetVar);
        }

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

        }

    }
}