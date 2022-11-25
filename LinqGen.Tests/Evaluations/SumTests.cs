// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class SumTests
{

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResult_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Sum();

        var actual = Gen.Enumerable.Range(start, count)
            .Sum();

        Assert.AreEqual(expected, actual);
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultDelegate_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Sum(x => x * 10);

        var actual = Gen.Enumerable.Range(start, count)
            .Sum(x => x * 10);

        Assert.AreEqual(expected, actual);
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultStruct_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Sum(x => x * 0.1m);

        var actual = Gen.Enumerable.Range(start, count)
            .Sum(new Selector());

        Assert.AreEqual(expected, actual);
    }

    private struct Selector : IStructFunction<int, decimal>
    {
        public decimal Invoke(int arg)
        {
            return arg * 0.1m;
        }
    }
}
