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

        public override IEnumerable<MemberDeclarationSyntax> RenderUpstreamMemberMethods()
        {
            int arityDiff = Arity - Upstream!.Arity;
            int upstreamDepth = UpstreamOrder!.Depth;

            var typeParameters = GetTypeParameters(take: arityDiff);
            var genericConstraints = GetGenericConstraints(take: arityDiff);

            // swap first argument with source
            var argumentList = ArgumentList(GetArguments(MemberKind.Enumerable));
            var parameterList = ParameterList(GetParameters(MemberKind.Enumerable).Skip(1 + upstreamDepth * 2));

            var body = Block(ReturnStatement(
                ObjectCreationExpression(ResolvedClassName, argumentList, default)));

            yield return MethodDeclaration(new(AggressiveInliningAttributeList),
                PublicTokenList, ResolvedClassName, default, MethodName.Identifier, typeParameters,
                parameterList, genericConstraints, body, default, default);
        }
    }
}