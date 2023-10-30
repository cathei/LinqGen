// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2023

using System;
using System.Collections;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen
{
    public static class StubExtensions
    {
        [LinqGenGeneration("GenEnumerable")]
        public static Stub<T> Gen<T>(this IEnumerable<T> enumerable)
        {
            throw new NotSupportedException();
        }

        [LinqGenGeneration("GenEnumerableObject")]
        public static Stub<object> Gen(this IEnumerable enumerable, bool dummy = false)
        {
            throw new NotSupportedException();
        }

        [LinqGenEvaluation("GetEnumerator")]
        public static IEnumerator<T> GetEnumerator<T>(this IStub<T> stub)
        {
            throw new NotSupportedException();
        }

        [LinqGenOperation("Select")]
        public static Stub<TOut> Select<T, TOut>(this IStub<T> stub, Func<T, TOut> selector)
        {
            throw new NotSupportedException();
        }

        [LinqGenOperation("Select")]
        public static Stub<TOut> Select<T, TOut>(this IStub<T> stub, IStructFunction<T, TOut> selector)
        {
            throw new NotSupportedException();
        }

        [LinqGenOperation("SelectAt")]
        public static Stub<TOut> Select<T, TOut>(this IStub<T> stub, Func<T, int, TOut> selector)
        {
            throw new NotSupportedException();
        }

        [LinqGenOperation("SelectAt")]
        public static Stub<TOut> Select<T, TOut>(this IStub<T> stub, IStructFunction<T, int, TOut> selector)
        {
            throw new NotSupportedException();
        }

        [LinqGenOperation("Where")]
        public static Stub<T> Where<T>(this IStub<T> stub, Func<T, bool> predicate)
        {
            throw new NotSupportedException();
        }

        [LinqGenOperation("Where")]
        public static Stub<T> Where<T>(this IStub<T> stub, IStructFunction<T, bool> predicate)
        {
            throw new NotSupportedException();
        }

        [LinqGenOperation("WhereAt")]
        public static Stub<T> Where<T>(this IStub<T> stub, Func<T, int, bool> predicate)
        {
            throw new NotSupportedException();
        }

        [LinqGenOperation("WhereAt")]
        public static Stub<T> Where<T>(this IStub<T> stub, IStructFunction<T, int, bool> predicate)
        {
            throw new NotSupportedException();
        }
    }
}