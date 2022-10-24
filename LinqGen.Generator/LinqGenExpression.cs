// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
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
        public ITypeSymbol ElementSymbol { get; }
        public INamedTypeSymbol? SignatureSymbol { get; }
        public INamedTypeSymbol? UpstreamSymbol { get; }

        private LinqGenExpression(SemanticModel semanticModel, InvocationExpressionSyntax invocationSyntax,
            MemberAccessExpressionSyntax memberAccessSyntax, IMethodSymbol methodSymbol,
            ITypeSymbol elementSymbol, INamedTypeSymbol? signatureSymbol, INamedTypeSymbol? upstreamSymbol)
        {
            SemanticModel = semanticModel;
            InvocationSyntax = invocationSyntax;
            MemberAccessSyntax = memberAccessSyntax;
            MethodSymbol = methodSymbol;
            ElementSymbol = elementSymbol;
            SignatureSymbol = signatureSymbol;
            UpstreamSymbol = upstreamSymbol;
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
                elementSymbol = returnTypeSymbol.TypeArguments[0];
                signatureSymbol = returnTypeSymbol.TypeArguments[1] as INamedTypeSymbol;

                if (signatureSymbol == null)
                {
                    // something is wrong
                    // generic signature is not allowed at the time
                    return false;
                }
            }

            if (methodSymbol.ReceiverType is not INamedTypeSymbol receiverTypeSymbol)
            {
                // first parameter (this) of method is not NamedTypeSymbol (should not be possible)
                return false;
            }

            INamedTypeSymbol? upstreamSymbol = null;

            // this means it takes LinqGen enumerable as input, and upstream type is required
            if (IsInputStubEnumerable(receiverTypeSymbol))
            {
                var callerTypeInfo = semanticModel.GetTypeInfo(memberAccessSyntax.Expression);

                if (callerTypeInfo.Type is not INamedTypeSymbol callerTypeSymbol ||
                    !TryParseStubInterface(callerTypeSymbol, out var upstreamElementSymbol, out upstreamSymbol))
                {
                    // How did this happen?
                    // TODO: Can we allow generic constrained upstream type?
                    return false;
                }

                if (signatureSymbol == null)
                {
                    // for evaluation, use upstream symbol's element type
                    elementSymbol = upstreamElementSymbol;
                }
            }

            if (elementSymbol == null)
            {
                // should not be possible
                return false;
            }

            result = new LinqGenExpression(
                semanticModel, invocationSyntax, memberAccessSyntax,
                methodSymbol, elementSymbol, signatureSymbol, upstreamSymbol);

            return true;
        }

        // index 0 is second argument because first argument is treated as caller when it's extension method
        public bool TryGetParameterType(int index, out ITypeSymbol result)
        {
            if (MethodSymbol.Parameters.Length <= index)
            {
                result = default!;
                return false;
            }

            result = MethodSymbol.Parameters[index].Type;
            return true;
        }

        public bool IsCompilingGeneration()
        {
            return SignatureSymbol != null;
        }
    }
}