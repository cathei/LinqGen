using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Do not use this struct manually, reserved for generated code
    /// </summary>
    public struct PooledQueueNative<T> : IDisposable
        where T : unmanaged
    {
        private DynamicArrayNative<T> _array;
        private int _cursor;
        private int _count;

        public PooledQueueNative(int capacity) : this()
        {
            _array = new DynamicArrayNative<T>(capacity);
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
        public void Dispose()
        {
            _array.Dispose();
            _cursor = _count = 0;
        }
    }
}
