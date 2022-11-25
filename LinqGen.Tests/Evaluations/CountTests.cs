// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class CountTests
{
    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResult_SameAsLinq(int elem, int count)
    {
        var expected = Enumerable.Repeat(elem, count)
            .Count();

        var actual = Gen.Enumerable.Repeat(elem, count)
            .Count();

        Assert.AreEqual(expected, actual);
    }


    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultDelegate_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Count(x => x % 2 == 0);

        var actual = Gen.Enumerable.Range(start, count)
            .Count(x => x % 2 == 0);

        Assert.AreEqual(expected, actual);
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultStruct_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Count(x => x % 2 == 0);

        var actual = Gen.Enumerable.Range(start, count)
            .Count(new Predicate());

        Assert.AreEqual(expected, actual);
    }

    private struct Predicate : IStructFunction<int, bool>
    {
        public bool Invoke(int arg)
        {
            return arg % 2 == 0;
        }
    }
}
