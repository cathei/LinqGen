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
