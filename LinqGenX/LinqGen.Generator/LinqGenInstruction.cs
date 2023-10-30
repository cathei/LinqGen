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
    public readonly LinqGenInstruction? Upstream;
    public readonly ImmutableArray<TypeSyntax> Identity;

    protected LinqGenInstruction(LinqGenInstruction? upstream, in ImmutableArray<TypeSyntax> identity)
    {
        Upstream = upstream;
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

        if (Upstream != null)
            return Upstream.Equals(other.Upstream);

        return other.Upstream == null;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as LinqGenInstruction);
    }

    public override int GetHashCode()
    {
        return (int)UniqueId;
    }

    private uint? _uniqueId = null;

    public uint UniqueId
    {
        get
        {
            if (_uniqueId.HasValue)
                return _uniqueId.Value;

            int id = StableHashCode(GetType().Name);

            if (Upstream != null)
                id = HashCombine(id, Upstream.GetHashCode());

            foreach (var type in Identity)
                id = HashCombine(id, StableHashCode(type.ToFullString()));

            _uniqueId = (uint)id;
            return _uniqueId.Value;
        }
    }

    protected readonly struct MemberInfo
    {
        public readonly uint UniqueId;
        public readonly TypeSyntax Type;
        public readonly string Name;
        public readonly ExpressionSyntax? DefaultValue;

        public MemberInfo(uint id, TypeSyntax type, string name, ExpressionSyntax? defaultValue = null)
        {
            UniqueId = id;
            Type = type;
            Name = name;
            DefaultValue = defaultValue;
        }
    }

    /// <summary>
    /// Will be used as method parameters.
    /// </summary>
    protected abstract void GetParameters(List<MemberInfo> infos);

    /// <summary>
    /// Will be added as Enumerable member.
    /// If return value is false, invalidates upstream members.
    /// </summary>
    protected virtual bool GetEnumerableMembers(List<MemberInfo> infos)
    {
        return true;
    }

    /// <summary>
    /// Will be added as Enumerator member.
    /// If return value is false, invalidates upstream members.
    /// </summary>
    protected virtual bool GetEnumeratorMembers(List<MemberInfo> infos)
    {
        return true;
    }

    /// <summary>
    /// Will be added as local evaluation member.
    /// If return value is false, invalidates upstream members.
    /// </summary>
    protected virtual bool GetLocalMembers(List<MemberInfo> infos)
    {
        return true;
    }
}
