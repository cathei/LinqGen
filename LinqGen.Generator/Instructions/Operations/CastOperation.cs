// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public class CastOperation : Operation
{
    private bool SkipIfMismatch { get; }

    public CastOperation(in LinqGenExpression expression, uint id, bool skipIfMismatch) : base(expression, id)
    {
        SkipIfMismatch = skipIfMismatch;
        OutputElementSymbol = expression.MethodSymbol.ConstructedFrom.TypeParameters[0];
        OutputElementType = TypeName("Out");
    }

    public override ITypeSymbol OutputElementSymbol { get; }
    public override TypeSyntax OutputElementType { get; }

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        yield return new TypeParameterInfo(TypeName("Out"));
    }

    protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    {
        yield break;
    }

    public override bool SupportPartition => false;

    public override ExpressionSyntax? RenderCount() => null;

    protected override StatementSyntax? RenderMoveNext()
    {
        if (SkipIfMismatch)
            return IfStatement(IsNotExpression(CurrentPlaceholder, OutputElementType), ContinueStatement());

        return null;
    }

    protected override ExpressionSyntax RenderCurrent()
    {
        return CastExpression(OutputElementType, CastExpression(ObjectType, CurrentPlaceholder));
    }
}