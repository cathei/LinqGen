// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// This interface is guideline for dynamic array implementation
    /// </summary>
    public interface IDynamicArray<T> : IDisposable
    {
        void IncreaseCapacity(int newSize, int copyCount = 0);

        void Clear();

        void CopyTo(T[] array, int count);

        ref T this[int index] { get; }

        ref T this[uint index] { get; }

        int Length { get; }
    }
}