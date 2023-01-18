using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Do not use this struct manually, reserved for generated code
    /// </summary>
    public struct PooledListNative<T> : IDisposable
        where T : unmanaged
    {
        private const int DefaultCapacity = 4;

        private DynamicArrayNative<T> _array;
        private int _count;

        public PooledListNative(int capacity) : this()
        {
            _array = new DynamicArrayNative<T>(capacity);
            _count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseCapacity()
        {
            int capacity = _array.Length == 0 ? DefaultCapacity : _array.Length * 2;
            _array.IncreaseCapacity(capacity, _count);
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

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        public DynamicArrayNative<T> Array => _array;

        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _array[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            _array.Clear();
            _count = 0;
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
