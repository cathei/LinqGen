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
    public static readonly List<ReferenceInt> ReferenceIntList;

    public static readonly IEnumerable<object> ObjectEmpty;
    public static readonly object?[] ObjectStringArray;
    public static readonly List<object?> ObjectStringList;
    public static readonly IEnumerable<object?> ObjectStringEnumerable;

    static TestData()
    {
        Random rand = new Random(42);

        IntEmpty = Enumerable.Empty<int>();

        // Even only
        IntArray = new int[100];
        for (int i = 0; i < 100; ++i)
            IntArray[i] = rand.Next(10) * 2;

        // Odd only
        IntList = new List<int>();
        for (int i = 0; i < 50; ++i)
            IntList.Add(rand.Next(-10, 50) * 2 + 1);

        IntEnumerable = GetIntEnumerable(rand.Next());

        ReferenceIntList = new List<ReferenceInt>();
        foreach (var value in IntArray)
            ReferenceIntList.Add(new(value));

        ObjectEmpty = Enumerable.Empty<object>();
        ObjectStringEnumerable = GetObjectStringEnumerable();
        ObjectStringArray = ObjectStringEnumerable.ToArray();
        ObjectStringList = ObjectStringArray.ToList();
    }

    private static IEnumerable<int> GetIntEnumerable(int seed)
    {
        var rand = new Random(seed);

        for (int i = 0; i < 89; ++i)
            yield return rand.Next(-20, 20);
    }

    private static IEnumerable<object?> GetObjectStringEnumerable()
    {
        yield return "AA";
        yield return "B";
        yield return null;
        yield return "Steve Jobs";
        yield return "Bill Gates";
        yield return null;
        yield return string.Empty;
        yield return "CCC";
    }
}
