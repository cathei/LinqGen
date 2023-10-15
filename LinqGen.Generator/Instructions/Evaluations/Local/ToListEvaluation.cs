// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class ToListEvaluation : LocalEvaluation
{
    public ToListEvaluation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
    }

    protected override TypeSyntax ReturnType =>
        GenericName(Identifier("List"), TypeArgumentList(Upstream.OutputElementType));

    protected override IEnumerable<StatementSyntax> RenderInitialization()
    {
        if (Upstream.SupportCount)
        {
            yield return LocalDeclarationStatement(LocalName("list").Identifier, ObjectCreationExpression(
                ReturnType, ArgumentList(InvocationExpression(CountMethod)), null));
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
        yield return ExpressionStatement(InvocationExpression(
            MemberAccessExpression(LocalName("list"), AddMethod), ArgumentList(CurrentPlaceholder)));
    }

    protected override IEnumerable<StatementSyntax> RenderReturn()
    {
        if (Upstream.SupportCount)
        {
            yield return ReturnStatement(LocalName("list"));
        }
        else
        {
            yield return ReturnStatement(InvocationExpression(LocalName("list"), IdentifierName("ToList")));
        }
    }
}