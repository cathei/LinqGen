// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class FirstEvaluation : LocalEvaluation
{
    private bool OrDefault { get; }

    public FirstEvaluation(in LinqGenExpression expression, uint id, bool orDefault) : base(expression, id)
    {
        OrDefault = orDefault;
    }

    protected override ExpressionSyntax? SkipExpression => null;
    protected override ExpressionSyntax TakeExpression => LiteralExpression(1);

    protected override TypeSyntax ReturnType => Upstream.OutputElementType;

    protected override IEnumerable<StatementSyntax> RenderAccumulation()
    {
        yield return ReturnStatement(CurrentPlaceholder);
    }

    protected override IEnumerable<StatementSyntax> RenderReturn()
    {
        if (!OrDefault)
            yield return ThrowInvalidOperationStatement();

        yield return ReturnStatement(DefaultLiteral);
    }
}