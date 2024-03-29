// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public class TakeLastOperation : Operation
{
    public TakeLastOperation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
    }

    public TypeSyntax ElementQueueType => PooledQueueType(OutputElementType, OutputElementSymbol.IsUnmanagedType);

    public override bool SupportPartition => true;

    public override ExpressionSyntax? RenderCount()
    {
        var upstreamCount = Upstream.RenderCount();

        if (upstreamCount == null)
            return null;

        return MathMin(ParenthesizedExpression(upstreamCount), Member("take"));
    }

    private bool? _easyPath;

    // Easy path, upstream supports partition and we know count
    public bool EasyPath => _easyPath ??= Upstream is { SupportPartition: true, SupportCount: true };

    protected override bool ClearsUpstreamEnumerator => !EasyPath;

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        if (EasyPath)
        {
            yield return new MemberInfo(MemberKind.Both, IntType, LocalName("take"));
        }
        else
        {
            yield return new MemberInfo(MemberKind.Enumerable, IntType, LocalName("take"));

            yield return new MemberInfo(MemberKind.Enumerator, ElementQueueType, LocalName("elements"),
                ObjectCreationExpression(ElementQueueType, ArgumentList(LiteralExpression(0)), null));

            yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("index"));
        }
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        if (EasyPath)
        {
            var upstreamCount = Upstream.RenderCount()!;
            ExpressionSyntax takeLastVar = Member("take");

            if (skipVar != null)
                takeLastVar = ParenthesizedExpression(SubtractExpression(takeLastVar, skipVar));

            // skip = count - (takeLast - skip))
            skipVar = SubtractExpression(ParenthesizedExpression(upstreamCount), takeLastVar);

            // take = Min(take, takeLast - skip)
            if (takeVar != null)
                takeVar = MathMin(takeLastVar, takeVar);
            else
                takeVar = takeLastVar;

            return base.RenderInitialization(isLocal, skipVar, takeVar);
        }

        var contextName = $"{IterPlaceholder}c{Id}_";
        var initStatements = new List<StatementSyntax>();

        initStatements.AddRange(Upstream.GetLocalDeclarations(MemberKind.Enumerator));
        initStatements.AddRange(Upstream.RenderInitialization(true, null, null));

        var elementsName = LocalName("localElements");

        // create queue
        initStatements.Add(LocalDeclarationStatement(elementsName.Identifier,
            ObjectCreationExpression(ElementQueueType, ArgumentList(Member("take")), null)));

        var enqueueStatements = SingletonList<StatementSyntax>(
            ExpressionStatement(InvocationExpression(
                MemberAccessExpression(elementsName, EnqueueMethod), ArgumentList(CurrentPlaceholder))));

        initStatements.AddRange(Upstream.RenderIteration(true, enqueueStatements).Statements);

        // TODO try block
        initStatements.AddRange(Upstream.RenderDispose(true));

        // keep this placeholder but replace context
        var thisRewriter = new ThisPlaceholderRewriter(ThisPlaceholder, contextName);
        for (int i = 0; i < initStatements.Count; ++i)
            initStatements[i] = (StatementSyntax)thisRewriter.Visit(initStatements[i]);

        // lastly access to iterator
        // initialize elements
        initStatements.Add(ExpressionStatement(SimpleAssignmentExpression(
            Iterator("elements"), elementsName)));

        // initialize index
        initStatements.Add(ExpressionStatement(SimpleAssignmentExpression(
            Iterator("index"), SubtractExpression(skipVar ?? LiteralExpression(0), LiteralExpression(1)))));

        return initStatements;
    }

    public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
    {
        if (EasyPath)
        {
            return base.RenderIteration(isLocal, statements);
        }

        var currentName = LocalName("current");
        var currentRewriter = new CurrentPlaceholderRewriter(currentName);

        // replace current variables of downstream
        statements = currentRewriter.VisitList(statements);

        statements = statements.InsertRange(0, new StatementSyntax[]
        {
            // for optimal JIT compile it needs to be inside of while, not the condition
            IfStatement(GreaterOrEqualExpression(
                    CastExpression(UIntType, PreIncrementExpression(Iterator("index"))),
                    CastExpression(UIntType, MemberAccessExpression(Iterator("elements"), CountProperty))),
                BreakStatement()),
            // set current
            LocalDeclarationStatement(
                currentName.Identifier, ElementAccessExpression(Iterator("elements"), Iterator("index")))
        });

        var result = WhileStatement(TrueExpression(), Block(statements));

        return Block(result);
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
