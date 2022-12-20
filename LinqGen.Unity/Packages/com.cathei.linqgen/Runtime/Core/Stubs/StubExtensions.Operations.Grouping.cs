// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupBy<TUp, TKey>> GroupBy<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> source,
            Func<T, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByComparer<TUp, TKey>> GroupBy<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> source,
            Func<T, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByElement<TUp, TKey, TElement>> GroupBy<T, TUp, TKey, TElement>(
            this IStub<IEnumerable<T>, TUp> source,
            Func<T, TKey> keySelector,
            Func<T, TElement> elementSelector)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByElementComparer<TUp, TKey, TElement>> GroupBy<T, TUp, TKey, TElement>(
            this IStub<IEnumerable<T>, TUp> source,
            Func<T, TKey> keySelector,
            Func<T, TElement> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByResult<TUp, TKey, TResult>> GroupBy<T, TUp, TKey, TResult>(
            this IStub<IEnumerable<T>, TUp> source,
            Func<T, TKey> keySelector,
            Func<TKey, IEnumerable<T>, TResult> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByResultComparer<TUp, TKey, TResult>> GroupBy<T, TUp, TKey, TResult>(
            this IStub<IEnumerable<T>, TUp> source,
            Func<T, TKey> keySelector,
            Func<TKey, IEnumerable<T>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByElementResult<TUp, TKey, TElement, TResult>> GroupBy<T, TUp, TKey, TElement, TResult>(
            this IStub<IEnumerable<T>, TUp> source,
            Func<T, TKey> keySelector,
            Func<T, TElement> elementSelector,
            Func<TKey, IEnumerable<T>, TResult> resultSelector)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByElementResultComparer<TUp, TKey, TElement, TResult>> GroupBy<T, TUp, TKey, TElement, TResult>(
            this IStub<IEnumerable<T>, TUp> source,
            Func<T, TKey> keySelector,
            Func<T, TElement> elementSelector,
            Func<TKey, IEnumerable<T>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByStruct<TUp, TKey>> GroupBy<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> source,
            IStructFunction<T, TKey> keySelector)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByStructComparer<TUp, TKey>> GroupBy<T, TUp, TKey>(
            this IStub<IEnumerable<T>, TUp> source,
            IStructFunction<T, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByStructElement<TUp, TKey, TElement>> GroupBy<T, TUp, TKey, TElement>(
            this IStub<IEnumerable<T>, TUp> source,
            IStructFunction<T, TKey> keySelector,
            IStructFunction<T, TElement> elementSelector)
        {
            throw new NotImplementedException();
        }

        public static IStub<IEnumerable<IGrouping<TKey, T>>, GroupByStructElementComparer<TUp, TKey, TElement>> GroupBy<T, TUp, TKey, TElement>(
            this IStub<IEnumerable<T>, TUp> source,
            IStructFunction<T, TKey> keySelector,
            IStructFunction<T, TElement> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }
    }
}