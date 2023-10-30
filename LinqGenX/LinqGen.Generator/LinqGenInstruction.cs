using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Cathei.LinqGen.Generator;

/// <summary>
/// Instructions correspond to unique LinqGen hierarchy.
/// Single node can be expanded to multiple instructions.
/// Identical list of instructions produces identical source code.
/// Instructions does not contain any symbol information.
/// </summary>
public abstract class LinqGenInstruction : IEquatable<LinqGenInstruction>
{
    public readonly ImmutableArray<TypeSyntax> Identity;

    public LinqGenInstruction(in ImmutableArray<TypeSyntax> identity)
    {
        Identity = identity;
    }

    public bool Equals(LinqGenInstruction? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (other.GetType() != GetType())
            return false;

        if (Identity.Length != other.Identity.Length)
            return false;

        for (int i = 0; i < Identity.Length; ++i)
        {
            if (!Identity[i].IsEquivalentTo(other.Identity[i]))
                return false;
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as LinqGenInstruction);
    }

    private int? _hashCode = null;

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        if (_hashCode.HasValue)
            return _hashCode.Value;

        int hashCode = StableHashCode(GetType().Name);

        foreach (var type in Identity)
        {
            hashCode = HashCombine(hashCode, StableHashCode(type.ToFullString()));
        }

        _hashCode = hashCode;
        return hashCode;
    }

    // Generate stable hash code across executions
    private static int StableHashCode(string str)
    {
        int hashCode = 0;

        foreach (var ch in str)
        {
            hashCode = HashCombine(hashCode, ch);
        }

        return hashCode;
    }
}
