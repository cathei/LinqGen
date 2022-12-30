// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using Cathei.LinqGen.Hidden;
using StructLinq;
using NetFabric.Hyperlinq;
using StructLinq.Range;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class ArraySelectToArray
{
    private int[] TestData { get; set; } = null!;

    [Params(100, 10_000, 1_000_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        TestData = Utils.Generate(42, Count).ToArray();
    }

    [Benchmark(Baseline = true)]
    public int[] Linq()
    {
        return TestData
            .Select(x=> x * 2)
            .ToArray();
    }

    [Benchmark]
    public int[] LinqGenDelegate()
    {
        return TestData
            .Gen()
            .Select(x => x * 2)
            .ToArray();
    }

    [Benchmark]
    public int[] LinqGenStruct()
    {
        return TestData
            .Gen()
            .Select(new Selector())
            .ToArray();
    }

    [Benchmark]
    public int[] StructLinqDelegate()
    {
        return TestData
            .ToStructEnumerable()
            .Select(x => x * 2)
            .ToArray();
    }

    [Benchmark]
    public int[] StructLinqStruct()
    {
        var selector = new Selector();

        return TestData
            .ToStructEnumerable()
            .Select(ref selector, x => x, x => x)
            .ToArray(x => x);
    }

    [Benchmark]
    public int[] HyperLinqDelegate()
    {
        return TestData
            .AsValueEnumerable()
            .Select(x => x * 2)
            .ToArray();
    }

    [Benchmark]
    public int[] HyperLinqStruct()
    {
        return TestData
            .AsValueEnumerable()
            .Select<int, Selector>()
            .ToArray();
    }

    readonly struct Selector :
        StructLinq.IFunction<int, int>,
        NetFabric.Hyperlinq.IFunction<int, int>,
        IStructFunction<int, int>
    {
        public int Eval(int element)
        {
            return element * 2;
        }

        public int Invoke(int arg)
        {
            return arg * 2;
        }
    }
}
