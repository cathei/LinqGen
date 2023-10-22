// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class SumEvaluation : ExtensionEvaluation
{
    private TypeSyntax? SelectorType { get; }
    private bool WithStruct { get; }

    public SumEvaluation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
        ReturnType = ParseTypeName(MethodSymbol.ReturnType);

        if (MethodSymbol.Parameters.Length >= 1)
        {
            // Sum with a parameter uses selector
            var parameterType = MethodSymbol.Parameters[0].Type;

            SelectorType = ParseTypeName(parameterType);
            WithStruct = IsStructFunction(parameterType);
        }
        else
        {
            // and single parameter only has default value
            SelectorType = null;
            WithStruct = false;
        }
    }

    protected override TypeSyntax ReturnType { get; }

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (WithStruct)
            yield return new(TypeName("Selector"), SelectorType!);
    }

    protected override IEnumerable<ParameterInfo> GetParameterInfos()
    {
        if (SelectorType != null)
        {
            yield return new ParameterInfo(
                WithStruct ? TypeName("Selector") : SelectorType, IdentifierName("selector"));
        }
    }

    protected override IEnumerable<StatementSyntax> RenderInitialization()
    {
        yield return LocalDeclarationStatement(ReturnType, LocalName("result").Identifier, DefaultLiteral);
    }

    protected override IEnumerable<StatementSyntax> RenderAccumulation()
    {
        ExpressionSyntax value = CurrentPlaceholder;

        if (SelectorType != null)
        {
            value = InvocationExpression(
                MemberAccessExpression(IdentifierName("selector"), InvokeMethod),
                ArgumentList(CurrentPlaceholder));
        }

        yield return ExpressionStatement(AddAssignmentExpression(LocalName("result"), value));
    }

    protected override IEnumerable<StatementSyntax> RenderReturn()
    {
        yield return ReturnStatement(LocalName("result"));
    }
}
