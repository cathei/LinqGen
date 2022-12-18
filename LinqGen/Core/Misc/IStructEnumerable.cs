// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen
{
    public interface IStructEnumerable<out T, out TEnumerator>
        where TEnumerator : IEnumerator<T>
    {
        public TEnumerator GetEnumerator();
    }
}