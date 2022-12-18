// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class ThenByDescTests : GenerationTestBase<int>
{
    public override IEnumerable<int> Build(int count)
    {
        return Gen.Enumerable.Range(-2, count)
            .OrderBy(x => x % 3)
            .ThenByDescending(x => -x)
            .AsEnumerable();
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultIdentity_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .OrderBy(x => x % 2)
            .ThenByDescending(x => x);

        var actual = Gen.Enumerable.Range(start, count)
            .OrderBy(x => x % 2)
            .ThenByDescending();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultIdentityStruct_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .OrderBy(x => x % 2)
            .ThenByDescending(x => x, new Comparer());

        var actual = Gen.Enumerable.Range(start, count)
            .OrderBy(x => x % 2)
            .ThenByDescending(new Comparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResult_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .OrderBy(x => x % 2)
            .ThenByDescending(x => 3 - x);

        var actual = Gen.Enumerable.Range(start, count)
            .OrderBy(x => x % 2)
            .ThenByDescending(x => 3 - x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultStruct_SameAsLinq(int start, int count)
    {
        var selector1 = new Selector1();
        var selector2 = new Selector2();

        var expected = Enumerable.Range(start, count)
            .OrderBy(selector1.Invoke)
            .ThenByDescending(selector2.Invoke);

        var actual = Gen.Enumerable.Range(start, count)
            .OrderBy(selector1)
            .ThenByDescending(selector2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    private struct Selector1 : IStructFunction<int, bool>
    {
        public bool Invoke(int arg)
        {
            return arg < 0;
        }
    }

    private struct Selector2 : IStructFunction<int, double>
    {
        public double Invoke(int arg)
        {
            return 5.5 - arg;
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
