// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;

namespace Cathei.LinqGen.Operations
{
    // Operation is type hint for LinqGen.
    // For example, for code like this:
    // Enumerable.Range(10).Generate().Where(x => x % 2 == 0).Select(x => x * 0.5)
    // The information is like this:
    // Select<Where<Gen<int>>, double>
    public interface ILinqGenOperation { }

    public abstract class Gen<T> : ILinqGenOperation { }

    public abstract class GenList<T> : ILinqGenOperation { }

    public abstract class Select<TParent, TOut> : ILinqGenOperation { }

    public abstract class Where<TParent> : ILinqGenOperation { }

}