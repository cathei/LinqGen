// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    public sealed class ThenByOperation : OrderingOperation
    {
        public ThenByOperation(in LinqGenExpression expression, int id,
            INamedTypeSymbol selectorType, bool withStruct) : base(expression, id, selectorType, withStruct)
        {
        }

        public override bool ShouldBeMemberMethod => true;

        public override ConstructorDeclarationSyntax RenderEnumerableConstructor()
        {
            return base.RenderEnumerableConstructor();
        }

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            var upstreamOrdering = Upstream as OrderingOperation;

            Debug.Assert(upstreamOrdering != null);

            // inherits all member info of parent
            foreach (var memberInfo in upstreamOrdering!.GetMemberInfos())
                yield return memberInfo;

            yield return new MemberInfo(MemberKind.Enumerable,
                WithStruct ? IdentifierName($"{TypeParameterPrefix}1") : SelectorTypeName, SelectorVar);

            yield return new MemberInfo(MemberKind.Enumerable,
                WithStruct ? IdentifierName($"{TypeParameterPrefix}2") : ComparerTypeName, ComparerVar,
                WithStruct ? null : NullLiteral);
        }
    }
}