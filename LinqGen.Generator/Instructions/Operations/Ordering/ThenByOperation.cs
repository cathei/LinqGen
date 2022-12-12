// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

    // public sealed class ThenByOperation : OrderingOperation
    // {
    //     public ThenByOperation(in LinqGenExpression expression, int id,
    //         INamedTypeSymbol? selectorType, bool withStruct) : base(expression, id, selectorType, withStruct)
    //     {
    //     }
    //
    //     public override bool ShouldBeMemberMethod => true;
    //
    //     public override IEnumerable<MemberDeclarationSyntax> RenderUpstreamMemberMethods()
    //     {
    //         int arityDiff = Arity - Upstream!.Arity;
    //
    //         var argumentList = SeparatedList(GetArguments(MemberKind.Enumerable));
    //
    //         // last argument is desc
    //         argumentList = argumentList.RemoveAt(argumentList.Count - 1);
    //
    //         int parameterCount = WithSelector ? 3 : 2;
    //
    //         var parameterList = new List<ParameterSyntax>(GetParameters(MemberKind.Enumerable, defaultValue: true));
    //
    //         // only need parameter for this operation
    //         parameterList.RemoveRange(0, parameterList.Count - parameterCount);
    //
    //         // last parameter is desc flag
    //         parameterList.RemoveAt(parameterList.Count - 1);
    //
    //         // ascending
    //         yield return MethodDeclaration(new(AggressiveInliningAttributeList),
    //             PublicTokenList, ResolvedClassName, default, Identifier("ThenBy"),
    //             GetTypeParameters(take: arityDiff),
    //             ParameterList(parameterList),
    //             GetGenericConstraints(take: arityDiff),
    //             Block(ReturnStatement(ObjectCreationExpression(ResolvedClassName,
    //                 ArgumentList(argumentList.Add(Argument(FalseExpression()))), default))),
    //             default, default);
    //
    //         // descending
    //         yield return MethodDeclaration(new(AggressiveInliningAttributeList),
    //             PublicTokenList, ResolvedClassName, default, Identifier("ThenByDescending"),
    //             GetTypeParameters(take: arityDiff),
    //             ParameterList(parameterList),
    //             GetGenericConstraints(take: arityDiff),
    //             Block(ReturnStatement(ObjectCreationExpression(ResolvedClassName,
    //                 ArgumentList(argumentList.Add(Argument(TrueExpression()))), default))),
    //             default, default);
    //     }
    // }
}