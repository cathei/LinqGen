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
        var gen = Enumerable.Range(0, 10).Generate();
    }
}