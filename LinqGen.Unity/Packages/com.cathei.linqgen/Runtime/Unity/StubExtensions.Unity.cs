// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;
using Unity.Collections;

namespace Cathei.LinqGen
{
    public static partial class StubExtensions
    {
        public static Stub<IEnumerable<T>, Gen<NativeArray<T>>> Gen<T>(this NativeArray<T> enumerable)
            where T : struct
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Gen<NativeList<T>>> Gen<T>(this NativeList<T> enumerable)
            where T : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Gen<FixedList32Bytes<T>>> Gen<T>(this FixedList32Bytes<T> enumerable)
            where T : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Gen<FixedList64Bytes<T>>> Gen<T>(this FixedList64Bytes<T> enumerable)
            where T : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Gen<FixedList128Bytes<T>>> Gen<T>(this FixedList128Bytes<T> enumerable)
            where T : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Gen<FixedList512Bytes<T>>> Gen<T>(this FixedList512Bytes<T> enumerable)
            where T : unmanaged
        {
            throw new NotImplementedException();
        }

        public static Stub<IEnumerable<T>, Gen<FixedList4096Bytes<T>>> Gen<T>(this FixedList4096Bytes<T> enumerable)
            where T : unmanaged
        {
            throw new NotImplementedException();
        }
    }
}