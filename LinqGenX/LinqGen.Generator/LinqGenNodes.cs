using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

/// <summary>
/// Nodes correspond with each LinqGen method invocation.
/// Nodes generate list of instructions that is unique for the chain.
/// Nodes can contain symbol information.
/// </summary>
public abstract class LinqGenNode
{
    public readonly IMethodSymbol MethodSymbol;

    private ImmutableList<LinqGenInstruction>? _expanded = null;

    protected LinqGenNode(IMethodSymbol methodSymbol)
    {
        MethodSymbol = methodSymbol;
    }

    protected abstract IEnumerable<LinqGenInstruction> Expand(in ExpansionContext ctx);

    public ImmutableList<LinqGenInstruction> ExpandToInstructions(
        ImmutableList<LinqGenNode> upstream,
        Dictionary<ArgumentSyntax, ImmutableList<LinqGenNode>> arguments)
    {
        if (_expanded != null)
            return _expanded;

        var result = ImmutableList<LinqGenInstruction>.Empty;

        if (upstream.Count >= 2)
        {
            // We can assume that direct upstream is already expanded
            result = upstream[upstream.Count - 2]._expanded!;
        }

        return _expanded = result.AddRange(Expand(new(result, arguments)));
    }

    protected readonly struct ExpansionContext
    {
        public readonly ImmutableList<LinqGenInstruction> Upstream;
        public readonly Dictionary<ArgumentSyntax, ImmutableList<LinqGenNode>> Arguments;

        public ExpansionContext(
            ImmutableList<LinqGenInstruction> upstream,
            Dictionary<ArgumentSyntax, ImmutableList<LinqGenNode>> arguments)
        {
            Upstream = upstream;
            Arguments = arguments;
        }
    }
}

public class GetEnumeratorNode : LinqGenNode
{
    // Allows null argument since this can be deduced from foreach
    public GetEnumeratorNode(IMethodSymbol? methodSymbol = null) : base(methodSymbol!) { }

    protected override IEnumerable<LinqGenInstruction> Expand(in ExpansionContext ctx)
    {
        return new LinqGenInstruction[] { };
    }
}

// public class SelectNode : LinqGenNode
// {
//     public SelectNode(IMethodSymbol methodSymbol) : base(methodSymbol) { }
//
//     protected override IEnumerable<LinqGenInstruction> Expand(in ExpansionContext ctx)
//     {
//     }
// }
//
// public class SelectAtNode : LinqGenNode
// {
//     public SelectAtNode(IMethodSymbol methodSymbol) : base(methodSymbol) { }
// }
//
// public class WhereNode : LinqGenNode
// {
//     public WhereNode(IMethodSymbol methodSymbol) : base(methodSymbol) { }
// }
//
// public class WhereAtNode : LinqGenNode
// {
//     public WhereAtNode(IMethodSymbol methodSymbol) : base(methodSymbol) { }
// }
