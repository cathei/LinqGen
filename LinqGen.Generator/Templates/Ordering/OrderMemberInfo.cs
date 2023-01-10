// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public readonly struct OrderMemberInfo
{
    public readonly OrderingOperation Operation;
    public readonly TypeSyntax? SelectorType;
    public readonly TypeSyntax? ComparerType;
    public readonly TypeSyntax KeyType;
    public readonly ITypeSymbol KeySymbol;

    public OrderMemberInfo(OrderingOperation operation, TypeSyntax? selectorType, TypeSyntax? comparerType,
        TypeSyntax keyType, ITypeSymbol keySymbol)
    {
        Operation = operation;
        SelectorType = selectorType;
        ComparerType = comparerType;
        KeyType = keyType;
        KeySymbol = keySymbol;
    }

    public string Id => Operation.Id;
    public bool Desc => Operation.Descending;

    public IdentifierNameSyntax SelectorName => IdentifierName($"selector_{Id}");
    public IdentifierNameSyntax ComparerName => IdentifierName($"comparer_{Id}");
    public IdentifierNameSyntax KeysName => IdentifierName($"keys_{Id}");

    public TypeSyntax KeyListType => PooledListType(KeyType, KeySymbol.IsUnmanagedType);
}