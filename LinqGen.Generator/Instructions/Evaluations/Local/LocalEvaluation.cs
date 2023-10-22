// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;

namespace Cathei.LinqGen.Generator;

public abstract class LocalEvaluation : Evaluation
{
    public LocalEvaluation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
    }

    protected virtual ExpressionSyntax? SkipExpression => null;
    protected virtual ExpressionSyntax? TakeExpression => null;

    protected abstract IEnumerable<StatementSyntax> RenderAccumulation();
    protected abstract IEnumerable<StatementSyntax> RenderReturn();

    protected virtual IEnumerable<StatementSyntax> RenderInitialization()
    {
        yield break;
    }

    protected virtual IEnumerable<StatementSyntax> RenderDispose()
    {
        yield break;
    }

    protected abstract TypeSyntax ReturnType { get; }

    protected virtual IEnumerable<ParameterInfo> GetParameterInfos()
    {
        yield break;
    }

    protected ParameterListSyntax GetParameters()
    {
        return ParameterList(GetParameterInfos().Select(x => x.AsParameter(true)));
    }

    public override IEnumerable<MemberDeclarationSyntax> RenderUpstreamMembers()
    {
        var arityDiff = Arity - Upstream.Arity;

        yield return MethodDeclaration(
            SingletonList(AggressiveInliningAttributeList), PublicTokenList,
            ReturnType, null, MethodName.Identifier, GetTypeParameters(arityDiff), GetParameters(),
            GetGenericConstraints(arityDiff), RenderBody(), null, default);
    }

    private BlockSyntax RenderBody()
    {
        var supportPartition = Upstream.SupportPartition;
        var skipVar = supportPartition ? SkipExpression : null;
        var takeVar = supportPartition ? TakeExpression : null;

        var copyName = IdentifierName("copy");
        var copyRewriter = new ThisPlaceholderRewriter(copyName, string.Empty);

        var initialDeclarations = new List<StatementSyntax>
        {
            LocalDeclarationStatement(copyName.Identifier, ThisExpression()),
        };

        initialDeclarations.AddRange(Upstream.GetLocalDeclarations(MemberKind.Enumerator));
        initialDeclarations.AddRange(Upstream.RenderInitialization(true, skipVar, takeVar));
        initialDeclarations.AddRange(RenderInitialization());

        var accumulationStatements = RenderAccumulation();

        var iterationBlock = Upstream.RenderIteration(true, List(accumulationStatements));

        var disposeBlock = Block(Upstream.RenderDispose(true).Concat(RenderDispose()));

        var iterationStatements = iterationBlock.Statements;
        iterationStatements = iterationStatements.AddRange(RenderReturn());

        BlockSyntax body;

        if (disposeBlock.Statements.Count == 0)
        {
            body = Block(initialDeclarations.Concat(iterationStatements));
        }
        else
        {
            StatementSyntax tryStatement = TryStatement(
                Block(iterationStatements), default, FinallyClause(disposeBlock));

            body = Block(initialDeclarations.Append(tryStatement));
        }

        return (BlockSyntax)copyRewriter.Visit(body);
    }
}