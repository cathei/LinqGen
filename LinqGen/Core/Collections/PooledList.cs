using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Do not use this struct manually, reserved for generated code
    /// </summary>
    public struct PooledList<T, TArray> : IDisposable
        where TArray : struct, IDynamicArray<T>
    {
        private TArray _array;
        private int _count;

        public PooledList(int capacity) : this()
        {
            _array = new TArray();
            if (capacity > 0)
                _array.SetCapacity(capacity);

            _count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseCapacity()
        {
            _array.IncreaseCapacity(_count + 1, _count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            var localArray = _array;
            int index = _count;

            // this should remove array bound check
            if ((uint)index < (uint)localArray.Length)
            {
                localArray[index] = item;
            }
            else
            {
                IncreaseCapacity();
                _array[index] = item;
            }

            _count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange<TEnumerator>(TEnumerator iter)
            where TEnumerator : IEnumerator<T>
        {
            var localArray = _array;
            int index = _count;

            while (iter.MoveNext())
            {
                // this should remove array bound check
                if ((uint)index < (uint)localArray.Length)
                {
                    localArray[index] = iter.Current;
                    index++;
                }
                else
                {
                    // resize and assign
                    _count = index;
                    IncreaseCapacity();
                    localArray = _array;
                    localArray[index] = iter.Current;
                }
            }

            _count = index;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        public TArray Array => _array;

        public T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _array[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _array[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            _array.Dispose();
            _count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArray()
        {
            int count = _count;
            var result = new T[count];

            _array.CopyTo(result, count);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<T> ToList()
        {
            int count = _count;
            var result = new List<T>(count);
            var listLayout = UnsafeUtils.As<List<T>, ListLayout<T>>(ref result);

            _array.CopyTo(listLayout.Items, _count);
            listLayout.Size = count;
            return result;
        }
    }
}
