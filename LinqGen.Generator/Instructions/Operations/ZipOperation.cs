// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;

namespace Cathei.LinqGen.Generator;

public class ZipOperation : Operation
{
    public ZipOperation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
        // IStub<IEnumerable<Element>, ...>
        var returnType = (INamedTypeSymbol)expression.MethodSymbol.ReturnType;
        var enumerableType = (INamedTypeSymbol)returnType.TypeArguments[0];
        OutputElementSymbol = enumerableType.TypeArguments[0];
    }

    public override bool SupportPartition => Upstreams.Any(x => x.SupportPartition);

    public override ITypeSymbol OutputElementSymbol { get; }

    private TypeSyntax? _outputElementType;

    public override TypeSyntax OutputElementType => _outputElementType ??= TupleType(
        SeparatedList(Upstreams.Select(t => TupleElement(t.OutputElementType))));

    private TypeArgumentListSyntax? GetSubTypeArguments(Generation subUpstream)
    {
        var parameters = subUpstream.TypeParameters;

        if (parameters.Count == 0)
            return null;

        return TypeArgumentList(SeparatedList(parameters.Select(x =>
        {
            // replace with upstream type
            if (x.Name.IsEquivalentTo(subUpstream.OutputElementType))
                return Upstream.OutputElementType;
            return x.AsTypeArgument();
        })));
    }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        for (int i = 1; i < Upstreams.Count; ++i)
        {
            var subUpstream = Upstreams[i];

            var resolvedName = MakeGenericName(subUpstream.ClassName, GetSubTypeArguments(subUpstream));
            yield return new MemberInfo(MemberKind.Enumerable, resolvedName, LocalName($"zip{i}"));

            foreach (var member in subUpstream.GetAllMemberInfos(MemberKind.Enumerator, false))
            {
                var overridenName = IdentifierName($"s{Id}_{i}_{member.Name.Identifier.ValueText}");
                yield return new(member.Kind, member.Type, overridenName, member.DefaultValue);
            }
        }
    }

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        for (int i = 1; i < Upstreams.Count; ++i)
        {
            var subUpstream = Upstreams[i];

            foreach (var type in subUpstream.TypeParameters)
            {
                if (type.Name.IsEquivalentTo(subUpstream.OutputElementType))
                    continue;

                yield return new TypeParameterInfo(type.Name, type.GenericConstraint?.Constraints.ToArray());
            }
        }
    }

    public override ExpressionSyntax? RenderCount()
    {
        ExpressionSyntax? countSyntax = Upstream.RenderCount();

        if (countSyntax == null)
            return null;

        for (int i = 1; i < Upstreams.Count; ++i)
        {
            var subUpstream = Upstreams[i];

            ExpressionSyntax? subCountSyntax = subUpstream.RenderCount();

            if (subCountSyntax == null)
                return null;

            countSyntax = MathMin(countSyntax, subCountSyntax);
        }

        return countSyntax;
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        foreach (var statement in base.RenderInitialization(isLocal, skipVar, takeVar))
            yield return statement;

        for (int i = 1; i < Upstreams.Count; ++i)
        {
            var subUpstream = Upstreams[i];
            var rewriter = GetRewriter(i);

            var statements = subUpstream.SupportPartition
                ? subUpstream.RenderInitialization(false, skipVar, takeVar)
                : subUpstream.RenderInitialization(false, null, null);

            foreach (var statement in statements)
                yield return (StatementSyntax)rewriter.Visit(statement);
        }
    }

    public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
    {
        var iterations = new List<StatementSyntax>();

        for (int i = 1; i < Upstreams.Count; ++i)
        {
            var subUpstream = Upstreams[i];

            iterations.Add(LocalDeclarationStatement(
                subUpstream.OutputElementType, LocalName($"current{i}").Identifier, DefaultLiteral));

            var successStatements = new StatementSyntax[]
            {
                ExpressionStatement(SimpleAssignmentExpression(
                    LocalName($"current{i}"), CurrentPlaceholder)),
                ReturnStatement(TrueExpression())
            };

            var rewriter = GetRewriter(i);

            var subIteration = Upstreams[i].RenderIteration(false, new(successStatements))
                .AddStatements(ReturnStatement(FalseExpression()));

            subIteration = (BlockSyntax)rewriter.Visit(subIteration);

            iterations.Add(LocalFunctionStatement(default, default,
                BoolType, LocalName($"moveNext{i}").Identifier, null, EmptyParameterList, default,
                subIteration, null));
        }

        iterations.AddRange(base.RenderIteration(isLocal, statements).Statements);

        return Block(iterations);
    }

    protected override StatementSyntax RenderMoveNext()
    {
        ExpressionSyntax? expression = null;

        for (int i = 1; i < Upstreams.Count; ++i)
        {
            var invocation = InvocationExpression(LocalName($"moveNext{i}"));

            if (expression == null)
                expression = invocation;
            else
                expression = LogicalAndExpression(expression, invocation);
        }

        return IfStatement(
            LogicalNotExpression(ParenthesizedExpression(expression!)),
            BreakStatement());
    }

    protected override ExpressionSyntax? RenderCurrent()
    {
        var arguments = new List<ArgumentSyntax>
        {
            Argument(CurrentPlaceholder)
        };

        for (int i = 1; i < Upstreams.Count; ++i)
        {
            arguments.Add(Argument(LocalName($"current{i}")));
        }

        // if tuple...
        return TupleExpression(SeparatedList(arguments));
    }

    public override IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
    {
        foreach (var statement in base.RenderDispose(isLocal))
            yield return statement;

        for (int i = 1; i < Upstreams.Count; ++i)
        {
            var subUpstream = Upstreams[i];
            var rewriter = GetRewriter(i);

            foreach (var statement in subUpstream.RenderDispose(false))
                yield return (StatementSyntax)rewriter.Visit(statement);
        }
    }

    private ThisPlaceholderRewriter GetRewriter(int index)
    {
        return new ThisPlaceholderRewriter(Member($"zip{index}"), $"{IterPlaceholder}s{Id}_{index}_");
    }
}