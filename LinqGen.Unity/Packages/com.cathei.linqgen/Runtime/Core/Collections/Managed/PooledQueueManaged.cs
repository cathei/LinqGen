using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Do not use this struct manually, reserved for generated code
    /// </summary>
    public struct PooledQueueManaged<T> : IDisposable
        where T : unmanaged
    {
        private T[] _array;
        private int _cursor;
        private int _count;

        // Quick access without resolving generic static classes
        private static readonly ArrayPool<T> Pool = SharedArrayPool<T>.Pool;
        private static readonly T[] EmptyArray = Array.Empty<T>();

        public PooledQueueManaged(int capacity) : this()
        {
            _array = Pool.Rent(capacity);
            _cursor = 0;
            _count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enqueue(T item)
        {
            var localArray = _array;
            int cursor = _cursor;

            if ((uint)cursor < (uint)(localArray.Length - 1))
            {
                localArray[cursor] = item;
                _cursor += 1;
            }
            else
            {
                localArray[cursor] = item;
                _cursor = 0;
            }

            _count = Math.Min(_count + 1, localArray.Length);
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var localArray = _array;

                if ((uint)_count < (uint)localArray.Length)
                    return ref localArray[index];

                index += _cursor;

                if ((uint)index < (uint)localArray.Length)
                    return ref localArray[index];

                return ref localArray[index - localArray.Length];
            }
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
            _cursor = _count = 0;
        }
    }
}
