// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cathei.LinqGen.Tests;

public class FundamentalTests
{
    // [Test]
    // public void Fundamental_DelegateComparison()
    // {
    //     Expression<Func<int, int>> a = x => x;
    //     Expression<Func<int, int>> b = y => y;
    //     Expression<Func<int, int>> c = x => x;
    //
    //     Assert.IsTrue();
    //     Assert.AreEqual(a, b);
    // }

    [TestCase(2)]
    [TestCase(5)]
    [TestCase(0)]
    public void TestArrayTypeGeneration(int c1)
    {
        int[] array = new int[c1];

        int count = 0;

        foreach (var i in array.Gen())
            count++;

        Assert.AreEqual(c1, count);
    }

    [Test]
    public void Test_ConcatFromSameGeneration()
    {
        double[] array1 = new double[10];
        double[] array2 = new double[5];

        var concat = array1.Gen().Concat(array2.Gen());

        int count = 0;

        foreach (var i in concat)
            count++;

        Assert.AreEqual(15, concat.Count());
        Assert.AreEqual(15, count);
    }

    [Test]
    public void Test_ConcatFromSameSource()
    {
        double[] array = new double[10];

        var concat = array.Gen().Concat(array.Gen().Select(x => x * 2));

        int count = 0;

        foreach (var i in concat)
            count++;

        Assert.AreEqual(20, concat.Count());
        Assert.AreEqual(20, count);
    }

    [Test]
    public void Test_ConcatPrepend()
    {
        double[] array = new double[10];

        var enumerable = array.Gen()
            .Concat(array.Gen().Select(x => x * 2)).Prepend(24.12);

        int count = 0;

        foreach (var i in enumerable)
            count++;

        Assert.AreEqual(21, enumerable.Count());
        Assert.AreEqual(21, count);
    }

    [Test]
    public void Test_ConcatAppend()
    {
        double[] array = new double[10];

        var enumerable = array.Gen()
            .Concat(array.Gen().Select(x => x * 2)).Append(43.21);

        int count = 0;

        foreach (var i in enumerable)
            count++;

        Assert.AreEqual(21, enumerable.Count());
        Assert.AreEqual(21, count);
    }

    // [TestCase(1, 2, 3, 4)]
    // [TestCase(8, 6, 4, 2)]
    // [TestCase(2, 0, 4, 2)]
    // public void TestArrayMultiTypeGeneration(int c1, int c2, int c3, int c4)
    // {
    //     int[,,,] array = new int[c1, c2, c3, c4];
    //
    //     int count = 0;
    //
    //     foreach (var i in array.Gen())
    //         count++;
    //
    //     Assert.AreEqual(c1 * c2 * c3 * c4, count);
    // }

    [Test]
    public void TestGenericTypeGeneration()
    {
        Assert.AreEqual(10, TypeGenerationTest(10));
        Assert.AreEqual(20.0, TypeGenerationTest(20.0));
        Assert.AreEqual("Test string", TypeGenerationTest("Test string"));
    }

    private TTestParam TypeGenerationTest<TTestParam>(TTestParam value)
    {
        return Enumerable.Repeat(value, 1)
            .Gen()
            .First();
    }

    [Test]
    public void CustomGenericEnumerableTest()
    {
        int value = new CustomEnumerable<int>().Gen()
            .Select(x => x * 10)
            .First();

        Assert.AreEqual(0, value);
    }

    public struct CustomEnumerable<T> : IStructEnumerable<T, CustomEnumerable<T>.Enumerator>
    {
        public struct Enumerator : IEnumerator<T>
        {
            public bool MoveNext()
            {
                return true;
            }

            public T Current => default!;

            object IEnumerator.Current => Current!;

            public void Reset() => throw new NotSupportedException();

            public void Dispose() { }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator();
        }
    }

    [Test]
    public void CustomEnumerableTest()
    {
        string value = new CustomEnumerable().Gen()
            .Skip(3)
            .First();

        Assert.AreEqual("TestResult", value);
    }

    public struct CustomEnumerable : IStructEnumerable<string, CustomEnumerable.Enumerator>
    {
        public struct Enumerator : IEnumerator<string>
        {
            public bool MoveNext()
            {
                return true;
            }

            public string Current => "TestResult";

            object IEnumerator.Current => Current!;

            public void Reset() => throw new NotSupportedException();

            public void Dispose() { }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator();
        }
    }

    [Test]
    public void CollectionInterfaceTest()
    {
        ICollection collection = TestData.IntList;
        var generation = collection.Gen();

        int value = generation
            .Cast<int>()
            .First();

        Assert.AreEqual(collection.Count, generation.Count());
        Assert.AreEqual(TestData.IntList[0], value);
    }

    [Test]
    public void CollectionGenericInterfaceTest()
    {
        ICollection<int> collection = TestData.IntList;
        var generation = collection.Gen();

        int value = generation
            .Select(x => x * 10)
            .First();

        Assert.AreEqual(collection.Count, generation.Count());
        Assert.AreEqual(TestData.IntList[0] * 10, value);
    }

    [Test]
    public void ReadOnlyCollectionInterfaceTest()
    {
        IReadOnlyCollection<int> collection = TestData.IntList;
        var generation = collection.Gen();

        int value = generation
            .Select(x => x * 10)
            .First();

        Assert.AreEqual(collection.Count, generation.Count());
        Assert.AreEqual(TestData.IntList[0] * 10, value);
    }
}