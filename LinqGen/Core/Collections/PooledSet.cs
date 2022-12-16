﻿using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    public struct PooledSetSlot<T>
    {
        internal int hashCode;
        internal int next; // Index of next entry, -1 if last
        internal T value;
    }

    /// <summary>
    /// Do not use this struct manually, reserved for generated code
    /// No need to provide Remove operation
    /// </summary>
    public struct PooledSet<T, TArray, TComparer> : IDisposable
        where TArray : struct, IDynamicArray<PooledSetSlot<T>>
        where TComparer : IEqualityComparer<T>
    {
        private readonly TComparer _comparer;

        private DynamicArrayManaged<int> _buckets;
        private TArray _slots;

        private int _size;
        private int _count;

        public PooledSet(int capacity, TComparer comparer) : this()
        {
            _comparer = comparer;

            _size = HashHelpers.GetPrime(capacity);
            _count = 0;

            _buckets = new DynamicArrayManaged<int>();
            _buckets.SetCapacity(_size, true);

            _slots = new TArray();
            _slots.SetCapacity(_size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetHashCode(T item)
        {
            return item == null ? 0 : _comparer.GetHashCode(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Reduce(int hashCode, int size)
        {
            return (uint)hashCode % (uint)size;
        }

        private void IncreaseCapacity()
        {
            int newSize = HashHelpers.ExpandPrime(_count);
            if (newSize <= _count)
            {
                throw new InvalidOperationException("Capacity overflow");
            }

            // Able to increase capacity; copy elements to larger array and rehash
            SetCapacity(newSize);
        }

        private void SetCapacity(int newSize)
        {
            DynamicArrayManaged<int> newBuckets;
            var localSlots = _slots;
            bool replaceBucket;

            // Because ArrayPool might have given us larger arrays than we asked for, see if we can
            // use the existing capacity without actually resizing.
            if (_buckets.Length >= newSize && _slots.Length >= newSize)
            {
                _buckets.Clear(0, _buckets.Length);
                newBuckets = _buckets;
                replaceBucket = false;

                localSlots.Clear(_size, newSize - _size);
            }
            else
            {
                newBuckets = new DynamicArrayManaged<int>();
                newBuckets.SetCapacity(newSize, true);
                replaceBucket = true;

                localSlots.IncreaseCapacity(newSize, _count);
            }

            for (int i = 0; i < _count; i++)
            {
                ref var slot = ref localSlots[i];
                uint bucket = Reduce(slot.hashCode, newSize);
                slot.next = newBuckets[bucket] - 1;
                newBuckets[bucket] = i + 1;
            }

            if (replaceBucket)
            {
                _buckets.Dispose();
                _buckets = newBuckets;
            }

            _size = newSize;
        }

        public bool Add(T value)
        {
            int hashCode = GetHashCode(value);
            uint bucket = Reduce(hashCode, _size);
            int collisionCount = 0;
            var localSlots = _slots;

            for (int i = _buckets[bucket] - 1; i >= 0; )
            {
                ref var slot = ref localSlots[i];
                if (slot.hashCode == hashCode && _comparer.Equals(slot.value, value))
                    return false;

                if (collisionCount >= _size)
                {
                    // The chain of entries forms a loop, which means a concurrent update has happened.
                    throw new InvalidOperationException("Concurrent operations are not supported.");
                }
                collisionCount++;
                i = slot.next;
            }

            if (_count == _size)
            {
                IncreaseCapacity();
                // this will change during resize
                localSlots = _slots;
                bucket = Reduce(hashCode, _size);
            }

            int index = _count;

            ref var lastSlot = ref localSlots[index];
            lastSlot.hashCode = hashCode;
            lastSlot.value = value;
            lastSlot.next = _buckets[bucket] - 1;

            _buckets[bucket] = index + 1;
            _count++;
            return true;
        }

        public bool Contains(T value)
        {
            int hashCode = GetHashCode(value);
            uint bucket = Reduce(hashCode, _size);

            var localSlots = _slots;

            for (int i = _buckets[bucket] - 1; i >= 0;)
            {
                ref var slot = ref localSlots[i];
                if (slot.hashCode == hashCode && _comparer.Equals(slot.value, value))
                    return true;

                i = slot.next;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            _slots.Dispose();
            _buckets.Dispose();

            _size = 0;
            _count = 0;
        }
    }
}
