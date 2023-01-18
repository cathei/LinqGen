// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Cathei.LinqGen.Tests;

public class GenericElementTests
{
    [Test]
    public void CastWithSelect()
    {
        // https://github.com/cathei/LinqGen/issues/3

        // var strings = TestData.ObjectStringArray.Gen()
        //     .OfType<string>().Where(string.IsNullOrEmpty);

        // foreach (var elem in strings)
        // {
        //
        // }
    }

    // public class Elem
    // {
    //     public float timeMs;
    //     public object behaviour;
    //     public int handType;
    // }
    //
    // [Test]
    // public void ThenByAllocation()
    // {
    //     List<int> list = new();
    //
    //     list.Select(x => x+1).Sum();
    //
    //     list
    //         .Gen()
    //         .OrderBy(x => x.timeMs)
    //         .ThenBy(x => x.behaviour)
    //         .ThenBy(x => x.handType)
    //         .ToArray();
    // }
}