using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Do not use this struct manually, reserved for generated code
    /// </summary>
    public struct PooledSet<T, TComparer> : IDisposable where TComparer : IEqualityComparer<T>
    {
        private const int Lower31BitMask = 0x7FFFFFFF;

        private readonly TComparer comparer;

        private int[] buckets;
        private Slot<T>[] slots;
        private int size;

        private int count;
        private int lastIndex;

        public PooledSet(int capacity, TComparer comparer)
        {
            this.comparer = comparer;

            size = HashHelpers.GetPrime(capacity);
            buckets = SharedArrayPool<int>.Rent(size);
            Array.Clear(buckets, 0, buckets.Length);

            slots = SharedArrayPool<Slot<T>>.Rent(size);
            count = 0;
            lastIndex = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int InternalGetHashCode(T item)
        {
            if (item == null)
            {
                return 0;
            }
            return comparer.GetHashCode(item) & Lower31BitMask;
        }

        private void IncreaseCapacity()
        {
            int newSize = HashHelpers.ExpandPrime(count);
            if (newSize <= count)
            {
                throw new InvalidOperationException("Capacity overflow");
            }

            // Able to increase capacity; copy elements to larger array and rehash
            SetCapacity(newSize);
        }

        private void SetCapacity(int newSize)
        {

            int[] newBuckets;
            Slot<T>[] newSlots;
            bool replaceArrays;

            // Because ArrayPool might have given us larger arrays than we asked for, see if we can
            // use the existing capacity without actually resizing.
            if (buckets?.Length >= newSize && slots?.Length >= newSize)
            {
                Array.Clear(buckets, 0, buckets.Length);
                Array.Clear(slots, size, newSize - size);
                newBuckets = buckets;
                newSlots = slots;
                replaceArrays = false;
            }
            else
            {
                newSlots = SharedArrayPool<Slot<T>>.Rent(newSize);
                newBuckets = SharedArrayPool<int>.Rent(newSize);

                Array.Clear(newBuckets, 0, newBuckets.Length);

                if (slots != null)
                {
                    Array.Copy(slots, 0, newSlots, 0, lastIndex);
                }
                replaceArrays = true;
            }

            for (int i = 0; i < lastIndex; i++)
            {
                ref var newSlot = ref newSlots[i];
                int bucket = newSlot.hashCode % newSize;
                newSlot.next = newBuckets[bucket] - 1;
                newBuckets[bucket] = i + 1;
            }

            if (replaceArrays)
            {
                ReturnArrays();
                slots = newSlots;
                buckets = newBuckets;
            }

            size = newSize;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReturnArrays()
        {
            if (size > 0)
            {
                try
                {
                    SharedArrayPool<Slot<T>>.Return(slots, true);
                }
                catch (ArgumentException)
                {
                    // oh well, the array pool didn't like our array
                }

                try
                {
                    SharedArrayPool<int>.Return(buckets, false);
                }
                catch (ArgumentException)
                {
                    // shucks
                }
            }

            // size 0 means that arrays are returned
            size = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Add(T value)
        {
            int hashCode = InternalGetHashCode(value);
            int bucket = hashCode % size;
            int collisionCount = 0;
            Slot<T>[] tmpSlots = slots;
            for (int i = buckets[bucket] - 1; i >= 0; )
            {
                ref var slot = ref tmpSlots[i];
                if (slot.hashCode == hashCode && comparer.Equals(slot.value, value))
                    return false;

                if (collisionCount >= size)
                {
                    // The chain of entries forms a loop, which means a concurrent update has happened.
                    throw new InvalidOperationException("Concurrent operations are not supported.");
                }
                collisionCount++;
                i = slot.next;
            }

            int index;
            if (lastIndex == size)
            {
                IncreaseCapacity();
                // this will change during resize
                tmpSlots = slots;
                bucket = hashCode % size;
            }

            index = lastIndex;
            lastIndex++;

            ref var slot1 = ref tmpSlots[index];
            slot1.hashCode = hashCode;
            slot1.value = value;
            slot1.next = buckets[bucket] - 1;
            buckets[bucket] = index + 1;
            count++;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(T item)
        {
            int hashCode = InternalGetHashCode(item);
            int bucket = hashCode % size;
            int last = -1;
            int collisionCount = 0;
            Slot<T>[] tmpSlots = slots;
            for (int i = buckets[bucket] - 1; i >= 0; last = i, i = tmpSlots[i].next)
            {
                ref var tmpSlot = ref tmpSlots[i];
                if (tmpSlot.hashCode == hashCode && comparer.Equals(tmpSlot.value, item))
                {
                    if (last < 0)
                    {
                        buckets[bucket] = tmpSlot.next + 1;
                    }
                    else
                    {
                        ref var lastSlot = ref tmpSlots[last];
                        lastSlot.next = tmpSlot.next;
                    }

                    tmpSlot.hashCode = -1;

                    count--;
                    if (count == 0)
                    {
                        lastIndex = 0;
                    }

                    return true;
                }

                if (collisionCount >= size)
                {
                    throw new InvalidOperationException("Concurrent operations are not supported.");
                }
                collisionCount++;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            if (lastIndex > 0)
            {
                // clear the elements so that the gc can reclaim the references.
                // clear only up to _lastIndex for _slots
                Array.Clear(slots, 0, lastIndex);
                Array.Clear(buckets, 0, buckets.Length);
                lastIndex = 0;
                count = 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            ReturnArrays();
            lastIndex = 0;
            count = 0;
        }
    }
}
