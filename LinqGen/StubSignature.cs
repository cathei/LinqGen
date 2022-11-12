// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;

namespace Cathei.LinqGen.Hidden
{
    // Operation is type hint for LinqGen.
    // For example, for code like this:
    // Enumerable.Range(10).Generate().Where(x => x % 2 == 0).Select(x => x * 0.5)
    // The signature is like this:
    // Select<Where<Specialized<IEnumerable<int>>>, double>
    public interface IStubSignature { }

    public abstract class Compiled : IStubSignature { }

    public abstract class Specialize<TEnumerable> : IStubSignature { }

    public abstract class AsEnumerable<TUp> : IStubSignature { }

    public abstract class Cast<TUp> : IStubSignature { }

    public abstract class OfType<TUp> : IStubSignature { }

    public abstract class Select<TUp, TOut> : IStubSignature { }

    public abstract class SelectAt<TUp, TOut> : IStubSignature { }

    public abstract class SelectStruct<TUp, TOut> : IStubSignature { }

    public abstract class SelectAtStruct<TUp, TOut> : IStubSignature { }

    public abstract class Where<TUp> : IStubSignature { }

    public abstract class WhereAt<TUp> : IStubSignature { }

    public abstract class WhereStruct<TUp> : IStubSignature { }

    public abstract class WhereAtStruct<TUp> : IStubSignature { }

    public abstract class Skip<TUp> : IStubSignature { }

    public abstract class Take<TUp> : IStubSignature { }

    public abstract class Distinct<TUp> : IStubSignature { }

    public abstract class DistinctStruct<TUp> : IStubSignature { }

    public abstract class OrderBy<TUp, TKey> : IStubSignature { }

    public abstract class OrderByStruct<TUp, TKey> : IStubSignature { }

    public abstract class OrderBySelf<TUp> : IStubSignature { }

    public abstract class OrderBySelfStruct<TUp> : IStubSignature { }

    public abstract class ThenBy<TUp, TKey> : IStubSignature { }

    public abstract class ThenByStruct<TUp, TKey> : IStubSignature { }

    public abstract class ThenBySelf<TUp> : IStubSignature { }

    public abstract class ThenBySelfStruct<TUp> : IStubSignature { }
}