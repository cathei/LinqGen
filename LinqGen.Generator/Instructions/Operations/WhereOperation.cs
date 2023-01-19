// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public class WhereOperation : Operation
{
    private bool WithIndex { get; }
    private bool WithStruct { get; }

    public WhereOperation(in LinqGenExpression expression, int id, bool withIndex, bool withStruct)
        : base(expression, id)
    {
        WithIndex = withIndex;
        WithStruct = withStruct;
    }

    private TypeSyntax? _predicateType;

    private TypeSyntax PredicateType
    {
        get
        {
            if (_predicateType != null)
                return _predicateType;

            TypeSyntax[] typeArguments = WithIndex
                ? new[] { Upstream.OutputElementType, IntType, BoolType }
                : new[] { Upstream.OutputElementType, BoolType };

            return _predicateType = WithStruct
                ? StructFunctionInterfaceType(typeArguments)
                : FuncDelegateType(typeArguments);
        }
    }

    public override TypeSyntax? DummyParameterType => WithStruct && WithIndex ? BoolType : null;

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (WithStruct)
        {
            yield return new TypeParameterInfo(TypeName("Predicate"), PredicateType);
        }
    }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield return new MemberInfo(MemberKind.Both,
            WithStruct ? TypeName("Predicate") : PredicateType, LocalName("predicate"));

        if (WithIndex)
            yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("index"), LiteralExpression(-1));
    }

    public override bool SupportPartition => false;

    public override ExpressionSyntax? RenderCount()
    {
        return null;
    }

    protected override StatementSyntax RenderMoveNext()
    {
        return IfStatement(
            LogicalNotExpression(InvocationExpression(
                MemberAccessExpression(Member("predicate"), InvokeMethod),
                ArgumentList(WithIndex
                    ? new ExpressionSyntax[] { CurrentPlaceholder, PreIncrementExpression(Iterator("index")) }
                    : new ExpressionSyntax[] { CurrentPlaceholder }))),
            ContinueStatement());
    }
}