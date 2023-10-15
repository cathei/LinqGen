// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public class SelectOperation : Operation
{
    private bool WithIndex { get; }
    private bool WithStruct { get; }

    public SelectOperation(in LinqGenExpression expression, uint id, bool withIndex, bool withStruct)
        : base(expression, id)
    {
        var selectorSymbol = expression.GetNamedParameterType(0);

        // Func<TIn, TOut> or IStructFunction<TIn, TOut>
        // Func<TIn, int, TOut> or IStructFunction<TIn, int, TOut>
        var elementSymbol = selectorSymbol.TypeArguments[withIndex ? 2 : 1];

        OutputElementSymbol = elementSymbol;
        OutputElementType = ParseTypeName(elementSymbol);

        WithIndex = withIndex;
        WithStruct = withStruct;
    }

    private TypeSyntax? _selectorType;

    private TypeSyntax SelectorType
    {
        get
        {
            if (_selectorType != null)
                return _selectorType;

            TypeSyntax[] typeArguments = WithIndex
                ? new[] { Upstream.OutputElementType, IntType, OutputElementType }
                : new[] { Upstream.OutputElementType, OutputElementType };

            return _selectorType = WithStruct
                ? StructFunctionInterfaceType(typeArguments)
                : FuncDelegateType(typeArguments);
        }
    }

    public override ITypeSymbol OutputElementSymbol { get; }
    public override TypeSyntax OutputElementType { get; }

    public override TypeSyntax? DummyParameterType
    {
        get
        {
            if (!WithStruct)
                return null;

            if (WithIndex)
                return TupleType(SeparatedList(new[] { TupleElement(OutputElementType), TupleElement(BoolType) }));

            return OutputElementType;
        }
    }

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (WithStruct)
        {
            yield return new TypeParameterInfo(TypeName("Selector"), SelectorType);
        }
    }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield return new MemberInfo(MemberKind.Both,
            WithStruct ? TypeName("Selector") : SelectorType, LocalName("selector"));

        if (WithIndex)
            yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("index"));
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(
        bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        if (WithIndex)
        {
            ExpressionSyntax initialIndex = LiteralExpression(-1);

            if (Upstream.SupportPartition && skipVar != null)
                initialIndex = SubtractExpression(skipVar, LiteralExpression(1));

            yield return ExpressionStatement(SimpleAssignmentExpression(Iterator("index"), initialIndex));
        }

        foreach (var statement in base.RenderInitialization(isLocal, skipVar, takeVar))
            yield return statement;
    }

    public override ExpressionSyntax? RenderCount()
    {
        return Upstream.RenderCount();
    }

    protected override ExpressionSyntax RenderCurrent()
    {
        return InvocationExpression(
            MemberAccessExpression(Member("selector"), InvokeMethod),
            ArgumentList(WithIndex
                ? new ExpressionSyntax[] { CurrentPlaceholder, PreIncrementExpression(Iterator("index")) }
                : new ExpressionSyntax[] { CurrentPlaceholder }));
    }
}