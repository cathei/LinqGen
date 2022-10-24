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
    /// The enumerable that getting compiled with. Stub exists for auto completion.
    /// </summary>
    public interface IEmbeddedStub : IStubSignature
    {
    }

    /// <summary>
    /// The enumerable that getting compiled with. Stub exists for auto completion.
    /// </summary>
    public interface IEmbeddedStub<T, TSignature> : IStub<T, TSignature>, IEmbeddedStub
        where TSignature : IEmbeddedStub<T, TSignature>
    {
    }

    /// <summary>
    /// Stub that can be showed from other assembly.
    /// Cast and OfType will not work otherwise defined within.
    /// </summary>
    public interface IExportedStub<T, TSignature> : IStub<T, TSignature>, IStubSignature
        where TSignature : IExportedStub<T, TSignature>
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
        // because of generic argument this can't be extension method
        public Stub<TOut, Cast<TSignature, TOut>> Cast<TOut>() => throw new NotImplementedException();

        // because of generic argument this can't be extension method
        public Stub<TOut, OfType<TSignature, TOut>> OfType<TOut>() => throw new NotImplementedException();
    }

    /// <summary>
    /// Stub for AsEnumerable.
    /// Note that this will not implement IStub, as it is the end of the call chain.
    /// </summary>
    public abstract class BoxedStub<T, TSignature> : IEnumerable<T>
        where TSignature : IStubSignature
    {
        public IEnumerator<T> GetEnumerator() => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}