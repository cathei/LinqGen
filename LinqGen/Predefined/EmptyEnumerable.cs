// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen.Hidden
{
    public readonly struct EmptyEnumerable<T> :
        IStub<IContent<T>, EmptyEnumerable<T>>,
        IStructCollection<T, EmptyEnumerable<T>.Enumerator>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator() => new Enumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetSliceEnumerator(int skip, int take)
            => new Enumerator();

        public int Count => 0;

        public struct Enumerator : IEnumerator<T>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                return false;
            }

            public T Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => throw new NotSupportedException();
            }

            object IEnumerator.Current => Current!;

            public void Reset() => throw new NotSupportedException();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Dispose() { }
        }
    }
}

namespace Cathei.LinqGen
{
    public static partial class GenEnumerable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EmptyEnumerable<T> Empty<T>()
        {
            return new EmptyEnumerable<T>();
        }
    }
}