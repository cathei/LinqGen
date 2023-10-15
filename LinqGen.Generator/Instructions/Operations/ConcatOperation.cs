// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;

namespace Cathei.LinqGen.Generator;

public class ConcatOperation : Operation
{
    private ThisPlaceholderRewriter SecondRewriter { get; }
    private ThisPlaceholderRewriter TempRewriter { get; }
    private ThisPlaceholderRewriter TempRevertRewriter { get; }

    public ConcatOperation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
        SecondRewriter = new ThisPlaceholderRewriter(Member("second"), $"{IterPlaceholder}s{Id}_");

        var tempThisPlaceholder = IdentifierName($"_concat_this_{Id}_");
        var tempIterPlaceholder = $"_concat_iter_{Id}_";

        TempRewriter = new(tempThisPlaceholder, tempIterPlaceholder);
        TempRevertRewriter = new(tempThisPlaceholder, ThisPlaceholder, tempIterPlaceholder, IterPlaceholder);
    }

    public Generation Second => Upstreams[1];

    private NameSyntax? _secondResolvedName;

    // TODO
    public override bool SupportPartition => false;

    public NameSyntax SecondResolvedName
    {
        get
        {
            if (_secondResolvedName != null)
                return _secondResolvedName;

            _secondResolvedName = MakeGenericName(Second.ClassName, GetSecondTypeArguments());
            return _secondResolvedName;
        }
    }

    public TypeArgumentListSyntax? GetSecondTypeArguments()
    {
        var parameters = Second.TypeParameters;

        if (parameters.Count == 0)
            return null;

        return TypeArgumentList(SeparatedList(parameters.Select(x =>
        {
            // replace with upstream type
            if (x.Name.IsEquivalentTo(Second.OutputElementType))
                return Upstream.OutputElementType;
            return x.AsTypeArgument();
        })));
    }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield return new MemberInfo(MemberKind.Enumerable, SecondResolvedName, LocalName("second"));

        if (!isLocal)
            yield return new MemberInfo(MemberKind.Enumerator, BoolType, LocalName("firstDone"));

        foreach (var member in Second.GetAllMemberInfos(MemberKind.Enumerator, isLocal))
        {
            var overridenName = IdentifierName($"s{Id}_{member.Name.Identifier.ValueText}");
            yield return new(member.Kind, member.Type, overridenName, member.DefaultValue);
        }
    }

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        foreach (var type in Second.TypeParameters)
        {
            if (type.Name.IsEquivalentTo(Second.OutputElementType))
                continue;

            yield return new TypeParameterInfo(type.Name, type.GenericConstraint?.Constraints.ToArray());
        }
    }

    public override ExpressionSyntax? RenderCount()
    {
        var firstCount = Upstream.RenderCount();
        var secondCount = Second.RenderCount();

        if (firstCount == null || secondCount == null)
            return null;

        secondCount = (ExpressionSyntax)SecondRewriter.Visit(secondCount);

        return AddExpression(ParenthesizedExpression(firstCount), ParenthesizedExpression(secondCount));
    }

    public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
    {
        statements = TempRewriter.VisitList(statements);

        // TODO: partition optimization (skip, take)
        var first = Upstream.RenderIteration(isLocal, statements);

        var secondInit = Block(Second.RenderInitialization(isLocal, null, null));
        secondInit = (BlockSyntax)SecondRewriter.Visit(secondInit);

        first = first.AddStatements(secondInit.Statements);

        var second = Second.RenderIteration(isLocal, statements);
        second = (BlockSyntax)SecondRewriter.Visit(second);

        BlockSyntax block;

        if (isLocal)
        {
            block = first.AddStatements(second.Statements);
        }
        else
        {
            block = Block(IfStatement(LogicalNotExpression(Iterator("firstDone")),
                first.AddStatements(ExpressionStatement(
                    SimpleAssignmentExpression(Iterator("firstDone"), TrueExpression())))));

            block = block.AddStatements(second.Statements);
        }

        return (BlockSyntax)TempRevertRewriter.Visit(block);
    }

    // protected override StatementSyntax? RenderMoveNext()
    // {
    //
    //
    // }

    public override IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
    {
        foreach (var statement in base.RenderDispose(isLocal))
            yield return statement;

        foreach (var statement in Second.RenderDispose(isLocal))
            yield return (StatementSyntax)SecondRewriter.Visit(statement);
    }
}