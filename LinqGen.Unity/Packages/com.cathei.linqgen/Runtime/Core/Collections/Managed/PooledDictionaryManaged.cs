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
    public struct PooledDictionaryManaged<TKey, TValue, TComparer> : IDisposable
        where TComparer : IEqualityComparer<TKey>
    {
        private readonly TComparer _comparer;

        private DynamicArrayNative<int> _buckets;
        private PooledDictionarySlot<TKey, TValue>[] _slots;
        private int _size;

        private int _count;

        // Quick access without resolving generic static classes
        private static readonly ArrayPool<PooledDictionarySlot<TKey, TValue>> Pool
            = SharedArrayPool<PooledDictionarySlot<TKey, TValue>>.Pool;

        private static readonly PooledDictionarySlot<TKey, TValue>[] EmptyArray
            = Array.Empty<PooledDictionarySlot<TKey, TValue>>();

        public PooledDictionaryManaged(int capacity, TComparer comparer) : this()
        {
            _comparer = comparer;

            _size = HashHelpers.GetPrime(capacity);
            _count = 0;

            _buckets = new DynamicArrayNative<int>(_size);
            _slots = _size > 0 ? Pool.Rent(_size) : EmptyArray;

            _buckets.Clear();
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetHashCode(TKey item)
        {
            return item == null ? 0 : _comparer.GetHashCode(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Reduce(int hashCode, int size)
        {
            return (uint)hashCode % (uint)size;
        }

        public PooledDictionarySlot<TKey, TValue>[] Slots
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _slots;
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
            var localBuckets = _buckets;
            var localSlots = _slots;

            // Because ArrayPool might have given us larger arrays than we asked for, see if we can
            // use the existing capacity without actually resizing.
            if (localBuckets.Length < newSize)
            {
                localBuckets.IncreaseCapacity(newSize);
                localBuckets.Clear();
                _buckets = localBuckets;
            }

            if (localSlots.Length < newSize)
            {
                var newSlots = Pool.Rent(newSize);
                Array.Copy(localSlots, newSlots, _count);
                Pool.Return(localSlots, true);

                _slots = localSlots = newSlots;
            }

            for (int i = 0; i < _count; i++)
            {
                ref var slot = ref localSlots[i];
                uint bucket = Reduce(slot.HashCode, newSize);
                slot.Next = localBuckets[bucket] - 1;
                localBuckets[bucket] = i + 1;
            }

            _size = newSize;
        }

        public ref TValue GetOrCreate(TKey key)
        {
            int hashCode = GetHashCode(key);
            uint bucket = Reduce(hashCode, _size);
            int collisionCount = 0;
            var localSlots = _slots;

            for (int i = _buckets[bucket] - 1; i >= 0; )
            {
                ref var slot = ref localSlots[i];

                if (slot.HashCode == hashCode && _comparer.Equals(slot.Key, key))
                {
                    // found existing slot
                    return ref slot.Value;
                }

                if (collisionCount >= _size)
                {
                    // The chain of entries forms a loop, which means a concurrent update has happened.
                    throw new InvalidOperationException("Concurrent operations are not supported.");
                }
                collisionCount++;
                i = slot.Next;
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
            lastSlot.HashCode = hashCode;
            lastSlot.Key = key;
            lastSlot.Value = default!;
            lastSlot.Next = _buckets[bucket] - 1;

            _buckets[bucket] = index + 1;
            _count++;
            return ref lastSlot.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            _buckets.Dispose();

            Pool.Return(_slots, true);
            _slots = EmptyArray;

            _size = 0;
            _count = 0;
        }
    }
}