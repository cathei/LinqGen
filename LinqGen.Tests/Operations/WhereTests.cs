// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class WhereTests : GenerationTestBase<int>
{
    public override IEnumerable<int> Build(int count)
    {
        return GenEnumerable.Range(0, count * 2)
            .Where(x => x % 2 == 0)
            .AsEnumerable();
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    [TestCase(-20, 10)]
    public void TestResult_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Where(x => x < 0);

        var actual = GenEnumerable.Range(start, count)
            .Where(x => x < 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [TestCase(0, 0)]
    [TestCase(0, 10)]
    [TestCase(-5, 10)]
    [TestCase(-20, 10)]
    public void TestResultStruct_SameAsLinq(int start, int count)
    {
        var expected = Enumerable.Range(start, count)
            .Where(x => x % 3 == 0);

        var actual = GenEnumerable.Range(start, count)
            .Where(new Predicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    private struct Predicate : IStructFunction<int, bool>
    {
        public bool Invoke(int arg)
        {
            return arg % 3 == 0;
        }
    }
}
