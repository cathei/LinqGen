using System;
using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public class GenEnumerableInstruction : GenerationInstruction
{
    private readonly TypeSyntax EnumeratorType;

    public GenEnumerableInstruction(TypeSyntax sourceType, TypeSyntax enumeratorType, TypeSyntax elementType)
        : base(sourceType, elementType)
    {
        EnumeratorType = enumeratorType;
    }
}

public class GenEnumerableNode : LinqGenNode
{
    public GenEnumerableNode(IMethodSymbol methodSymbol) : base(methodSymbol) { }

    protected override IEnumerable<LinqGenInstruction> Expand(in ExpansionContext ctx)
    {
        ITypeSymbol targetType = NormalizeSignature((INamedTypeSymbol)MethodSymbol.Parameters[0].Type);

        if (!TryGetEnumerableInterface(targetType, out var enumerableSymbol) ||
            enumerableSymbol.Arity < 1)
        {
            throw new LinqGenException("Can't specialize non-IEnumerable<T>!");
        }

        TypeSyntax elementType = ParseTypeName(enumerableSymbol.TypeArguments[0]);

        return ImmutableList.Create<LinqGenInstruction>(
            new GenEnumerableInstruction(
                EnumerableInterfaceType(elementType),
                EnumeratorInterfaceType(elementType),
                elementType),
            new GenerationRender(IdentifierName(MethodSymbol.Name)));
    }
}

public class GenEnumerableObjectNode : LinqGenNode
{
    public GenEnumerableObjectNode(IMethodSymbol methodSymbol) : base(methodSymbol) { }

    protected override IEnumerable<LinqGenInstruction> Expand(in ExpansionContext ctx)
    {
        return ImmutableList.Create<LinqGenInstruction>(
            new GenEnumerableInstruction(
                IdentifierName("IEnumerable"),
                IdentifierName("IEnumerator"),
                ObjectType),
            new GenerationRender(IdentifierName(MethodSymbol.Name)));
    }
}
