// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Unsafe for .NET environment (in contrast of Unity)
    /// </summary>
    public static class UnsafeUtils
    {
        public static ref TTo As<TFrom, TTo>(ref TFrom source)
        {
            return ref Unsafe.As<TFrom, TTo>(ref source);
        }

        public static unsafe ref T ArrayElement<T>(IntPtr array, int index)
            where T : unmanaged
        {
            return ref Unsafe.AsRef<T>((void*)(array + index * sizeof(T)));
        }

        public static unsafe IntPtr ArrayAlloc<T>(int size)
            where T : unmanaged
        {
            return Marshal.AllocHGlobal(size * sizeof(T));
        }

        public static void ArrayFree(IntPtr array)
        {
            Marshal.FreeHGlobal(array);
        }

        public static unsafe void ArrayClear<T>(IntPtr array, int size)
            where T : unmanaged
        {
            Unsafe.InitBlock((void*)array, 0, (uint)(size * sizeof(T)));
        }

        public static unsafe void ArrayCopy<T>(IntPtr src, IntPtr dst, int size)
            where T : unmanaged
        {
            Unsafe.CopyBlock((void*)src, (void*)dst, (uint)(size * sizeof(T)));
        }
    }
}
