// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

public class GenerationTests
{
    public struct MyTempStruct<T>
    {
        public T value;
    }


    [Test]
    public void TestGeneration()
    {
        var gen0 = Enumerable.Range(0, 10).Gen();
        gen0.Select(x => x / 10);

        var gen = Enumerable.Range(0, 10)
            .Select(x => x / 10)
            .Gen()
            .Select(x => x / 10)
            .Select(x => x * 100)
            .AsEnumerable();

        int x = GenEnumerable.Range(0, 10)
            .First();

        int y = GenEnumerable.Range(0, 10)
            .FirstOrDefault();



        foreach (var temp in gen)
        {

        }

        var t = Enumerable.Repeat(new MyTempStruct<int>(), 10).Gen();
        // var a = t.Select(x => 10);
        var b = t.Select(x => 10)
            .Where(x => x % 2 == 0)
            .Where(x => x / 10 == 1);

        var tt = Enumerable.Repeat(3m, 10).Gen().Sum();
    }

    private void Temp()
    {
        var a = GenEnumerable.Range(0, 10)
            .Where(new PredicateWithIndex())
            .Select(new Selector())
            .FirstOrDefault();

        var b = GenEnumerable.Range(0, 10)
            .Where(new Predicate())
            .Where(new Predicate())
            .Where(new Predicate())
            .Where(new Predicate())
            .Where(new Predicate())
            .Where(new Predicate())
            .Where(new Predicate())
            .Where(new Predicate())
            .Where(new Predicate())
            .Where(new Predicate())
            .Where(new Predicate())
            .Sum(new Selector());

        var c = GenEnumerable.Range(0, 10)
            .Select(new Selector())
            .Select(x => x);
    }

    public struct Selector : IStructFunction<int, double>
    {
        public double Invoke(int arg)
        {
            return arg / 2.0;
        }
    }

    public struct Predicate : IStructFunction<int, bool>
    {
        public bool Invoke(int arg)
        {
            return arg % 2 == 0;
        }
    }

    public struct SelectorWithIndex : IStructFunction<int, int, double>
    {
        public double Invoke(int arg, int index)
        {
            return arg / 2.0 + index;
        }
    }

    public struct PredicateWithIndex : IStructFunction<int, int, bool>
    {
        public bool Invoke(int arg, int index)
        {
            return arg % 2 == 0 && index % 2 == 0;
        }
    }
}
