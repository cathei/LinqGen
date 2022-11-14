// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using Cathei.LinqGen;
using StructLinq;
using NetFabric.Hyperlinq;
using StructLinq.Range;

namespace LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class ArrayWhereToArray
{
    private const int Count = 10_000;

    private static int[] TestData;

    static ArrayWhereToArray()
    {
        Random r = new Random();

        TestData = new int[Count];

        for (int i = 0; i < Count; i++)
        {
            TestData[i] = r.Next(0, 100);
        }
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
            .Specialize()
            .Where(x => x % 2 == 0)
            .ToArray();
    }

    [Benchmark]
    public int[] LinqGenStruct()
    {
        return TestData
            .Specialize()
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
