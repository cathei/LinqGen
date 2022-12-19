// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

public static class TestData
{
    public static readonly int[] IntArrayEmpty;
    public static readonly int[] IntArray;

    public static readonly List<int> IntListEmpty;
    public static readonly List<int> IntList;


    static TestData()
    {
        Random rand = new Random(42);

        IntArrayEmpty = Array.Empty<int>();
        IntArray = new int[100];

        for (int i = 0; i < 100; ++i)
            IntArray[i] = rand.Next(10);

        IntListEmpty = new List<int>();
        IntList = new List<int>();

        for (int i = 0; i < 50; ++i)
            IntList.Add(rand.Next(50));
    }
}
