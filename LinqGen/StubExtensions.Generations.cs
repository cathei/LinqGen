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
        public static Stub<T, Gen<T>> Gen<T>(this IEnumerable<T> enumerable, bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<T, GenList<T>> Gen<T>(this IList<T> enumerable, bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static BoxedStub<T, AsEnumerable<TUp>> AsEnumerable<T, TUp>(this IStub<T, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        // only used for auto completion, embedded stub never be public and this method should never be parsed.
        public static Stub<TOut, Cast<IEmbeddedStub, TOut>> Cast<TOut>(this IEmbeddedStub enumerable)
        {
            throw new NotImplementedException();
        }

        // only used for auto completion, embedded stub never be public and this method should never be parsed.
        public static Stub<TOut, OfType<IEmbeddedStub, TOut>> OfType<TOut>(this IEmbeddedStub enumerable)
        {
            throw new NotImplementedException();
        }

        public static Stub<T, Where<TUp>> Where<T, TUp>(
            this IStub<T, TUp> enumerable, Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<T, WhereAt<TUp>> Where<T, TUp>(
            this IStub<T, TUp> enumerable, Func<T, int, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<T, WhereStruct<TUp>> Where<T, TUp>(
            this IStub<T, TUp> enumerable, IStructFunction<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<T, WhereAtStruct<TUp>> Where<T, TUp>(
            this IStub<T, TUp> enumerable, IStructFunction<T, int, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<TOut, Select<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<T, TUp> enumerable, Func<T, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<TOut, SelectAt<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<T, TUp> enumerable, Func<T, int, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<TOut, SelectStruct<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<T, TUp> enumerable, IStructFunction<T, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<TOut, SelectAtStruct<TUp, TOut>> Select<T, TUp, TOut>(
            this IStub<T, TUp> enumerable, IStructFunction<T, int, TOut> select)
        {
            throw new NotImplementedException();
        }
    }
}