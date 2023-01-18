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
        // fake stub for autocomplete, not used for parsing because can't take signature type from extension
        public static Stub<IEnumerable<TOut>, Cast<Compiled>> Cast<TOut>(this IInternalStub enumerable)
        {
            throw new NotImplementedException();
        }

        // fake stub for autocomplete, not used for parsing because can't take signature type from extension
        public static Stub<IEnumerable<TOut>, OfType<Compiled>> OfType<TOut>(this IInternalStub enumerable)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Where<TUp>> Where<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, WhereAt<TUp>> Where<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, Func<T, int, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, WhereStruct<TUp>> Where<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, WhereAtStruct<TUp>> Where<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, int, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<TOut>, Select<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<IEnumerable<T>, TUp> enumerable, Func<T, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<TOut>, SelectAt<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<IEnumerable<T>, TUp> enumerable, Func<T, int, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<TOut>, SelectStruct<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<TOut>, SelectAtStruct<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<IEnumerable<T>, TUp> enumerable, IStructFunction<T, int, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Skip<TUp>> Skip<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, int skip)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Take<TUp>> Take<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, int take)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Distinct<TUp>> Distinct<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, DistinctComparer<TUp>> Distinct<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, IEqualityComparer<T> comparer)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, DistinctStruct<TUp>> Distinct<T, TUp, TComparer>(
            this IStub<IEnumerable<T>, TUp> enumerable, TComparer comparer)
            where TComparer : struct, IEqualityComparer<T>
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Concat<TUp1, TUp2>> Concat<T, TUp1, TUp2>(
            this IStub<IEnumerable<T>, TUp1> first, IStub<IEnumerable<T>, TUp2> second)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Prepend<TUp>> Prepend<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, T element)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Append<TUp>> Append<T, TUp>(
            this IStub<IEnumerable<T>, TUp> enumerable, T element)
        {
            throw new NotImplementedException();
        }
    }
}