// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Stub interface for seamless code generation.
    /// The extensions are not actually implemented, only used for source generation.
    /// </summary>
    public interface IStub<out T, TSignature> where T : IEnumerable
    {
    }

    public interface IInternalStub
    {
    }

    public interface IInternalStub<out T> : IInternalStub, IStub<IEnumerable<T>, Compiled>
    {
    }

    /// <summary>
    /// Returned from OrderBy, to chain ThenBy
    /// </summary>
    public interface IOrderedStub<out T, TSignature> : IStub<T, TSignature> where T : IEnumerable
    {
    }

    public interface IInternalOrderedStub<out T> : IInternalStub, IOrderedStub<IEnumerable<T>, Compiled>
    {
    }

    /// <summary>
    /// This is empty stub that will be replaced with source generator.
    /// For value types, the parameter interface will be replaced with actual type to avoid boxing.
    /// Use AsEnumerable to safely box generated type and store as IEnumerable.
    /// </summary>
    public abstract class Stub<T, TSignature> : IStub<T, TSignature>
        where T : IEnumerable
        where TSignature : IStubSignature
    {
        // this has to be instance method to get TSignature
        public Stub<IEnumerable<TOut>, Cast<TSignature>> Cast<TOut>()
        {
            throw new NotImplementedException();
        }

        // this has to be instance method to get TSignature
        public Stub<IEnumerable<TOut>, OfType<TSignature>> OfType<TOut>()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Stub for OrderBy
    /// </summary>
    public abstract class OrderedStub<T, TSignature> : IOrderedStub<T, TSignature>
        where T : IEnumerable
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