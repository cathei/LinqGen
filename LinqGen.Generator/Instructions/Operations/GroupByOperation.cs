// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;

namespace Cathei.LinqGen.Generator;

public sealed class GroupByOperation : Operation
{
    private ITypeSymbol KeySymbol { get; }
    private TypeSyntax KeyType { get; }

    private FunctionKind KeySelectorKind { get; }
    private FunctionKind ValueSelectorKind { get; }
    private FunctionKind ResultSelectorKind { get; }
    private ComparerKind ComparerKind { get; }

    public GroupByOperation(in LinqGenExpression expression, uint id,
        FunctionKind keySelectorKind, FunctionKind valueSelectorKind, FunctionKind resultSelectorKind,
        ComparerKind comparerKind) : base(expression, id)
    {
        KeySelectorKind = keySelectorKind;
        ValueSelectorKind = valueSelectorKind;
        ResultSelectorKind = resultSelectorKind;
        ComparerKind = comparerKind;

        int paramIndex = -1;

        // NOTE: key selector should always exist
        if (true) // KeySelectorKind != FunctionKind.Default)
        {
            var param = expression.GetNamedParameterType(++paramIndex);
            // Func<TIn, TOut> or IStructFunction<TIn, TOut>
            KeySymbol = param.TypeArguments[1];
            KeyType = ParseTypeName(KeySymbol);
        }

        if (ValueSelectorKind != FunctionKind.Default)
        {
            var param = expression.GetNamedParameterType(++paramIndex);
            // Func<TIn, TOut> or IStructFunction<TIn, TOut>
            _explicitValueSymbol = param.TypeArguments[1];
            _explicitValueType = ParseTypeName(_explicitValueSymbol);
        }

        if (ResultSelectorKind != FunctionKind.Default)
        {
            var param = expression.GetNamedParameterType(++paramIndex);
            // Func<TKey, TGroup, TOut> or IStructFunction<TKey, TGroup, TOut>
            _explicitResultSymbol = param.TypeArguments[2];
            _explicitResultType = ParseTypeName(_explicitResultSymbol);
        }

        // IStub<IEnumerable<Element>, ...>
        var returnType = (INamedTypeSymbol)expression.MethodSymbol.ReturnType;
        var enumerableType = (INamedTypeSymbol)returnType.TypeArguments[0];
        OutputElementSymbol = enumerableType.TypeArguments[0];
    }

    private bool IsUnmanaged => KeySymbol.IsUnmanagedType && ValueSymbol.IsUnmanagedType;

    private TypeSyntax ComparerType => ComparerKind switch
    {
        ComparerKind.Default => EqualityComparerDefaultType(KeyType, KeySymbol),
        ComparerKind.Interface => EqualityComparerInterfaceType(KeyType),
        ComparerKind.Struct => TypeName("Comparer"),
        _ => throw new ArgumentOutOfRangeException()
    };


    private readonly ITypeSymbol? _explicitValueSymbol;
    private ITypeSymbol ValueSymbol => ValueSelectorKind switch
    {
        FunctionKind.Default => Upstream.OutputElementSymbol,
        FunctionKind.Delegate => _explicitValueSymbol!,
        FunctionKind.Struct => _explicitValueSymbol!,
        _ => throw new ArgumentOutOfRangeException()
    };

    private readonly TypeSyntax? _explicitValueType;
    private TypeSyntax ValueType => ValueSelectorKind switch
    {
        FunctionKind.Default => Upstream.OutputElementType,
        FunctionKind.Delegate => _explicitValueType!,
        FunctionKind.Struct => _explicitValueType!,
        _ => throw new ArgumentOutOfRangeException()
    };

    private readonly ITypeSymbol? _explicitResultSymbol;
    private ITypeSymbol ResultSymbol => ResultSelectorKind switch
    {
        FunctionKind.Default => Upstream.OutputElementSymbol,
        FunctionKind.Delegate => _explicitResultSymbol!,
        FunctionKind.Struct => _explicitResultSymbol!,
        _ => throw new ArgumentOutOfRangeException()
    };

    private readonly TypeSyntax? _explicitResultType;
    private TypeSyntax ResultType => ResultSelectorKind switch
    {
        FunctionKind.Default => Upstream.OutputElementType,
        FunctionKind.Delegate => _explicitResultType!,
        FunctionKind.Struct => _explicitResultType!,
        _ => throw new ArgumentOutOfRangeException()
    };

    private TypeSyntax DictionaryType => PooledDictionaryType(
        KeyType, PooledListType, ComparerType, IsUnmanaged);

    private TypeSyntax PooledListType => PooledListType(ValueType, IsUnmanaged);

    private TypeSyntax GroupingType => GroupingType(KeyType, ValueType, IsUnmanaged);

    public override ITypeSymbol OutputElementSymbol { get; }

    public override TypeSyntax OutputElementType => ResultSelectorKind switch
    {
        FunctionKind.Default => GroupingType,
        FunctionKind.Delegate => ResultType,
        FunctionKind.Struct => ResultType,
        _ => throw new ArgumentOutOfRangeException()
    };

    protected override bool ClearsUpstreamEnumerator => true;

    public override bool SupportPartition => true;

    // cannot decide count unless enumerated
    public override ExpressionSyntax? RenderCount() => null;

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield return new MemberInfo(MemberKind.Enumerator, DictionaryType, LocalName("dict"));
        yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("index"));

        if (KeySelectorKind == FunctionKind.Delegate)
        {
            yield return new MemberInfo(MemberKind.Enumerable,
                FuncDelegateType(Upstream.OutputElementType, KeyType), LocalName("keySelector"));
        }
        else if (KeySelectorKind == FunctionKind.Struct)
        {
            yield return new MemberInfo(MemberKind.Enumerable, TypeName("KeySelector"), LocalName("keySelector"));
        }

        if (ValueSelectorKind == FunctionKind.Delegate)
        {
            yield return new MemberInfo(MemberKind.Enumerable,
                FuncDelegateType(Upstream.OutputElementType, ValueType), LocalName("valueSelector"));
        }
        else if (ValueSelectorKind == FunctionKind.Struct)
        {
            yield return new MemberInfo(MemberKind.Enumerable, TypeName("ValueSelector"), LocalName("valueSelector"));
        }

        if (ResultSelectorKind == FunctionKind.Delegate)
        {
            yield return new MemberInfo(MemberKind.Both,
                FuncDelegateType(KeyType, GroupingType, ResultType), LocalName("resultSelector"));
        }
        else if (ResultSelectorKind == FunctionKind.Struct)
        {
            yield return new MemberInfo(MemberKind.Both, TypeName("ResultSelector"), LocalName("resultSelector"));
        }

        if (ComparerKind != ComparerKind.Default)
        {
            yield return new MemberInfo(MemberKind.Enumerable, ComparerType, LocalName("comparer"));
        }
    }

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (KeySelectorKind == FunctionKind.Struct)
        {
            yield return new TypeParameterInfo(TypeName("KeySelector"),
                StructFunctionInterfaceType(Upstream.OutputElementType, KeyType));
        }

        if (ValueSelectorKind == FunctionKind.Struct)
        {
            yield return new TypeParameterInfo(TypeName("ValueSelector"),
                StructFunctionInterfaceType(Upstream.OutputElementType, ValueType));
        }

        if (ResultSelectorKind == FunctionKind.Struct)
        {
            yield return new TypeParameterInfo(TypeName("ResultSelector"),
                StructFunctionInterfaceType(KeyType, GroupingType, ResultType));
        }

        if (ComparerKind == ComparerKind.Struct)
        {
            yield return new TypeParameterInfo(TypeName("Comparer"),
                EqualityComparerInterfaceType(KeyType));
        }
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(
        bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        var dictName = LocalName("localDict");
        var keyName = LocalName("key");
        var valueName = LocalName("value");
        var listName = LocalName("list");

        ExpressionSyntax keySelectExpression = KeySelectorKind == FunctionKind.Default
            ? CurrentPlaceholder
            : InvocationExpression(MemberAccessExpression(
                Member("keySelector"), InvokeMethod), ArgumentList(CurrentPlaceholder));

        ExpressionSyntax valueSelectExpression = ValueSelectorKind == FunctionKind.Default
            ? CurrentPlaceholder
            : InvocationExpression(MemberAccessExpression(
                Member("valueSelector"), InvokeMethod), ArgumentList(CurrentPlaceholder));

        var addStatements = new StatementSyntax[]
        {
            // select key
            LocalDeclarationStatement(keyName.Identifier, keySelectExpression),
            // get or create list from dict
            LocalDeclarationStatement(default, RefTokenList, VariableDeclaration(listName.Identifier,
                RefExpression(InvocationExpression(
                    MemberAccessExpression(dictName, GetOrCreateMethod), ArgumentList(keyName))))),
            // if count is 0 here it means that the list is not initialized
            IfStatement(
                GreaterOrEqualExpression(LiteralExpression(0), MemberAccessExpression(listName, CountProperty)),
                // initialize list
                ExpressionStatement(SimpleAssignmentExpression(listName,
                    ObjectCreationExpression(PooledListType, ArgumentList(LiteralExpression(0)), null)))),
            // select value
            LocalDeclarationStatement(valueName.Identifier, valueSelectExpression),
            // add value to the list
            ExpressionStatement(
                InvocationExpression(MemberAccessExpression(listName, AddMethod), ArgumentList(valueName)))
        };

        var comparerExpression = ComparerKind == ComparerKind.Default
            ? ComparerDefault(KeyType, KeySymbol)
            : Member("comparer");

        var contextName = $"{IterPlaceholder}c{Id}_";

        var initStatements = new List<StatementSyntax>
        {
            // create dictionary
            LocalDeclarationStatement(dictName.Identifier, ObjectCreationExpression(
                DictionaryType, ArgumentList(LiteralExpression(0), comparerExpression), null))
        };

        // local declaration
        initStatements.AddRange(Upstream.GetLocalDeclarations(MemberKind.Enumerator));

        // initialization
        initStatements.AddRange(Upstream.RenderInitialization(true, null, null));

        // iteration
        initStatements.AddRange(Upstream.RenderIteration(isLocal, List(addStatements)).Statements);

        // TODO try block
        // dispose used resources
        initStatements.AddRange(Upstream.RenderDispose(true));

        // keep this placeholder but replace context
        var thisRewriter = new ThisPlaceholderRewriter(ThisPlaceholder, contextName);
        for (int i = 0; i < initStatements.Count; ++i)
            initStatements[i] = (StatementSyntax)thisRewriter.Visit(initStatements[i]);

        // initialize index
        initStatements.Add(ExpressionStatement(SimpleAssignmentExpression(Iterator("index"),
            skipVar != null ? SubtractExpression(skipVar, LiteralExpression(1)) : LiteralExpression(-1))));

        // initialize dictionary
        initStatements.Add(ExpressionStatement(SimpleAssignmentExpression(Iterator("dict"), dictName)));

        return initStatements;
    }

    public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
    {
        var slotName = LocalName("slot");
        var groupingName = LocalName("grouping");
        var currentName = LocalName("current");
        var currentRewriter = new CurrentPlaceholderRewriter(currentName);

        // replace current variables of downstream
        statements = currentRewriter.VisitList(statements);

        // should this be ref?
        var currentGetStatements = new StatementSyntax[]
        {
            // for optimal JIT compile it needs to be inside of while, not the condition
            IfStatement(GreaterOrEqualExpression(
                    CastExpression(UIntType, PreIncrementExpression(Iterator("index"))),
                    CastExpression(UIntType, MemberAccessExpression(Iterator("dict"), CountProperty))),
                BreakStatement()),
            // set current
            LocalDeclarationStatement(
                slotName.Identifier, ElementAccessExpression(
                    MemberAccessExpression(Iterator("dict"), IdentifierName("Slots")), Iterator("index"))),
            LocalDeclarationStatement(
                groupingName.Identifier, ObjectCreationExpression(
                    GroupingType, ArgumentList(
                        MemberAccessExpression(slotName, IdentifierName("Key")),
                        MemberAccessExpression(slotName, IdentifierName("Value"))), default)),
            ResultSelectorKind == FunctionKind.Default
                ? LocalDeclarationStatement(currentName.Identifier, groupingName)
                : LocalDeclarationStatement(currentName.Identifier,
                    InvocationExpression(Member("resultSelector"),
                        ArgumentList(MemberAccessExpression(groupingName, KeyProperty), groupingName)))
        };

        statements = statements.InsertRange(0, currentGetStatements);

        var result = WhileStatement(TrueExpression(), Block(statements));

        return Block(result);
    }

    public override IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
    {
        yield return ForStatement(LocalName("i"), LiteralExpression(0),
            MemberAccessExpression(Iterator("dict"), CountProperty),
            ExpressionStatement(InvocationExpression(ElementAccessExpression(
                    MemberAccessExpression(Iterator("dict"), IdentifierName("Slots")), LocalName("i")),
                ValueProperty, DisposeMethod)));

        yield return ExpressionStatement(InvocationExpression(Iterator("dict"), DisposeMethod));
    }
}