﻿using System;
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
        private int _count;
        private int _front;
        private int _rear;

        // Quick access without resolving generic static classes
        private static readonly ArrayPool<T> Pool = SharedArrayPool<T>.Pool;
        private static readonly T[] EmptyArray = Array.Empty<T>();

        public PooledQueueManaged(int capacity) : this()
        {
            _array = Pool.Rent(capacity);
            _count = _front = _rear = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enqueue(T item)
        {
            var localArray = _array;
            int rear = _rear;
            int count = _count;

            localArray[rear] = item;
            _rear = ++rear == localArray.Length ? 0 : rear;

            if (count == localArray.Length)
            {
                // push front
                int front = _front;
                _front = ++front == localArray.Length ? 0 : front;
            }
            else
            {
                ++_count;
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
            _count = _front = _rear = 0;
        }
    }
}
