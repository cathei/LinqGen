using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

/// <summary>
/// Nodes correspond with each LinqGen method invocation.
/// Nodes generate list of instructions that is unique for the chain.
/// Nodes can contain symbol information.
/// </summary>
public abstract class LinqGenNode
{
    public readonly LinqGenNode? Upstream;
    public readonly IMethodSymbol MethodSymbol;

    private LinqGenRender? _expanded = null;

    protected LinqGenNode(LinqGenNode? upstream, IMethodSymbol methodSymbol)
    {
        Upstream = upstream;
        MethodSymbol = methodSymbol;
    }

    protected abstract LinqGenRender Expand(in ExpansionContext ctx);

    public LinqGenRender ExpandToRender(Dictionary<ArgumentSyntax, LinqGenNode> arguments)
    {
        if (_expanded != null)
            return _expanded;

        var upstreamRender = Upstream?.ExpandToRender(arguments);
        return _expanded = Expand(new(upstreamRender, arguments));
    }

    protected readonly struct ExpansionContext
    {
        public readonly LinqGenRender? Upstream;
        public readonly Dictionary<ArgumentSyntax, LinqGenNode> Arguments;

        public ExpansionContext(
            LinqGenRender? upstream,
            Dictionary<ArgumentSyntax, LinqGenNode> arguments)
        {
            Upstream = upstream;
            Arguments = arguments;
        }
    }
}

public class GetEnumeratorNode : LinqGenNode
{
    // Allows null argument since this can be deduced from foreach
    public GetEnumeratorNode(LinqGenNode upstream, IMethodSymbol? methodSymbol = null)
        : base(upstream, methodSymbol!) { }

    protected override LinqGenRender Expand(in ExpansionContext ctx)
    {
        return null!;
    }
}
