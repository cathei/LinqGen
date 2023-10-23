// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public class SetEqualComparer<T> : IEqualityComparer<HashSet<T>>
{
    public static readonly SetEqualComparer<T> Default = new();

    public bool Equals(HashSet<T> x, HashSet<T> y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            return false;

        if (x.Count != y.Count)
            return false;

        foreach (var elem in x)
        {
            if (!y.Contains(elem))
                return false;
        }

        return true;
    }

    public int GetHashCode(HashSet<T> arr)
    {
        // Should not be called
        return 0;
    }
}