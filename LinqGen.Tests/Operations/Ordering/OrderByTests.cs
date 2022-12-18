// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class OrderByTests : GenerationTestBase<int>
{
    public override IEnumerable<int> Build(int count)
    {
        return Gen.Enumerable.Range(-2, count)
            .OrderBy(x => -x)
            .AsEnumerable();
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultIdentity_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .OrderBy(x => x);

        var actual = Gen.Enumerable.Range(start, count)
            .OrderBy();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultIdentityStruct_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .OrderBy(x => x, new Comparer());

        var actual = Gen.Enumerable.Range(start, count)
            .OrderBy(new Comparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResult_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .OrderBy(x => x % 2 == 0 ? -x : x);

        var actual = Gen.Enumerable.Range(start, count)
            .OrderBy(x => x % 2 == 0 ? -x : x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultStruct_SameAsLinq(int start, int count)
    {
        var selector = new Selector();

        var expected = Enumerable.Range(start, count)
            .OrderBy(selector.Invoke);

        var actual = Gen.Enumerable.Range(start, count)
            .OrderBy(selector);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    private struct Selector : IStructFunction<int, double>
    {
        public double Invoke(int arg)
        {
            if (arg % 2 == 0)
                return 10.5 - arg;
            return 10.5 + arg;
        }
    }

    private struct Comparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x - y;
        }
    }
}
