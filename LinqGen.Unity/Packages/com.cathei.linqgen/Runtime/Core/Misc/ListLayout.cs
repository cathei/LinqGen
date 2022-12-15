#nullable disable

using System;

namespace Cathei.LinqGen.Hidden
{
    public class ListLayout<T>
    {
        public T[] Items;
#if !NETCOREAPP3_0_OR_GREATER
        public Object SyncRoot;
#endif
        public int Size;
    }
}