// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Enumerable that can be boosted with slicing.
    /// For example, OrderBy.
    /// </summary>
    public interface IPartition<out TEnumerator>
        where TEnumerator : IEnumerator
    {
        TEnumerator GetSliceEnumerator(int skip, int? take);
    }

    /// <summary>
    /// Enumerable that can be boosted with counting
    /// For example, IList.
    /// </summary>
    public interface ICountable
    {
        int Count { get; }
    }
}