// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using StructLinq;
using NetFabric.Hyperlinq;
using StructLinq.Range;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class ListWhereToArray
{
    private List<int> TestData { get; set; } = null!;

    [Params(100, 10_000, 1_000_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        TestData = Utils.Generate(42, Count).ToList();
    }

    [Benchmark(Baseline = true)]
    public int[] Linq()
    {
        return TestData
            .Where(x=> x % 2 == 0)
            .ToArray();
    }

    [Benchmark]
    public int[] LinqGenDelegate()
    {
        return TestData
            .Gen()
            .Where(x => x % 2 == 0)
            .ToArray();
    }

    [Benchmark]
    public int[] LinqGenStruct()
    {
        return TestData
            .Gen()
            .Where(new Predicate())
            .ToArray();
    }

    [Benchmark]
    public int[] StructLinqDelegate()
    {
        return TestData
            .ToStructEnumerable()
            .Where(x => x % 2 == 0)
            .ToArray();
    }

    [Benchmark]
    public int[] StructLinqStruct()
    {
        var predicate = new Predicate();

        return TestData
            .ToStructEnumerable()
            .Where(ref predicate, x => x)
            .ToArray(x => x);
    }

    [Benchmark]
    public int[] HyperLinqDelegate()
    {
        return TestData
            .AsValueEnumerable()
            .Where(x => x % 2 == 0)
            .ToArray();
    }

    [Benchmark]
    public int[] HyperLinqStruct()
    {
        return TestData
            .AsValueEnumerable()
            .Where<Predicate>()
            .ToArray();
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
}
