// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen.Hidden
{
    public interface IStub
    {
    }

    /// <summary>
    /// Stub interface for seamless code generation.
    /// The extensions are not actually implemented, only used for source generation.
    /// </summary>
    public interface IStub<in T, TSignature> : IStub
    {
    }

    public interface IContentSource<in T>
    {
    }

    public interface IContent<in T> : IContentSource<IEnumerable<T>>
    {
    }

    /// <summary>
    /// This is empty stub that will be replaced with source generator.
    /// For value types, the parameter interface will be replaced with actual type to avoid boxing.
    /// Use AsEnumerable to safely box generated type and store as IEnumerable.
    /// </summary>
    public abstract class Stub<T, TSignature> : IStub<T, TSignature>
        // where T : IEnumerable
        where TSignature : IStubSignature
    {
        // because of generic argument this can't be extension method
        public Stub<IEnumerable<TOut>, Cast<TSignature, TOut>> Cast<TOut>() => throw new NotImplementedException();

        // because of generic argument this can't be extension method
        public Stub<IEnumerable<TOut>, OfType<TSignature, TOut>> OfType<TOut>() => throw new NotImplementedException();

        public static implicit operator (IEnumerable<T>, TSignature)(Stub<T, TSignature> tmp)
        {
            throw new NotImplementedException();
        }
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