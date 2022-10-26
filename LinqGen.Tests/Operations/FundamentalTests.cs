using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cathei.LinqGen.Tests;

public class FundamentalTests
{

    public static readonly Func<int, int> temp1 = x => x;
    public static readonly Func<int, int> temp2 = x => x;


    [Test]
    public void Fundamental_DelegateComparison()
    {
        Func<int, int> a = static x => x;
        Func<int, int> b = static x => x;

        // Assert.AreEqual(a, b);
        Assert.True(object.ReferenceEquals(temp1, temp2));
    }

    [Test]
    public void TypeGeneration()
    {
        var result = new List<int>()
            .Specialize()
            .Where(x => x == 0)
            .Select(new Selector())
            .Sum();

        var result2 = new List<int>()
            .Specialize()
            .Where(x => x == 0)
            .Select(new Selector())
            .Sum();

        var result3 = new List<int>()
            .Specialize()
            .Where(x => x == 0)
            .Select(new Selector2())
            .Sum();

        var t = new Dictionary<int, double>().Values
            .Specialize()
            .Where(y => y == 0)
            .Cast<decimal>()
            .OfType<int>()
            .Sum();

        var t2 = new Dictionary<int, double>().Values
            .Specialize()
            .Where(y => y == 0)
            .Cast<decimal>()
            .OfType<double>()
            .Sum();
    }

    public struct Selector : IStructFunction<int, double>
    {
        public double Invoke(int arg)
        {
            return arg / 10.0;
        }
    }


    public struct Selector2 : IStructFunction<int, double>
    {
        public double Invoke(int arg)
        {
            return arg / 10.0;
        }
    }

    public struct Predicate : IStructFunction<int, bool>
    {
        public bool Invoke(int arg)
        {
            return arg == 0;
        }
    }
}