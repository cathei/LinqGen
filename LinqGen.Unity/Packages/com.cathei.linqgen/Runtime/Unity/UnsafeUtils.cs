// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Unsafe for Unity environment
    /// </summary>
    public static unsafe class UnsafeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo As<TFrom, TTo>(ref TFrom source)
        {
            return ref UnsafeUtility.As<TFrom, TTo>(ref source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* ArrayAlloc<T>(int size)
            where T : unmanaged
        {
            return (T*)UnsafeUtility.Malloc(size * sizeof(T), UnsafeUtility.AlignOf<T>(), Allocator.Temp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArrayFree(void* array)
        {
            UnsafeUtility.Free(array, Allocator.Temp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArrayClear<T>(T* array, int size)
            where T : unmanaged
        {
            UnsafeUtility.MemClear(array, (uint)(size * sizeof(T)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArrayCopy<T>(T* src, T* dst, int size)
            where T : unmanaged
        {
            UnsafeUtility.MemCpy(dst, src, size * sizeof(T));
        }
    }
}
