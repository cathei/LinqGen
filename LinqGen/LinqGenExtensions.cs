// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using Cathei.LinqGen.Operations;

namespace Cathei.LinqGen
{
    public static partial class LinqGenExtensions
    {
        /// <summary>
        /// This will return empty stub, and will be replaced by generated method.
        /// Thus it should never be called on runtime.
        /// </summary>
        public static StubEnumerable<T, Gen<T>> Generate<T>(this IEnumerable<T> enumerable)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This will return empty stub, and will be replaced by generated method.
        /// Thus it should never be called on runtime.
        /// </summary>
        public static StubEnumerable<T, GenList<T>> Generate<T>(this IList<T> enumerable)
        {
            throw new NotImplementedException();
        }
    }
}