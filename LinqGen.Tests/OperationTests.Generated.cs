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
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Select(x => x * 2);

        var actual = TestData.IntEmpty
            .Gen()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Select(x => x * 2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Select(x => x * 2);

        var actual = TestData.IntArray
            .Gen()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Select(x => x * 2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Select(x => x * 2);

        var actual = TestData.IntList
            .Gen()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Select(x => x * 2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Select(x => x * 2);

        var actual = TestData.IntEnumerable
            .Gen()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Select(x => x * 2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Select(x => x * 2);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Select(x => x * 2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class SelectStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Select(x => x * 2);

        var actual = TestData.IntEmpty
            .Gen()
            .Select(new DoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Select(new DoubleSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Select(new DoubleSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Select(x => x * 2);

        var actual = TestData.IntArray
            .Gen()
            .Select(new DoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Select(new DoubleSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Select(new DoubleSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Select(x => x * 2);

        var actual = TestData.IntList
            .Gen()
            .Select(new DoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Select(new DoubleSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Select(new DoubleSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Select(x => x * 2);

        var actual = TestData.IntEnumerable
            .Gen()
            .Select(new DoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Select(new DoubleSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Select(new DoubleSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Select(x => x * 2);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Select(new RefIntDoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Select(new RefIntDoubleSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Select(x => x * 2)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Select(new RefIntDoubleSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class SelectAt_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Select((x, i) => x + i);

        var actual = TestData.IntEmpty
            .Gen()
            .Select((x, i) => x + i);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Select((x, i) => x + i);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Select((x, i) => x + i);

        var actual = TestData.IntArray
            .Gen()
            .Select((x, i) => x + i);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Select((x, i) => x + i);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Select((x, i) => x + i);

        var actual = TestData.IntList
            .Gen()
            .Select((x, i) => x + i);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Select((x, i) => x + i);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Select((x, i) => x + i);

        var actual = TestData.IntEnumerable
            .Gen()
            .Select((x, i) => x + i);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Select((x, i) => x + i);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Select((x, i) => x + i);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Select((x, i) => x + i);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Select((x, i) => x + i);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class SelectAtStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Select((x, i) => x + i);

        var actual = TestData.IntEmpty
            .Gen()
            .Select(new AddSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Select(new AddSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Select(new AddSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Select((x, i) => x + i);

        var actual = TestData.IntArray
            .Gen()
            .Select(new AddSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Select(new AddSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Select(new AddSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Select((x, i) => x + i);

        var actual = TestData.IntList
            .Gen()
            .Select(new AddSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Select(new AddSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Select(new AddSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Select((x, i) => x + i);

        var actual = TestData.IntEnumerable
            .Gen()
            .Select(new AddSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Select(new AddSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Select(new AddSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Select((x, i) => x + i);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Select(new RefIntAddSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Select(new RefIntAddSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Select((x, i) => x + i)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Select(new RefIntAddSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class Where_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Where(x => x % 2 == 0);

        var actual = TestData.IntEmpty
            .Gen()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Where(x => x % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Where(x => x % 2 == 0);

        var actual = TestData.IntArray
            .Gen()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Where(x => x % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Where(x => x % 2 == 0);

        var actual = TestData.IntList
            .Gen()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Where(x => x % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Where(x => x % 2 == 0);

        var actual = TestData.IntEnumerable
            .Gen()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Where(x => x % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Where(x => x % 2 == 0);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Where(x => x % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class WhereStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Where(x => x % 2 == 0);

        var actual = TestData.IntEmpty
            .Gen()
            .Where(new EvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Where(new EvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Where(new EvenPredicate())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Where(x => x % 2 == 0);

        var actual = TestData.IntArray
            .Gen()
            .Where(new EvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Where(new EvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Where(new EvenPredicate())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Where(x => x % 2 == 0);

        var actual = TestData.IntList
            .Gen()
            .Where(new EvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Where(new EvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Where(new EvenPredicate())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Where(x => x % 2 == 0);

        var actual = TestData.IntEnumerable
            .Gen()
            .Where(new EvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Where(new EvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Where(new EvenPredicate())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Where(x => x % 2 == 0);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Where(new RefIntEvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Where(new RefIntEvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Where(x => x % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Where(new RefIntEvenPredicate())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class WhereAt_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Where((x, i) => (x - i) % 2 == 0);

        var actual = TestData.IntEmpty
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Where((x, i) => (x - i) % 2 == 0);

        var actual = TestData.IntArray
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Where((x, i) => (x - i) % 2 == 0);

        var actual = TestData.IntList
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Where((x, i) => (x - i) % 2 == 0);

        var actual = TestData.IntEnumerable
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Where((x, i) => (x - i) % 2 == 0);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class WhereAtStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Where((x, i) => (x - i) % 2 == 0);

        var actual = TestData.IntEmpty
            .Gen()
            .Where(new MinusEvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Where(new MinusEvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Where(new MinusEvenPredicate())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Where((x, i) => (x - i) % 2 == 0);

        var actual = TestData.IntArray
            .Gen()
            .Where(new MinusEvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Where(new MinusEvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Where(new MinusEvenPredicate())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Where((x, i) => (x - i) % 2 == 0);

        var actual = TestData.IntList
            .Gen()
            .Where(new MinusEvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Where(new MinusEvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Where(new MinusEvenPredicate())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Where((x, i) => (x - i) % 2 == 0);

        var actual = TestData.IntEnumerable
            .Gen()
            .Where(new MinusEvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Where(new MinusEvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Where(new MinusEvenPredicate())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Where((x, i) => (x - i) % 2 == 0);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Where(new RefIntMinusEvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Where(new RefIntMinusEvenPredicate());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Where((x, i) => (x - i) % 2 == 0)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Where(new RefIntMinusEvenPredicate())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class Distinct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Distinct();

        var actual = TestData.IntEmpty
            .Gen()
            .Distinct();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Distinct();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Distinct()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Distinct();

        var actual = TestData.IntArray
            .Gen()
            .Distinct();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Distinct();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Distinct()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Distinct();

        var actual = TestData.IntList
            .Gen()
            .Distinct();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Distinct();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Distinct()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Distinct();

        var actual = TestData.IntEnumerable
            .Gen()
            .Distinct();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Distinct();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Distinct()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Distinct();

        var actual = TestData.ReferenceIntList
            .Gen()
            .Distinct();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Distinct();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Distinct()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class DistinctInterface_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Distinct();

        var actual = TestData.IntEmpty
            .Gen()
            .Distinct(EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Distinct(EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Distinct(EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Distinct();

        var actual = TestData.IntArray
            .Gen()
            .Distinct(EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Distinct(EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Distinct(EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Distinct();

        var actual = TestData.IntList
            .Gen()
            .Distinct(EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Distinct(EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Distinct(EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Distinct();

        var actual = TestData.IntEnumerable
            .Gen()
            .Distinct(EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Distinct(EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Distinct(EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Distinct();

        var actual = TestData.ReferenceIntList
            .Gen()
            .Distinct(EqualityComparer<ReferenceInt>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Distinct(EqualityComparer<ReferenceInt>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Distinct(EqualityComparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class DistinctStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Distinct();

        var actual = TestData.IntEmpty
            .Gen()
            .Distinct(new StructEqualityComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Distinct(new StructEqualityComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Distinct(new StructEqualityComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Distinct();

        var actual = TestData.IntArray
            .Gen()
            .Distinct(new StructEqualityComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Distinct(new StructEqualityComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Distinct(new StructEqualityComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Distinct();

        var actual = TestData.IntList
            .Gen()
            .Distinct(new StructEqualityComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Distinct(new StructEqualityComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Distinct(new StructEqualityComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Distinct();

        var actual = TestData.IntEnumerable
            .Gen()
            .Distinct(new StructEqualityComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Distinct(new StructEqualityComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Distinct(new StructEqualityComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Distinct();

        var actual = TestData.ReferenceIntList
            .Gen()
            .Distinct(new RefIntStructEqualityComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Distinct(new RefIntStructEqualityComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Distinct()
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Distinct(new RefIntStructEqualityComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class Skip_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Skip(2);

        var actual = TestData.IntEmpty
            .Gen()
            .Skip(2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Skip(2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Skip(2)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Skip(2)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Skip(2);

        var actual = TestData.IntArray
            .Gen()
            .Skip(2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Skip(2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Skip(2)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Skip(2)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Skip(2);

        var actual = TestData.IntList
            .Gen()
            .Skip(2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Skip(2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Skip(2)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Skip(2)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Skip(2);

        var actual = TestData.IntEnumerable
            .Gen()
            .Skip(2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Skip(2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Skip(2)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Skip(2)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Skip(2);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Skip(2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Skip(2);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Skip(2)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Skip(2)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class Take_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Take(7);

        var actual = TestData.IntEmpty
            .Gen()
            .Take(7);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Take(7);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Take(7)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Take(7)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Take(7);

        var actual = TestData.IntArray
            .Gen()
            .Take(7);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Take(7);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Take(7)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Take(7)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Take(7);

        var actual = TestData.IntList
            .Gen()
            .Take(7);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Take(7);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Take(7)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Take(7)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Take(7);

        var actual = TestData.IntEnumerable
            .Gen()
            .Take(7);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Take(7);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Take(7)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Take(7)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Take(7);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Take(7);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Take(7);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Take(7)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Take(7)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class Order_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x);

        var actual = TestData.IntEmpty
            .Gen()
            .Order();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Order();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Order()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x);

        var actual = TestData.IntArray
            .Gen()
            .Order();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Order();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Order()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x);

        var actual = TestData.IntList
            .Gen()
            .Order();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Order();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Order()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x);

        var actual = TestData.IntEnumerable
            .Gen()
            .Order();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Order();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Order()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Order();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Order();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Order()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderComparer_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x);

        var actual = TestData.IntEmpty
            .Gen()
            .Order(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Order(Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Order(Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x);

        var actual = TestData.IntArray
            .Gen()
            .Order(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Order(Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Order(Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x);

        var actual = TestData.IntList
            .Gen()
            .Order(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Order(Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Order(Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x);

        var actual = TestData.IntEnumerable
            .Gen()
            .Order(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Order(Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Order(Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Order(Comparer<ReferenceInt>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Order(Comparer<ReferenceInt>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Order(Comparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x);

        var actual = TestData.IntEmpty
            .Gen()
            .Order(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Order(new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Order(new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x);

        var actual = TestData.IntArray
            .Gen()
            .Order(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Order(new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Order(new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x);

        var actual = TestData.IntList
            .Gen()
            .Order(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Order(new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Order(new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x);

        var actual = TestData.IntEnumerable
            .Gen()
            .Order(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Order(new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Order(new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Order(new RefIntStructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Order(new RefIntStructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Order(new RefIntStructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderDesc_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderDescending();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderDescending();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderDescending()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x);

        var actual = TestData.IntArray
            .Gen()
            .OrderDescending();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderDescending();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderDescending()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x);

        var actual = TestData.IntList
            .Gen()
            .OrderDescending();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderDescending();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderDescending()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderDescending();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderDescending();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderDescending()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderDescending();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderDescending();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderDescending()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderDescComparer_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderDescending(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderDescending(Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderDescending(Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x);

        var actual = TestData.IntArray
            .Gen()
            .OrderDescending(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderDescending(Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderDescending(Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x);

        var actual = TestData.IntList
            .Gen()
            .OrderDescending(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderDescending(Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderDescending(Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderDescending(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderDescending(Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderDescending(Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderDescending(Comparer<ReferenceInt>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderDescending(Comparer<ReferenceInt>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderDescending(Comparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderDescStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderDescending(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderDescending(new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderDescending(new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x);

        var actual = TestData.IntArray
            .Gen()
            .OrderDescending(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderDescending(new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderDescending(new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x);

        var actual = TestData.IntList
            .Gen()
            .OrderDescending(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderDescending(new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderDescending(new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderDescending(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderDescending(new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderDescending(new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderDescending(new RefIntStructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderDescending(new RefIntStructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderDescending(new RefIntStructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderBy_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderByKey_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(new Mod3Selector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(new Mod3Selector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(new Mod3Selector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(new Mod3Selector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(new Mod3Selector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(new Mod3Selector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(new Mod3Selector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(new Mod3Selector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(new RefIntMod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(new RefIntMod3Selector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(new RefIntMod3Selector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderByComparer_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3, Comparer<ReferenceInt>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3, Comparer<ReferenceInt>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3, Comparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderByStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(new Mod3Selector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(new RefIntMod3Selector(), new RefIntStructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(new RefIntMod3Selector(), new RefIntStructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(new RefIntMod3Selector(), new RefIntStructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderByDesc_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderByDescending(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderByDescending(x => x % 3);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntArray
            .Gen()
            .OrderByDescending(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderByDescending(x => x % 3);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntList
            .Gen()
            .OrderByDescending(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderByDescending(x => x % 3);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(x => x % 3);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x % 3);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(x => x % 3);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderByDescKey_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderByDescending(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderByDescending(new Mod3Selector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderByDescending(new Mod3Selector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntArray
            .Gen()
            .OrderByDescending(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderByDescending(new Mod3Selector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderByDescending(new Mod3Selector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntList
            .Gen()
            .OrderByDescending(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderByDescending(new Mod3Selector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderByDescending(new Mod3Selector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(new Mod3Selector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(new Mod3Selector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x % 3);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(new RefIntMod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(new RefIntMod3Selector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(new RefIntMod3Selector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderByDescComparer_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntArray
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntList
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x % 3);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<ReferenceInt>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<ReferenceInt>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(x => x % 3, Comparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OrderByDescStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntArray
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntList
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x % 3);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderByDescending(new Mod3Selector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x % 3);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(new RefIntMod3Selector(), new RefIntStructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(new RefIntMod3Selector(), new RefIntStructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderByDescending(x => x % 3)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderByDescending(new RefIntMod3Selector(), new RefIntStructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ThenBy_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ThenByKey_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new RefIntNegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new RefIntNegateSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new RefIntNegateSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ThenByComparer_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<ReferenceInt>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<ReferenceInt>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ThenByStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenBy(x => -x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new RefIntNegateSelector(), new RefIntStructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new RefIntNegateSelector(), new RefIntStructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenBy(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenBy(new RefIntNegateSelector(), new RefIntStructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ThenByDesc_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ThenByDescKey_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new RefIntNegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new RefIntNegateSelector());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new RefIntNegateSelector())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ThenByDescComparer_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<ReferenceInt>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<ReferenceInt>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ThenByDescStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new RefIntNegateSelector(), new RefIntStructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new RefIntNegateSelector(), new RefIntStructComparer());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .OrderBy(x => x % 3).ThenByDescending(x => -x)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .OrderBy(x => x % 3).ThenByDescending(new RefIntNegateSelector(), new RefIntStructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class GroupBy_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3).Select(x => x.Sum());

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3).Select(x => x.Sum());

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3).Select(x => x.Sum());

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3).Select(x => x.Sum());

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3).Select(x => x.Sum());

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class GroupByComparer_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, EqualityComparer<ReferenceInt>.Default).Select(x => x.Sum());

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<ReferenceInt>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<ReferenceInt>.Default).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, EqualityComparer<ReferenceInt>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, EqualityComparer<ReferenceInt>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class GroupByElement_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class GroupByElementComparer_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, x => x, EqualityComparer<ReferenceInt>.Default).Select(x => x.Sum());

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<ReferenceInt>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<ReferenceInt>.Default).Select(x => x.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, x => x, EqualityComparer<ReferenceInt>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x, EqualityComparer<ReferenceInt>.Default).Select(x => x.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class GroupByResult_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class GroupByResultComparer_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<ReferenceInt>.Default);

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<ReferenceInt>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<ReferenceInt>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class GroupByElementResult_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class GroupByElementResultComparer_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<ReferenceInt>.Default);

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<ReferenceInt>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<ReferenceInt>.Default);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<ReferenceInt>.Default)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class Cast_Tests
{

    [Test]
    public void SameAsLinq_ObjectEmpty()
    {
        var expected = TestData.ObjectEmpty
            .Cast<string>();

        var actual = TestData.ObjectEmpty
            .Gen()
            .Cast<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectEmpty()
    {
        var enumerable = TestData.ObjectEmpty
            .Gen()
            .Cast<string>();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectEmpty(int skip, int take)
    {
        var expected = TestData.ObjectEmpty
            .Cast<string>()
            .Skip(skip).Take(take);

        var actual = TestData.ObjectEmpty
            .Gen()
            .Cast<string>()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringArray()
    {
        var expected = TestData.ObjectStringArray
            .Cast<string>();

        var actual = TestData.ObjectStringArray
            .Gen()
            .Cast<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringArray()
    {
        var enumerable = TestData.ObjectStringArray
            .Gen()
            .Cast<string>();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringArray(int skip, int take)
    {
        var expected = TestData.ObjectStringArray
            .Cast<string>()
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringArray
            .Gen()
            .Cast<string>()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringList()
    {
        var expected = TestData.ObjectStringList
            .Cast<string>();

        var actual = TestData.ObjectStringList
            .Gen()
            .Cast<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringList()
    {
        var enumerable = TestData.ObjectStringList
            .Gen()
            .Cast<string>();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringList(int skip, int take)
    {
        var expected = TestData.ObjectStringList
            .Cast<string>()
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringList
            .Gen()
            .Cast<string>()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringEnumerable()
    {
        var expected = TestData.ObjectStringEnumerable
            .Cast<string>();

        var actual = TestData.ObjectStringEnumerable
            .Gen()
            .Cast<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringEnumerable()
    {
        var enumerable = TestData.ObjectStringEnumerable
            .Gen()
            .Cast<string>();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringEnumerable(int skip, int take)
    {
        var expected = TestData.ObjectStringEnumerable
            .Cast<string>()
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringEnumerable
            .Gen()
            .Cast<string>()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OfType_Tests
{

    [Test]
    public void SameAsLinq_ObjectEmpty()
    {
        var expected = TestData.ObjectEmpty
            .OfType<string>();

        var actual = TestData.ObjectEmpty
            .Gen()
            .OfType<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectEmpty()
    {
        var enumerable = TestData.ObjectEmpty
            .Gen()
            .OfType<string>();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectEmpty(int skip, int take)
    {
        var expected = TestData.ObjectEmpty
            .OfType<string>()
            .Skip(skip).Take(take);

        var actual = TestData.ObjectEmpty
            .Gen()
            .OfType<string>()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringArray()
    {
        var expected = TestData.ObjectStringArray
            .OfType<string>();

        var actual = TestData.ObjectStringArray
            .Gen()
            .OfType<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringArray()
    {
        var enumerable = TestData.ObjectStringArray
            .Gen()
            .OfType<string>();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringArray(int skip, int take)
    {
        var expected = TestData.ObjectStringArray
            .OfType<string>()
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringArray
            .Gen()
            .OfType<string>()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringList()
    {
        var expected = TestData.ObjectStringList
            .OfType<string>();

        var actual = TestData.ObjectStringList
            .Gen()
            .OfType<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringList()
    {
        var enumerable = TestData.ObjectStringList
            .Gen()
            .OfType<string>();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringList(int skip, int take)
    {
        var expected = TestData.ObjectStringList
            .OfType<string>()
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringList
            .Gen()
            .OfType<string>()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringEnumerable()
    {
        var expected = TestData.ObjectStringEnumerable
            .OfType<string>();

        var actual = TestData.ObjectStringEnumerable
            .Gen()
            .OfType<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringEnumerable()
    {
        var enumerable = TestData.ObjectStringEnumerable
            .Gen()
            .OfType<string>();

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringEnumerable(int skip, int take)
    {
        var expected = TestData.ObjectStringEnumerable
            .OfType<string>()
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringEnumerable
            .Gen()
            .OfType<string>()
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OfTypeSelect_Tests
{

    [Test]
    public void SameAsLinq_ObjectEmpty()
    {
        var expected = TestData.ObjectEmpty
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        var actual = TestData.ObjectEmpty
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectEmpty()
    {
        var enumerable = TestData.ObjectEmpty
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectEmpty(int skip, int take)
    {
        var expected = TestData.ObjectEmpty
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0])
            .Skip(skip).Take(take);

        var actual = TestData.ObjectEmpty
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0])
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringArray()
    {
        var expected = TestData.ObjectStringArray
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        var actual = TestData.ObjectStringArray
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringArray()
    {
        var enumerable = TestData.ObjectStringArray
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringArray(int skip, int take)
    {
        var expected = TestData.ObjectStringArray
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0])
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringArray
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0])
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringList()
    {
        var expected = TestData.ObjectStringList
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        var actual = TestData.ObjectStringList
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringList()
    {
        var enumerable = TestData.ObjectStringList
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringList(int skip, int take)
    {
        var expected = TestData.ObjectStringList
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0])
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringList
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0])
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringEnumerable()
    {
        var expected = TestData.ObjectStringEnumerable
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        var actual = TestData.ObjectStringEnumerable
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringEnumerable()
    {
        var enumerable = TestData.ObjectStringEnumerable
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0]);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringEnumerable(int skip, int take)
    {
        var expected = TestData.ObjectStringEnumerable
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0])
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringEnumerable
            .Gen()
            .OfType<string>().Select(g => string.IsNullOrEmpty(g) ? ' ' : g[0])
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class OfTypeWhere_Tests
{

    [Test]
    public void SameAsLinq_ObjectEmpty()
    {
        var expected = TestData.ObjectEmpty
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        var actual = TestData.ObjectEmpty
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectEmpty()
    {
        var enumerable = TestData.ObjectEmpty
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectEmpty(int skip, int take)
    {
        var expected = TestData.ObjectEmpty
            .OfType<string>().Where(g => string.IsNullOrEmpty(g))
            .Skip(skip).Take(take);

        var actual = TestData.ObjectEmpty
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringArray()
    {
        var expected = TestData.ObjectStringArray
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        var actual = TestData.ObjectStringArray
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringArray()
    {
        var enumerable = TestData.ObjectStringArray
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringArray(int skip, int take)
    {
        var expected = TestData.ObjectStringArray
            .OfType<string>().Where(g => string.IsNullOrEmpty(g))
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringArray
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringList()
    {
        var expected = TestData.ObjectStringList
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        var actual = TestData.ObjectStringList
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringList()
    {
        var enumerable = TestData.ObjectStringList
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringList(int skip, int take)
    {
        var expected = TestData.ObjectStringList
            .OfType<string>().Where(g => string.IsNullOrEmpty(g))
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringList
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ObjectStringEnumerable()
    {
        var expected = TestData.ObjectStringEnumerable
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        var actual = TestData.ObjectStringEnumerable
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringEnumerable()
    {
        var enumerable = TestData.ObjectStringEnumerable
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ObjectStringEnumerable(int skip, int take)
    {
        var expected = TestData.ObjectStringEnumerable
            .OfType<string>().Where(g => string.IsNullOrEmpty(g))
            .Skip(skip).Take(take);

        var actual = TestData.ObjectStringEnumerable
            .Gen()
            .OfType<string>().Where(g => string.IsNullOrEmpty(g))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class Concat_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Concat(TestData.IntEmpty);

        var actual = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Concat(TestData.IntEmpty)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray);

        var actual = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Concat(TestData.IntList);

        var actual = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Concat(TestData.IntList)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntEnumerable);

        var actual = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntEnumerable)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Concat(TestData.ReferenceIntList);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Concat(TestData.ReferenceIntList)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ConcatComplex_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Concat(TestData.IntEmpty.Where(x => x % 2 == 0));

        var actual = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen().Where(x => x % 2 == 0));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen().Where(x => x % 2 == 0));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Concat(TestData.IntEmpty.Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen().Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray.Where(x => x % 2 == 0));

        var actual = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen().Where(x => x % 2 == 0));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen().Where(x => x % 2 == 0));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray.Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen().Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Concat(TestData.IntList.Where(x => x % 2 == 0));

        var actual = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen().Where(x => x % 2 == 0));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen().Where(x => x % 2 == 0));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Concat(TestData.IntList.Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen().Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntEnumerable.Where(x => x % 2 == 0));

        var actual = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen().Where(x => x % 2 == 0));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen().Where(x => x % 2 == 0));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntEnumerable.Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen().Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Concat(TestData.ReferenceIntList.Where(x => x % 2 == 0));

        var actual = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen().Where(x => x % 2 == 0));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen().Where(x => x % 2 == 0));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Concat(TestData.ReferenceIntList.Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen().Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ConcatTwoOne_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Concat(TestData.IntEmpty.Where(x => x % 2 == 0)).Concat(TestData.IntEmpty.OrderBy(x => x));

        var actual = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen().Where(new EvenPredicate())).Concat(TestData.IntEmpty.Gen().Order());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen().Where(new EvenPredicate())).Concat(TestData.IntEmpty.Gen().Order());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Concat(TestData.IntEmpty.Where(x => x % 2 == 0)).Concat(TestData.IntEmpty.OrderBy(x => x))
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen().Where(new EvenPredicate())).Concat(TestData.IntEmpty.Gen().Order())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray.Where(x => x % 2 == 0)).Concat(TestData.IntArray.OrderBy(x => x));

        var actual = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen().Where(new EvenPredicate())).Concat(TestData.IntArray.Gen().Order());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen().Where(new EvenPredicate())).Concat(TestData.IntArray.Gen().Order());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray.Where(x => x % 2 == 0)).Concat(TestData.IntArray.OrderBy(x => x))
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen().Where(new EvenPredicate())).Concat(TestData.IntArray.Gen().Order())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Concat(TestData.IntList.Where(x => x % 2 == 0)).Concat(TestData.IntList.OrderBy(x => x));

        var actual = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen().Where(new EvenPredicate())).Concat(TestData.IntList.Gen().Order());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen().Where(new EvenPredicate())).Concat(TestData.IntList.Gen().Order());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Concat(TestData.IntList.Where(x => x % 2 == 0)).Concat(TestData.IntList.OrderBy(x => x))
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen().Where(new EvenPredicate())).Concat(TestData.IntList.Gen().Order())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntEnumerable.Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.OrderBy(x => x));

        var actual = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen().Where(new EvenPredicate())).Concat(TestData.IntEnumerable.Gen().Order());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen().Where(new EvenPredicate())).Concat(TestData.IntEnumerable.Gen().Order());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntEnumerable.Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.OrderBy(x => x))
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen().Where(new EvenPredicate())).Concat(TestData.IntEnumerable.Gen().Order())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Concat(TestData.ReferenceIntList.Where(x => x % 2 == 0)).Concat(TestData.ReferenceIntList.OrderBy(x => x));

        var actual = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen().Where(new RefIntEvenPredicate())).Concat(TestData.ReferenceIntList.Gen().Order());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen().Where(new RefIntEvenPredicate())).Concat(TestData.ReferenceIntList.Gen().Order());

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Concat(TestData.ReferenceIntList.Where(x => x % 2 == 0)).Concat(TestData.ReferenceIntList.OrderBy(x => x))
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen().Where(new RefIntEvenPredicate())).Concat(TestData.ReferenceIntList.Gen().Order())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class ConcatOneTwo_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Concat(TestData.IntEmpty.Where(x => x % 2 == 0).Concat(TestData.IntEmpty.OrderBy(x => x)));

        var actual = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen().Where(new EvenPredicate()).Concat(TestData.IntEmpty.Gen().Order()));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen().Where(new EvenPredicate()).Concat(TestData.IntEmpty.Gen().Order()));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Concat(TestData.IntEmpty.Where(x => x % 2 == 0).Concat(TestData.IntEmpty.OrderBy(x => x)))
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Concat(TestData.IntEmpty.Gen().Where(new EvenPredicate()).Concat(TestData.IntEmpty.Gen().Order()))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray.Where(x => x % 2 == 0).Concat(TestData.IntArray.OrderBy(x => x)));

        var actual = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen().Where(new EvenPredicate()).Concat(TestData.IntArray.Gen().Order()));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen().Where(new EvenPredicate()).Concat(TestData.IntArray.Gen().Order()));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray.Where(x => x % 2 == 0).Concat(TestData.IntArray.OrderBy(x => x)))
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Concat(TestData.IntArray.Gen().Where(new EvenPredicate()).Concat(TestData.IntArray.Gen().Order()))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Concat(TestData.IntList.Where(x => x % 2 == 0).Concat(TestData.IntList.OrderBy(x => x)));

        var actual = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen().Where(new EvenPredicate()).Concat(TestData.IntList.Gen().Order()));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen().Where(new EvenPredicate()).Concat(TestData.IntList.Gen().Order()));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Concat(TestData.IntList.Where(x => x % 2 == 0).Concat(TestData.IntList.OrderBy(x => x)))
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Concat(TestData.IntList.Gen().Where(new EvenPredicate()).Concat(TestData.IntList.Gen().Order()))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntEnumerable.Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.OrderBy(x => x)));

        var actual = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen().Where(new EvenPredicate()).Concat(TestData.IntEnumerable.Gen().Order()));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen().Where(new EvenPredicate()).Concat(TestData.IntEnumerable.Gen().Order()));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntEnumerable.Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.OrderBy(x => x)))
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Concat(TestData.IntEnumerable.Gen().Where(new EvenPredicate()).Concat(TestData.IntEnumerable.Gen().Order()))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Concat(TestData.ReferenceIntList.Where(x => x % 2 == 0).Concat(TestData.ReferenceIntList.OrderBy(x => x)));

        var actual = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen().Where(new RefIntEvenPredicate()).Concat(TestData.ReferenceIntList.Gen().Order()));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen().Where(new RefIntEvenPredicate()).Concat(TestData.ReferenceIntList.Gen().Order()));

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Concat(TestData.ReferenceIntList.Where(x => x % 2 == 0).Concat(TestData.ReferenceIntList.OrderBy(x => x)))
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Concat(TestData.ReferenceIntList.Gen().Where(new RefIntEvenPredicate()).Concat(TestData.ReferenceIntList.Gen().Order()))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class Prepend_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Prepend(77);

        var actual = TestData.IntEmpty
            .Gen()
            .Prepend(77);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Prepend(77);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Prepend(77)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Prepend(77)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Prepend(77);

        var actual = TestData.IntArray
            .Gen()
            .Prepend(77);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Prepend(77);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Prepend(77)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Prepend(77)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Prepend(77);

        var actual = TestData.IntList
            .Gen()
            .Prepend(77);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Prepend(77);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Prepend(77)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Prepend(77)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Prepend(77);

        var actual = TestData.IntEnumerable
            .Gen()
            .Prepend(77);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Prepend(77);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Prepend(77)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Prepend(77)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Prepend(77);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Prepend(77);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Prepend(77);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Prepend(77)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Prepend(77)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
[TestFixture]
public class Append_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Append(42);

        var actual = TestData.IntEmpty
            .Gen()
            .Append(42);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Gen()
            .Append(42);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        var expected = TestData.IntEmpty
            .Append(42)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Gen()
            .Append(42)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Append(42);

        var actual = TestData.IntArray
            .Gen()
            .Append(42);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Gen()
            .Append(42);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        var expected = TestData.IntArray
            .Append(42)
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Gen()
            .Append(42)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Append(42);

        var actual = TestData.IntList
            .Gen()
            .Append(42);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Gen()
            .Append(42);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        var expected = TestData.IntList
            .Append(42)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Gen()
            .Append(42)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Append(42);

        var actual = TestData.IntEnumerable
            .Gen()
            .Append(42);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Gen()
            .Append(42);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        var expected = TestData.IntEnumerable
            .Append(42)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Gen()
            .Append(42)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_ReferenceIntList()
    {
        var expected = TestData.ReferenceIntList
            .Append(42);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Append(42);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ReferenceIntList()
    {
        var enumerable = TestData.ReferenceIntList
            .Gen()
            .Append(42);

        var array1 = enumerable.ToArray();
        var array2 = enumerable.ToArray();

        CollectionAssert.AreEqual(array1, array2);
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_ReferenceIntList(int skip, int take)
    {
        var expected = TestData.ReferenceIntList
            .Append(42)
            .Skip(skip).Take(take);

        var actual = TestData.ReferenceIntList
            .Gen()
            .Append(42)
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
