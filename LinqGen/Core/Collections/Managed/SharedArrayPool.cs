using System.Buffers;

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
