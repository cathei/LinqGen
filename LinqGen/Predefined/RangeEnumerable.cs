// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen.Hidden
{
    public readonly struct RangeEnumerable :
        IStub<IContent<int>, RangeEnumerable>,
        IStructCollection<int, RangeEnumerable.Enumerator>
    {
        private readonly int start;
        private readonly int count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RangeEnumerable(int start, int count)
        {
            this.start = start;
            this.count = count;
        }

        public int Count => count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator() => new Enumerator(start, count);

        public Enumerator GetSliceEnumerator(int skip, int take)
            => new Enumerator(start + skip, Math.Min(count - skip, take));

        public struct Enumerator : IStructCollectionEnumerator<int>
        {
            private readonly int start;
            private readonly int count;
            private int index;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator(int start, int count)
            {
                this.start = start;
                this.count = count;
                index = -1;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                return ++index < count;
            }

            public int Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => start + index;
            }

            object IEnumerator.Current => Current;

            public void Reset() => throw new NotSupportedException();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Dispose() { }

            public int Count => count;

            public int Get(int index) => start + index;
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