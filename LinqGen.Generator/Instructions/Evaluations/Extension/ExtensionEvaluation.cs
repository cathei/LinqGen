// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;

namespace Cathei.LinqGen.Generator;

/// <summary>
/// Visitor evaluation is used for extension methods to not expose private variables.
/// </summary>
public abstract class ExtensionEvaluation : Evaluation
{
    protected ExtensionEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
    {
        InputElementSymbol = expression.InputElementSymbol!;
        InputElementType = ParseTypeName(InputElementSymbol);
    }

    public ITypeSymbol InputElementSymbol { get; }
    protected override TypeSyntax InputElementType { get; }

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

    protected ParameterListSyntax GetParameters(bool extensionMethod)
    {
        var parameters = GetParameterInfos()
            .Select(x => x.AsParameter(extensionMethod));

        var sourceParameter =
            Parameter(UpstreamResolvedClassName, Identifier("source"));

        if (extensionMethod)
        {
            // here we intentionally value copy for locality
            sourceParameter = sourceParameter.WithModifiers(ThisTokenList);
        }

        return ParameterList(parameters.Prepend(sourceParameter));
    }

    public override IEnumerable<MemberDeclarationSyntax> RenderExtensionMembers()
    {
        yield return MethodDeclaration(
            SingletonList(AggressiveInliningAttributeList), PublicStaticTokenList,
            ReturnType, null, MethodName.Identifier, GetTypeParameters(Arity), GetParameters(true),
            GetGenericConstraints(Arity), RenderBody(), null, default);
    }

    private BlockSyntax RenderBody()
    {
        var sourceRewriter = new ThisPlaceholderRewriter(IdentifierName("source"), string.Empty);

        var initialDeclarations = Upstream.GetLocalDeclarations(MemberKind.Enumerator)
            .Concat(Upstream.RenderInitialization(true, null, null))
            .Concat(RenderInitialization());

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

        return (BlockSyntax)sourceRewriter.Visit(body);
    }
}