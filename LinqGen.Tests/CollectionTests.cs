// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cathei.LinqGen.Tests;

public class CollectionTests
{
    [TestCase()]
    [TestCase(1, 2, 3, 4, 5, 6)]
    [TestCase(2, 4, 6)]
    [TestCase(72, 13, 28, 31, 6, 7)]
    public void Test_RemoveAll(params int[] testData)
    {
        var list1 = testData.ToList();
        var list2 = testData.ToList();

        int r1 = list1.RemoveAll(x => x % 2 == 0);
        int r2 = list2.RemoveAll(new EvenPredicate());

        CollectionAssert.AreEqual(list1, list2);
        Assert.AreEqual(r1, r2);
    }
}