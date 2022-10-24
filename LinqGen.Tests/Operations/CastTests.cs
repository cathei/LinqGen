// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class CastTests : GenerationTestBase<string>
{
    private readonly FundamentalTests _fundamentalTests = new FundamentalTests();

    public override IEnumerable<string> Build(int count)
    {
        return Enumerable.Repeat((object)"AAA", count)
            .Gen()
            .Cast<string>()
            .AsEnumerable();
    }

    [Test]
    public void TestResult_SameAsLinq()
    {
        object?[] list = new object?[]
        {
            "A", "B", null, "C", "DDD", "EEE"
        };

        var expected = list.Cast<string>();
        var actual = list.Gen().Cast<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void CastFail_ExceptionShouldBeThrown()
    {
        object[] list = new object[]
        {
            "A", "B", new object(), "DDD", "EEE"
        };

        var enumerable = list.Gen().Cast<string>();

        Assert.Throws<InvalidCastException>(() =>
        {
            foreach (var str in enumerable)
            {
                // do nothing
            }
        });
    }
}
