// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022


using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class Select_Tests
{

    [Test]
    public void SameAsLinq_IntArrayEmpty()
    {
        var expected = TestData.IntArrayEmpty
            .Select(x => x * 2);

        var actual = TestData.IntArrayEmpty
            .Specialize()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArrayEmpty()
    {
        var enumerable = TestData.IntArrayEmpty
            .Specialize()
            .Select(x => x * 2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Select(x => x * 2);

        var actual = TestData.IntArray
            .Specialize()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
            .Select(x => x * 2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntListEmpty()
    {
        var expected = TestData.IntListEmpty
            .Select(x => x * 2);

        var actual = TestData.IntListEmpty
            .Specialize()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntListEmpty()
    {
        var enumerable = TestData.IntListEmpty
            .Specialize()
            .Select(x => x * 2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Select(x => x * 2);

        var actual = TestData.IntList
            .Specialize()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
            .Select(x => x * 2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }
}
[TestFixture]
public class SelectStruct_Tests
{

    [Test]
    public void SameAsLinq_IntArrayEmpty()
    {
        var expected = TestData.IntArrayEmpty
            .Select(x => x * 2);

        var actual = TestData.IntArrayEmpty
            .Specialize()
            .Select(new DoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArrayEmpty()
    {
        var enumerable = TestData.IntArrayEmpty
            .Specialize()
            .Select(new DoubleSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Select(x => x * 2);

        var actual = TestData.IntArray
            .Specialize()
            .Select(new DoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
            .Select(new DoubleSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntListEmpty()
    {
        var expected = TestData.IntListEmpty
            .Select(x => x * 2);

        var actual = TestData.IntListEmpty
            .Specialize()
            .Select(new DoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntListEmpty()
    {
        var enumerable = TestData.IntListEmpty
            .Specialize()
            .Select(new DoubleSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Select(x => x * 2);

        var actual = TestData.IntList
            .Specialize()
            .Select(new DoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
            .Select(new DoubleSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }
}
[TestFixture]
public class Where_Tests
{

    [Test]
    public void SameAsLinq_IntArrayEmpty()
    {
        var expected = TestData.IntArrayEmpty
            .Where(x => x % 2 == 0);

        var actual = TestData.IntArrayEmpty
            .Specialize()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArrayEmpty()
    {
        var enumerable = TestData.IntArrayEmpty
            .Specialize()
            .Where(x => x % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Where(x => x % 2 == 0);

        var actual = TestData.IntArray
            .Specialize()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
            .Where(x => x % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntListEmpty()
    {
        var expected = TestData.IntListEmpty
            .Where(x => x % 2 == 0);

        var actual = TestData.IntListEmpty
            .Specialize()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntListEmpty()
    {
        var enumerable = TestData.IntListEmpty
            .Specialize()
            .Where(x => x % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Where(x => x % 2 == 0);

        var actual = TestData.IntList
            .Specialize()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
            .Where(x => x % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }
}
[TestFixture]
public class WhereStruct_Tests
{

    [Test]
    public void SameAsLinq_IntArrayEmpty()
    {
        var expected = TestData.IntArrayEmpty
            .Where(x => x % 2 == 0);

        var actual = TestData.IntArrayEmpty
            .Specialize()
            .Where(new EvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArrayEmpty()
    {
        var enumerable = TestData.IntArrayEmpty
            .Specialize()
            .Where(new EvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Where(x => x % 2 == 0);

        var actual = TestData.IntArray
            .Specialize()
            .Where(new EvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
            .Where(new EvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntListEmpty()
    {
        var expected = TestData.IntListEmpty
            .Where(x => x % 2 == 0);

        var actual = TestData.IntListEmpty
            .Specialize()
            .Where(new EvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntListEmpty()
    {
        var enumerable = TestData.IntListEmpty
            .Specialize()
            .Where(new EvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Where(x => x % 2 == 0);

        var actual = TestData.IntList
            .Specialize()
            .Where(new EvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
            .Where(new EvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }
}
