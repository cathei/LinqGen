// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class OfTypeTests : GenerationTestBase<string>
{
    public override IEnumerable<string> Build(int count)
    {
        return Enumerable.Repeat((object)"AAA", count)
            .Specialize()
            .OfType<string>()
            .AsEnumerable();
    }

    [Test]
    public void TestResult_SameAsLinq()
    {
        object?[] list = new object?[]
        {
            "A", "B", new object(), "DDD", "EEE", null
        };

        var expected = list.OfType<string>();
        var actual = list.Specialize().OfType<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void FailedElement_ShouldBeSkipped()
    {
        object?[] list = new object?[]
        {
            "A", "B", new object(), "DDD", "EEE", null
        };

        var enumerable = list.Specialize().OfType<string>();

        int count = 0;

        foreach (var x in enumerable)
        {
            count++;
        }

        Assert.AreEqual(list.Length - 2, count);
    }
}
