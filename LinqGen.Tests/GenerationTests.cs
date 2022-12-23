// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cathei.LinqGen.Tests;

public class GenerationTests
{
    [Test]
    public void Empty_MustBeEmpty()
    {
        var actual = Gen.Enumerable.Empty<object>().ToArray();

        Assert.IsEmpty(actual);
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(2, 5)]
    [TestCase(-100, 50)]
    public void Range_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count);
        var actual = Gen.Enumerable.Range(start, count);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(2, 5)]
    [TestCase(-100, 50)]
    public void Repeat_SameAsLinq(int element, int count)
    {
        var expected = Enumerable.Repeat(element, count);
        var actual = Gen.Enumerable.Repeat(element, count);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}