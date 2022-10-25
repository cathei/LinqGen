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
        public static Stub<T, Specialize<T>> Specialize<T>(this T enumerable, bool unused = false)
            where T : IEnumerable
        {
            throw new NotImplementedException();
        }

        public static BoxedStub<T, AsEnumerable<TUp>> AsEnumerable<T, TUp>(this IStub<IEnumerable<T>, TUp> enumerable)
        {
            throw new NotImplementedException();
        }

        // only used for auto completion, embedded stub never be public and this method should never be parsed.
        public static Stub<IEnumerable<TOut>, Cast<IStub, TOut>> Cast<TOut>(this IStub enumerable)
        {
            throw new NotImplementedException();
        }

        // only used for auto completion, embedded stub never be public and this method should never be parsed.
        public static Stub<IEnumerable<TOut>, OfType<IStub, TOut>> OfType<TOut>(this IStub enumerable)
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
    }
}