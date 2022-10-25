// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen
{
    public interface IStructFunction<in T, out TOut>
    {
        public TOut Invoke(T arg);
    }

    public interface IStructFunction<in T1, in T2, out TOut>
    {
        public TOut Invoke(T1 arg1, T2 arg2);
    }
}