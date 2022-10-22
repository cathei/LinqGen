// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Stub interface for seamless code generation.
    /// The extensions are not actually implemented, only used for source generation.
    /// </summary>
    public interface IStub<T, TSignature>
    {
    }

    /// <summary>
    /// This is empty stub that will be replaced with source generator.
    /// For value types, the parameter interface will be replaced with actual type to avoid boxing.
    /// Use AsEnumerable to safely box generated type and store as IEnumerable.
    /// </summary>
    public abstract class Stub<T, TSignature> : IStub<T, TSignature>
        where TSignature : IStubSignature
    {
    }

    /// <summary>
    /// Stub for AsEnumerable
    /// </summary>
    public abstract class BoxedStub<T, TSignature> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator() => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}