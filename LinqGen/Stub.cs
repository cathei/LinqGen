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
    public interface IStub<in T, TSignature>
    {
    }

    public interface IInternalStub
    {
    }

    public interface IInternalStub<out T> : IInternalStub, IStub<IContent<T>, Compiled>
    {
    }

    public interface IContentSource<in T>
    {
    }

    public interface IContent<in T> : IContentSource<IEnumerable<T>>
    {
    }

    /// <summary>
    /// Returned from OrderBy, to chain ThenBy
    /// </summary>
    public interface IOrderedStub<in T, TSignature> : IStub<T, TSignature>
    {
    }

    public interface IInternalOrderedStub<out T> : IInternalStub, IOrderedStub<IContent<T>, Compiled>
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
        // this has to be instance method to get TSignature
        public Stub<IContent<TOut>, Cast<TSignature>> Cast<TOut>()
        {
            throw new NotImplementedException();
        }

        // this has to be instance method to get TSignature
        public Stub<IContent<TOut>, OfType<TSignature>> OfType<TOut>()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Stub for OrderBy
    /// </summary>
    public abstract class OrderedStub<T, TSignature> : IOrderedStub<T, TSignature>
    {
    }

    /// <summary>
    /// Stub for predefined generations
    /// </summary>
    public interface IGenerationStub
    {
    }

    /// <summary>
    /// Stub for predefined generations
    /// </summary>
    public struct GenerationStub : IGenerationStub
    {
    }

}