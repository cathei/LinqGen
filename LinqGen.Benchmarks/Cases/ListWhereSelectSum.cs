// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;
using Cathei.LinqGen;
using NetFabric.Hyperlinq;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class ListWhereSelectSum
{
    private List<int> TestData { get; set; } = null!;

    [Params(100, 10_000, 1_000_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        TestData = Utils.Generate(42, Count).ToList();
    }

    [Benchmark]
    public double ForLoop()
    {
        double sum = 0;

        for (int i = 0; i < Count; ++i)
        {
            int value = TestData[i];

            if (value % 2 == 0)
                sum += value * 2.0;
        }

        return sum;
    }

    [Benchmark]
    public double ForEachLoop()
    {
        double sum = 0;

        foreach (var value in TestData)
        {
            if (value % 2 == 0)
                sum += value * 2.0;
        }

        return sum;
    }

    [Benchmark(Baseline = true)]
    public double Linq()
    {
        return TestData
            .Where(x => x % 2 == 0)
            .Select(x=> x * 2.0)
            .Sum();
    }

    [Benchmark]
    public double LinqGenDelegate()
    {
        return TestData
            .Gen()
            .Where(x => x % 2 == 0)
            .Select(x => x * 2.0)
            .Sum();
    }

    [Benchmark]
    public double LinqGenStruct()
    {
        var predicate = new Predicate();
        var selector = new Selector();

        return TestData
           .Gen()
           .Where(predicate)
           .Select(selector)
           .Sum();
    }

    [Benchmark]
    public double StructLinqDelegate()
    {
        return TestData
            .ToStructEnumerable()
            .Where(x => x % 2 == 0)
            .Select(x => x * 2.0)
            .Sum();
    }

    [Benchmark]
    public double StructLinqStruct()
    {
        var predicate = new Predicate();
        var selector = new Selector();

        return TestData
            .ToStructEnumerable()
            .Where(ref predicate, x => x)
            .Select(ref selector, x => x, x => x)
            .Sum(x=> x);
    }

    [Benchmark]
    public double HyperLinqDelegate()
    {
        return TestData
            .AsValueEnumerable()
            .Where(x => x % 2 == 0)
            .Select(x => x * 2.0)
            .Sum();
    }

    [Benchmark]
    public double HyperLinqStruct()
    {
        return TestData
            .AsValueEnumerable()
            .Where<Predicate>()
            .Select<double, Selector>()
            .Sum();
    }

    readonly struct Predicate :
        StructLinq.IFunction<int, bool>,
        NetFabric.Hyperlinq.IFunction<int, bool>,
        IStructFunction<int, bool>
    {
        public bool Eval(int element)
        {
            return element % 2 == 0;
        }

        public bool Invoke(int arg)
        {
            return arg % 2 == 0;
        }
    }

    readonly struct Selector :
        StructLinq.IFunction<int, double>,
        NetFabric.Hyperlinq.IFunction<int, double>,
        IStructFunction<int, double>
    {
        public double Eval(int element)
        {
            return element * 2.0;
        }

        public double Invoke(int arg)
        {
            return arg * 2.0;
        }
    }
}
