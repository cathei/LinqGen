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
        private int _count;
        private int _front;
        private int _rear;

        public PooledQueueNative(int capacity) : this()
        {
            _array = new DynamicArrayNative<T>(capacity);
            _count = _front = _rear = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Enqueue(T item)
        {
            var localArray = _array;
            int rear = _rear;
            int count = _count;

            localArray[rear] = item;
            _rear = ++rear == localArray.Length ? 0 : rear;

            if (count == localArray.Length)
            {
                // push front, keeping count same
                int front = _front;
                _front = ++front == localArray.Length ? 0 : front;
                return true;
            }
            else
            {
                // return value indicates if queue is full
                return ++_count == localArray.Length;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Dequeue()
        {
            var localArray = _array;
            int front = _front;

            T value = localArray[front];
            _front = ++front == localArray.Length ? 0 : front;
            --_count;

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Forward(int step)
        {
            // drops n elements
            var localArray = _array;
            int front = _front;

            step = Math.Min(step, _count);
            front += step;

            if (front >= localArray.Length)
                front -= localArray.Length;

            _front = front;
            _count -= step;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            _array.Dispose();
            _count = _front = _rear = 0;
        }
    }
}
