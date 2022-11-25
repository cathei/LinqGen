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

    [Test]
    public void Test_ArrayWhereToArray()
    {
        int[] values = new []{ 1, 2, 3, 7 , 8, 24, 242,};

        var arr =values.Specialize()
            .Where(x => x % 2 == 0)
            .Select(x => x * 2)
            .ToArray();


    }




    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    public void TestResult_SameAsLinq(int elem, int count)
    {
        var expected = Enumerable.Repeat(elem, count)
            .ToArray();

        var actual = Gen.Enumerable.Repeat(elem, count)
            .ToArray();

        Assert.AreEqual(expected, actual);
    }
}
