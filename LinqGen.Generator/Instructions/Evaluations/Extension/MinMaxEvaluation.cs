// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class MinMaxEvaluation : ExtensionEvaluation
{
    private bool IsMin { get; }

    private FunctionKind KeySelectorKind { get; }
    private ComparerKind ComparerKind { get; }

    private TypeSyntax KeyType { get; }
    private ITypeSymbol KeySymbol { get; }

    public MinMaxEvaluation(in LinqGenExpression expression, uint id, bool isMin, bool withKey)
        : base(expression, id)
    {
        IsMin = isMin;

        if (withKey)
        {
            var parameterType = expression.GetNamedParameterType(0);

            // Func<TIn, TOut> or IStructFunction<TIn, TOut>
            KeySymbol = parameterType.TypeArguments[1];
            KeyType = ParseTypeName(KeySymbol);

            if (IsStructFunction(parameterType))
                KeySelectorKind = FunctionKind.Struct;
            else
                KeySelectorKind = FunctionKind.Delegate;

            if (expression.MethodSymbol.Parameters.Length == 2)
                ComparerKind = ComparerKind.Struct;
            else
                ComparerKind = ComparerKind.Default;
        }
        else
        {
            KeyType = InputElementType;
            KeySymbol = InputElementSymbol;

            KeySelectorKind = FunctionKind.Default;

            if (expression.MethodSymbol.Parameters.Length == 1)
                ComparerKind = ComparerKind.Struct;
            else
                ComparerKind = ComparerKind.Default;
        }

    }

    protected override TypeSyntax ReturnType => InputElementType;

    private TypeSyntax ComparerInterfaceType =>
        GenericName(Identifier("IComparer"), TypeArgumentList(InputElementType));

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (KeySelectorKind == FunctionKind.Struct)
            yield return new(TypeName("Selector"), StructFunctionInterfaceType(InputElementType, KeyType));

        if (ComparerKind == ComparerKind.Struct)
            yield return new(TypeName("Comparer"), ComparerInterfaceType);
    }

    protected override IEnumerable<ParameterInfo> GetParameterInfos()
    {
        if (KeySelectorKind != FunctionKind.Default)
        {
            yield return new(KeySelectorKind == FunctionKind.Delegate
                ? FuncDelegateType(InputElementType, KeyType)
                : TypeName("Selector"), IdentifierName("selector"));
        }

        if (ComparerKind == ComparerKind.Struct)
            yield return new(TypeName("Comparer"), IdentifierName("comparer"));
    }

    protected override IEnumerable<StatementSyntax> RenderInitialization()
    {
        if (KeySelectorKind != FunctionKind.Default)
            yield return LocalDeclarationStatement(KeyType, LocalName("resultKey").Identifier, DefaultLiteral);

        if (ComparerKind == ComparerKind.Default)
        {
            yield return LocalDeclarationStatement(ComparerDefaultType(KeyType, KeySymbol),
                IdentifierName("comparer").Identifier, ComparerDefault(KeyType, KeySymbol));
        }

        if (!IsNullAssignable(InputElementSymbol))
        {
            yield return LocalDeclarationStatement(BoolType, LocalName("isSet").Identifier, DefaultLiteral);
        }

        yield return LocalDeclarationStatement(ReturnType, LocalName("result").Identifier, DefaultLiteral);
    }

    protected override IEnumerable<StatementSyntax> RenderAccumulation()
    {
        var resultSetBlock = new List<StatementSyntax>();

        if (!IsNullAssignable(InputElementSymbol))
        {
            resultSetBlock.Add(ExpressionStatement(SimpleAssignmentExpression(LocalName("isSet"), TrueExpression())));
        }

        resultSetBlock.Add(ExpressionStatement(SimpleAssignmentExpression(LocalName("result"), CurrentPlaceholder)));

        ExpressionSyntax key, resultKey;

        if (KeySelectorKind == FunctionKind.Default)
        {
            key = CurrentPlaceholder;
            resultKey = LocalName("result");
        }
        else
        {
            yield return LocalDeclarationStatement(LocalName("key").Identifier,
                InvocationExpression(MemberAccessExpression(IdentifierName("selector"), InvokeMethod),
                    ArgumentList(CurrentPlaceholder)));

            key = LocalName("key");
            resultKey = LocalName("resultKey");

            resultSetBlock.Add(ExpressionStatement(SimpleAssignmentExpression(resultKey, key)));
        }

        ExpressionSyntax comparison = BinaryExpression(
            IsMin ? SyntaxKind.LessThanExpression : SyntaxKind.GreaterThanExpression,
            LiteralExpression(0),
            InvocationExpression(MemberAccessExpression(IdentifierName("comparer"), CompareMethod),
                ArgumentList(resultKey, key)));

        if (IsNullAssignable(KeySymbol))
        {
            comparison = LogicalAndExpression(IsNotExpression(key, NullLiteral), comparison);
        }

        if (IsNullAssignable(InputElementSymbol))
        {
            comparison = LogicalOrExpression(IsExpression(LocalName("result"), NullLiteral), comparison);
        }
        else
        {
            comparison = LogicalOrExpression(LogicalNotExpression(LocalName("isSet")), comparison);
        }

        yield return IfStatement(comparison, Block(resultSetBlock));
    }

    protected override IEnumerable<StatementSyntax> RenderReturn()
    {
        if (!IsNullAssignable(InputElementSymbol))
        {
            yield return IfStatement(LogicalNotExpression(LocalName("isSet")), ThrowInvalidOperationStatement());
        }

        yield return ReturnStatement(LocalName("result"));
    }
}