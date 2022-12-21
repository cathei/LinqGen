// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022


using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class Count_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Count();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Count();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Skip(skip).Take(take)
                .Count();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Skip(skip).Take(take)
                .Count();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Count();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Count();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Skip(skip).Take(take)
                .Count();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Skip(skip).Take(take)
                .Count();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Count();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Count();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Skip(skip).Take(take)
                .Count();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Skip(skip).Take(take)
                .Count();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Count();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Count();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Skip(skip).Take(take)
                .Count();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Skip(skip).Take(take)
                .Count();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }
}
[TestFixture]
public class First_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .First();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .First();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Skip(skip).Take(take)
                .First();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Skip(skip).Take(take)
                .First();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .First();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .First();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Skip(skip).Take(take)
                .First();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Skip(skip).Take(take)
                .First();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .First();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .First();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Skip(skip).Take(take)
                .First();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Skip(skip).Take(take)
                .First();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .First();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .First();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Skip(skip).Take(take)
                .First();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Skip(skip).Take(take)
                .First();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }
}
[TestFixture]
public class FirstOrDefault_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .FirstOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .FirstOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Skip(skip).Take(take)
                .FirstOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Skip(skip).Take(take)
                .FirstOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .FirstOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .FirstOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Skip(skip).Take(take)
                .FirstOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Skip(skip).Take(take)
                .FirstOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .FirstOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .FirstOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Skip(skip).Take(take)
                .FirstOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Skip(skip).Take(take)
                .FirstOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .FirstOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .FirstOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Skip(skip).Take(take)
                .FirstOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Skip(skip).Take(take)
                .FirstOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }
}
[TestFixture]
public class Last_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Last();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Last();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Skip(skip).Take(take)
                .Last();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Skip(skip).Take(take)
                .Last();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Last();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Last();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Skip(skip).Take(take)
                .Last();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Skip(skip).Take(take)
                .Last();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Last();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Last();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Skip(skip).Take(take)
                .Last();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Skip(skip).Take(take)
                .Last();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Last();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Last();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Skip(skip).Take(take)
                .Last();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Skip(skip).Take(take)
                .Last();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }
}
[TestFixture]
public class LastOrDefault_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .LastOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .LastOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Skip(skip).Take(take)
                .LastOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Skip(skip).Take(take)
                .LastOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .LastOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .LastOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Skip(skip).Take(take)
                .LastOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Skip(skip).Take(take)
                .LastOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .LastOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .LastOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Skip(skip).Take(take)
                .LastOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Skip(skip).Take(take)
                .LastOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .LastOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .LastOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Skip(skip).Take(take)
                .LastOrDefault();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Skip(skip).Take(take)
                .LastOrDefault();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }
}
[TestFixture]
public class Min_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Min();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Min();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Skip(skip).Take(take)
                .Min();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Skip(skip).Take(take)
                .Min();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Min();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Min();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Skip(skip).Take(take)
                .Min();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Skip(skip).Take(take)
                .Min();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Min();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Min();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Skip(skip).Take(take)
                .Min();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Skip(skip).Take(take)
                .Min();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Min();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Min();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Skip(skip).Take(take)
                .Min();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Skip(skip).Take(take)
                .Min();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }
}
[TestFixture]
public class Max_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Max();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Max();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEmpty(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Skip(skip).Take(take)
                .Max();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Specialize()
                .Skip(skip).Take(take)
                .Max();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntArray()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Max();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Max();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntArray(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Skip(skip).Take(take)
                .Max();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Specialize()
                .Skip(skip).Take(take)
                .Max();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntList()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Max();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Max();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntList(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Skip(skip).Take(take)
                .Max();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Specialize()
                .Skip(skip).Take(take)
                .Max();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [Test]
    public void SameAsLinq_IntEnumerable()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Max();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Max();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }

    [TestCase(0, 0)]
    [TestCase(5, 0)]
    [TestCase(0, 8)]
    [TestCase(7, 9)]
    public void SliceSameAsLinq_IntEnumerable(int skip, int take)
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Skip(skip).Take(take)
                .Max();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Specialize()
                .Skip(skip).Take(take)
                .Max();

        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }
}
