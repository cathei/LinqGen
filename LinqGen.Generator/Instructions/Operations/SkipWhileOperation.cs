// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public class SkipWhileOperation : Operation
{
    private bool WithIndex { get; }
    private bool WithStruct { get; }

    public SkipWhileOperation(in LinqGenExpression expression, uint id, bool withIndex, bool withStruct)
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

    // Cannot tell count after predicate
    public override ExpressionSyntax? RenderCount() => null;

    public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal,
        ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        var initStatements = new List<StatementSyntax>();

        initStatements.AddRange(base.RenderInitialization(isLocal, skipVar, takeVar));

        ExpressionSyntax predicateExpression =
            InvocationExpression(MemberAccessExpression(Member("predicate"), InvokeMethod),
                ArgumentList(WithIndex
                    ? new ExpressionSyntax[] { CurrentPlaceholder, PreIncrementExpression(Iterator("index")) }
                    : new ExpressionSyntax[] { CurrentPlaceholder }));

        initStatements.Add(Upstream.RenderIteration(isLocal,
            SingletonList<StatementSyntax>(IfStatement(LogicalNotExpression(predicateExpression), BreakStatement()))));

        return initStatements;
    }
}