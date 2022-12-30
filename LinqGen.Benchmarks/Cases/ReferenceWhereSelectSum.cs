// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;
using Cathei.LinqGen;
using NetFabric.Hyperlinq;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class ReferenceWhereSelectSum
{
    private const int Count = 10_000;

    private static List<ReferenceValue<int>> TestData;

    static ReferenceWhereSelectSum()
    {
        TestData = new List<ReferenceValue<int>>();

        for (int i = 0; i < Count; ++i)
            TestData.Add(new ReferenceValue<int>(i));
    }

    [Benchmark]
    public double ForEachLoop()
    {
        double sum = 0;

        foreach (var i in TestData)
        {
            if (i.Value % 2 == 0)
                sum += i.Value * 2.0;
        }

        return sum;
    }

    [Benchmark(Baseline = true)]
    public double Linq()
    {
        return TestData
            .Where(x => x.Value % 2 == 0)
            .Select(x=> x.Value * 2.0)
            .Sum();
    }

    [Benchmark]
    public double LinqGenDelegate()
    {
        return TestData.Gen()
            .Where(x => x.Value % 2 == 0)
            .Select(x => x.Value * 2.0)
            .Sum();
    }

    [Benchmark]
    public double LinqGenStruct()
    {
        var predicate = new Predicate();
        var selector = new Selector();

        return TestData.Gen()
           .Where(predicate)
           .Select(selector)
           .Sum();
    }

    [Benchmark]
    public double StructLinqDelegate()
    {
        return TestData.ToStructEnumerable()
            .Where(x => x.Value % 2 == 0)
            .Select(x => x.Value * 2.0)
            .Sum();
    }

    [Benchmark]
    public double StructLinqStruct()
    {
        var predicate = new Predicate();
        var selector = new Selector();

        return TestData.ToStructEnumerable()
            .Where(ref predicate, x => x)
            .Select(ref selector, x => x, x => x)
            .Sum(x=> x);
    }

    [Benchmark]
    public double HyperLinqDelegate()
    {
        return TestData.AsValueEnumerable()
            .Where(x => x.Value % 2 == 0)
            .Select(x => x.Value * 2.0)
            .Sum();
    }

    [Benchmark]
    public double HyperLinqStruct()
    {
        return TestData.AsValueEnumerable()
            .Where<Predicate>()
            .Select<double, Selector>()
            .Sum();
    }

    readonly struct Predicate :
        StructLinq.IFunction<ReferenceValue<int>, bool>,
        NetFabric.Hyperlinq.IFunction<ReferenceValue<int>, bool>,
        IStructFunction<ReferenceValue<int>, bool>
    {
        public bool Eval(ReferenceValue<int> element)
        {
            return element.Value % 2 == 0;
        }

        public bool Invoke(ReferenceValue<int> arg)
        {
            return arg.Value % 2 == 0;
        }
    }

    readonly struct Selector :
        StructLinq.IFunction<ReferenceValue<int>, double>,
        NetFabric.Hyperlinq.IFunction<ReferenceValue<int>, double>,
        IStructFunction<ReferenceValue<int>, double>
    {
        public double Eval(ReferenceValue<int> element)
        {
            return element.Value * 2.0;
        }

        public double Invoke(ReferenceValue<int> arg)
        {
            return arg.Value * 2.0;
        }
    }
}
