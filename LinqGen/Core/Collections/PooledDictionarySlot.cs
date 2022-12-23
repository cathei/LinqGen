// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Hidden
{
    public struct PooledDictionarySlot<TKey, TValue>
    {
        internal int HashCode;
        internal int Next; // index of next entry, -1 if last
        public TKey Key;
        public TValue Value;
    }
}