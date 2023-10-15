// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class NotEmptyEvaluation : LocalEvaluation
{
    public NotEmptyEvaluation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
    }

    protected override ExpressionSyntax? SkipExpression => null;
    protected override ExpressionSyntax TakeExpression => LiteralExpression(1);

    protected override TypeSyntax ReturnType => BoolType;

    protected override IEnumerable<StatementSyntax> RenderAccumulation()
    {
        yield return ReturnStatement(TrueExpression());
    }

    protected override IEnumerable<StatementSyntax> RenderReturn()
    {
        yield return ReturnStatement(FalseExpression());
    }
}