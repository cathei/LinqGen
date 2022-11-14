using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Do not use this struct manually, reserved for generated code
    /// </summary>
    public struct PooledList<T> : IDisposable
    {
        private T[] array;
        private int count;

        private static readonly T[] EmptyArray = new T[0];

        public PooledList(int capacity)
        {
            array = capacity > 0 ? SharedArrayPool<T>.Rent(capacity) : EmptyArray;
            count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseCapacity()
        {
            var newItems = SharedArrayPool<T>.Rent(count + 1);

            if (count > 0)
                System.Array.Copy(array, newItems, count);

            ReturnArray();
            array = newItems;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReturnArray()
        {
            if (array.Length == 0)
                return;

            try
            {
                // Clear the elements so that the gc can reclaim the references.
                SharedArrayPool<T>.Return(array, default(T) is not ValueType);
            }
            catch (ArgumentException)
            {
                // oh well, the array pool didn't like our array
            }

            array = EmptyArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            var localArray = array;
            uint index = unchecked((uint)count);

            if (index >= array.Length)
            {
                IncreaseCapacity();
                localArray = array;
            }

            localArray[index] = item;
            count++;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => count;
        }

        public T[] Array => array;

        public T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => array[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => array[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            ReturnArray();
            count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArray()
        {
#if NET5_0_OR_GREATER
            var result = GC.AllocateUninitializedArray<T>(count);
#else
            var result = new T[count];
#endif
            System.Array.Copy(array, result, count);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<T> ToList()
        {
            var localArray = array;
            var result = new List<T>(count);

            // TODO this can be optimized with Unsafe..
            for (int i = 0; i < count; ++i)
                result.Add(localArray[i]);

            return result;
        }
    }
}
