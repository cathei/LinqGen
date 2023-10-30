using System;
using System.Collections.Immutable;
using System.Linq;

namespace Cathei.LinqGen.Generator;

public readonly struct LinqGenSignature : IEquatable<LinqGenSignature>
{
    public readonly ImmutableList<LinqGenInstruction> Instructions;

    public LinqGenSignature(ImmutableList<LinqGenInstruction> instructions)
    {
        Instructions = instructions;
    }

    public bool Equals(LinqGenSignature other)
    {
        return Instructions.SequenceEqual(other.Instructions);
    }

    public override bool Equals(object? obj)
    {
        return obj is LinqGenSignature other && Equals(other);
    }

    public override int GetHashCode()
    {
        int hashCode = 0;

        foreach (var instruction in Instructions)
        {
            hashCode = HashCombine(hashCode, instruction.GetHashCode());
        }

        return hashCode;
    }
}

