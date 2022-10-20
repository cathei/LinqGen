// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
    public readonly struct OperationItem : IEquatable<OperationItem>
    {
        public readonly IMethodSymbol MethodSymbol;
        public readonly INamedTypeSymbol OpTypeSymbol;

        public OperationItem(IMethodSymbol methodSymbol, INamedTypeSymbol opTypeSymbol)
        {
            MethodSymbol = methodSymbol;
            OpTypeSymbol = opTypeSymbol;
        }

        public bool Equals(OperationItem other)
        {
            return SymbolEqualityComparer.Default.Equals(MethodSymbol, other.MethodSymbol) &&
                   SymbolEqualityComparer.Default.Equals(OpTypeSymbol, other.OpTypeSymbol);
        }

        public override bool Equals(object obj)
        {
            return obj is OperationItem other && Equals(other);
        }

        public override int GetHashCode()
        {
            return SymbolEqualityComparer.Default.GetHashCode(MethodSymbol) ^
                   SymbolEqualityComparer.Default.GetHashCode(OpTypeSymbol);
        }
    }
}