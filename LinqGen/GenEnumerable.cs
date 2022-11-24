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

    public static class GenEnumerable
    {
        public static Stub<IContent<int>, Range> Range(int start, int count)
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, Empty> Empty<T>()
        {
            throw new NotImplementedException();
        }

        public static Stub<IContent<T>, Repeat<T>> Repeat<T>(T element, int count)
        {
            throw new NotImplementedException();
        }
    }
}
