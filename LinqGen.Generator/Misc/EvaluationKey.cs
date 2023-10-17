// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;

namespace Cathei.LinqGen.Generator;

public readonly struct EvaluationKey : IEquatable<EvaluationKey>
{
    public readonly SymbolKey SignatureSymbol;
    public readonly SymbolKey MethodSymbol;
    public readonly SymbolKey InputElementSymbol;

    public EvaluationKey(
        INamedTypeSymbol signatureSymbol, IMethodSymbol methodSymbol, ITypeSymbol inputElementSymbol)
    {
        SignatureSymbol = new(signatureSymbol);
        MethodSymbol = new(methodSymbol.OriginalDefinition);
        InputElementSymbol = new(inputElementSymbol);
    }

    public bool Equals(EvaluationKey other)
    {
        return (SignatureSymbol, MethodSymbol, InputElementSymbol)
            .Equals((other.SignatureSymbol, MethodSymbol, other.InputElementSymbol));
    }

    public override int GetHashCode()
    {
        return (SignatureSymbol, MethodSymbol, InputElementSymbol).GetHashCode();
    }

    public override string ToString()
    {
        return string.Join(" ", SignatureSymbol.ToString(), MethodSymbol.ToString(), InputElementSymbol.ToString());
    }
}