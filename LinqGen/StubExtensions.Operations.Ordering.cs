// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
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
        public static OrderedStub<IContent<T>, OrderBy<TUp, TKey>> OrderBy<T, TUp, TKey>(
            this IStub<IContent<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IContent<T>, OrderByStruct<TUp, TKey>> OrderBy<T, TUp, TKey, TComparer>(
            this IStub<IContent<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : IComparer<TKey>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IContent<T>, ThenBy<TUp, TKey>> ThenBy<T, TUp, TKey>(
            this IOrderedStub<IContent<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IContent<T>, ThenByStruct<TUp, TKey>> ThenBy<T, TUp, TKey, TComparer>(
            this IOrderedStub<IContent<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : IComparer<TKey>
        {
            throw new NotImplementedException();
        }
    }
}