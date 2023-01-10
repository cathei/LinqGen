// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public readonly struct EvaluationKey : IEqualityComparer<EvaluationKey>
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

    public bool Equals(EvaluationKey x, EvaluationKey y)
    {
        return SymbolComparer.Equals(x.SignatureSymbol, y.SignatureSymbol) &&
               SymbolComparer.Equals(x.MethodSymbol, y.MethodSymbol) &&
               SymbolComparer.Equals(x.InputElementSymbol, y.InputElementSymbol);
    }

    public int GetHashCode(EvaluationKey obj)
    {
        return SymbolComparer.GetHashCode(obj.SignatureSymbol) ^
               SymbolComparer.GetHashCode(obj.MethodSymbol) ^
               SymbolComparer.GetHashCode(obj.InputElementSymbol);
    }
}