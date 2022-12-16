using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    public struct DynamicArrayNative<T> : IDynamicArray<T>
        where T : unmanaged
    {
        private IntPtr _array;
        private int _capacity;

        // static readonly int SizePerItem = UnsafeUtils.SizeOf<T>();

        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref UnsafeUtils.ArrayElement<T>(_array, index);
        }

        public ref T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref UnsafeUtils.ArrayElement<T>(_array, (int)index);
        }

        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _capacity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCapacity(int newSize, bool clear = false)
        {
            Debug.Assert(_array == IntPtr.Zero, "SetCapacity should not be called while allocated");

            _array = UnsafeUtils.ArrayAlloc<T>(newSize);
            _capacity = newSize;

            if (clear)
                Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IncreaseCapacity(int newSize, int copyCount = 0)
        {
            var newItems = UnsafeUtils.ArrayAlloc<T>(newSize);
            if (copyCount > 0)
                UnsafeUtils.ArrayCopy<T>(_array, newItems, copyCount);

            ReturnArray();
            _array = newItems;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            UnsafeUtils.ArrayClear<T>(_array, _capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReturnArray()
        {
            if (_array == IntPtr.Zero)
                return;

            UnsafeUtils.ArrayFree(_array);
            _array = IntPtr.Zero;
            _capacity = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void CopyTo(T[] array, int count)
        {
            if (_array == IntPtr.Zero)
                return;

            fixed (void* arrayPtr = array)
                UnsafeUtils.ArrayCopy<T>(_array, (IntPtr)arrayPtr, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            ReturnArray();
        }
    }
}
