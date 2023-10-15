// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class EmptyGeneration : PredefinedGeneration
{
    public EmptyGeneration(in LinqGenExpression expression, uint id) : base(expression, id)
    {
        OutputElementSymbol = expression.MethodSymbol.ConstructedFrom.TypeParameters[0];
    }

    public override ITypeSymbol OutputElementSymbol { get; }
    public override TypeSyntax OutputElementType => TypeName("Element");

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        yield return new TypeParameterInfo(TypeName("Element"));
    }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield break;
    }

    public override ExpressionSyntax RenderCount()
    {
        return LiteralExpression(0);
    }

    public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal,
        ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        yield break;
    }

    public override BlockSyntax RenderIteration(bool isLocal,
        SyntaxList<StatementSyntax> statements)
    {
        return Block();
    }
}