namespace Cathei.LinqGen.Generator;

public sealed class SingleEvaluation : LocalEvaluation {
    private bool OrDefault { get; }

    public SingleEvaluation(in LinqGenExpression expression, uint id, bool orDefault) : base(expression, id)
    {
        OrDefault = orDefault;
    }

    protected override TypeSyntax ReturnType => Upstream.OutputElementType;

    protected override ExpressionSyntax TakeExpression => LiteralExpression(2);

    protected override IEnumerable<StatementSyntax> RenderInitialization() {
        if (Upstream.SupportCount) {
            if (!OrDefault)
                yield return IfStatement(BinaryExpression(SyntaxKind.EqualsExpression,
                                                          Upstream.RenderCount()!,
                                                          LiteralExpression(0)), Block(SingletonList(ThrowInvalidOperationStatement("Sequence contains no elements"))));
            yield return IfStatement(GreaterOrEqualExpression(Upstream.RenderCount()!, LiteralExpression(2)), Block(SingletonList(ThrowInvalidOperationStatement("Sequence contains more than one element"))));
        } else {
            yield return LocalDeclarationStatement(LocalName("count").Identifier, LiteralExpression(0));
            yield return LocalDeclarationStatement(ReturnType, LocalName("result").Identifier, DefaultLiteral);
        }
    }

    protected override IEnumerable<StatementSyntax> RenderAccumulation() {
        if (Upstream.SupportCount) {
            yield return ReturnStatement(CurrentPlaceholder);
        } else {
            yield return IfStatement(GreaterOrEqualExpression(PrefixUnaryExpression(SyntaxKind.PreIncrementExpression, LocalName("count")), LiteralExpression(2)), Block(SingletonList(ThrowInvalidOperationStatement("Sequence contains more than one element"))));
            yield return ExpressionStatement(SimpleAssignmentExpression(LocalName("result"), CurrentPlaceholder));
        }
    }

    protected override IEnumerable<StatementSyntax> RenderReturn() {
        if (Upstream.SupportCount) {
            yield return ReturnStatement(DefaultLiteral);
            yield break;
        }
        if (!OrDefault)
            yield return IfStatement(BinaryExpression(SyntaxKind.EqualsExpression,
                                                      LocalName("count"),
                                                      LiteralExpression(0)),
                                     ThrowInvalidOperationStatement("Sequence contains no elements"));
        yield return ReturnStatement(LocalName("result"));
    }
}