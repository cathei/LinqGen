// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Unsafe for Unity environment
    /// </summary>
    public static class UnsafeUtils
    {
        public static ref TTo As<TFrom, TTo>(ref TFrom source)
        {
            return ref UnsafeUtility.As<TFrom, TTo>(ref source);
        }
    }
}
