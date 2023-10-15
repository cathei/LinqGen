// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class ArrayOrListGeneration : Generation
{
    public TypeSyntax SourceType { get; }
    public IdentifierNameSyntax ListCountProperty { get; }
    public bool AsStruct { get; }

    public ArrayOrListGeneration(in LinqGenExpression expression, uint id,
        ITypeSymbol listSymbol, ITypeSymbol elementSymbol, IdentifierNameSyntax countProperty, bool asStruct)
        : base(expression, id)
    {
        // TODO generic type element
        OutputElementSymbol = elementSymbol;
        OutputElementType = ParseTypeName(OutputElementSymbol);
        SourceType = ParseTypeName(listSymbol);
        ListCountProperty = countProperty;
        AsStruct = asStruct;
    }

    public override ITypeSymbol OutputElementSymbol { get; }
    public override TypeSyntax OutputElementType { get; }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        var enumerableType = AsStruct ? TypeName("Enumerable") : SourceType;

        yield return new MemberInfo(MemberKind.Both, enumerableType, LocalName("source"));
        yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("index"));
    }

    public override bool SupportPartition => true;

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (AsStruct)
        {
            yield return new TypeParameterInfo(TypeName("Enumerable"), SourceType);
        }
    }

    public override ExpressionSyntax RenderCount()
    {
        return MemberAccessExpression(Member("source"), ListCountProperty);
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal,
        ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        if (skipVar != null)
        {
            yield return ExpressionStatement(SimpleAssignmentExpression(
                Iterator("index"), SubtractExpression(skipVar, LiteralExpression(1))));
        }
        else
        {
            yield return ExpressionStatement(SimpleAssignmentExpression(
                Iterator("index"), LiteralExpression(-1)));
        }
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
                    CastExpression(UIntType, MemberAccessExpression(Member("source"), ListCountProperty))),
                BreakStatement()),
            // set current
            LocalDeclarationStatement(
                currentName.Identifier, ElementAccessExpression(Member("source"), Iterator("index")))
        });

        var result = WhileStatement(TrueExpression(), Block(statements));

        return Block(result);
    }
}