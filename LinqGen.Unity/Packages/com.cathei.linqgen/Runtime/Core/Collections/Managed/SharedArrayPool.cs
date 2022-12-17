using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    internal static class SharedArrayPool<T>
    {
        public static readonly ArrayPool<T> Pool;

        static SharedArrayPool()
        {
            Pool = ArrayPool<T>.Create();
        }
    }
}
