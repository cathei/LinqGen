// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class AnyAllEvaluation : LocalEvaluation
{
    private bool WithStruct { get; }
    private bool IsAll { get; }

    public AnyAllEvaluation(in LinqGenExpression expression, uint id, bool isAll) : base(expression, id)
    {
        var parameterType = expression.GetNamedParameterType(0);
        WithStruct = IsStructFunction(parameterType);
        IsAll = isAll;
    }

    private TypeSyntax? _predicateType;

    private TypeSyntax PredicateType
    {
        get
        {
            if (_predicateType != null)
                return _predicateType;

            TypeSyntax[] typeArguments = { Upstream.OutputElementType, BoolType };

            return _predicateType = WithStruct
                ? StructFunctionInterfaceType(typeArguments)
                : FuncDelegateType(typeArguments);
        }
    }

    protected override ExpressionSyntax? SkipExpression => null;
    protected override ExpressionSyntax? TakeExpression => null;

    protected override TypeSyntax ReturnType => BoolType;

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (WithStruct)
            yield return new(TypeName("Predicate"), PredicateType);
    }

    protected override IEnumerable<ParameterInfo> GetParameterInfos()
    {
        yield return new ParameterInfo(
            WithStruct ? TypeName("Predicate") : PredicateType, IdentifierName("predicate"));
    }

    protected override IEnumerable<StatementSyntax> RenderAccumulation()
    {
        var condition = InvocationExpression(
            MemberAccessExpression(IdentifierName("predicate"), InvokeMethod), ArgumentList(CurrentPlaceholder));

        if (IsAll)
        {
            // All - if any predicate fails return false
            yield return IfStatement(LogicalNotExpression(condition), ReturnStatement(FalseExpression()));
        }
        else
        {
            // Any - if any predicate success return true
            yield return IfStatement(condition, ReturnStatement(TrueExpression()));
        }
    }

    protected override IEnumerable<StatementSyntax> RenderReturn()
    {
        if (IsAll)
        {
            // All - if all predicate success return true
            yield return ReturnStatement(TrueExpression());
        }
        else
        {
            // Any - if all predicate fails return false
            yield return ReturnStatement(FalseExpression());
        }
    }
}