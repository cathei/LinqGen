// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen
{
    using Range = Hidden.Range;

    public static class Gen
    {
        public static readonly GenerationStub Enumerable = default;
    }

    public static partial class StubExtensions
    {
        /// <summary>
        /// Only reference type can use covariance support
        /// </summary>
        public static Stub<T, Gen<T>> Gen<T>(this T enumerable)
            where T : class, IEnumerable
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Struct version of specialize
        /// </summary>
        public static Stub<IList<T>, GenList<T>> Gen<T>(this IList<T> enumerable)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Struct version of specialize
        /// </summary>
        public static Stub<IEnumerable<T>, GenStruct<T, TEnumerator>> Gen<T, TEnumerator>(
            this IStructEnumerable<T, TEnumerator> enumerable)
            where TEnumerator : IEnumerator<T>
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<TValue>, Gen<GroupingManaged<TKey, TValue>>> Gen<TKey, TValue>(
            this GroupingManaged<TKey, TValue> enumerable)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<TValue>, Gen<GroupingNative<TKey, TValue>>> Gen<TKey, TValue>(
            this GroupingNative<TKey, TValue> enumerable)
            where TKey : unmanaged
            where TValue : unmanaged
        {
            throw new NotImplementedException();
        }

        // TODO span support
        public static Stub<IEnumerable<T>, GenSpan<T>> Gen<T>(this Span<T> enumerable)
        {
            throw new NotImplementedException();
        }

        // TODO span support
        public static Stub<IEnumerable<T>, GenSpan<T>> Gen<T>(this ReadOnlySpan<T> enumerable)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<int>, Range> Range(this IGenerationStub stub, int start, int count)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Empty> Empty<T>(this IGenerationStub stub)
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Repeat> Repeat<T>(this IGenerationStub stub, T element, int count)
        {
            throw new NotImplementedException();
        }
    }
}
