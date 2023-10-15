// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class StructEnumerableGeneration : Generation
{
    public StructEnumerableGeneration(in LinqGenExpression expression, uint id,
        INamedTypeSymbol sourceSymbol) : base(expression, id)
    {
        // TODO ICollection, ICollection<T>, IReadOnlyCollection<T> ...
        // IsCollection = TryGetGenericCollectionInterface(sourceSymbol, out _);

        var enumeratorSymbol = sourceSymbol.TypeArguments[1];
        var elementSymbol = sourceSymbol.TypeArguments[0];

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
    // private bool IsCollection { get; }

    public override bool SupportPartition => false;

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield return new MemberInfo(MemberKind.Enumerable, TypeName("Enumerable"), LocalName("source"));
        yield return new MemberInfo(MemberKind.Enumerator, SourceEnumeratorType, LocalName("iter"));
    }

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (GenericElement)
            yield return new TypeParameterInfo(TypeName("Element"));

        yield return new TypeParameterInfo(TypeName("Enumerable"), SourceEnumerableType);
    }

    public override ExpressionSyntax? RenderCount()
    {
        return null;
        // return IsCollection ? MemberAccessExpression(VarName("source"), CountProperty) : null;
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal,
        ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        yield return ExpressionStatement(SimpleAssignmentExpression(Iterator("iter"),
            InvocationExpression(Member("source"), GetEnumeratorMethod)));
    }

    public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
    {
        var currentName = LocalName("current");
        var currentRewriter = new CurrentPlaceholderRewriter(currentName);

        // replace current variables of downstream
        statements = currentRewriter.VisitList(statements);

        statements = statements.Insert(0, LocalDeclarationStatement(
            currentName.Identifier, MemberAccessExpression(Iterator("iter"), CurrentProperty)));

        var result = WhileStatement(
            InvocationExpression(Iterator("iter"), MoveNextMethod),
            Block(statements));

        return Block(result);
    }
}