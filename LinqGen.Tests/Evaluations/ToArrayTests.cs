// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class ToArrayTests
{
    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResult_SameAsLinq(int elem, int count)
    {
        var expected = Enumerable.Repeat(elem, count)
            .ToArray();

        var actual = GenEnumerable.Repeat(elem, count)
            .ToArray();

        Assert.AreEqual(expected, actual);
    }
}
