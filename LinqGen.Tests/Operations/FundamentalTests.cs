// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;
using NUnit.Framework;

namespace Cathei.LinqGen.Tests;

public class FundamentalTests
{
    // [Test]
    // public void Fundamental_DelegateComparison()
    // {
    //     Expression<Func<int, int>> a = x => x;
    //     Expression<Func<int, int>> b = y => y;
    //     Expression<Func<int, int>> c = x => x;
    //
    //     Assert.IsTrue();
    //     Assert.AreEqual(a, b);
    // }

    [TestCase(2)]
    [TestCase(5)]
    [TestCase(0)]
    public void TestArrayTypeGeneration(int c1)
    {
        int[] array = new int[c1];

        int count = 0;

        foreach (var i in array.Specialize())
            count++;

        Assert.AreEqual(c1, count);
    }


    // [TestCase(1, 2, 3, 4)]
    // [TestCase(8, 6, 4, 2)]
    // [TestCase(2, 0, 4, 2)]
    // public void TestArrayMultiTypeGeneration(int c1, int c2, int c3, int c4)
    // {
    //     int[,,,] array = new int[c1, c2, c3, c4];
    //
    //     int count = 0;
    //
    //     foreach (var i in array.Specialize())
    //         count++;
    //
    //     Assert.AreEqual(c1 * c2 * c3 * c4, count);
    // }

    [Test]
    public void TestGenericTypeGeneration()
    {
        Assert.AreEqual(10, TypeGenerationTest(10));
        Assert.AreEqual(20.0, TypeGenerationTest(20.0));
        Assert.AreEqual("Test string", TypeGenerationTest("Test string"));
    }

    private TTestParam TypeGenerationTest<TTestParam>(TTestParam value)
    {
        return Enumerable.Repeat(value, 1)
            .Specialize()
            .First();
    }
}