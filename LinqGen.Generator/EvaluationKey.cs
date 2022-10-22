// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
    public readonly struct EvaluationKey : IEqualityComparer<EvaluationKey>
    {
        private static readonly SymbolEqualityComparer SymbolComparer = SymbolEqualityComparer.Default;

        public readonly INamedTypeSymbol SignatureSymbol;
        public readonly IMethodSymbol MethodSymbol;

        public EvaluationKey(INamedTypeSymbol signatureSymbol, IMethodSymbol methodSymbol)
        {
            SignatureSymbol = signatureSymbol;
            MethodSymbol = methodSymbol;
        }

        public bool Equals(EvaluationKey x, EvaluationKey y)
        {
            return SymbolComparer.Equals(x.SignatureSymbol, y.SignatureSymbol) &&
                   SymbolComparer.Equals(x.MethodSymbol, y.MethodSymbol);
        }

        public int GetHashCode(EvaluationKey obj)
        {
            return SymbolComparer.GetHashCode(obj.SignatureSymbol) ^
                   SymbolComparer.GetHashCode(obj.MethodSymbol);
        }
    }
}