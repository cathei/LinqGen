// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

public struct RefIntEvenPredicate : IStructFunction<ReferenceInt, bool>
{
    public bool Invoke(ReferenceInt arg)
    {
        return arg % 2 == 0;
    }
}

public struct RefIntDoubleSelector : IStructFunction<ReferenceInt, ReferenceInt>
{
    public ReferenceInt Invoke(ReferenceInt arg)
    {
        return arg * 2;
    }
}

public struct RefIntMod3Selector : IStructFunction<ReferenceInt, ReferenceInt>
{
    public ReferenceInt Invoke(ReferenceInt arg)
    {
        return arg % 3;
    }
}

public struct RefIntNegateSelector : IStructFunction<ReferenceInt, ReferenceInt>
{
    public ReferenceInt Invoke(ReferenceInt arg)
    {
        return -arg;
    }
}

public struct RefIntMinusEvenPredicate : IStructFunction<ReferenceInt, int, bool>
{
    public bool Invoke(ReferenceInt arg, int index)
    {
        return (arg - index) % 2 == 0;
    }
}

public struct RefIntAddSelector : IStructFunction<ReferenceInt, int, ReferenceInt>
{
    public ReferenceInt Invoke(ReferenceInt arg, int index)
    {
        return arg + index;
    }
}

public struct RefIntStructEqualityComparer : IEqualityComparer<ReferenceInt>
{
    public bool Equals(ReferenceInt? x, ReferenceInt? y)
    {
        return x == y;
    }

    public int GetHashCode(ReferenceInt obj)
    {
        return obj.GetHashCode();
    }
}

public struct RefIntStructComparer : IComparer<ReferenceInt>
{
    public int Compare(ReferenceInt? x, ReferenceInt? y)
    {
        if (x is null)
            return y is null ? 0 : -1;
        return x.CompareTo(y);
    }
}

public static class ReferenceIntExtensions
{
    public static ReferenceInt? Sum(this IEnumerable<ReferenceInt> source)
    {
        ReferenceInt? result = null;

        foreach (var item in source)
        {
            result += item;
        }

        return result;
    }

    public static ReferenceInt? Sum(this IEnumerable<ReferenceInt> source, Func<ReferenceInt, ReferenceInt> selector)
    {
        ReferenceInt? result = null;

        foreach (var item in source)
        {
            result += selector(item);
        }

        return result;
    }
}