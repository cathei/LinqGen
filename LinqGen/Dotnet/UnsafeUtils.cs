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

        // public static IntPtr Allocate<T>()
        // {
        //     // return Marshal.AllocHGlobal()
        //
        // }
    }
}
