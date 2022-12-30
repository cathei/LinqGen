// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;

namespace Cathei.LinqGen.Benchmarks.Cases;

[MemoryDiagnoser]
public class OrderBySkipTake : OrderByBenchmarkBase
{
    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double Linq(int[] list)
    {
        return list.OrderBy(x => x)
            .Skip(10)
            .Take(10)
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double LinqGenDelegate(int[] list)
    {
        return list.Gen()
            .Order()
            .Skip(10)
            .Take(10)
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double LinqGenStruct(int[] list)
    {
        return list.Gen()
            .Order(new Comparer())
            .Skip(10)
            .Take(10)
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double StructLinqDelegate(int[] list)
    {
        return list.ToStructEnumerable()
            .Order()
            .Skip(10)
            .Take(10)
            .Sum();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Lists))]
    public double StructLinqStruct(int[] list)
    {
        var comparer = new Comparer();

        return list.ToStructEnumerable()
            .Order(ref comparer, x => x)
            .Skip(10, x => x)
            .Take(10, x => x)
            .Sum(x => x);
    }

    readonly struct Comparer :
        IStructFunction<int, int, int>,
        IComparer<int>
    {
        public int Invoke(int arg1, int arg2)
        {
            return arg1 - arg2;
        }

        public int Compare(int x, int y)
        {
            return x - y;
        }
    }
}
