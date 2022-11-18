// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using Cathei.LinqGen.Hidden;
using StructLinq;
using NetFabric.Hyperlinq;
using StructLinq.Range;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class AddVsAddRange
{
    private int[] TestData { get; set; }

    [Params(100, 10_000, 1_000_000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        TestData = Utils.Generate(42, Count).ToArray();
    }

    [Benchmark()]
    public List<int> ListAddLinq()
    {
        var query = TestData.Where(x => x % 2 == 0);

        var list = new List<int>();

        using var iter = query.GetEnumerator();

        while (iter.MoveNext())
            list.Add(iter.Current);

        return list;
    }

    [Benchmark()]
    public List<int>  ListAddRangeLinq()
    {
        var query = TestData.Where(x => x % 2 == 0);

        var list = new List<int>(query);

        return list;
    }

    [Benchmark()]
    public List<int>  ToListLinq()
    {
        var query = TestData.Where(x => x % 2 == 0);

        return query.ToList();
    }

    [Benchmark()]
    public List<int> ListAdd()
    {
        var query = TestData.Specialize().Where(x => x % 2 == 0);

        var list = new List<int>();

        using var iter = query.GetEnumerator();

        while (iter.MoveNext())
            list.Add(iter.Current);

        return list;
    }

    // [Benchmark()]
    // public List<int> ListAddRange()
    // {
    //     var query = TestData.Where(x => x % 2 == 0);
    //
    //     var list = new List<int>();
    //
    //     list.AddRange(query);
    //
    //     return list;
    // }

    [Benchmark(Baseline = true)]
    public List<int> Add()
    {
        var query = TestData.Specialize().Where(x => x % 2 == 0);

        using var temp = new PooledList<int>(0);

        using var iter = query.GetEnumerator();

        while (iter.MoveNext())
            temp.Add(iter.Current);

        return temp.ToList();
    }

    [Benchmark]
    public List<int> AddRange()
    {
        var query = TestData.Specialize().Where(x => x % 2 == 0);

        using var temp = new PooledList<int>(0);

        using var iter = query.GetEnumerator();

        temp.AddRange(iter);

        return temp.ToList();
    }

    // [Benchmark]
    // public int[] AddRange2()
    // {
    //     var query = TestData.Specialize().Where(x => x % 2 == 0);
    //
    //     PooledList<int> temp = new PooledList<int>(0);
    //
    //     using var iter = query.GetEnumerator();
    //
    //     temp.AddRange2(iter);
    //
    //     return temp.Array;
    // }
    //
    // [Benchmark]
    // public int[] RefAddRange()
    // {
    //     var query = TestData.Specialize().Where(x => x % 2 == 0);
    //
    //     PooledList<int> temp = new PooledList<int>(0);
    //
    //     var iter = query.GetEnumerator();
    //
    //     try
    //     {
    //         temp.AddRange(ref iter);
    //     }
    //     finally
    //     {
    //         iter.Dispose();
    //     }
    //
    //     return temp.Array;
    // }
    //
    // [Benchmark]
    // public int[] RefAddRange2()
    // {
    //     var query = TestData.Specialize().Where(x => x % 2 == 0);
    //
    //     PooledList<int> temp = new PooledList<int>(0);
    //
    //     var iter = query.GetEnumerator();
    //
    //     try
    //     {
    //         temp.AddRange2(ref iter);
    //     }
    //     finally
    //     {
    //         iter.Dispose();
    //     }
    //
    //     return temp.Array;
    // }
}
