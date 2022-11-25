// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class SelectTests : GenerationTestBase<int>
{
    public override IEnumerable<int> Build(int count)
    {
        return Gen.Enumerable.Range(-2, count)
            .Select(x => x * 2)
            .AsEnumerable();
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResult_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Select(x => x * 10);

        var actual = Gen.Enumerable.Range(start, count)
            .Select(x => x * 10);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResultStruct_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Select(x => x % 3 == 0);

        var actual = Gen.Enumerable.Range(start, count)
            .Select(new Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    private struct Selector : IStructFunction<int, bool>
    {
        public bool Invoke(int arg)
        {
            return arg % 3 == 0;
        }
    }
}
