using System;
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
}