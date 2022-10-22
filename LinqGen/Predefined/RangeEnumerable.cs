// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cathei.LinqGen.Hidden;


namespace Cathei.LinqGen.Hidden
{
    public readonly struct RangeEnumerable : IStub<int, Compiled>
    {
        private readonly int start;
        private readonly int count;

        public RangeEnumerable(int start, int count)
        {
            this.start = start;
            this.count = count;
        }

        public Enumerator GetEnumerator() => new Enumerator(this);

        public struct Enumerator : IEnumerator<int>
        {
            private int start;
            private int count;
            private int index;

            public Enumerator(in RangeEnumerable parent)
            {
                start = parent.start;
                count = parent.count;
                index = -1;
            }

            public bool MoveNext()
            {
                return ++index < count;
            }

            public int Current => start + index;

            object IEnumerator.Current => Current;

            public void Reset() => throw new NotImplementedException();

            public void Dispose() { }
        }
    }
}

namespace Cathei.LinqGen
{
    public static partial class GenEnumerable
    {
        public static RangeEnumerable Range(int start, int count)
        {
            return new RangeEnumerable(start, count);
        }
    }
}