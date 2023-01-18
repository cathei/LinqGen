// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using StructLinq;
using NetFabric.Hyperlinq;
using StructLinq.Range;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class MaxBy
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

            if (max < value * -2)
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
            if (max < value * -2)
                max = value;
        }

        return max;
    }

#if NET6_0_OR_GREATER
    [Benchmark(Baseline = true)]
    public int Linq()
    {
        return TestData.MaxBy(x => x * -2);
    }
#endif

    [Benchmark]
    public int LinqGen()
    {
        return TestData.Gen().MaxBy(new Selector());
    }

    struct Selector : IStructFunction<int, int>, StructLinq.IFunction<int, int>
    {
        public int Invoke(int arg)
        {
            return arg * -2;
        }

        public int Eval(int element)
        {
            return element * -2;
        }
    }
}
