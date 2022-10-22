// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen
{
    /// <summary>
    /// This will return empty stub, and will be replaced by generated method.
    /// Thus it should never be called on runtime.
    /// Unused parameter ensures that the this version never called with overloading resolution.
    /// </summary>
    public static partial class StubExtensions
    {
        public static IEnumerable<T> AsEnumerable<T, TUp>(this IStub<T, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        public static T First<T, TUp>(this IStub<T, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        public static T FirstOrDefault<T, TUp>(this IStub<T, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        public static T Last<T, TUp>(this IStub<T, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        public static T LastOrDefault<T, TUp>(this IStub<T, TUp> enumerable)
        {
            throw new NotImplementedException();
        }
    }
}