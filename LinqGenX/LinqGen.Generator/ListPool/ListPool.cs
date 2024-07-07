using System;

namespace Cathei.LinqGen.Generator;

internal static class ListPool<T>
{
    public struct Disposer : IDisposable
    {
        public void Dispose()
        {
            // TODO
        }
    }

    public static Disposer Rent(out List<T> list)
    {
        list = new List<T>();
        return new Disposer();
    }
}

internal static class ListPool
{
    public static ListPool<T>.Disposer Rent<T>(out List<T> list)
    {
        return ListPool<T>.Rent(out list);
    }
}
