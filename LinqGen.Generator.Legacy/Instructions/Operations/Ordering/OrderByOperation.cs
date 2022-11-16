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

    public sealed class OrderByOperation : OrderingOperation
    {
        public OrderByOperation(in LinqGenExpression expression, int id,
            INamedTypeSymbol? selectorType, bool withStruct) : base(expression, id, selectorType, withStruct)
        {
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderExtensionMethods()
        {
            var argumentList = SeparatedList(GetArguments(MemberKind.Enumerable));

            // last argument is desc
            argumentList = argumentList.RemoveAt(argumentList.Count - 1);

            int parameterPerDepth = WithSelector ? 3 : 2;

            // first parameter is source, last parameter is desc flag
            var parameterList = ParameterList(
                GetParameters(MemberKind.Enumerable, firstThisParam: true, defaultValue: true)
                    .Take(parameterPerDepth));

            // ascending
            yield return MethodDeclaration(new(AggressiveInliningAttributeList),
                PublicStaticTokenList, ResolvedClassName, default, Identifier("OrderBy"),
                GetTypeParameters(), parameterList, GetGenericConstraints(),
                Block(ReturnStatement(ObjectCreationExpression(ResolvedClassName,
                    ArgumentList(argumentList.Add(Argument(FalseExpression()))), default))),
                default, default);

            // descending
            yield return MethodDeclaration(new(AggressiveInliningAttributeList),
                PublicStaticTokenList, ResolvedClassName, default, Identifier("OrderByDescending"),
                GetTypeParameters(), parameterList, GetGenericConstraints(),
                Block(ReturnStatement(ObjectCreationExpression(ResolvedClassName,
                    ArgumentList(argumentList.Add(Argument(TrueExpression()))), default))),
                default, default);
        }
    }
}