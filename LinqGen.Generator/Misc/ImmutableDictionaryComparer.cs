// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public class ImmutableDictionaryComparer<TKey, TValue> : IEqualityComparer<ImmutableDictionary<TKey, TValue>>
    where TKey : notnull
{
    public static readonly ImmutableDictionaryComparer<TKey, TValue> Default = new();

    public readonly IEqualityComparer<TKey> KeyComparer;
    public readonly IEqualityComparer<TValue> ValueComparer;

    public ImmutableDictionaryComparer()
    {
        KeyComparer = EqualityComparer<TKey>.Default;
        ValueComparer = EqualityComparer<TValue>.Default;
    }

    public ImmutableDictionaryComparer(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
    {
        KeyComparer = keyComparer;
        ValueComparer = valueComparer;
    }

    public bool Equals(ImmutableDictionary<TKey, TValue>? x, ImmutableDictionary<TKey, TValue>? y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            return false;

        if (x.Count != y.Count)
            return false;

        foreach (var pair in x)
        {
            if (!y.TryGetValue(pair.Key, out var otherValue))
                return false;

            if (!ValueComparer.Equals(pair.Value, otherValue))
                return false;
        }

        return true;
    }

    public int GetHashCode(ImmutableDictionary<TKey, TValue> dict)
    {
        int hash = 0;

        foreach (var pair in dict)
        {
            // Just XOR to get same result with same set
            hash ^= KeyComparer.GetHashCode(pair.Key);
            hash ^= ValueComparer.GetHashCode(pair.Value);
        }

        return hash;
    }
}