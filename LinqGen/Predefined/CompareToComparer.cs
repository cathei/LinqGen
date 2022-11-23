// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen.Hidden
{
    public struct CompareToComparer<T> : IComparer<T> where T : IComparable<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(T? x, T? y)
        {
            if (x == null)
                return y == null ? 0 : -1;

            if (y == null)
                return 1;

            return x.CompareTo(y);
        }
    }
}
