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
        var gen = Enumerable.Range(0, 10).Generate()
            .Select(x => x / 10)
            .Where(x => x % 2 == 1);

        var t = Enumerable.Repeat(0.1f, 10).Generate();
        // var a = t.Select(x => 10);
        var b = t.Select(x => 10).Where(x => x % 2 == 0);
    }
}
