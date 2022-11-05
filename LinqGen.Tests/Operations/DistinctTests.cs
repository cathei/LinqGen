// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class DistinctTests : GenerationTestBase<int>
{
    public override IEnumerable<int> Build(int count)
    {
        return GenEnumerable.Range(-2, count)
            .Distinct()
            .AsEnumerable();
    }

    [TestCase]
    [TestCase(0, 1, 2, 0, 1, 2)]
    [TestCase(3, 3, 3)]
    [TestCase(1, 3, 5, 7)]
    public void TestResult_SameAsLinq(params int[] testData)
    {
        var expected = testData.Distinct();

        var actual = testData.Specialize().Distinct();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [TestCase]
    [TestCase(0, 1, 2, 0, 1, 2)]
    [TestCase(3, 3, 3)]
    [TestCase(1, 3, 5, 7)]
    public void TestResult_SameAsLinq_Struct(params int[] testData)
    {
        var expected = testData.Distinct();

        var actual = testData.Specialize().Distinct(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    public struct StructComparer : IEqualityComparer<int>
    {
        public bool Equals(int x, int y)
        {
            return x == y;
        }

        public int GetHashCode(int obj)
        {
            return obj;
        }
    }
}
