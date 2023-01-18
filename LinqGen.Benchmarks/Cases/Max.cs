// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using StructLinq;
using NetFabric.Hyperlinq;
using StructLinq.Range;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class Max
{
    private int[] TestData { get; set; } = null!;

    [Params(100, 10_000, 1_000_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        TestData = Utils.Generate(42, Count).ToArray();
    }

    [Benchmark]
    public int ForLoop()
    {
        int max = int.MinValue;

        for (int i = 0; i < TestData.Length; ++i)
        {
            int value = TestData[i];

            if (max < value)
                max = value;
        }

        return max;
    }

    [Benchmark]
    public int ForEachLoop()
    {
        int max = int.MinValue;

        foreach (var value in TestData)
        {
            if (max < value)
                max = value;
        }

        return max;
    }

    [Benchmark(Baseline = true)]
    public int Linq()
    {
        return TestData.Max();
    }

    [Benchmark]
    public int LinqGen()
    {
        return TestData.Gen().Max();
    }

    [Benchmark]
    public int StructLinq()
    {
        return TestData.ToStructEnumerable().Max(x => x);
    }
}
