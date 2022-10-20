// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using Cathei.LinqGen.Operations;

namespace Cathei.LinqGen
{
    /// <summary>
    /// Stub interface for seamless code generation.
    /// The extensions are not actually implemented, only used for source generation.
    /// </summary>
    public interface ILinqGenEnumerable<T, TOp> { }

    /// <summary>
    /// This is empty stub that will be replaced with source generator.
    /// For value types, the parameter interface will be replaced with actual type to avoid boxing.
    /// Use AsEnumerable to safely box generated type and store as IEnumerable.
    /// </summary>
    public abstract class StubEnumerable<T, TOp> : ILinqGenEnumerable<T, TOp>
        where TOp : ILinqGenOperation
    { }

    public static partial class StubExtensions
    {
        public static StubEnumerable<T, Where<TOp>> Where<T, TOp>(
            this ILinqGenEnumerable<T, TOp> enumerable, Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public static StubEnumerable<TOut, Select<TOp, TOut>> Select<T, TOp, TOut>(
            this ILinqGenEnumerable<T, TOp> enumerable, Func<T, TOut> selector)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<T> AsEnumerable<T, TOp>(
            this ILinqGenEnumerable<T, TOp> enumerable)
        {
            throw new NotImplementedException();
        }
    }
}