// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;
using Cathei.LinqGen;
using NetFabric.Hyperlinq;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class ArrayWhereSingle
{
    private int[] TestData     { get; set; } = null!;
    private int   SingularItem { get; set; } = 0;

    [Params(100, 10_000, 1_000_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        TestData      = Utils.GenerateLarge(42, Count).ToArray();
        SingularItem = TestData.GroupBy(a => a).OrderBy(a => a.Count()).First().Key;
    }

    [Benchmark]
    public int ForLoop()
    {
        int? matched = null;

        for (int i = 0; i < Count; ++i)
        {
            int value = TestData[i];

            if (value == SingularItem) {
                if (matched is null)
                    matched = value;
                else
                    throw new InvalidOperationException();
            }
        }
        
        return matched ?? throw new InvalidOperationException();
    }

    [Benchmark]
    public int ForEachLoop()
    {
        int? matched = null;

        foreach (var value in TestData)
        {
            if (value == SingularItem) {
                if (matched is null)
                    matched = value;
                else
                    throw new InvalidOperationException();
            }
        }

        return matched ?? throw new InvalidOperationException();
    }

    [Benchmark(Baseline = true)]
    public int Linq()
    {
        // ReSharper disable once ReplaceWithSingleCallToSingle
        return TestData
              .Where(x => x == SingularItem)
              .Single();
    }

    [Benchmark]
    public int CondensedLinq()
    {
        return TestData.Single(x => x == SingularItem);
    }

    [Benchmark]
    public int LinqGenDelegate()
    {
        return TestData
            .Gen()
            .Where(x => x == SingularItem)
            .Single();
    }

    [Benchmark]
    public int LinqGenStruct()
    {
        var predicate = new Predicate(SingularItem);

        return TestData
           .Gen()
           .Where(predicate)
           .Sum();
    }

    [Benchmark]
    public int StructLinqDelegate()
    {
        return TestData
            .ToStructEnumerable()
            .Where(x => x % 2 == 0)
            .Sum();
    }

    [Benchmark]
    public int StructLinqStruct()
    {
        var predicate = new Predicate(SingularItem);

        return TestData
            .ToStructEnumerable()
            .Where(ref predicate, x => x)
            .Sum(x=> x);
    }

    [Benchmark]
    public int HyperLinqDelegate()
    {
        return TestData
            .AsValueEnumerable()
            .Where(x => x % 2 == 0)
            .Sum();
    }

    [Benchmark]
    public int HyperLinqStruct()
    {
        var predicate = new Predicate(SingularItem);

        return TestData
            .AsValueEnumerable()
            .Where(predicate)
            .Sum();
    }

    readonly struct Predicate :
        StructLinq.IFunction<int, bool>,
        NetFabric.Hyperlinq.IFunction<int, bool>,
        IStructFunction<int, bool>
    {
        private readonly int _singularItem;

        public Predicate(int singularItem) {
            _singularItem = singularItem;
        }

        public bool Eval(int element)
        {
            return element == _singularItem;
        }

        public bool Invoke(int arg)
        {
            return arg == _singularItem;
        }
    }
}
