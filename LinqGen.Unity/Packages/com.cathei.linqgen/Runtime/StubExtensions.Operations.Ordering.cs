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
        // OrderBy with keySelector and comparer interface
        public static OrderedStub<IEnumerable<T>, OrderBy<TUp, TKey>> OrderBy<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            throw new NotImplementedException();
        }

        // OrderBy with keySelector and comparer struct function
        public static OrderedStub<IEnumerable<T>, OrderByStruct<TUp, TKey>> OrderBy<T, TUp, TKey, TComparer>(
            this IStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : IComparer<TKey>
        {
            throw new NotImplementedException();
        }

        // OrderBy with comparer interface
        public static OrderedStub<IEnumerable<T>, OrderBySelf<TUp>> OrderBy<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, IComparer<T>? comparer = null)
        {
            throw new NotImplementedException();
        }

        // OrderBy with comparer struct function
        public static OrderedStub<IEnumerable<T>, OrderBySelfStruct<TUp>> OrderBy<T, TUp, TComparer>(
            this IStub<IEnumerable<T>, TUp> enumerable, TComparer comparer)
            where TComparer : struct, IComparer<T>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderByDesc<TUp, TKey>> OrderByDescending<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderByDescStruct<TUp, TKey>> OrderByDescending<T, TUp, TKey, TComparer>(
            this IStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : IComparer<TKey>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderByDescSelf<TUp>> OrderByDescending<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, IComparer<T>? comparer = null)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, OrderByDescSelfStruct<TUp>> OrderByDescending<T, TUp, TComparer>(
            this IStub<IEnumerable<T>, TUp> enumerable, TComparer comparer)
            where TComparer : struct, IComparer<T>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenBy<TUp, TKey>> ThenBy<T, TUp, TKey>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByStruct<TUp, TKey>> ThenBy<T, TUp, TKey, TComparer>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : IComparer<TKey>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenBySelf<TUp>> ThenBy<T, TUp>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, IComparer<T>? comparer = null)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenBySelfStruct<TUp>> ThenBy<T, TUp, TComparer>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, TComparer comparer)
            where TComparer : struct, IComparer<T>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByDesc<TUp, TKey>> ThenByDescending<T, TUp, TKey>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByDescStruct<TUp, TKey>> ThenByDescending<T, TUp, TKey, TComparer>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : IComparer<TKey>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByDescSelf<TUp>> ThenByDescending<T, TUp>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, IComparer<T>? comparer = null)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IEnumerable<T>, ThenByDescSelfStruct<TUp>> ThenByDescending<T, TUp, TComparer>(
            this IOrderedStub<IEnumerable<T>, TUp> enumerable, TComparer comparer)
            where TComparer : struct, IComparer<T>
        {
            throw new NotImplementedException();
        }
    }
}