// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;

namespace Cathei.LinqGen.Generator;

public class DistinctOperation : Operation
{
    private ComparerKind ComparerKind { get; }

    public DistinctOperation(in LinqGenExpression expression, uint id, ComparerKind comparerKind)
        : base(expression, id)
    {
        ComparerKind = comparerKind;
    }

    private TypeSyntax ComparerType
    {
        get
        {
            switch (ComparerKind)
            {
                case ComparerKind.Default:
                    return EqualityComparerDefaultType(OutputElementType, OutputElementSymbol);

                case ComparerKind.Interface:
                    return EqualityComparerInterfaceType(OutputElementType);

                case ComparerKind.Struct:
                    return TypeName("Comparer");
            }

            throw new InvalidOperationException();
        }
    }

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (ComparerKind == ComparerKind.Struct)
        {
            yield return new TypeParameterInfo(TypeName("Comparer"),
                StructConstraint, TypeConstraint(EqualityComparerInterfaceType(OutputElementType)));
        }
    }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        if (ComparerKind != ComparerKind.Default)
            yield return new MemberInfo(MemberKind.Enumerable, ComparerType, LocalName("comparer"));

        var pooledSetType = PooledSetType(OutputElementType, ComparerType, OutputElementSymbol.IsUnmanagedType);
        yield return new MemberInfo(MemberKind.Enumerator, pooledSetType, LocalName("hashSet"));
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(
        bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        var comparerExpression = ComparerKind == ComparerKind.Default
            ? EqualityComparerDefault(OutputElementType, OutputElementSymbol)
            : Member("comparer");

        var countExpression = Upstream.RenderCount() ?? LiteralExpression(0);

        var pooledSetType = PooledSetType(OutputElementType, ComparerType, OutputElementSymbol.IsUnmanagedType);

        var pooledSetCreation = ObjectCreationExpression(
            pooledSetType, ArgumentList(countExpression, comparerExpression), null);

        yield return ExpressionStatement(SimpleAssignmentExpression(Iterator("hashSet"), pooledSetCreation));

        foreach (var statement in base.RenderInitialization(isLocal, skipVar, takeVar))
            yield return statement;
    }

    public override bool SupportPartition => false;

    public override ExpressionSyntax? RenderCount() => null;

    protected override StatementSyntax? RenderMoveNext()
    {
        return IfStatement(
            LogicalNotExpression(InvocationExpression(
                MemberAccessExpression(Iterator("hashSet"), AddMethod), ArgumentList(CurrentPlaceholder))),
            ContinueStatement());
    }

    public override IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
    {
        foreach (var statement in base.RenderDispose(isLocal))
            yield return statement;

        yield return ExpressionStatement(InvocationExpression(Iterator("hashSet"), DisposeMethod));
    }
}