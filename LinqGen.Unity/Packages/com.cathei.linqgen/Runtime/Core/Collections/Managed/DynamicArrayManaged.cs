using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    public struct DynamicArrayManaged<T> : IDynamicArray<T>
    {
        private T[] _array;

        private static readonly T[] EmptyArray = Array.Empty<T>();

        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _array[index];
        }

        public ref T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _array[index];
        }

        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _array.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DynamicArrayManaged(int capacity) : this()
        {
            _array = capacity > 0 ? SharedArrayPool<T>.Rent(capacity) : EmptyArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IncreaseCapacity(int newSize, int copyCount = 0)
        {
            var newItems = SharedArrayPool<T>.Rent(newSize);
            if (copyCount > 0)
                Array.Copy(_array, newItems, copyCount);
            ReturnArray();
            _array = newItems;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            Array.Clear(_array, 0, _array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReturnArray()
        {
            if (_array == null)
                return;

            try
            {
                // Clear the elements so that the gc can reclaim the references.
                SharedArrayPool<T>.Return(_array, true);
            }
            catch (ArgumentException)
            {
                // oh well, the array pool didn't like our array
            }

            _array = null!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, int count)
        {
            if (_array == null)
                return;

            Array.Copy(_array, array, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            ReturnArray();
        }
    }
}
