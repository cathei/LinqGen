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
public class DistinctStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        var expected = TestData.IntEmpty
            .Distinct();

        var actual = TestData.IntEmpty
            .Specialize()
            .Distinct(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEmpty()
    {
        var enumerable = TestData.IntEmpty
            .Specialize()
            .Distinct(new StructComparer());

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
            .Distinct(new StructComparer())
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
            .Distinct(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntArray()
    {
        var enumerable = TestData.IntArray
            .Specialize()
            .Distinct(new StructComparer());

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
            .Distinct(new StructComparer())
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
            .Distinct(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntList()
    {
        var enumerable = TestData.IntList
            .Specialize()
            .Distinct(new StructComparer());

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
            .Distinct(new StructComparer())
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
            .Distinct(new StructComparer());

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }

    [Test]
    public void MultipleEnumeration_MustBeSame_IntEnumerable()
    {
        var enumerable = TestData.IntEnumerable
            .Specialize()
            .Distinct(new StructComparer());

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
            .Distinct(new StructComparer())
            .Skip(skip).Take(take);

        CollectionAssert.AreEqual(expected, actual.AsEnumerable());
    }
}
