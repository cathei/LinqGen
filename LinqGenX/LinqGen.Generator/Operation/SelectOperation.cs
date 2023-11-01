using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public class SelectOperation : LinqGenOperation
{
    public readonly TypeSyntax SelectorType;
    public readonly bool WithIndex;

    public SelectOperation(LinqGenInstruction upstream, TypeSyntax selectorType, bool withIndex)
        : base(upstream, ImmutableArray.Create(selectorType))
    {
        SelectorType = selectorType;
        WithIndex = withIndex;
    }
}

public class SelectNode : LinqGenNode
{
    public SelectNode(LinqGenNode? upstream, IMethodSymbol methodSymbol)
        : base(upstream, methodSymbol)
    {}

    protected override LinqGenRender Expand(in ExpansionContext ctx)
    {

    }
}

public class SelectAtNode : LinqGenNode
{
    public SelectAtNode(LinqGenNode? upstream, IMethodSymbol methodSymbol)
        : base(upstream, methodSymbol)
    {}

    protected override LinqGenRender Expand(in ExpansionContext ctx)
    {

    }
}
