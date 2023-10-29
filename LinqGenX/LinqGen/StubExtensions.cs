// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2023

using System;
using System.Collections;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen
{
    public static class StubExtensions
    {
        public static Stub<T> Gen<T>(this IEnumerable<T> enumerable)
        {
            throw new NotSupportedException();
        }

        public static IEnumerator<T> GetEnumerator<T>(this IStub<T> stub)
        {
            throw new NotSupportedException();
        }

        public static Stub<TOut> Select<T, TOut>(this IStub<T> stub, Func<T, TOut> selector)
        {
            throw new NotSupportedException();
        }

        public static Stub<TOut> Select<T, TOut>(this IStub<T> stub, IStructFunction<T, TOut> selector)
        {
            throw new NotSupportedException();
        }

        public static Stub<T> Where<T>(this IStub<T> stub, Func<T, bool> predicate)
        {
            throw new NotSupportedException();
        }

        public static Stub<T> Where<T>(this IStub<T> stub, IStructFunction<T, bool> predicate)
        {
            throw new NotSupportedException();
        }
    }
}