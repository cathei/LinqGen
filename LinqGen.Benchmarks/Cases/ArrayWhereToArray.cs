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
    private static int[] smallTestData;
    private static int[] mediumTestData;
    private static int[] largeTestData;

    static ArrayWhereToArray()
    {
        Random r = new Random(42);

        Fill(smallTestData = new int[100], r);
        Fill(mediumTestData = new int[10_000], r);
        Fill(largeTestData = new int[1_000_000], r);
    }

    static void Fill(int[] testData, Random r)
    {
        for (int i = 0; i < testData.Length; i++)
        {
            testData[i] = r.Next(0, 100);
        }
    }

    public static IEnumerable<int[]> TestData
    {
        get
        {
            yield return smallTestData;
            yield return mediumTestData;
            yield return largeTestData;
        }
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(TestData))]
    public int[] Linq(int[] testData)
    {
        return testData
            .Where(x=> x % 2 == 0)
            .ToArray();
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public int[] LinqGenDelegate(int[] testData)
    {
        return testData
            .Specialize()
            .Where(x => x % 2 == 0)
            .ToArray();
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public int[] LinqGenStruct(int[] testData)
    {
        return testData
            .Specialize()
            .Where(new Predicate())
            .ToArray();
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public int[] StructLinqDelegate(int[] testData)
    {
        return testData
            .ToStructEnumerable()
            .Where(x => x % 2 == 0)
            .ToArray();
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public int[] StructLinqStruct(int[] testData)
    {
        var predicate = new Predicate();

        return testData
            .ToStructEnumerable()
            .Where(ref predicate, x => x)
            .ToArray(x => x);
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public int[] HyperLinqDelegate(int[] testData)
    {
        return testData
            .AsValueEnumerable()
            .Where(x => x % 2 == 0)
            .ToArray();
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public int[] HyperLinqStruct(int[] testData)
    {
        return testData
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
