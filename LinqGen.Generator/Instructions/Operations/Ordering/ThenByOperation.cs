// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class ThenByOperation : OrderingOperation
{
    public ThenByOperation(in LinqGenExpression expression, uint id,
        FunctionKind selectorKind, ComparerKind comparerKind, bool descending)
        : base(in expression, id, selectorKind, comparerKind, descending)
    {
    }

    protected override OrderingOperation? UpstreamOrder => Upstream as OrderingOperation;
}