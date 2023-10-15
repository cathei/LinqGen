// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public class TakeOperation : Operation
{
    public TakeOperation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
    }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield return new MemberInfo(MemberKind.Both, IntType, LocalName("take"));
        yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("index"));
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal,
        ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        ExpressionSyntax newTakeVar = Member("take");

        if (skipVar != null)
            newTakeVar = SubtractExpression(newTakeVar, skipVar);

        if (takeVar != null)
            newTakeVar = MathMin(newTakeVar, takeVar);

        if (SupportPartition && skipVar != null)
        {
            yield return ExpressionStatement(SimpleAssignmentExpression(
                Iterator("index"), SubtractExpression(skipVar, LiteralExpression(1))));
        }
        else
        {
            yield return ExpressionStatement(SimpleAssignmentExpression(
                Iterator("index"), LiteralExpression(-1)));
        }

        foreach (var statement in base.RenderInitialization(isLocal, skipVar, newTakeVar))
            yield return statement;
    }

    public override ExpressionSyntax? RenderCount()
    {
        var upstreamCount = Upstream.RenderCount();

        if (upstreamCount == null)
            return null;

        return MathMin(ParenthesizedExpression(upstreamCount), Member("take"));
    }

    protected override StatementSyntax? RenderMoveNext()
    {
        return IfStatement(
            GreaterOrEqualExpression(
                CastExpression(UIntType, PreIncrementExpression(Iterator("index"))),
                CastExpression(UIntType, Member("take"))),
            BreakStatement());
    }
}