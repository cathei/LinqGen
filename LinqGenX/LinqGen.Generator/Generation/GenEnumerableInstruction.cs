using System;
using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public class GenEnumerableInstruction : GenerationInstruction
{
    private readonly TypeSyntax EnumeratorType;

    public GenEnumerableInstruction(
        LinqGenInstruction? upstream, TypeSyntax sourceType, TypeSyntax enumeratorType, TypeSyntax elementType)
        : base(upstream, sourceType, elementType)
    {
        EnumeratorType = enumeratorType;
    }

    protected override IEnumerable<MemberInfo> Parameters()
    {
        yield return new MemberInfo(SourceType, "source");
    }

    protected override IEnumerable<MemberInfo> LocalMembers(IterationContext ctx)
    {
        yield return new MemberInfo(EnumeratorType, "iter");
    }

    protected override IEnumerable<StatementSyntax> LocalStep(IterationContext ctx)
    {
        yield return IfStatement(
            LogicalNotExpression(InvocationExpression(ctx.Member("iter"), IdentifierName("MoveNext"))),
            BreakStatement());
    }

    public override ExpressionSyntax GetCurrent(ScanContext ctx)
    {
        return MemberAccessExpression(ctx.Member("iter"), IdentifierName("Current"));
    }

    public override bool SupportsPartition => false;
    public override bool SupportsCount => false;
}

public class GenEnumerableNode : LinqGenNode
{
    public GenEnumerableNode(LinqGenNode? upstream, IMethodSymbol methodSymbol)
        : base(upstream, methodSymbol) { }

    protected override LinqGenRender Expand(in ExpansionContext ctx)
    {
        ITypeSymbol targetType = NormalizeSignature((INamedTypeSymbol)MethodSymbol.Parameters[0].Type);

        if (!TryGetEnumerableInterface(targetType, out var enumerableSymbol) ||
            enumerableSymbol.Arity < 1)
        {
            throw new LinqGenException("Can't specialize non-IEnumerable<T>!");
        }

        TypeSyntax elementType = ParseTypeName(enumerableSymbol.TypeArguments[0]);

        return new GenerationRender(new GenEnumerableInstruction(
            ctx.Upstream,
            EnumerableInterfaceType(elementType),
            EnumeratorInterfaceType(elementType),
            elementType
        ), IdentifierName(MethodSymbol.Name));
    }
}

public class GenEnumerableObjectNode : LinqGenNode
{
    public GenEnumerableObjectNode(LinqGenNode? upstream, IMethodSymbol methodSymbol)
        : base(upstream, methodSymbol) { }

    protected override LinqGenRender Expand(in ExpansionContext ctx)
    {
        return new GenerationRender(new GenEnumerableInstruction(
            ctx.Upstream,
            IdentifierName("IEnumerable"),
            IdentifierName("IEnumerator"),
            ObjectType
        ), IdentifierName(MethodSymbol.Name));
    }
}
