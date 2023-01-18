// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;

namespace Cathei.LinqGen
{
    public interface IStructEnumerable<out T, out TEnumerator>
        where TEnumerator : IEnumerator<T>
    {
        public TEnumerator GetEnumerator();
    }
}