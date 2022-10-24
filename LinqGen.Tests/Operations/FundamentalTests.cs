using System;
using NUnit.Framework;

namespace Cathei.LinqGen.Tests;

public class FundamentalTests
{
    [Test]
    public void Fundamental_DelegateComparison()
    {
        Func<int, int> a = x => x;
        Func<int, int> b = y => y;

        Assert.Equals(a, b);
    }
}