// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

#nullable enable

using System;
using System.Collections.Generic;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Specialized equality comparer for IEquatable implementing types.
    /// </summary>
    public struct EquatableComparer<T> : IEqualityComparer<T> where T : class, IEquatable<T>
    {
        public bool Equals(T? x, T? y)
        {
            if (x == null)
                return y == null;

            // Equals allows null argument
#pragma warning disable CS8604
            return x.Equals(y);
#pragma warning restore CS8604
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }

    /// <summary>
    /// Specialized equality comparer for IEquatable implementing types.
    /// Comparing to null with value type can cause boxing allocation in Debug settings.
    /// https://github.com/cathei/LinqGen/issues/4
    /// </summary>
    public struct ValueEquatableComparer<T> : IEqualityComparer<T> where T : struct, IEquatable<T>
    {
        public bool Equals(T x, T y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}