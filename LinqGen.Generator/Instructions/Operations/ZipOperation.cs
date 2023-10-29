// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;

namespace Cathei.LinqGen.Generator;

public class ZipOperation : Operation
{
    private FunctionKind SelectorKind { get; }

    public ZipOperation(in LinqGenExpression expression, uint id, FunctionKind selectorKind) : base(expression, id)
    {
        SelectorKind = selectorKind;

        // IStub<IEnumerable<Element>, ...>
        var returnType = (INamedTypeSymbol)expression.MethodSymbol.ReturnType;
        var enumerableType = (INamedTypeSymbol)returnType.TypeArguments[0];
        OutputElementSymbol = enumerableType.TypeArguments[0];

        var tempThisPlaceholder = IdentifierName($"_zip_this_{Id}_");
        var tempIterPlaceholder = $"_zip_iter_{Id}_";

        TempRewriter = new(tempThisPlaceholder, tempIterPlaceholder);
        TempRevertRewriter = new(tempThisPlaceholder, ThisPlaceholder, tempIterPlaceholder, IterPlaceholder);
    }

    private ThisPlaceholderRewriter TempRewriter { get; }
    private ThisPlaceholderRewriter TempRevertRewriter { get; }

    public override bool SupportPartition => Upstreams.Any(x => x.SupportPartition);

    public override ITypeSymbol OutputElementSymbol { get; }

    private TypeSyntax? _outputElementType;

    public override TypeSyntax OutputElementType
    {
        get
        {
            if (_outputElementType != null)
                return _outputElementType;

            if (SelectorKind == FunctionKind.Default)
            {
                _outputElementType = TupleType(
                    SeparatedList(Upstreams.Select(t => TupleElement(t.OutputElementType))));
            }
            else
            {
                _outputElementType = ParseTypeName(OutputElementSymbol);
            }

            return _outputElementType;
        }
    }

    private TypeSyntax? _selectorInterfaceType;

    public TypeSyntax SelectorInterfaceType
    {
        get
        {
            if (_selectorInterfaceType != null)
                return _selectorInterfaceType;

            var typeArguments = Upstreams
                .Select(static x => x.OutputElementType)
                .Append(OutputElementType)
                .ToArray();

            return _selectorInterfaceType = SelectorKind switch
            {
                FunctionKind.Delegate => FuncDelegateType(typeArguments),
                FunctionKind.Struct => StructFunctionInterfaceType(typeArguments),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

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

        switch (SelectorKind)
        {
            case FunctionKind.Delegate:
                yield return new MemberInfo(MemberKind.Both, SelectorInterfaceType, LocalName("selector"));
                break;

            case FunctionKind.Struct:
                yield return new MemberInfo(MemberKind.Both, TypeName("Selector"), LocalName("selector"));
                break;
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

        if (SelectorKind == FunctionKind.Struct)
        {
            yield return new TypeParameterInfo(TypeName("Selector"), SelectorInterfaceType);
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
            var rewriter = GetRewriter(i, Member($"zip{i}"));

            // skip and take should be escaped as they are unrelated to upstream
            var statements = subUpstream.SupportPartition
                ? subUpstream.RenderInitialization(false,
                    (ExpressionSyntax?)TempRewriter.Visit(skipVar),
                    (ExpressionSyntax?)TempRewriter.Visit(takeVar))
                : subUpstream.RenderInitialization(false, null, null);

            foreach (var statement in statements)
                yield return (StatementSyntax)TempRevertRewriter.Visit(rewriter.Visit(statement));
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

            var subIteration = Upstreams[i].RenderIteration(false, new(successStatements))
                .AddStatements(ReturnStatement(FalseExpression()));

            var memberInfos = subUpstream.GetAllMemberInfos(MemberKind.Enumerator, false);
            var parameters = memberInfos
                .Select(x => Parameter(default, RefTokenList, x.Type,
                    Identifier($"{IterPlaceholder}{x.Name.Identifier.ValueText}"), null));

            var resolvedName = MakeGenericName(subUpstream.ClassName, GetSubTypeArguments(subUpstream));
            parameters = parameters
                .Prepend(Parameter(default, InTokenList, resolvedName, LocalName($"zip{i}").Identifier, null));

            var funcStatement = LocalFunctionStatement(default, default,
                BoolType, LocalName($"moveNext{i}").Identifier, null, ParameterList(parameters), default,
                subIteration, null);

            var rewriter = GetRewriter(i, LocalName($"zip{i}"));
            iterations.Add((StatementSyntax)rewriter.Visit(funcStatement));
        }

        iterations.AddRange(base.RenderIteration(isLocal, statements).Statements);

        return Block(iterations);
    }

    protected override StatementSyntax RenderMoveNext()
    {
        ExpressionSyntax? expression = null;

        for (int i = 1; i < Upstreams.Count; ++i)
        {
            var subUpstream = Upstreams[i];
            var memberInfos = subUpstream.GetAllMemberInfos(MemberKind.Enumerator, false);

            var arguments = memberInfos
                .Select(x => Argument(null, Token(SyntaxKind.RefKeyword),
                    IdentifierName($"{IterPlaceholder}{x.Name.Identifier.ValueText}")));

            arguments = arguments.Prepend(Argument(null, Token(SyntaxKind.InKeyword), ThisPlaceholder));

            var invocation = InvocationExpression(LocalName($"moveNext{i}"), ArgumentList(arguments));

            var rewriter = GetRewriter(i, Member($"zip{i}"));
            invocation = (InvocationExpressionSyntax)rewriter.Visit(invocation);

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

        if (SelectorKind == FunctionKind.Default)
        {
            return TupleExpression(SeparatedList(arguments));
        }
        else
        {
            return InvocationExpression(
                MemberAccessExpression(Member("selector"), InvokeMethod),
                ArgumentList(arguments));
        }
    }

    public override IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
    {
        foreach (var statement in base.RenderDispose(isLocal))
            yield return statement;

        for (int i = 1; i < Upstreams.Count; ++i)
        {
            var subUpstream = Upstreams[i];
            var rewriter = GetRewriter(i, Member($"zip{i}"));

            foreach (var statement in subUpstream.RenderDispose(false))
                yield return (StatementSyntax)rewriter.Visit(statement);
        }
    }

    private ThisPlaceholderRewriter GetRewriter(int index, ExpressionSyntax thisReplacement)
    {
        return new ThisPlaceholderRewriter(thisReplacement, $"{IterPlaceholder}s{Id}_{index}_");
    }
}