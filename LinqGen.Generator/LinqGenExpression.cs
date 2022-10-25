// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static CodeGenUtils;

    public readonly struct LinqGenExpression
    {
        public SemanticModel SemanticModel { get; }
        public InvocationExpressionSyntax InvocationSyntax { get; }
        public MemberAccessExpressionSyntax MemberAccessSyntax { get; }
        public IMethodSymbol MethodSymbol { get; }
        public INamedTypeSymbol? SignatureSymbol { get; }
        public INamedTypeSymbol? UpstreamSignatureSymbol { get; }
        public INamedTypeSymbol CallerTypeSymbol { get; }

        private LinqGenExpression(SemanticModel semanticModel, InvocationExpressionSyntax invocationSyntax,
            MemberAccessExpressionSyntax memberAccessSyntax, IMethodSymbol methodSymbol,
            INamedTypeSymbol? signatureSymbol, INamedTypeSymbol? upstreamSignatureSymbol,
            INamedTypeSymbol callerTypeSymbol)
        {
            SemanticModel = semanticModel;
            InvocationSyntax = invocationSyntax;
            MemberAccessSyntax = memberAccessSyntax;
            MethodSymbol = methodSymbol;
            SignatureSymbol = signatureSymbol;
            UpstreamSignatureSymbol = upstreamSignatureSymbol;
            CallerTypeSymbol = callerTypeSymbol;
        }

        public static bool TryParse(SemanticModel semanticModel,
            InvocationExpressionSyntax invocationSyntax, out LinqGenExpression result)
        {
            result = default;

            if (invocationSyntax.Expression is not MemberAccessExpressionSyntax memberAccessSyntax)
            {
                // not a method invocation
                return false;
            }

            var memberSymbolInfo = semanticModel.GetSymbolInfo(memberAccessSyntax.Name);

            if (memberSymbolInfo.Symbol is not IMethodSymbol methodSymbol || !IsStubMethod(methodSymbol))
            {
                // not a stub method
                return false;
            }

            ITypeSymbol? elementSymbol = null;
            INamedTypeSymbol? signatureSymbol = null;

            // returning stub enumerable, meaning it's compiling generation
            if (methodSymbol.ReturnType is INamedTypeSymbol returnTypeSymbol && IsOutputStubEnumerable(returnTypeSymbol))
            {
                // elementSymbol = returnTypeSymbol.TypeArguments[0];
                //
                // if (elementSymbol is ITypeParameterSymbol)
                // {
                //     // generic type parameter should not be used
                //     return false;
                // }

                signatureSymbol = returnTypeSymbol.TypeArguments[1] as INamedTypeSymbol;

                if (signatureSymbol == null)
                {
                    // something is wrong
                    // generic signature is not allowed
                    return false;
                }
            }

            if (methodSymbol.ReceiverType is not INamedTypeSymbol receiverTypeSymbol)
            {
                // first parameter (this) of method is not NamedTypeSymbol (should not be possible)
                return false;
            }

            var callerTypeInfo = semanticModel.GetTypeInfo(memberAccessSyntax.Expression);

            if (callerTypeInfo.Type is not INamedTypeSymbol callerTypeSymbol)
            {
                // TODO consider array type?
                return false;
            }

            INamedTypeSymbol? upstreamSignatureSymbol = null;

            // this means it takes LinqGen enumerable as input, and upstream type is required
            if (IsInputStubEnumerable(receiverTypeSymbol))
            {
                if (!TryParseStubInterface(callerTypeSymbol, out upstreamSignatureSymbol))
                {
                    // How did this happen?
                    // TODO: Can we allow generic constrained upstream type?
                    return false;
                }

                // if (signatureSymbol == null)
                // {
                //     // for evaluation, use upstream symbol's element type
                //     elementSymbol = upstreamElementSymbol;
                // }
            }

            // if (elementSymbol == null)
            // {
            //     // should not be possible
            //     return false;
            // }

            result = new LinqGenExpression(
                semanticModel, invocationSyntax, memberAccessSyntax,
                methodSymbol, signatureSymbol, upstreamSignatureSymbol, callerTypeSymbol);

            return true;
        }

        // index 0 is second argument because first argument is treated as caller when it's extension method
        public bool TryGetParameterType(int index, out INamedTypeSymbol result)
        {
            if (MethodSymbol.Parameters.Length <= index)
            {
                result = default!;
                return false;
            }

            if (MethodSymbol.Parameters[index].Type is not INamedTypeSymbol namedTypeSymbol)
            {
                result = default!;
                return false;
            }

            result = namedTypeSymbol;
            return true;
        }

        public bool IsCompilingGeneration()
        {
            return SignatureSymbol != null;
        }
    }
}