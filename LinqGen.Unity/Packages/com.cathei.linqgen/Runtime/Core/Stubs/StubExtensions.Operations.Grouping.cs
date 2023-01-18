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
        #region Managed Overloads

        public static Stub<IEnumerable<GroupingManaged<TKey, T>>, GroupBy<TUp, TKey>>
            GroupBy<T, TUp, TKey>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, T>>, GroupByComparer<TUp, TKey>>
            GroupBy<T, TUp, TKey>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                IEqualityComparer<TKey> comparer,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, TElement>>, GroupByElement<TUp, TKey, TElement>>
            GroupBy<T, TUp, TKey, TElement>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<T, TElement> elementSelector,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, T>>, GroupByElementComparer<TUp, TKey, TElement>>
            GroupBy<T, TUp, TKey, TElement>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<T, TElement> elementSelector,
                IEqualityComparer<TKey> comparer,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, T>>, GroupByResult<TUp, TKey, TResult>>
            GroupBy<T, TUp, TKey, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<TKey, GroupingManaged<TKey, T>, TResult> resultSelector,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, T>>, GroupByResultComparer<TUp, TKey, TResult>>
            GroupBy<T, TUp, TKey, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<TKey, GroupingManaged<TKey, T>, TResult> resultSelector,
                IEqualityComparer<TKey> comparer,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, TElement>>,
                GroupByElementResult<TUp, TKey, TElement, TResult>>
            GroupBy<T, TUp, TKey, TElement, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<T, TElement> elementSelector,
                Func<TKey, GroupingManaged<TKey, TElement>, TResult> resultSelector,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, TElement>>,
                GroupByElementResultComparer<TUp, TKey, TElement, TResult>>
            GroupBy<T, TUp, TKey, TElement, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<T, TElement> elementSelector,
                Func<TKey, GroupingManaged<TKey, TElement>, TResult> resultSelector,
                IEqualityComparer<TKey> comparer,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, T>>, GroupByStruct<TUp, TKey>>
            GroupBy<T, TUp, TKey>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, T>>, GroupByStructComparer<TUp, TKey>>
            GroupBy<T, TUp, TKey>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IEqualityComparer<TKey> comparer,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, TElement>>, GroupByStructElement<TUp, TKey, TElement>>
            GroupBy<T, TUp, TKey, TElement>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<T, TElement> elementSelector,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static
            Stub<IEnumerable<GroupingManaged<TKey, TElement>>, GroupByStructElementComparer<TUp, TKey, TElement>>
            GroupBy<T, TUp, TKey, TElement>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<T, TElement> elementSelector,
                IEqualityComparer<TKey> comparer,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, T>>, GroupByStructResult<TUp, TKey, TResult>>
            GroupBy<T, TUp, TKey, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<TKey, GroupingManaged<TKey, T>, TResult> resultSelector,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, T>>, GroupByStructResultComparer<TUp, TKey, TResult>>
            GroupBy<T, TUp, TKey, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<TKey, GroupingManaged<TKey, T>, TResult> resultSelector,
                IEqualityComparer<TKey> comparer,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, TElement>>,
                GroupByStructElementResult<TUp, TKey, TElement, TResult>>
            GroupBy<T, TUp, TKey, TElement, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<T, TElement> elementSelector,
                IStructFunction<TKey, GroupingManaged<TKey, TElement>, TResult> resultSelector,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingManaged<TKey, TElement>>,
                GroupByStructElementResultComparer<TUp, TKey, TElement, TResult>>
            GroupBy<T, TUp, TKey, TElement, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<T, TElement> elementSelector,
                IStructFunction<TKey, GroupingManaged<TKey, TElement>, TResult> resultSelector,
                IEqualityComparer<TKey> comparer,
                bool unused = false)
        {
            throw new NotImplementedException();
        }

        #endregion Managed Overloads

        #region Native Overloads

        public static Stub<IEnumerable<GroupingNative<TKey, T>>, GroupBy<TUp, TKey>>
            GroupBy<T, TUp, TKey>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector)
            where T : unmanaged
            where TKey : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, T>>, GroupByComparer<TUp, TKey>>
            GroupBy<T, TUp, TKey>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                IEqualityComparer<TKey> comparer)
            where T : unmanaged
            where TKey : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, TElement>>, GroupByElement<TUp, TKey, TElement>>
            GroupBy<T, TUp, TKey, TElement>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<T, TElement> elementSelector)
            where TKey : unmanaged
            where TElement : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, TElement>>, GroupByElementComparer<TUp, TKey, TElement>>
            GroupBy<T, TUp, TKey, TElement>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<T, TElement> elementSelector,
                IEqualityComparer<TKey> comparer)
            where TKey : unmanaged
            where TElement : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, T>>, GroupByResult<TUp, TKey, TResult>>
            GroupBy<T, TUp, TKey, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<TKey, GroupingNative<TKey, T>, TResult> resultSelector)
            where T : unmanaged
            where TKey : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, T>>, GroupByResultComparer<TUp, TKey, TResult>>
            GroupBy<T, TUp, TKey, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<TKey, GroupingNative<TKey, T>, TResult> resultSelector,
                IEqualityComparer<TKey> comparer)
            where T : unmanaged
            where TKey : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, TElement>>,
                GroupByElementResult<TUp, TKey, TElement, TResult>>
            GroupBy<T, TUp, TKey, TElement, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<T, TElement> elementSelector,
                Func<TKey, GroupingNative<TKey, TElement>, TResult> resultSelector)
            where TKey : unmanaged
            where TElement : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, TElement>>,
                GroupByElementResultComparer<TUp, TKey, TElement, TResult>>
            GroupBy<T, TUp, TKey, TElement, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                Func<T, TKey> keySelector,
                Func<T, TElement> elementSelector,
                Func<TKey, GroupingNative<TKey, TElement>, TResult> resultSelector,
                IEqualityComparer<TKey> comparer)
            where TKey : unmanaged
            where TElement : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, T>>, GroupByStruct<TUp, TKey>>
            GroupBy<T, TUp, TKey>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector)
            where T : unmanaged
            where TKey : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, T>>, GroupByStructComparer<TUp, TKey>>
            GroupBy<T, TUp, TKey>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IEqualityComparer<TKey> comparer)
            where T : unmanaged
            where TKey : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, TElement>>, GroupByStructElement<TUp, TKey, TElement>>
            GroupBy<T, TUp, TKey, TElement>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<T, TElement> elementSelector)
            where TKey : unmanaged
            where TElement : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, TElement>>,
                GroupByStructElementComparer<TUp, TKey, TElement>>
            GroupBy<T, TUp, TKey, TElement>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<T, TElement> elementSelector,
                IEqualityComparer<TKey> comparer)
            where TKey : unmanaged
            where TElement : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, T>>, GroupByStructResult<TUp, TKey, TResult>>
            GroupBy<T, TUp, TKey, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<TKey, GroupingNative<TKey, T>, TResult> resultSelector)
            where T : unmanaged
            where TKey : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, T>>, GroupByStructResultComparer<TUp, TKey, TResult>>
            GroupBy<T, TUp, TKey, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<TKey, GroupingNative<TKey, T>, TResult> resultSelector,
                IEqualityComparer<TKey> comparer)
            where T : unmanaged
            where TKey : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, TElement>>,
                GroupByStructElementResult<TUp, TKey, TElement, TResult>>
            GroupBy<T, TUp, TKey, TElement, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<T, TElement> elementSelector,
                IStructFunction<TKey, GroupingNative<TKey, TElement>, TResult> resultSelector)
            where TKey : unmanaged
            where TElement : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<GroupingNative<TKey, TElement>>,
                GroupByStructElementResultComparer<TUp, TKey, TElement, TResult>>
            GroupBy<T, TUp, TKey, TElement, TResult>(
                this IStub<IEnumerable<T>, TUp> source,
                IStructFunction<T, TKey> keySelector,
                IStructFunction<T, TElement> elementSelector,
                IStructFunction<TKey, GroupingNative<TKey, TElement>, TResult> resultSelector,
                IEqualityComparer<TKey> comparer)
            where TKey : unmanaged
            where TElement : unmanaged
        {
            throw new NotImplementedException();
        }

        #endregion Native Overloads
    }
}