using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Cathei.LinqGen.Hidden
{
    public struct GroupingNative<TKey, TValue>
        : IGrouping<TKey, TValue>, IStructEnumerable<TValue, GroupingNative<TKey, TValue>.Enumerator>
        where TKey : unmanaged
        where TValue : unmanaged
    {
        private TKey _key;
        private PooledListNative<TValue> _values;

        public GroupingNative(TKey key, PooledListNative<TValue> values)
        {
            _key = key;
            _values = values;
        }

        public TKey Key
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _key;
        }

        public Enumerator GetEnumerator() => new Enumerator(_values);

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<TValue>
        {
            private PooledListNative<TValue> _values;
            private int _index;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Enumerator(PooledListNative<TValue> values)
            {
                _values = values;
                _index = -1;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                return (uint)++_index < (uint)_values.Count;
            }

            public TValue Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _values[_index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Dispose() { }

            public void Reset() => throw new NotSupportedException();

            object IEnumerator.Current => Current;
        }
    }
}
