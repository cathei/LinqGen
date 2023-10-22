// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public class SkipLastOperation : Operation
{
    public SkipLastOperation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
    }

    public TypeSyntax ElementQueueType => PooledQueueType(OutputElementType, OutputElementSymbol.IsUnmanagedType);

    public override ExpressionSyntax? RenderCount()
    {
        var upstreamCount = Upstream.RenderCount();

        if (upstreamCount == null)
            return null;

        return SubtractExpression(ParenthesizedExpression(upstreamCount), Member("skip"));
    }

    private bool? _easyPath;

    // Easy path, upstream supports partition and we know count
    public bool EasyPath => _easyPath ??= Upstream is { SupportPartition: true, SupportCount: true };

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield return new MemberInfo(MemberKind.Enumerable, IntType, LocalName("skip"));

        if (EasyPath)
        {
            yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("take"));
        }
        else
        {
            yield return new MemberInfo(MemberKind.Enumerator, ElementQueueType, LocalName("elements"),
                ObjectCreationExpression(ElementQueueType, ArgumentList(LiteralExpression(0)), null));
        }
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        var initStatements = new List<StatementSyntax>();

        if (EasyPath)
        {
            var upstreamCount = Upstream.RenderCount()!;
            ExpressionSyntax tempVar = Member("skip");

            if (skipVar != null)
                tempVar = ParenthesizedExpression(AddExpression(tempVar, skipVar));

            tempVar = SubtractExpression(ParenthesizedExpression(upstreamCount), tempVar);

            // take = Min(take, count - (skipLast + skip))
            if (takeVar != null)
                takeVar = MathMin(tempVar, takeVar);
            else
                takeVar = tempVar;

            initStatements.AddRange(base.RenderInitialization(isLocal, skipVar, takeVar));
            initStatements.Add(ExpressionStatement(SimpleAssignmentExpression(Iterator("take"),
                AddExpression(takeVar, LiteralExpression(1)))));
        }
        else
        {
            initStatements.AddRange(base.RenderInitialization(isLocal, skipVar, takeVar));
            initStatements.Add(ExpressionStatement(SimpleAssignmentExpression(Iterator("elements"),
                ObjectCreationExpression(ElementQueueType, ArgumentList(
                    AddExpression(Member("skip"), LiteralExpression(1))), null))));
        }

        return initStatements;
    }

    protected override StatementSyntax? RenderMoveNext()
    {
        if (EasyPath)
        {
            return IfStatement(
                GreaterOrEqualExpression(LiteralExpression(0), PreDecrementExpression(Iterator("take"))),
                BreakStatement());
        }

        return IfStatement(LogicalNotExpression(
                InvocationExpression(MemberAccessExpression(Iterator("elements"), EnqueueMethod),
                    ArgumentList(CurrentPlaceholder))),
            ContinueStatement());
    }

    protected override ExpressionSyntax? RenderCurrent()
    {
        if (EasyPath)
        {
            return base.RenderCurrent();
        }

        return InvocationExpression(Iterator("elements"), DequeueMethod);
    }

    public override IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
    {
        if (EasyPath)
        {
            return base.RenderDispose(isLocal);
        }

        return new StatementSyntax[]
        {
            ExpressionStatement(InvocationExpression(Iterator("elements"), DisposeMethod))
        };
    }
}
