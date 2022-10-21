// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen
{
    public static partial class StubExtensions
    {
        /// <summary>
        /// This will return empty stub, and will be replaced by generated method.
        /// Thus it should never be called on runtime.
        /// Unused parameter ensures that the this version never called with overloading resolution.
        /// </summary>
        public static Stub<T, Gen<T>> Gen<T>(this IEnumerable<T> enumerable, bool unused = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This will return empty stub, and will be replaced by generated method.
        /// Thus it should never be called on runtime.
        /// Unused parameter ensures that the this version never called with overloading resolution.
        /// </summary>
        public static Stub<T, GenList<T>> Gen<T>(this IList<T> enumerable, bool unused = false)
        {
            throw new NotImplementedException();
        }

        public static Stub<T, Where<TOp>> Where<T, TOp>(
            this IStub<T, TOp> enumerable, Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<T, WhereAt<TOp>> Where<T, TOp>(
            this IStub<T, TOp> enumerable, Func<T, int, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<T, WhereStruct<TOp>> Where<T, TOp>(
            this IStub<T, TOp> enumerable, IStructFunction<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<T, WhereAtStruct<TOp>> Where<T, TOp>(
            this IStub<T, TOp> enumerable, IStructFunction<T, int, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static Stub<TOut, Select<TOp, TOut>> Select<T, TOp, TOut>(
            this IStub<T, TOp> enumerable, Func<T, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<TOut, SelectAt<TOp, TOut>> Select<T, TOp, TOut>(
            this IStub<T, TOp> enumerable, Func<T, int, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<TOut, SelectStruct<TOp, TOut>> Select<T, TOp, TOut>(
            this IStub<T, TOp> enumerable, IStructFunction<T, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static Stub<TOut, SelectAtStruct<TOp, TOut>> Select<T, TOp, TOut>(
            this IStub<T, TOp> enumerable, IStructFunction<T, int, TOut> select)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<T> AsEnumerable<T, TOp>(this IStub<T, TOp> enumerable)
        {
            throw new NotImplementedException();
        }
    }
}