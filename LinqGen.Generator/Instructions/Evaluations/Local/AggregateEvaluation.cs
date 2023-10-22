// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class AggregateEvaluation : LocalEvaluation
{
    private TypeSyntax? SeedType { get; }
    private TypeSyntax SelectorType { get; }
    private bool WithStruct { get; }

    public AggregateEvaluation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
        if (MethodSymbol.Parameters.Length >= 2)
        {
            // Aggregate with seed
            SeedType = ParseTypeName(MethodSymbol.Parameters[0].Type);

            var selectorType = MethodSymbol.Parameters[1].Type;
            SelectorType = ParseTypeName(selectorType);
            WithStruct = IsStructFunction(selectorType);
        }
        else
        {
            // Aggregate without seed
            SeedType = null;

            var selectorType = MethodSymbol.Parameters[0].Type;
            SelectorType = ParseTypeName(selectorType);
            WithStruct = IsStructFunction(selectorType);
        }
    }

    protected override TypeSyntax ReturnType => SeedType ?? Upstream.OutputElementType;

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (WithStruct)
        {
            yield return new(TypeName("Selector"),
                StructFunctionInterfaceType(ReturnType, Upstream.OutputElementType, ReturnType));
        }
    }

    protected override IEnumerable<ParameterInfo> GetParameterInfos()
    {
        if (SeedType != null)
            yield return new(SeedType, IdentifierName("seed"));

        yield return new(WithStruct ? TypeName("Selector") : SelectorType, IdentifierName("selector"));
    }

    protected override IEnumerable<StatementSyntax> RenderInitialization()
    {
        if (SeedType != null)
        {
            yield return LocalDeclarationStatement(ReturnType, LocalName("result").Identifier, IdentifierName("seed"));
        }
        else
        {
            yield return LocalDeclarationStatement(BoolType, LocalName("isSet").Identifier, DefaultLiteral);
            yield return LocalDeclarationStatement(ReturnType, LocalName("result").Identifier, DefaultLiteral);
        }
    }

    protected override IEnumerable<StatementSyntax> RenderAccumulation()
    {
        if (SeedType == null)
        {
            yield return IfStatement(LogicalNotExpression(LocalName("isSet")),
                Block(
                    ExpressionStatement(SimpleAssignmentExpression(LocalName("isSet"), TrueExpression())),
                    ExpressionStatement(SimpleAssignmentExpression(LocalName("result"), CurrentPlaceholder))),
                ElseClause(
                    ExpressionStatement(SimpleAssignmentExpression(LocalName("result"),
                        InvocationExpression(MemberAccessExpression(IdentifierName("selector"), InvokeMethod),
                            ArgumentList(LocalName("result"), CurrentPlaceholder))))));
        }
        else
        {
            yield return ExpressionStatement(SimpleAssignmentExpression(LocalName("result"),
                InvocationExpression(MemberAccessExpression(IdentifierName("selector"), InvokeMethod),
                    ArgumentList(LocalName("result"), CurrentPlaceholder))));
        }
    }

    protected override IEnumerable<StatementSyntax> RenderReturn()
    {
        if (SeedType == null)
        {
            yield return IfStatement(LogicalNotExpression(LocalName("isSet")), ThrowInvalidOperationStatement());
        }

        yield return ReturnStatement(LocalName("result"));
    }
}
