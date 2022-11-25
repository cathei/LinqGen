// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class MinMaxTests
{
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultMin_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Min();

        var actual = Gen.Enumerable.Range(start, count)
            .Min();

        Assert.AreEqual(expected, actual);
    }


    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultMinStruct_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Min(new Comparer());

        var actual = Gen.Enumerable.Range(start, count)
            .Min(new Comparer());

        Assert.AreEqual(expected, actual);
    }

    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultMax_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Max();

        var actual = Gen.Enumerable.Range(start, count)
            .Max();

        Assert.AreEqual(expected, actual);
    }

    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultMaxStruct_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Max(new Comparer());

        var actual = Gen.Enumerable.Range(start, count)
            .Max(new Comparer());

        Assert.AreEqual(expected, actual);
    }
    private struct Comparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x % 3 - y % 3;
        }
    }
}
