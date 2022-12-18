using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Do not use this struct manually, reserved for generated code
    /// </summary>
    public struct PooledListManaged<T> : IDisposable
    {
        private T[] _array;
        private int _count;

        // Quick access without resolving generic static classes
        private static readonly ArrayPool<T> Pool = SharedArrayPool<T>.Pool;
        private static readonly T[] EmptyArray = System.Array.Empty<T>();

        public PooledListManaged(int capacity) : this()
        {
            _array = capacity > 0 ? Pool.Rent(capacity) : EmptyArray;
            _count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseCapacity()
        {
            var newItems = Pool.Rent(_count + 1);
            if (_count > 0)
                System.Array.Copy(_array, newItems, _count);

            ReturnArray();
            _array = newItems;
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

        public T[] Array
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _array;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _array[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            System.Array.Clear(_array, 0, _array.Length);
            _count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReturnArray()
        {
            if (_array == EmptyArray)
                return;

            Pool.Return(_array, true);
            _array = EmptyArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            ReturnArray();
            _count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArray()
        {
            int count = _count;
            var result = new T[count];

            System.Array.Copy(_array, result, count);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<T> ToList()
        {
            int count = _count;
            var result = new List<T>(count);
            var listLayout = UnsafeUtils.As<List<T>, ListLayout<T>>(ref result);

            System.Array.Copy(_array, listLayout.Items, count);
            listLayout.Size = count;
            return result;
        }
    }
}
