using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Do not use this struct manually, reserved for generated code
    /// No need to provide Remove operation
    /// </summary>
    public struct PooledSet<T, TComparer> : IDisposable where TComparer : IEqualityComparer<T>
    {
        private struct Slot
        {
            internal int hashCode;
            internal int next; // Index of next entry, -1 if last
            internal T value;
        }

        private readonly TComparer _comparer;

        private int[] _buckets;
        private Slot[] _slots;
        private int _size;
        private int _count;

        public PooledSet(int capacity, TComparer comparer)
        {
            this._comparer = comparer;

            _size = HashHelpers.GetPrime(capacity);
            _buckets = SharedArrayPool<int>.Rent(_size);
            Array.Clear(_buckets, 0, _buckets.Length);

            _slots = SharedArrayPool<Slot>.Rent(_size);
            _count = 0;
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
            int[] newBuckets;
            Slot[] newSlots;
            bool replaceArrays;

            // Because ArrayPool might have given us larger arrays than we asked for, see if we can
            // use the existing capacity without actually resizing.
            if (_buckets?.Length >= newSize && _slots?.Length >= newSize)
            {
                Array.Clear(_buckets, 0, _buckets.Length);
                Array.Clear(_slots, _size, newSize - _size);
                newBuckets = _buckets;
                newSlots = _slots;
                replaceArrays = false;
            }
            else
            {
                newSlots = SharedArrayPool<Slot>.Rent(newSize);
                newBuckets = SharedArrayPool<int>.Rent(newSize);

                Array.Clear(newBuckets, 0, newBuckets.Length);

                if (_slots != null)
                {
                    Array.Copy(_slots, 0, newSlots, 0, _count);
                }
                replaceArrays = true;
            }

            for (int i = 0; i < _count; i++)
            {
                ref var newSlot = ref newSlots[i];
                uint bucket = Reduce(newSlot.hashCode, newSize);
                newSlot.next = newBuckets[bucket] - 1;
                newBuckets[bucket] = i + 1;
            }

            if (replaceArrays)
            {
                ReturnArrays();
                _slots = newSlots;
                _buckets = newBuckets;
            }

            _size = newSize;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReturnArrays()
        {
            if (_size > 0)
            {
                try
                {
                    SharedArrayPool<Slot>.Return(_slots, true);
                }
                catch (ArgumentException)
                {
                    // oh well, the array pool didn't like our array
                }

                try
                {
                    SharedArrayPool<int>.Return(_buckets, false);
                }
                catch (ArgumentException)
                {
                    // shucks
                }
            }

            // size 0 means that arrays are returned
            _size = 0;
        }

        public bool Add(T value)
        {
            int hashCode = GetHashCode(value);
            uint bucket = Reduce(hashCode, _size);
            int collisionCount = 0;
            Slot[] tmpSlots = _slots;
            for (int i = _buckets[bucket] - 1; i >= 0; )
            {
                ref var slot = ref tmpSlots[i];
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
                tmpSlots = _slots;
                bucket = Reduce(hashCode, _size);
            }

            int index = _count;

            ref var lastSlot = ref tmpSlots[index];
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

            Slot[] tmpSlots = _slots;

            for (int i = _buckets[bucket] - 1; i >= 0;)
            {
                ref var slot = ref tmpSlots[i];
                if (slot.hashCode == hashCode && _comparer.Equals(slot.value, value))
                    return true;

                i = slot.next;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            ReturnArrays();
            _count = 0;
        }
    }
}
