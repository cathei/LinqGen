// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;

namespace Cathei.LinqGen.Hidden
{
    // Operation is type hint for LinqGen.
    // For example, for code like this:
    // Enumerable.Range(10).Generate().Where(x => x % 2 == 0).Select(x => x * 0.5)
    // The information is like this:
    // Select<Where<Gen<int>>, double>
    public interface IStubSignature { }

    /// <summary>
    /// Compiled will be used far all actual implementation.
    /// Since type information will be only used for code generation, we don't need to keep it after compiling.
    /// </summary>
    public abstract class Compiled : IStubSignature { }

    public abstract class Gen<T> : IStubSignature { }

    public abstract class GenList<T> : IStubSignature { }

    public abstract class AsEnumerable<TUp> : IStubSignature { }

    public abstract class Select<TUp, TOut> : IStubSignature { }

    public abstract class SelectAt<TUp, TOut> : IStubSignature { }

    public abstract class SelectStruct<TUp, TOut> : IStubSignature { }

    public abstract class SelectAtStruct<TUp, TOut> : IStubSignature { }

    public abstract class Where<TUp> : IStubSignature { }

    public abstract class WhereAt<TUp> : IStubSignature { }

    public abstract class WhereStruct<TUp> : IStubSignature { }

    public abstract class WhereAtStruct<TUp> : IStubSignature { }

}