using System;
using System.Collections.Immutable;
using System.Linq;

namespace Cathei.LinqGen.Generator;

public readonly struct LinqGenSignature : IEquatable<LinqGenSignature>
{
    public readonly ImmutableList<LinqGenNode> Nodes;

    public LinqGenSignature(ImmutableList<LinqGenNode> nodes)
    {
        Nodes = nodes;
    }

    public bool Equals(LinqGenSignature other)
    {
        return Nodes.SequenceEqual(other.Nodes);
    }

    public override bool Equals(object? obj)
    {
        return obj is LinqGenSignature other && Equals(other);
    }

    public override int GetHashCode()
    {
        // should not be called
        return 0;
    }
}

