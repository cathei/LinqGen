using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    public struct GroupingManaged<TKey, TValue>
        : IGrouping<TKey, TValue>, IStructEnumerable<TValue, GroupingManaged<TKey, TValue>.Enumerator>
    {
        private TKey _key;
        private TValue[] _values;
        private int _count;

        public GroupingManaged(TKey key, PooledListManaged<TValue> values)
        {
            _key = key;
            _values = values.Array;
            _count = values.Count;
        }

        public TKey Key
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _key;
        }

        public Enumerator GetEnumerator() => new Enumerator(_values, _count);

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<TValue>
        {
            private TValue[] _values;
            private int _count;
            private int _index;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator(TValue[] values, int count)
            {
                _values = values;
                _count = count;
                _index = -1;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                return (uint)++_index < (uint)_count;
            }

            public TValue Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _values[_index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Dispose()
            {
            }

            public void Reset() => throw new NotSupportedException();

            object IEnumerator.Current => Current!;
        }
    }
}