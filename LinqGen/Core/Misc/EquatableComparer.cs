// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen.Hidden
{
    public struct EquatableComparer<T> : IEqualityComparer<T> where T : IEquatable<T>
    {
        public bool Equals(T? x, T? y)
        {
            if (x == null)
                return y == null;

            if (y == null)
                return false;

            return x.Equals(y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}