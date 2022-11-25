// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using StructLinq;
using NetFabric.Hyperlinq;
using StructLinq.Range;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class SelectSum
{
    private const int Count = 10_000;

    [Benchmark]
    public double ForLoop()
    {
        double sum = 0;

        for (int i = 0; i < Count; ++i)
            sum += i * 2.0;

        return sum;
    }

    [Benchmark]
    public double ForEachLoop()
    {
        double sum = 0;

        foreach (var i in Enumerable.Range(0, Count))
            sum += i * 2.0;

        return sum;
    }

    [Benchmark(Baseline = true)]
    public double Linq()
    {
        return Enumerable
            .Range(0, Count)
            .Select(x=> x * 2.0)
            .Sum();
    }

    [Benchmark]
    public double LinqGenDelegate()
    {
        return Gen.Enumerable
            .Range(0, Count)
            .Select(x => x * 2.0)
            .Sum();
    }

    [Benchmark]
    public double LinqGenStruct()
    {
        var selector = new Selector();

        return Gen.Enumerable
            .Range(0, Count)
            .Select(selector)
            .Sum();
    }

    [Benchmark]
    public double StructLinqDelegate()
    {
        return StructEnumerable
            .Range(0, Count)
            .Select(x => x * 2.0)
            .Sum();
    }

    [Benchmark]
    public double StructLinqStruct()
    {
        var selector = new Selector();

        return StructEnumerable
            .Range(0, Count)
            .Select(ref selector, x => x, x => x)
            .Sum(x=> x);
    }

    [Benchmark]
    public double HyperLinqDelegate()
    {
        return ValueEnumerable
            .Range(0, Count)
            .Select(x => x * 2.0)
            .Sum();
    }

    [Benchmark]
    public double HyperLinqStruct()
    {
        return ValueEnumerable
            .Range(0, Count)
            .Select<double, Selector>()
            .Sum();
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
