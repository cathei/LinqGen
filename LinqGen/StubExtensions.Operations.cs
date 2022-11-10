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
        public static Stub<IContentSource<T>, Specialize<T>> Specialize<T>(this T enumerable)
            where T : IEnumerable
        {
            throw new NotImplementedException();
        }

        public static BoxedStub<T, AsEnumerable<TUp>> AsEnumerable<T, TUp>(this IStub<IContent<T>, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        // fake stub for autocomplete, not used for parsing because can't take signature type from extension
        public static Stub<IContent<TOut>, Cast<Compiled>> Cast<TOut>(this IInternalStub enumerable)
        {
            throw new NotImplementedException();
        }

        // fake stub for autocomplete, not used for parsing because can't take signature type from extension
        public static Stub<IContent<TOut>, OfType<Compiled>> OfType<TOut>(this IInternalStub enumerable)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, Where<TUp>> Where<T, TUp>(
            this IStub<IContent<T>, TUp> enumerable, Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, WhereAt<TUp>> Where<T, TUp>(
            this IStub<IContent<T>, TUp> enumerable, Func<T, int, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, WhereStruct<TUp>> Where<T, TUp>(
            this IStub<IContent<T>, TUp> enumerable, IStructFunction<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, WhereAtStruct<TUp>> Where<T, TUp>(
            this IStub<IContent<T>, TUp> enumerable, IStructFunction<T, int, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<TOut>, Select<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<IContent<T>, TUp> enumerable, Func<T, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<TOut>, SelectAt<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<IContent<T>, TUp> enumerable, Func<T, int, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<TOut>, SelectStruct<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<IContent<T>, TUp> enumerable, IStructFunction<T, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<TOut>, SelectAtStruct<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<IContent<T>, TUp> enumerable, IStructFunction<T, int, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, Skip<TUp>> Skip<T, TUp>(
            this IStub<IContent<T>, TUp> enumerable, int skip)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, Take<TUp>> Take<T, TUp>(
            this IStub<IContent<T>, TUp> enumerable, int take)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, Distinct<TUp>> Distinct<T, TUp>(this IStub<IContent<T>, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, Distinct<TUp>> Distinct<T, TUp, TComparer>(
            this IStub<IContent<T>, TUp> enumerable, TComparer comparer) where TComparer : IEqualityComparer<T>
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IContent<T>, OrderBy<TUp, TKey>> OrderBy<T, TUp, TKey>(
            this IStub<IContent<T>, TUp> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            throw new NotImplementedException();
        }

        public static OrderedStub<IContent<T>, OrderByStruct<TUp, TKey>> OrderBy<T, TUp, TKey, TComparer>(
            this IStub<IContent<T>, TUp> enumerable, IStructFunction<T, TKey> keySelector, TComparer comparer)
            where TComparer : struct, IComparer<TKey>
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
            where TComparer : struct, IComparer<TKey>
        {
            throw new NotImplementedException();
        }
    }
}