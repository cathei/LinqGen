// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;
using Unity.Collections;

namespace Cathei.LinqGen
{
    public static partial class StubExtensions
    {
        public static Stub<IEnumerable<T>, Gen<NativeArray<T>>> Gen<T>(this NativeArray<T> enumerable)
            where T : struct
        {
            throw new NotImplementedException();
        }
    }
}
