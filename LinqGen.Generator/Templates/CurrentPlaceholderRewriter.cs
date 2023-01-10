// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public class CurrentPlaceholderRewriter : CSharpSyntaxRewriter
{
    private readonly ExpressionSyntax _current;

    public CurrentPlaceholderRewriter(ExpressionSyntax current)
    {
        _current = current;
    }

    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        if (node.IsEquivalentTo(Instruction.CurrentPlaceholder))
            return _current;

        return base.VisitIdentifierName(node);
    }
}