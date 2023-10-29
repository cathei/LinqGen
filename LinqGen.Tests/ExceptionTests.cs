// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

[TestFixture]
public class ExceptionTests
{
    [Test]
    public void Cast_Fail_ExceptionShouldBeThrown()
    {
        object?[] list = new object?[]
        {
            "A", "B", new object(), 1, "DDD", null, "EEE"
        };

        var enumerable = list.Gen().Cast<string>();

        Assert.Throws<InvalidCastException>(() =>
        {
            foreach (var str in enumerable)
            {
                // do nothing
            }
        });
    }

    [Test]
    public void OfType_Fail_ShouldBeSkipped()
    {
        object?[] list = new object?[]
        {
            "A", "B", new object(), 1, "DDD", null, "EEE"
        };

        var enumerable = list.Gen().OfType<string>();

        int count = 0;

        foreach (var x in enumerable)
        {
            count++;
        }

        Assert.AreEqual(list.Length - 3, count);
    }
    
    [Test]
    public void Single_Fail_ExceptionShouldBeThrown()
    {
        IEnumerable<object?> list = new object?[]
        {
            "A", "B", new object(), 1, "DDD", null, "EEE"
        };

        Assert.Throws<InvalidOperationException>(() => _ = list.Gen().Single());
    }
    
    [Test]
    public void Single_Fail_Empty_ExceptionShouldBeThrown()
    {
        IEnumerable<object?> list = Array.Empty<object?>();

        Assert.Throws<InvalidOperationException>(() => _ = list.Gen().Single());
    }
    
    [Test]
    public void Single_Fail_Two_Elements_ExceptionShouldBeThrown()
    {
        IEnumerable<object?> list = new object?[]
        {
            "A", "B"
        };

        Assert.Throws<InvalidOperationException>(() => _ = list.Gen().Single());
    }
}
