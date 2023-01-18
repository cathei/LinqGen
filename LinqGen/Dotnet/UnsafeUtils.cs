// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Unsafe for .NET environment (in contrast of Unity)
    /// </summary>
    public static unsafe class UnsafeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo As<TFrom, TTo>(ref TFrom source)
        {
            return ref Unsafe.As<TFrom, TTo>(ref source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* ArrayAlloc<T>(int size)
            where T : unmanaged
        {
            return (T*)Marshal.AllocHGlobal(size * sizeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArrayFree(void* array)
        {
            Marshal.FreeHGlobal((IntPtr)array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArrayClear<T>(T* array, int size)
            where T : unmanaged
        {
            Unsafe.InitBlock(array, 0, (uint)(size * sizeof(T)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArrayCopy<T>(T* src, T* dst, int size)
            where T : unmanaged
        {
            Unsafe.CopyBlock(dst, src, (uint)(size * sizeof(T)));
        }
    }
}
