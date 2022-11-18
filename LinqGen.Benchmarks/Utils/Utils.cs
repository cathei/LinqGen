// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Attributes;
using StructLinq;
using Cathei.LinqGen;
using NetFabric.Hyperlinq;

namespace Cathei.LinqGen.Benchmarks;

public static class Utils
{
    public static IEnumerable<int> Generate(int seed, int count)
    {
        var rand = new Random(seed);

        for (int i = 0; i < count; ++i)
        {
            yield return rand.Next(0, 100);
        }
    }
}
