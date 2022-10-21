// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

public class GenerationTests
{
    [Test]
    public void TestGeneration()
    {
        var gen0 = Enumerable.Range(0, 10).Gen();
        gen0.Select(x => x / 10);

        var gen = Enumerable.Range(0, 10)
            .Select(x => x / 10)
            .Gen()
            .Select(x => x / 10)
            .Select(x => x / 10)
            .Where(r => r % 2 == 1)
            .Select(m => m * 3);

        foreach (var temp in gen)
        {

        }

        var t = Enumerable.Repeat(0.1f, 10).Gen();
        // var a = t.Select(x => 10);
        var b = t.Select(x => 10)
            .Where(x => x % 2 == 0)
            .Select(x => x * 3)
            .Where(x => x / 10 == 1);

        var tt = Enumerable.Repeat(3m, 10).Gen();


    }
}
