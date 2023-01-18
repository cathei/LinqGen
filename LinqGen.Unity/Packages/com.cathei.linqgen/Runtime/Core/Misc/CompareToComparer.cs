// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Specialized comparer for IComparer implementing types.
    /// </summary>
    public struct CompareToComparer<T> : IComparer<T> where T : class, IComparable<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(T? x, T? y)
        {
            if (x == null)
                return y == null ? 0 : -1;

            // CompareTo allows null argument
#pragma warning disable CS8604
            return x.CompareTo(y);
#pragma warning restore CS8604
        }
    }

    /// <summary>
    /// Specialized comparer for IComparer implementing types.
    /// Comparing to null with value type can cause boxing allocation in Debug settings.
    /// https://github.com/cathei/LinqGen/issues/4
    /// </summary>
    public struct ValueCompareToComparer<T> : IComparer<T> where T : struct, IComparable<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(T x, T y)
        {
            return x.CompareTo(y);
        }
    }
}