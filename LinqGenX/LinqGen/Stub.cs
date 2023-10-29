// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2023

using System;
using System.Collections;
using System.Collections.Generic;

namespace Cathei.LinqGen.Hidden
{
    public interface IStub<out T> : IEnumerable<T> { }

    public class Stub<T> : IStub<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException();
        }
    }

    public interface IStructFunction<in T, out TOut>
    {
        public TOut Invoke(T arg);
    }

    public interface IStructFunction<in T1, in T2, out TOut>
    {
        public TOut Invoke(T1 arg1, T2 arg2);
    }
}