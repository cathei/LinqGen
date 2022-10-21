// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    public struct LinqGenExpression
    {
        public SemanticModel SemanticModel { get; }
        public InvocationExpressionSyntax InvocationSyntax { get; }
        public MemberAccessExpressionSyntax MemberAccessSyntax { get; }
        public IMethodSymbol MethodSymbol { get; }
        public INamedTypeSymbol ReturnTypeSymbol { get; }
        public INamedTypeSymbol ElementSymbol { get; }
        public INamedTypeSymbol OpSymbol { get; }
        public INamedTypeSymbol? ParentSymbol { get; }

        private LinqGenExpression(SemanticModel semanticModel, InvocationExpressionSyntax invocationSyntax,
            MemberAccessExpressionSyntax memberAccessSyntax, IMethodSymbol methodSymbol,
            INamedTypeSymbol returnTypeSymbol, INamedTypeSymbol elementSymbol, INamedTypeSymbol opSymbol,
            INamedTypeSymbol? parentSymbol)
        {
            SemanticModel = semanticModel;
            InvocationSyntax = invocationSyntax;
            MemberAccessSyntax = memberAccessSyntax;
            MethodSymbol = methodSymbol;
            ReturnTypeSymbol = returnTypeSymbol;
            ElementSymbol = elementSymbol;
            OpSymbol = opSymbol;
            ParentSymbol = parentSymbol;
        }

        public static bool TryParse(SemanticModel semanticModel,
            InvocationExpressionSyntax invocationSyntax, out LinqGenExpression result)
        {
            result = default;

            if (invocationSyntax.Expression is not MemberAccessExpressionSyntax memberAccessSyntax)
            {
                // not a extension method invocation
                return false;
            }

            var memberSymbolInfo = semanticModel.GetSymbolInfo(memberAccessSyntax.Name);

            if (memberSymbolInfo.Symbol is not IMethodSymbol methodSymbol ||
                !CodeGenUtils.IsStubMethod(methodSymbol))
            {
                // not a stub method
                return false;
            }

            if (methodSymbol.ReturnType is not INamedTypeSymbol returnTypeSymbol ||
                returnTypeSymbol.TypeArguments[0] is not INamedTypeSymbol elementSymbol ||
                returnTypeSymbol.TypeArguments[1] is not INamedTypeSymbol opSymbol)
            {
                // something is wrong
                // TODO: sorry, LinqGen does not support enumerating over generic argument (yet)
                return false;
            }

            if (methodSymbol.ReceiverType is not INamedTypeSymbol parameterTypeSymbol)
            {
                // this parameter of method is not NamedTypeSymbol (should not be possible)
                return false;
            }

            INamedTypeSymbol? parentSymbol = null;

            // this means it is not generation method and parent type is required
            if (CodeGenUtils.IsStubInterface(parameterTypeSymbol))
            {
                var callerTypeInfo = semanticModel.GetTypeInfo(memberAccessSyntax.Expression);

                if (callerTypeInfo.Type is not INamedTypeSymbol callerTypeSymbol ||
                    !CodeGenUtils.IsStubEnumerable(callerTypeSymbol))
                {
                    // not called from Stub enumerable.
                    // currently LinqGen does not support generated LinqGen enumerable from another assembly.
                    return false;
                }

                parentSymbol = callerTypeSymbol.TypeArguments[1] as INamedTypeSymbol;

                if (parentSymbol == null)
                {
                    // should not be possible here
                    return false;
                }
            }

            result = new LinqGenExpression(
                semanticModel, invocationSyntax, memberAccessSyntax,
                methodSymbol, returnTypeSymbol, elementSymbol, opSymbol, parentSymbol);

            return true;
        }

        // index 0 is second argument because first argument is treated as caller when it's extension method
        public bool TryGetArgumentType(int index, out ITypeSymbol result)
        {
            result = default!;

            var arguments = InvocationSyntax.ArgumentList.Arguments;
            if (arguments.Count <= index)
                return false;

            var argumentTypeInfo = SemanticModel.GetTypeInfo(arguments[index].Expression);

            // prefer original argument type if possible
            if (argumentTypeInfo.Type != null)
            {
                result = argumentTypeInfo.Type;
                return true;
            }

            // we couldn't retrieve argument type (most likely lambda)
            // uses parameter type instead
            result = MethodSymbol.Parameters[index].Type;
            return true;
        }
    }
}