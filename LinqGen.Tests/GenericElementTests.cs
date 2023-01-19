// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cathei.LinqGen.Tests;

public class GenericElementTests
{
    [Test]
    public void OfTypeWithWhereSelect()
    {
        // https://github.com/cathei/LinqGen/issues/3

        var expected = TestData.ObjectStringArray
            .OfType<string>()
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => x + x);

        var actual = TestData.ObjectStringArray.Gen()
            .OfType<string>()
            .Where(new Predicate())
            .Select(x => x + x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void GenerationWithWhereSelect()
    {
        // https://github.com/cathei/LinqGen/issues/3

        var expected = Enumerable.Repeat("LinqGenTest", 10)
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => x + x);

        var actual = Gen.Enumerable.Repeat("LinqGenTest", 10)
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => x + x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    public struct Predicate : IStructFunction<string, bool>
    {
        public bool Invoke(string arg) => !string.IsNullOrEmpty(arg);
    }
}