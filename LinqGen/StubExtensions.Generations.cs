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
        public static Stub<IContent<int>, Range> Range(this IGenerationStub stub, int start, int count)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, Empty> Empty<T>(this IGenerationStub stub)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, Repeat> Repeat<T>(this IGenerationStub stub, T element, int count)
        {
            throw new NotImplementedException();
        }
    }
}
