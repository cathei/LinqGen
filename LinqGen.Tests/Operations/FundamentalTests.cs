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
        var result = Enumerable.Range(0, 10)
            .Specialize()
            .Select(new Selector())
            .Where(x => x == 0)
            .Sum();

        List<double> x = new List<double>();

        var t = x.Specialize()
            .Where(y => y == 0);
    }

    public struct Selector : IStructFunction<int, double>
    {
        public double Invoke(int arg)
        {
            return arg / 10.0;
        }
    }
}