// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;

namespace Cathei.LinqGen.Generator;

public readonly struct EvaluationKey : IEquatable<EvaluationKey>
{
    private static readonly SymbolEqualityComparer SymbolComparer = SymbolEqualityComparer.Default;

    public readonly INamedTypeSymbol SignatureSymbol;
    public readonly IMethodSymbol MethodSymbol;
    public readonly ITypeSymbol InputElementSymbol;

    public EvaluationKey(
        INamedTypeSymbol signatureSymbol, IMethodSymbol methodSymbol, ITypeSymbol inputElementSymbol)
    {
        SignatureSymbol = signatureSymbol;
        MethodSymbol = methodSymbol.OriginalDefinition;
        InputElementSymbol = inputElementSymbol;
    }

    public bool Equals(EvaluationKey other)
    {
        return SymbolComparer.Equals(SignatureSymbol, other.SignatureSymbol) &&
               SymbolComparer.Equals(MethodSymbol, other.MethodSymbol) &&
               SymbolComparer.Equals(InputElementSymbol, other.InputElementSymbol);
    }

    public override int GetHashCode()
    {
        int hashCode = SymbolComparer.GetHashCode(SignatureSymbol);
        hashCode = HashCombine(hashCode, SymbolComparer.GetHashCode(MethodSymbol));
        hashCode = HashCombine(hashCode, SymbolComparer.GetHashCode(InputElementSymbol));
        return hashCode;
    }
}