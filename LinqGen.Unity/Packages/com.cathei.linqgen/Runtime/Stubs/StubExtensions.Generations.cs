// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
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
        public static Stub<T, Specialize<T>> Specialize<T>(this T enumerable) where T : class, IEnumerable
        {
            throw new NotImplementedException();
        }

#if UNITY_COLLECTIONS
        public static Stub<IEnumerable<T>, Specialize<Unity.Collections.NativeArray<T>>> Specialize<T>(
            this Unity.Collections.NativeArray<T> enumerable) where T : struct
        {
            throw new NotImplementedException();
        }
#endif

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
