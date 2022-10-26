// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.LinqGen.Hidden;


namespace Cathei.LinqGen.Hidden
{
    public readonly struct RangeEnumerable : IStub<IContent<int>, RangeEnumerable>
    {
        private readonly int start;
        private readonly int count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RangeEnumerable(int start, int count)
        {
            this.start = start;
            this.count = count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator() => new Enumerator(this);

        public struct Enumerator : IEnumerator<int>
        {
            private int current;
            private int end;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator(in RangeEnumerable parent)
            {
                current = parent.start - 1;
                end = parent.start + parent.count;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                return ++current < end;
            }

            public int Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => current;
            }

            object IEnumerator.Current => Current;

            public void Reset() => throw new NotImplementedException();

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
        public static RangeEnumerable Range(int start, int count)
        {
            return new RangeEnumerable(start, count);
        }
    }
}