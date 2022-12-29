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
            .Specialize()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Select(x => x * 2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Select(new DoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Select(new DoubleSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .Select(new DoubleSelector())
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
            .Specialize()
            .Select((x, i) => x + i);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Select((x, i) => x + i);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Select((x, i) => x + i);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Select((x, i) => x + i);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Select(new AddSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Select(new AddSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Select(new AddSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Select(new AddSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .Select(new AddSelector())
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
            .Specialize()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Where(x => x % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Where(new EvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Where(new EvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .Where(new EvenPredicate())
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
            .Specialize()
            .Where((x, i) => (x - i) % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Where((x, i) => (x - i) % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Where((x, i) => (x - i) % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Where((x, i) => (x - i) % 2 == 0);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Where(new MinusEvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Where(new MinusEvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Where(new MinusEvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Where(new MinusEvenPredicate());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .Where(new MinusEvenPredicate())
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
            .Specialize()
            .Distinct();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Distinct();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Distinct();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Distinct();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Distinct(EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Distinct(EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Distinct(EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Distinct(EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .Distinct(EqualityComparer<int>.Default)
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
            .Specialize()
            .Distinct(new StructEqualityComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Distinct(new StructEqualityComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Distinct(new StructEqualityComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Distinct(new StructEqualityComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .Distinct(new StructEqualityComparer())
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
            .Specialize()
            .Skip(2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Skip(2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Skip(2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Skip(2);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Take(7);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Take(7);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Take(7);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Take(7);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .Order(Comparer<int>.Default)
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
            .Specialize()
            .Order(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Order(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .Order(new StructComparer())
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
            .Specialize()
            .OrderDescending();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderDescending();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderDescending();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderDescending();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderDescending(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderDescending(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderDescending(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderDescending(Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderDescending(Comparer<int>.Default)
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
            .Specialize()
            .OrderDescending(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderDescending(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderDescending(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderDescending(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderDescending(new StructComparer())
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
            .Specialize()
            .OrderBy(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderBy(new Mod3Selector())
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
            .Specialize()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3, Comparer<int>.Default)
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
            .Specialize()
            .OrderBy(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderBy(new Mod3Selector(), new StructComparer())
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
            .Specialize()
            .OrderByDescending(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(x => x % 3);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(new Mod3Selector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderByDescending(new Mod3Selector())
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
            .Specialize()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(x => x % 3, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderByDescending(x => x % 3, Comparer<int>.Default)
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
            .Specialize()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderByDescending(new Mod3Selector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderByDescending(new Mod3Selector(), new StructComparer())
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector())
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(x => -x, Comparer<int>.Default)
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenBy(new NegateSelector(), new StructComparer())
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(x => -x);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector())
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(x => -x, Comparer<int>.Default)
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .OrderBy(x => x % 3).ThenByDescending(new NegateSelector(), new StructComparer())
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
            .Specialize()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, EqualityComparer<int>.Default).Select(x => x.Sum())
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
            .Specialize()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, EqualityComparer<int>.Default).Select(x => x.Sum())
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
            .Specialize()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, (k, v) => v.Sum(), EqualityComparer<int>.Default)
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
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
            .Specialize()
            .GroupBy(x => x % 3, x => x, (k, v) => v.Sum(), EqualityComparer<int>.Default)
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
            .Specialize()
            .Cast<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectEmpty()
    {
        var enumerable = TestData.ObjectEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Cast<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringArray()
    {
        var enumerable = TestData.ObjectStringArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Cast<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringList()
    {
        var enumerable = TestData.ObjectStringList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .Cast<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringEnumerable()
    {
        var enumerable = TestData.ObjectStringEnumerable
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OfType<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectEmpty()
    {
        var enumerable = TestData.ObjectEmpty
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OfType<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringArray()
    {
        var enumerable = TestData.ObjectStringArray
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OfType<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringList()
    {
        var enumerable = TestData.ObjectStringList
            .Specialize()
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
            .Specialize()
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
            .Specialize()
            .OfType<string>();

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_ObjectStringEnumerable()
    {
        var enumerable = TestData.ObjectStringEnumerable
            .Specialize()
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
            .Specialize()
            .OfType<string>()
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
            .Concat(TestData.IntArray);

        var actual = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize());

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
            .Concat(TestData.IntArray)
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray);

        var actual = TestData.IntArray
            .Specialize()
            .Concat(TestData.IntArray.Specialize());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
            .Concat(TestData.IntArray.Specialize());

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
            .Specialize()
            .Concat(TestData.IntArray.Specialize())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Concat(TestData.IntArray);

        var actual = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize());

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
            .Concat(TestData.IntArray)
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntArray);

        var actual = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize());

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
            .Concat(TestData.IntArray)
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize())
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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0));

        var actual = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0));

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray.Where(x => x % 2 == 0));

        var actual = TestData.IntArray
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0));

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
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Concat(TestData.IntArray.Where(x => x % 2 == 0));

        var actual = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0));

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntArray.Where(x => x % 2 == 0));

        var actual = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0));

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0))
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0))
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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.OrderBy(x => x));

        var actual = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order());

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.OrderBy(x => x))
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray.Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.OrderBy(x => x));

        var actual = TestData.IntArray
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order());

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.OrderBy(x => x))
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Concat(TestData.IntArray.Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.OrderBy(x => x));

        var actual = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order());

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.OrderBy(x => x))
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntArray.Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.OrderBy(x => x));

        var actual = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order());

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.OrderBy(x => x))
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0)).Concat(TestData.IntEnumerable.Specialize().Order())
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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.OrderBy(x => x)));

        var actual = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()));

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.OrderBy(x => x)))
            .Skip(skip).Take(take);

        var actual = TestData.IntEmpty
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        var expected = TestData.IntArray
            .Concat(TestData.IntArray.Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.OrderBy(x => x)));

        var actual = TestData.IntArray
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()));

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.OrderBy(x => x)))
            .Skip(skip).Take(take);

        var actual = TestData.IntArray
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        var expected = TestData.IntList
            .Concat(TestData.IntArray.Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.OrderBy(x => x)));

        var actual = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()));

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.OrderBy(x => x)))
            .Skip(skip).Take(take);

        var actual = TestData.IntList
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        var expected = TestData.IntEnumerable
            .Concat(TestData.IntArray.Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.OrderBy(x => x)));

        var actual = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()));

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()));

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
            .Concat(TestData.IntArray.Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.OrderBy(x => x)))
            .Skip(skip).Take(take);

        var actual = TestData.IntEnumerable
            .Specialize()
            .Concat(TestData.IntArray.Specialize().Where(x => x % 2 == 0).Concat(TestData.IntEnumerable.Specialize().Order()))
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
