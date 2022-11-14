// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using StructLinq;
using NetFabric.Hyperlinq;
using StructLinq.Range;

namespace LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class ArrayWhereToList
{
    private const int Count = 10_000;

    private static int[] TestData;

    static ArrayWhereToList()
    {
        Random r = new Random();

        TestData = new int[Count];

        for (int i = 0; i < Count; i++)
        {
            TestData[i] = r.Next(0, 100);
        }
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
            .Specialize()
            .Where(x => x % 2 == 0)
            .ToList();
    }

    [Benchmark]
    public List<int> LinqGenStruct()
    {
        return TestData
            .Specialize()
            .Where(new Predicate())
            .ToList();
    }

    [Benchmark]
    public List<int> StructLinqDelegate()
    {
        return TestData
            .ToStructEnumerable()
            .Where(x => x % 2 == 0)
            .ToList();
    }

    [Benchmark]
    public List<int> StructLinqStruct()
    {
        var predicate = new Predicate();

        return TestData
            .ToStructEnumerable()
            .Where(ref predicate, x => x)
            .ToList(x => x);
    }

    [Benchmark]
    public List<int> HyperLinqDelegate()
    {
        return TestData
            .AsValueEnumerable()
            .Where(x => x % 2 == 0)
            .ToList();
    }

    [Benchmark]
    public List<int> HyperLinqStruct()
    {
        return TestData
            .AsValueEnumerable()
            .Where<Predicate>()
            .ToList();
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
