// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Visitor that local evaluations will implement
    /// </summary>
    public interface IVisitor<in T> : IDisposable
    {
        bool Visit(T element);
    }
}
