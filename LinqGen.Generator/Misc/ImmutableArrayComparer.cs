// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public class ImmutableArrayComparer<T> : IEqualityComparer<ImmutableArray<T>>
{
    public static readonly ImmutableArrayComparer<T> Default = new();

    public readonly IEqualityComparer<T> ElementComparer;

    public ImmutableArrayComparer()
    {
        ElementComparer = EqualityComparer<T>.Default;
    }

    public ImmutableArrayComparer(IEqualityComparer<T> elementComparer)
    {
        ElementComparer = elementComparer;
    }

    public bool Equals(ImmutableArray<T> x, ImmutableArray<T> y)
    {
        if (x.IsDefault)
            return y.IsDefault;

        if (x.Length != y.Length)
            return false;

        for (int i = 0; i < x.Length; ++i)
        {
            if (!ElementComparer.Equals(x[i], y[i]))
                return false;
        }

        return true;
    }

    public int GetHashCode(ImmutableArray<T> arr)
    {
        int hash = 0;

        if (arr.IsDefaultOrEmpty)
            return hash;

        foreach (var element in arr)
        {
            hash = HashCombine(hash, ElementComparer.GetHashCode(element));
        }

        return hash;
    }
}