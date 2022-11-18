// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;
using Cathei.LinqGen;
using NetFabric.Hyperlinq;

namespace Cathei.LinqGen.Benchmarks;

public class ReferenceValue<T>
{
    public T Value { get; }

    public ReferenceValue(T value)
    {
        Value = value;
    }
}
