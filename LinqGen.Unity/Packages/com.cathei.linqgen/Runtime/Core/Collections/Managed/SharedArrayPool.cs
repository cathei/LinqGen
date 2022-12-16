using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    internal static class SharedArrayPool<T>
    {
        private static readonly ArrayPool<T> Pool;

        static SharedArrayPool()
        {
            Pool = ArrayPool<T>.Create();
        }

        public static T[] Rent(int size) => Pool.Rent(size);

        public static void Return(T[] array, bool clearArray) => Pool.Return(array, clearArray);
    }
}
