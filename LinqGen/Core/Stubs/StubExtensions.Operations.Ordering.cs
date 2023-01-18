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
        // OrderBy with keySelector
        public static OrderedStub<IEnumerable<T>, OrderBy<TUp, TKey>> OrderBy<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        // OrderBy with struct keySelector
        public static OrderedStub<IEnumerable<T>, OrderByKey<TUp, TKey>> OrderBy<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        // OrderBy with keySelector and comparer interface
        public static OrderedStub<IEnumerable<T>, OrderByComparer<TUp, TKey>> OrderBy<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        // OrderBy with keySelector and comparer struct function
        public static OrderedStub<IEnumerable<T>, OrderByStruct<TUp, TKey>> OrderBy<T, TUp, TKey, TComparer>(
            this IStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : struct, IComparer<TKey>
        {
            throw new NotImplementedException();
        }

        // OrderBy with self key
        public static OrderedStub<IEnumerable<T>, Order<TUp>> Order<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        // OrderBy with comparer interface
        public static OrderedStub<IEnumerable<T>, OrderComparer<TUp>> Order<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, IComparer<T> comparer)
        {
            throw new NotImplementedException();
        }

        // OrderBy with comparer struct function
        public static OrderedStub<IEnumerable<T>, OrderStruct<TUp>> Order<T, TUp, TComparer>(
            this IStub<IEnumerable<T>, TUp> enumerable, TComparer comparer)
            where TComparer : struct, IComparer<T>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderByDesc<TUp, TKey>> OrderByDescending<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderByDescKey<TUp, TKey>> OrderByDescending<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderByDescComparer<TUp, TKey>> OrderByDescending<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderByDescStruct<TUp, TKey>> OrderByDescending<T, TUp, TKey, TComparer>(
            this IStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : struct, IComparer<TKey>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderDesc<TUp>> OrderDescending<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderDescComparer<TUp>> OrderDescending<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, IComparer<T> comparer)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderDescStruct<TUp>> OrderDescending<T, TUp, TComparer>(
            this IStub<IEnumerable<T>, TUp> enumerable, TComparer comparer)
            where TComparer : struct, IComparer<T>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenBy<TUp, TKey>> ThenBy<T, TUp, TKey>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByKey<TUp, TKey>> ThenBy<T, TUp, TKey>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByComparer<TUp, TKey>> ThenBy<T, TUp, TKey>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByStruct<TUp, TKey>> ThenBy<T, TUp, TKey, TComparer>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : struct, IComparer<TKey>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByDesc<TUp, TKey>> ThenByDescending<T, TUp, TKey>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByDescKey<TUp, TKey>> ThenByDescending<T, TUp, TKey>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByDescComparer<TUp, TKey>> ThenByDescending<T, TUp, TKey>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByDescStruct<TUp, TKey>> ThenByDescending<T, TUp, TKey, TComparer>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : struct, IComparer<TKey>
        {
            throw new NotImplementedException();
        }
    }
}