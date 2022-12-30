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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
public class CountPred_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .Count(x => x % 2 == 0);

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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .Skip(skip).Take(take)
                .Count(x => x % 2 == 0);
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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .Count(x => x % 2 == 0);

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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .Skip(skip).Take(take)
                .Count(x => x % 2 == 0);
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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .Count(x => x % 2 == 0);

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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .Skip(skip).Take(take)
                .Count(x => x % 2 == 0);
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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .Count(x => x % 2 == 0);

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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .Skip(skip).Take(take)
                .Count(x => x % 2 == 0);
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
public class CountPredStruct_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .Count(new EvenPredicate());

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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .Skip(skip).Take(take)
                .Count(new EvenPredicate());
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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .Count(new EvenPredicate());

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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .Skip(skip).Take(take)
                .Count(new EvenPredicate());
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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .Count(new EvenPredicate());

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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .Skip(skip).Take(take)
                .Count(new EvenPredicate());
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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .Count(new EvenPredicate());

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
                .Count(x => x % 2 == 0);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .Skip(skip).Take(take)
                .Count(new EvenPredicate());
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
public class MinComp_Tests
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
                .Gen()
                .Min(new StructComparer());

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
                .Gen()
                .Skip(skip).Take(take)
                .Min(new StructComparer());
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
                .Gen()
                .Min(new StructComparer());

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
                .Gen()
                .Skip(skip).Take(take)
                .Min(new StructComparer());
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
                .Gen()
                .Min(new StructComparer());

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
                .Gen()
                .Skip(skip).Take(take)
                .Min(new StructComparer());
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
                .Gen()
                .Min(new StructComparer());

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
                .Gen()
                .Skip(skip).Take(take)
                .Min(new StructComparer());
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
                .Gen()
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
[TestFixture]
public class MaxComp_Tests
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
                .Gen()
                .Max(new StructComparer());

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
                .Gen()
                .Skip(skip).Take(take)
                .Max(new StructComparer());
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
                .Gen()
                .Max(new StructComparer());

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
                .Gen()
                .Skip(skip).Take(take)
                .Max(new StructComparer());
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
                .Gen()
                .Max(new StructComparer());

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
                .Gen()
                .Skip(skip).Take(take)
                .Max(new StructComparer());
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
                .Gen()
                .Max(new StructComparer());

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
                .Gen()
                .Skip(skip).Take(take)
                .Max(new StructComparer());
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
public class Sum_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Sum();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .Sum();

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
                .Sum();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .Skip(skip).Take(take)
                .Sum();
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
                .Sum();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .Sum();

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
                .Sum();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .Skip(skip).Take(take)
                .Sum();
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
                .Sum();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .Sum();

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
                .Sum();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .Skip(skip).Take(take)
                .Sum();
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
                .Sum();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .Sum();

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
                .Sum();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .Skip(skip).Take(take)
                .Sum();
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
public class SumSelector_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Sum(x => x * 2);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .Sum(new DoubleSelector());

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
                .Sum(x => x * 2);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .Skip(skip).Take(take)
                .Sum(new DoubleSelector());
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
                .Sum(x => x * 2);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .Sum(new DoubleSelector());

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
                .Sum(x => x * 2);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .Skip(skip).Take(take)
                .Sum(new DoubleSelector());
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
                .Sum(x => x * 2);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .Sum(new DoubleSelector());

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
                .Sum(x => x * 2);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .Skip(skip).Take(take)
                .Sum(new DoubleSelector());
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
                .Sum(x => x * 2);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .Sum(new DoubleSelector());

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
                .Sum(x => x * 2);
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .Skip(skip).Take(take)
                .Sum(new DoubleSelector());
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
public class ToArray_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        int[] expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .ToArray();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .ToArray();

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
        int[] expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Skip(skip).Take(take)
                .ToArray();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .Skip(skip).Take(take)
                .ToArray();
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
        int[] expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .ToArray();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .ToArray();

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
        int[] expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Skip(skip).Take(take)
                .ToArray();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .Skip(skip).Take(take)
                .ToArray();
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
        int[] expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .ToArray();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .ToArray();

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
        int[] expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Skip(skip).Take(take)
                .ToArray();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .Skip(skip).Take(take)
                .ToArray();
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
        int[] expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .ToArray();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .ToArray();

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
        int[] expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Skip(skip).Take(take)
                .ToArray();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .Skip(skip).Take(take)
                .ToArray();
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
public class ToList_Tests
{

    [Test]
    public void SameAsLinq_IntEmpty()
    {
        Exception ex1 = null, ex2 = null;
        List<int> expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .ToList();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .ToList();

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
        List<int> expected = default, actual = default;

        try
        {
            expected = TestData.IntEmpty
                .Skip(skip).Take(take)
                .ToList();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEmpty
                .Gen()
                .Skip(skip).Take(take)
                .ToList();
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
        List<int> expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .ToList();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .ToList();

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
        List<int> expected = default, actual = default;

        try
        {
            expected = TestData.IntArray
                .Skip(skip).Take(take)
                .ToList();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntArray
                .Gen()
                .Skip(skip).Take(take)
                .ToList();
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
        List<int> expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .ToList();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .ToList();

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
        List<int> expected = default, actual = default;

        try
        {
            expected = TestData.IntList
                .Skip(skip).Take(take)
                .ToList();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntList
                .Gen()
                .Skip(skip).Take(take)
                .ToList();
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
        List<int> expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .ToList();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .ToList();

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
        List<int> expected = default, actual = default;

        try
        {
            expected = TestData.IntEnumerable
                .Skip(skip).Take(take)
                .ToList();
        }
        catch (Exception e)
        {
            ex1 = e;
        }

        try
        {
            actual = TestData.IntEnumerable
                .Gen()
                .Skip(skip).Take(take)
                .ToList();
        }
        catch (Exception e)
        {
            ex2 = e;
        }

        Assert.AreEqual(expected, actual);
        Assert.AreEqual(ex1?.GetType(), ex2?.GetType());
    }
}
