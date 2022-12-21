// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

public static class TestData
{
    public static readonly IEnumerable<int> IntEmpty;
    public static readonly int[] IntArray;
    public static readonly List<int> IntList;
    public static readonly IEnumerable<int> IntEnumerable;

    static TestData()
    {
        Random rand = new Random(42);

        IntEmpty = Enumerable.Empty<int>();

        IntArray = new int[100];
        for (int i = 0; i < 100; ++i)
            IntArray[i] = rand.Next(10);

        IntList = new List<int>();
        for (int i = 0; i < 50; ++i)
            IntList.Add(rand.Next(-10, 50));

        IntEnumerable = GetEnumerable(rand.Next());
    }

    private static IEnumerable<int> GetEnumerable(int seed)
    {
        var rand = new Random(seed);

        for (int i = 0; i < 89; ++i)
            yield return rand.Next(-20, 20);
    }
}

public class ReferenceInt : IEquatable<ReferenceInt>, IComparable<ReferenceInt>
{
    private readonly int _value;

    public ReferenceInt(int value)
    {
        _value = value;
    }

    public static ReferenceInt operator +(ReferenceInt x, ReferenceInt y)
    {
        return new(x._value + y._value);
    }

    public static ReferenceInt operator -(ReferenceInt x, ReferenceInt y)
    {
        return new(x._value - y._value);
    }

    public static ReferenceInt operator *(ReferenceInt x, ReferenceInt y)
    {
        return new(x._value * y._value);
    }

    public static ReferenceInt operator /(ReferenceInt x, ReferenceInt y)
    {
        return new(x._value / y._value);
    }

    public static ReferenceInt operator %(ReferenceInt x, ReferenceInt y)
    {
        return new(x._value % y._value);
    }

    public bool Equals(ReferenceInt? other)
    {
        if (other == null)
            return false;
        return _value == other._value;
    }

    public int CompareTo(ReferenceInt? other)
    {
        if (other == null)
            return 1;
        return _value.CompareTo(other._value);
    }

    public override int GetHashCode()
    {
        return _value;
    }
}
