// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class EnumerableGeneration : Generation
{
    public EnumerableGeneration(in LinqGenExpression expression, uint id,
        ITypeSymbol sourceSymbol) : base(expression, id)
    {
        IsCollection = TryGetGenericCollectionInterface(sourceSymbol, out _);

        // TODO fallback to interface specific implementation
        var enumeratorSymbol = GetEnumeratorSymbol(sourceSymbol)!.ReturnType;
        var elementSymbol = GetCurrentSymbol(enumeratorSymbol)!;

        SourceEnumerableType = ParseTypeName(sourceSymbol);
        SourceEnumeratorType = ParseTypeName(enumeratorSymbol);

        OutputElementSymbol = elementSymbol;

        if (elementSymbol is ITypeParameterSymbol typeParameterSymbol)
        {
            var outputElementName = TypeName("Element");

            OutputElementType = outputElementName;
            GenericElement = true;

            var rewriter = new GenericRewriter(IdentifierName(typeParameterSymbol.Name), outputElementName);
            SourceEnumerableType = (TypeSyntax)rewriter.Visit(SourceEnumerableType);
            SourceEnumeratorType = (TypeSyntax)rewriter.Visit(SourceEnumeratorType);
        }
        else
        {
            OutputElementType = ParseTypeName(elementSymbol);
            GenericElement = false;
        }
    }

    public override ITypeSymbol OutputElementSymbol { get; }
    public override TypeSyntax OutputElementType { get; }

    private TypeSyntax SourceEnumerableType { get; }
    private TypeSyntax SourceEnumeratorType { get; }

    private bool GenericElement { get; }
    private bool IsCollection { get; }

    public override bool SupportPartition => false;

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield return new MemberInfo(MemberKind.Enumerable, SourceEnumerableType, LocalName("source"));
        yield return new MemberInfo(MemberKind.Enumerator, SourceEnumeratorType, LocalName("enumerator"));
    }

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (GenericElement)
            yield return new TypeParameterInfo(TypeName("Element"));
    }

    public override ExpressionSyntax? RenderCount()
    {
        return IsCollection ? MemberAccessExpression(Member("source"), CountProperty) : null;
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal,
        ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        yield return ExpressionStatement(SimpleAssignmentExpression(Iterator("enumerator"),
            InvocationExpression(Member("source"), GetEnumeratorMethod)));
    }

    public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
    {
        var currentName = LocalName("current");
        var currentRewriter = new CurrentPlaceholderRewriter(currentName);

        // replace current variables of downstream
        statements = currentRewriter.VisitList(statements);

        statements = statements.Insert(0, LocalDeclarationStatement(
            currentName.Identifier, MemberAccessExpression(Iterator("enumerator"), CurrentProperty)));

        var result = WhileStatement(
            InvocationExpression(Iterator("enumerator"), MoveNextMethod),
            Block(statements));

        return Block(result);
    }
}
