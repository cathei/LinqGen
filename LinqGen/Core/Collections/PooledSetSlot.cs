// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Hidden
{
    public struct PooledSetSlot<T>
    {
        internal int HashCode;
        internal int Next; // Index of next entry, -1 if last
        internal T Value;
    }
}