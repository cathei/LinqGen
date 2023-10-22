// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;

namespace Cathei.LinqGen.Generator;

public abstract class OrderingOperation : Operation
{
    private FunctionKind SelectorKind { get; }
    private ComparerKind ComparerKind { get; }
    public bool Descending { get; }

    public OrderingOperation(in LinqGenExpression expression, uint id,
        FunctionKind selectorKind, ComparerKind comparerKind, bool descending) : base(expression, id)
    {
        SelectorKind = selectorKind;
        ComparerKind = comparerKind;
        Descending = descending;

        if (selectorKind != FunctionKind.Default)
        {
            expression.TryGetNamedParameterType(0, out var selectorType);

            SelectorInterfaceType = ParseTypeName(selectorType);
            SelectorKeySymbol = selectorType.TypeArguments[1];
            SelectorKeyType = ParseTypeName(SelectorKeySymbol);
        }
        else
        {
            SelectorInterfaceType = null;
            SelectorKeySymbol = null;
            SelectorKeyType = null;
        }
    }

    /// <summary>
    /// Upstream ordering chained with ThenBy.
    /// </summary>
    protected virtual OrderingOperation? UpstreamOrder => null;

    protected OrderingOperation RootOrder => UpstreamOrder?.RootOrder ?? this;

    private TypeSyntax? SelectorInterfaceType { get; }
    private ITypeSymbol? SelectorKeySymbol { get; }
    private TypeSyntax? SelectorKeyType { get; }

    private TypeSyntax KeyType =>
        SelectorKind == FunctionKind.Default ? OutputElementType : SelectorKeyType!;

    private ITypeSymbol KeySymbol =>
        SelectorKind == FunctionKind.Default ? OutputElementSymbol : SelectorKeySymbol!;

    private TypeSyntax ComparerInterfaceType =>
        GenericName(Identifier("IComparer"), TypeArgumentList(KeyType));

    public override TypeSyntax EnumerableInterfaceType =>
        GenericName(Identifier("IInternalOrderedStub"), TypeArgumentList(OutputElementType));

    public TypeSyntax ElementListType => PooledListType(OutputElementType, OutputElementSymbol.IsUnmanagedType);

    public TypeSyntax IndexListType => PooledListType(IntType, true);

    public override TypeSyntax? DummyParameterType
    {
        get
        {
            // parameter can collide in this case
            if (SelectorKind == FunctionKind.Struct)
                return SelectorKeyType;
            return null;
        }
    }

    protected override bool ClearsUpstreamEnumerator => true;

    public override bool SupportPartition => true;

    public override ExpressionSyntax? RenderCount()
    {
        return Upstream.RenderCount();
    }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield return new MemberInfo(MemberKind.Enumerator, ElementListType, LocalName("elements"),
            ObjectCreationExpression(ElementListType, ArgumentList(LiteralExpression(0)), null));

        yield return new MemberInfo(MemberKind.Enumerator, IndexListType, LocalName("indices"),
            ObjectCreationExpression(IndexListType, ArgumentList(LiteralExpression(0)), null));

        yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("index"));

        switch (SelectorKind)
        {
            case FunctionKind.Delegate:
                yield return new MemberInfo(MemberKind.Enumerable, SelectorInterfaceType!, LocalName("selector"));
                break;

            case FunctionKind.Struct:
                yield return new MemberInfo(MemberKind.Enumerable, TypeName("Selector"), LocalName("selector"));
                break;
        }

        switch (ComparerKind)
        {
            case ComparerKind.Interface:
                yield return new MemberInfo(MemberKind.Enumerable, ComparerInterfaceType, LocalName("comparer"));
                break;

            case ComparerKind.Struct:
                yield return new MemberInfo(MemberKind.Enumerable, TypeName("Comparer"), LocalName("comparer"));
                break;
        }
    }

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (SelectorKind == FunctionKind.Struct)
        {
            yield return new TypeParameterInfo(
                TypeName("Selector"), SelectorInterfaceType!);
        }

        if (ComparerKind == ComparerKind.Struct)
        {
            yield return new TypeParameterInfo(
                TypeName("Comparer"), StructConstraint, TypeConstraint(ComparerInterfaceType));
        }
    }

    public IEnumerable<ParameterSyntax> GetOrderParameters()
    {
        foreach (var member in GetOrderMemberInfos())
        {
            if (member.SelectorType != null)
                yield return Parameter(member.SelectorType, member.SelectorName.Identifier);
            if (member.ComparerType != null)
                yield return Parameter(member.ComparerType, member.ComparerName.Identifier);
        }
    }

    public IEnumerable<ArgumentSyntax> GetOrderArguments()
    {
        foreach (var member in GetOrderMemberInfos())
        {
            if (member.SelectorType != null)
                yield return Argument(MemberAccessExpression(ThisPlaceholder, member.SelectorName));
            if (member.ComparerType != null)
                yield return Argument(MemberAccessExpression(ThisPlaceholder, member.ComparerName));
        }
    }

    public override IEnumerable<MemberDeclarationSyntax> RenderEnumerableMembers()
    {
        foreach (var member in base.RenderEnumerableMembers())
            yield return member;

        yield return SorterTemplate.Render(this);
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal,
        ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        var rootUpstream = RootOrder.Upstream;

        var contextName = $"{IterPlaceholder}c{Id}_";
        var initStatements = new List<StatementSyntax>();

        initStatements.AddRange(rootUpstream.GetLocalDeclarations(MemberKind.Enumerator));
        initStatements.AddRange(rootUpstream.RenderInitialization(true, null, null));

        var elementsName = LocalName("localElements");

        ExpressionSyntax countExpression = rootUpstream.RenderCount() ?? LiteralExpression(0);
        var elementsCount = MemberAccessExpression(elementsName, CountProperty);

        // create dictionary
        initStatements.Add(LocalDeclarationStatement(elementsName.Identifier,
            ObjectCreationExpression(ElementListType, ArgumentList(countExpression), null)));

        var addElementStatements = SingletonList<StatementSyntax>(
            ExpressionStatement(InvocationExpression(
                MemberAccessExpression(elementsName, AddMethod), ArgumentList(CurrentPlaceholder))));

        initStatements.AddRange(rootUpstream.RenderIteration(true, addElementStatements).Statements);

        // TODO try block
        initStatements.AddRange(rootUpstream.RenderDispose(true));

        var minName = LocalName("min");
        var maxName = LocalName("max");

        initStatements.Add(LocalDeclarationStatement(minName.Identifier, skipVar ?? LiteralExpression(0)));

        initStatements.Add(LocalDeclarationStatement(maxName.Identifier, takeVar == null
            ? SubtractExpression(elementsCount, LiteralExpression(1))
            : SubtractExpression(MathMin(elementsCount, AddExpression(minName, takeVar)), LiteralExpression(1))));

        // keep this placeholder but replace context
        var thisRewriter = new ThisPlaceholderRewriter(ThisPlaceholder, contextName);
        for (int i = 0; i < initStatements.Count; ++i)
            initStatements[i] = (StatementSyntax)thisRewriter.Visit(initStatements[i]);

        var sortBody = new List<StatementSyntax>();

        var sorterName = LocalName("sorter");
        var indicesName = Iterator("indices");

        sortBody.Add(ExpressionStatement(SimpleAssignmentExpression(indicesName,
            ObjectCreationExpression(IndexListType, ArgumentList(elementsCount), null))));

        var loopVar = IdentifierName("i");

        sortBody.Add(ForStatement(loopVar, LiteralExpression(0), elementsCount, ExpressionStatement(
            InvocationExpression(MemberAccessExpression(indicesName, AddMethod), ArgumentList(loopVar)))));

        sortBody.Add(LocalDeclarationStatement(sorterName.Identifier,
            ObjectCreationExpression(QualifiedName(ResolvedClassName, IdentifierName("Sorter")),
                ArgumentList(GetOrderArguments().Prepend(Argument(elementsName))), null)));

        sortBody.Add(ExpressionStatement(InvocationExpression(
            MemberAccessExpression(sorterName, IdentifierName("PartialQuickSort")),
            ArgumentList(MemberAccessExpression(indicesName, IdentifierName("Array")), LiteralExpression(0),
                SubtractExpression(elementsCount, LiteralExpression(1)), minName, maxName))));

        sortBody.Add(ExpressionStatement(InvocationExpression(sorterName, DisposeMethod)));

        var elseBody = new List<StatementSyntax>();

        elseBody.Add(ExpressionStatement(InvocationExpression(elementsName, DisposeMethod)));

        initStatements.Add(IfStatement(GreaterOrEqualExpression(maxName, minName),
            Block(sortBody), ElseClause(Block(elseBody))));

        // lastly access to iterator
        // initialize elements
        initStatements.Add(ExpressionStatement(SimpleAssignmentExpression(
            Iterator("elements"), elementsName)));

        // initialize index
        initStatements.Add(ExpressionStatement(SimpleAssignmentExpression(
            Iterator("index"), SubtractExpression(minName, LiteralExpression(1)))));

        return initStatements;
    }

    public override IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
    {
        yield return ExpressionStatement(InvocationExpression(Iterator("elements"), DisposeMethod));
        yield return ExpressionStatement(InvocationExpression(Iterator("indices"), DisposeMethod));
    }

    public IEnumerable<OrderMemberInfo> GetOrderMemberInfos()
    {
        if (UpstreamOrder != null)
        {
            foreach (var member in UpstreamOrder.GetOrderMemberInfos())
                yield return member;
        }

        TypeSyntax? selectorType;
        TypeSyntax? comparerType;

        switch (SelectorKind)
        {
            case FunctionKind.Default:
                selectorType = null;
                break;

            case FunctionKind.Delegate:
                selectorType = SelectorInterfaceType;
                break;

            case FunctionKind.Struct:
                selectorType = TypeName("Selector");
                break;

            default:
                throw new InvalidOperationException();
        }

        switch (ComparerKind)
        {
            case ComparerKind.Default:
                comparerType = null;
                break;

            case ComparerKind.Interface:
                comparerType = ComparerInterfaceType;
                break;

            case ComparerKind.Struct:
                comparerType = TypeName("Comparer");
                break;

            default:
                throw new InvalidOperationException();
        }

        yield return new OrderMemberInfo(this, selectorType, comparerType, KeyType, KeySymbol);
    }

    public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
    {
        var currentName = LocalName("current");
        var currentRewriter = new CurrentPlaceholderRewriter(currentName);

        // replace current variables of downstream
        statements = currentRewriter.VisitList(statements);

        statements = statements.InsertRange(0, new StatementSyntax[]
        {
            // for optimal JIT compile it needs to be inside of while, not the condition
            IfStatement(GreaterOrEqualExpression(
                    CastExpression(UIntType, PreIncrementExpression(Iterator("index"))),
                    CastExpression(UIntType, MemberAccessExpression(Iterator("indices"), CountProperty))),
                BreakStatement()),
            // set current
            LocalDeclarationStatement(
                currentName.Identifier, ElementAccessExpression(Iterator("elements"),
                    ElementAccessExpression(Iterator("indices"), Iterator("index"))))
        });

        var result = WhileStatement(TrueExpression(), Block(statements));

        return Block(result);
    }
}