// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class OrderByOperation : OrderingOperation
{
    public OrderByOperation(in LinqGenExpression expression, uint id,
        FunctionKind selectorKind, ComparerKind comparerKind, bool descending)
        : base(in expression, id, selectorKind, comparerKind, descending)
    {
    }
}