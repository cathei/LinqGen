// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class ToArrayEvaluation : LocalEvaluation
{
    public ToArrayEvaluation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
    }

    protected override TypeSyntax ReturnType => ArrayType(
        Upstream.OutputElementType, SingletonList(ArrayRankSpecifier()));

    protected override IEnumerable<StatementSyntax> RenderInitialization()
    {
        if (Upstream.SupportCount)
        {
            yield return LocalDeclarationStatement(LocalName("array").Identifier,
                ObjectCreationExpression(ArrayType(Upstream.OutputElementType,
                    SingletonList(ArrayRankSpecifier(SingletonSeparatedList(
                        (ExpressionSyntax)InvocationExpression(CountMethod)))))));

            yield return LocalDeclarationStatement(LocalName("index").Identifier, LiteralExpression(-1));
        }
        else
        {
            yield return UsingLocalDeclarationStatement(LocalName("list").Identifier, ObjectCreationExpression(
                PooledListType(Upstream.OutputElementType, Upstream.OutputElementSymbol.IsUnmanagedType),
                ArgumentList(LiteralExpression(0)), null));
        }
    }

    protected override IEnumerable<StatementSyntax> RenderAccumulation()
    {
        if (Upstream.SupportCount)
        {
            yield return ExpressionStatement(SimpleAssignmentExpression(
                ElementAccessExpression(LocalName("array"), PreIncrementExpression(LocalName("index"))),
                CurrentPlaceholder));
        }
        else
        {
            yield return ExpressionStatement(InvocationExpression(
                MemberAccessExpression(LocalName("list"), AddMethod), ArgumentList(CurrentPlaceholder)));
        }
    }

    protected override IEnumerable<StatementSyntax> RenderReturn()
    {
        if (Upstream.SupportCount)
        {
            yield return ReturnStatement(LocalName("array"));
        }
        else
        {
            yield return ReturnStatement(InvocationExpression(LocalName("list"), IdentifierName("ToArray")));
        }
    }
}