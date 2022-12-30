// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using StructLinq;
using NetFabric.Hyperlinq;
using StructLinq.Range;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class ArrayWhereToList
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
    public List<int> ForEach()
    {
        List<int> list = new List<int>();

        foreach (var item in TestData)
        {
            if (item % 2 == 0)
                list.Add(item);
        }

        return list;
    }

    [Benchmark(Baseline = true)]
    public List<int> Linq()
    {
        return TestData
            .Where(x=> x % 2 == 0)
            .ToList();
    }

    [Benchmark]
    public List<int> LinqGenDelegate()
    {
        return TestData
            .Gen()
            .Where(x => x % 2 == 0)
            .ToList();
    }

    [Benchmark]
    public List<int> LinqGenStruct()
    {
        return TestData
            .Gen()
            .Where(new Predicate())
            .ToList();
    }

    // [Benchmark]
    // public List<int> StructLinqDelegate()
    // {
    //     return TestData
    //         .ToStructEnumerable()
    //         .Where(x => x % 2 == 0)
    //         .ToList();
    // }
    //
    // [Benchmark]
    // public List<int> StructLinqStruct()
    // {
    //     var predicate = new Predicate();
    //
    //     return TestData
    //         .ToStructEnumerable()
    //         .Where(ref predicate, x => x)
    //         .ToList(x => x);
    // }
    //
    // [Benchmark]
    // public List<int> HyperLinqDelegate()
    // {
    //     return TestData
    //         .AsValueEnumerable()
    //         .Where(x => x % 2 == 0)
    //         .ToList();
    // }
    //
    // [Benchmark]
    // public List<int> HyperLinqStruct()
    // {
    //     return TestData
    //         .AsValueEnumerable()
    //         .Where<Predicate>()
    //         .ToList();
    // }

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
}
