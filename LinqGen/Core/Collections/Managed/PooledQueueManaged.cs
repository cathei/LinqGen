using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Do not use this struct manually, reserved for generated code
    /// </summary>
    public struct PooledQueueManaged<T> : IDisposable
    {
        private T[] _array;
        private int _capacity;
        private int _count;
        private int _front;
        private int _rear;

        // Quick access without resolving generic static classes
        private static readonly ArrayPool<T> Pool = SharedArrayPool<T>.Pool;
        private static readonly T[] EmptyArray = Array.Empty<T>();

        public PooledQueueManaged(int capacity) : this()
        {
            _array = Pool.Rent(capacity);
            _capacity = capacity;
            _count = _front = _rear = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Enqueue(T item)
        {
            int capacity = _capacity;
            int rear = _rear;

            _array[rear] = item;
            _rear = ++rear == capacity ? 0 : rear;

            if (_count == capacity)
            {
                // push front
                int front = _front;
                _front = ++front == capacity ? 0 : front;
                return true;
            }
            else
            {
                // return value indicates if queue is full
                return ++_count == capacity;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Dequeue()
        {
            int front = _front;

            T value = _array[front];
            _front = ++front == _capacity ? 0 : front;
            --_count;

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Forward(int step)
        {
            // drops n elements
            int capacity = _capacity;
            int front = _front;

            step = Math.Min(step, _count);
            front += step;

            if (front >= capacity)
                front -= capacity;

            _front = front;
            _count -= step;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReturnArray()
        {
            if (_array == EmptyArray)
                return;

            Pool.Return(_array, true);
            _array = EmptyArray;
            _capacity = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            ReturnArray();
            _count = _front = _rear = 0;
        }
    }
}
