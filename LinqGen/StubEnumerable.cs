// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using Cathei.LinqGen.Operations;

namespace Cathei.LinqGen
{
    /// <summary>
    /// This is empty stub that will be replaced with source generator.
    /// For value types, the parameter interface will be replaced with actual type to avoid boxing.
    /// Also defined as ref struct to prevent using this type as member.
    /// Use AsEnumerable to safely box and store as IEnumerable.
    /// </summary>
    public abstract class StubEnumerable<T, TOp>
    {
        public StubEnumerable<T, Where<TOp>> Where(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public StubEnumerable<TOut, Select<TOp, TOut>> Select<TOut>(Func<T, TOut> selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> AsEnumerable()
        {
            throw new NotImplementedException();
        }
    }
}