// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using Cathei.LinqGen.Hidden;
using StructLinq;
using NetFabric.Hyperlinq;
using StructLinq.Range;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class ToArrayVsToList
{
    // private const int Count = 1_000_000;
    // private static PooledList<int> TestData;
    //
    // static ToArrayVsToList()
    // {
    //     Random r = new Random(42);
    //
    //     TestData = new PooledList<int>(Count);
    //
    //     for (int i = 0; i < Count; i++)
    //     {
    //         TestData[i] = r.Next(0, 100);
    //     }
    // }
    //
    // [Benchmark(Baseline = true)]
    // public int[] PooledToArray()
    // {
    //     return TestData.ToArray();
    // }
    //
    // [Benchmark]
    // public List<int> PooledToList()
    // {
    //     return TestData.ToList();
    // }

    private const int Count = 1_000_000;

    private static int[] TestData;

    static ToArrayVsToList()
    {
        Random r = new Random(42);

        TestData = new int[Count];

        for (int i = 0; i < Count; i++)
        {
            TestData[i] = r.Next(0, 100);
        }
    }

    [Benchmark]
    public int[] ToArray()
    {
        return TestData
            .Gen()
            .Where(x => x % 2 == 0)
            .ToArray();
    }

    [Benchmark]
    public int[] ToArrayStruct()
    {
        return TestData
            .Gen()
            .Where(new Predicate())
            .ToArray();
    }

    [Benchmark]
    public List<int> ToList()
    {
        return TestData
            .Gen()
            .Where(x => x % 2 == 0)
            .ToList();
    }

    [Benchmark]
    public List<int> ToListStruct()
    {
        return TestData
            .Gen()
            .Where(new Predicate())
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
