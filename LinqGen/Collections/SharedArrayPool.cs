using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    internal static class SharedArrayPool<T>
    {
        private static readonly ArrayPool<T> pool;

        static SharedArrayPool()
        {
            pool = ArrayPool<T>.Create();
        }

        public static T[] Rent(int size) => pool.Rent(size);

        public static void Return(T[] array, bool clearArray) => pool.Return(array, clearArray);
    }
}
