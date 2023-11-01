using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Cathei.LinqGen.Generator;

public abstract class LinqGenOperation : LinqGenInstruction
{
    protected LinqGenOperation(LinqGenInstruction upstream, in ImmutableArray<TypeSyntax> identity)
        : base(upstream, identity)
    {}

    protected new LinqGenInstruction Upstream => base.Upstream!;

    public override ExpressionSyntax GetCurrent(in ScanContext ctx)
    {
        return Upstream.GetCurrent(ctx);
    }

    public override bool SupportsPartition => Upstream.SupportsPartition;

    public override bool SupportsCount => Upstream.SupportsCount;

    public override ExpressionSyntax? GetCount(in ScanContext ctx)
    {
        return Upstream.GetCount(ctx);
    }
}
