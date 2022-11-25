// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using StructLinq;
using NetFabric.Hyperlinq;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class SelectCount
{
    private const int Count = 10_000;

    [Benchmark(Baseline = true)]
    public double Linq()
    {
        return Enumerable
            .Range(0, Count)
            .Select(x=> x * 2.0)
            .Count();
    }

    [Benchmark]
    public double LinqGenDelegate()
    {
        return Gen.Enumerable
            .Range(0, Count)
            .Select(x => x * 2.0)
            .Count();
    }

    [Benchmark]
    public double LinqGenStruct()
    {
        var selector = new Selector();

        return Gen.Enumerable
               .Range(0, Count)
               .Select(selector)
               .Count();
    }

    [Benchmark]
    public double StructLinqDelegate()
    {
        return StructEnumerable
            .Range(0, Count)
            .Select(x => x * 2.0)
            .Count();
    }

    [Benchmark]
    public double StructLinqStruct()
    {
        var selector = new Selector();

        return StructEnumerable
            .Range(0, Count)
            .Select(ref selector, x => x, x => x)
            .Count(x=> x);
    }

    [Benchmark]
    public double HyperLinqDelegate()
    {
        return ValueEnumerable
            .Range(0, Count)
            .Select(x => x * 2.0)
            .Count();
    }

    [Benchmark]
    public double HyperLinqStruct()
    {
        return ValueEnumerable
            .Range(0, Count)
            .Select<double, Selector>()
            .Count();
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
