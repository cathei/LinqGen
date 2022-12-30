// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class OrderByReference
{
    public class IntWrapper : IComparable<IntWrapper>
    {
        public int inner;

        public int CompareTo(IntWrapper? other)
        {
            return inner.CompareTo(other!.inner);
        }
    }

    private static List<IntWrapper> smallList;
    private static List<IntWrapper> mediumList;
    private static List<IntWrapper> largeList;

    static OrderByReference()
    {
        var rand = new Random(1024);

        Fill(smallList = new(), 20, rand);
        Fill(mediumList = new(), 500, rand);
        Fill(largeList = new(), 10000, rand);
    }

    private static void Fill(List<IntWrapper> list, int count, Random random)
    {
        for (int i = 0; i < count; i++)
            list.Add(new IntWrapper { inner = random.Next(1000) });
    }

    public static IEnumerable<IntWrapper[]> Lists
    {
        get
        {
            yield return smallList.ToArray();
            yield return mediumList.ToArray();
            yield return largeList.ToArray();
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double Linq(IntWrapper[] list)
    {
        return list.OrderBy(x => x)
            .Sum(x => x.inner);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double LinqGenDelegate(IntWrapper[] list)
    {
        return list.Gen()
            .Order()
            .Sum(x => x.inner);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double LinqGenStruct(IntWrapper[] list)
    {
        return list.Gen()
            .Order(new Comparer())
            .Sum(new Selector());
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double StructLinqDelegate(IntWrapper[] list)
    {
        return list.ToStructEnumerable()
            .Order()
            .Sum(x => x.inner);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double StructLinqStruct(IntWrapper[] list)
    {
        var comparer = new Comparer();
        var selector = new Selector();

        return list.ToStructEnumerable()
            .Order(ref comparer, x => x)
            .Sum(ref selector, x => x, x => x);
    }

    readonly struct Identity : IStructFunction<IntWrapper, IntWrapper>
    {
        public IntWrapper Invoke(IntWrapper arg)
        {
            return arg;
        }
    }

    readonly struct Selector :
        StructLinq.IFunction<IntWrapper, int>,
        IStructFunction<IntWrapper, int>
    {
        public int Eval(IntWrapper element)
        {
            return element.inner;
        }

        public int Invoke(IntWrapper arg)
        {
            return arg.inner;
        }
    }

    readonly struct Comparer :
        IStructFunction<IntWrapper, IntWrapper, int>,
        IComparer<IntWrapper>
    {
        public int Invoke(IntWrapper arg1, IntWrapper arg2)
        {
            return arg1.inner - arg2.inner;
        }

        public int Compare(IntWrapper? x, IntWrapper? y)
        {
            return x!.inner - y!.inner;
        }
    }
}
