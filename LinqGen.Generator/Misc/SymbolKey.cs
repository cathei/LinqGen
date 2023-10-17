// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;

namespace Cathei.LinqGen.Generator;

public readonly struct SymbolKey : IEquatable<SymbolKey>
{
    private static readonly SymbolEqualityComparer SymbolComparer = SymbolEqualityComparer.Default;

    public readonly ISymbol Symbol;

    public SymbolKey(ISymbol symbol)
    {
        Symbol = symbol;
    }

    public bool Equals(SymbolKey other)
    {
        return SymbolComparer.Equals(Symbol, other.Symbol);
    }

    public override bool Equals(object? obj)
    {
        return obj is SymbolKey other && Equals(other);
    }

    public override int GetHashCode()
    {
        return SymbolComparer.GetHashCode(Symbol);
    }

    public override string ToString()
    {
        return Symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }
}