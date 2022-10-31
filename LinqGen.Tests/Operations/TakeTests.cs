// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class TakeTests : GenerationTestBase<int>
{
    public override IEnumerable<int> Build(int count)
    {
        return GenEnumerable.Range(-2, count + 5)
            .Take(count)
            .AsEnumerable();
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResult_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Take(5);

        var actual = GenEnumerable.Range(start, count)
            .Take(5);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
