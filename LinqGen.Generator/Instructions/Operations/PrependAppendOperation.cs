// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public class PrependAppendOperation : Operation
{
    private bool IsAppend { get; }

    public PrependAppendOperation(in LinqGenExpression expression, uint id, bool isAppend) : base(expression, id)
    {
        IsAppend = isAppend;
    }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield return new MemberInfo(MemberKind.Enumerable, OutputElementType, LocalName("element"));
        if (!isLocal)
            yield return new MemberInfo(MemberKind.Enumerator, BoolType, LocalName("processed"));
    }

    // TODO Support append as well
    public override bool SupportPartition => Upstream.SupportPartition && !IsAppend;

    public override IEnumerable<StatementSyntax> RenderInitialization(
        bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        if (skipVar != null && !IsAppend)
        {
            if (!isLocal)
            {
                yield return IfStatement(GreaterOrEqualExpression(skipVar, LiteralExpression(1)),
                    ExpressionStatement(SimpleAssignmentExpression(Iterator("processed"), TrueExpression())));
            }

            skipVar = MathMax(SubtractExpression(
                ParenthesizedExpression(skipVar), LiteralExpression(1)), LiteralExpression(0));
        }

        foreach (var statement in base.RenderInitialization(isLocal, skipVar, takeVar))
            yield return statement;
    }

    public override ExpressionSyntax? RenderCount()
    {
        var count = Upstream.RenderCount();

        if (count == null)
            return null;

        return AddExpression(ParenthesizedExpression(count), LiteralExpression(1));
    }

    public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
    {
        // TODO: partition optimization (skip, take)
        var iteration = Upstream.RenderIteration(isLocal, statements);

        var currentRewriter = new CurrentPlaceholderRewriter(Member("element"));
        var additionStatements = currentRewriter.VisitList(statements);

        BlockSyntax block;
        StatementSyntax addition;

        if (isLocal)
        {
            addition = DoStatement(Block(additionStatements), FalseExpression());
        }
        else
        {
            // mark as processed first, so return can happen
            addition = WhileStatement(
                LogicalNotExpression(Iterator("processed")), Block(additionStatements.Insert(0,
                    ExpressionStatement(SimpleAssignmentExpression(Iterator("processed"), TrueExpression())))));
        }

        if (IsAppend)
        {
            block = iteration.AddStatements(addition);
        }
        else
        {
            block = iteration.WithStatements(iteration.Statements.Insert(0, addition));
        }

        return block;
    }
}