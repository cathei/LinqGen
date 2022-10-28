// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Enumerable that can be boosted with slicing.
    /// For example, OrderBy.
    /// </summary>
    public interface IStructPartition<out TElement, out TEnumerator>
        where TEnumerator : IEnumerator<TElement>
    {
        TEnumerator GetEnumerator();
        TEnumerator GetSliceEnumerator(int skip, int take);
    }

    /// <summary>
    /// Enumerable that can be boosted with slicing, counting and indexing.
    /// For example, Select from IList.
    /// Note that this behaves different from standard List enumerable, where throws exception with version checking.
    /// </summary>
    public interface IStructCollection<out TElement, out TEnumerator> : IStructPartition<TElement, TEnumerator>
        where TEnumerator : IStructCollectionEnumerator<TElement>
    {
        int Count { get; }
    }

    public interface IStructCollectionEnumerator<out TElement> : IEnumerator<TElement>
    {
        int Count { get; }
        TElement Get(int index);
    }
}