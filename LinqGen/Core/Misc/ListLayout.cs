#nullable disable

using System;

namespace Cathei.LinqGen.Hidden
{
    // Unity List<T> layout: https://github.com/Unity-Technologies/UnityCsReference/blob/3d74e124da422b5ab79e573ca4d5b4829ba09c34/Runtime/Export/Scripting/NoAllocHelpers.bindings.cs#L78
    public class ListLayout<T>
    {
        public T[] Items;
#if !UNITY_5_3_OR_NEWER && !NETCOREAPP3_0_OR_GREATER
        public Object SyncRoot;
#endif
        public int Size;
    }
}