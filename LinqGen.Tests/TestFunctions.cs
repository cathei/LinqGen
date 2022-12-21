// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

public struct EvenPredicate : IStructFunction<int, bool>
{
    public bool Invoke(int arg)
    {
        return arg % 2 == 0;
    }
}

public struct DoubleSelector : IStructFunction<int, int>
{
    public int Invoke(int arg)
    {
        return arg * 2;
    }
}

public struct Mod3Selector : IStructFunction<int, int>
{
    public int Invoke(int arg)
    {
        return arg % 3;
    }
}

public struct NegateSelector : IStructFunction<int, int>
{
    public int Invoke(int arg)
    {
        return -arg;
    }
}

public struct MinusEvenPredicate : IStructFunction<int, int, bool>
{
    public bool Invoke(int arg, int index)
    {
        return (arg - index) % 2 == 0;
    }
}

public struct AddSelector : IStructFunction<int, int, int>
{
    public int Invoke(int arg, int index)
    {
        return arg + index;
    }
}

public struct StructEqualityComparer : IEqualityComparer<int>
{
    public bool Equals(int x, int y)
    {
        return x == y;
    }

    public int GetHashCode(int obj)
    {
        return obj;
    }
}

public struct StructComparer : IComparer<int>
{
    public int Compare(int x, int y)
    {
        return x - y;
    }
}