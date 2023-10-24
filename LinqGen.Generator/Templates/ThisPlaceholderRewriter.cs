// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

using static SyntaxFactory;

public class ThisPlaceholderRewriter : CSharpSyntaxRewriter
{
    private readonly ExpressionSyntax _thisOriginal;
    private readonly ExpressionSyntax _thisReplacement;
    private readonly string _iterOriginal;
    private readonly string _iterReplacement;

    public ThisPlaceholderRewriter(ExpressionSyntax @this, string iter)
    {
        _thisOriginal = Instruction.ThisPlaceholder;
        _thisReplacement = @this;

        _iterOriginal = Instruction.IterPlaceholder;
        _iterReplacement = iter;
    }

    public ThisPlaceholderRewriter(
        ExpressionSyntax thisOriginal, ExpressionSyntax thisReplacement,
        string iterOriginal, string iterReplacement)
    {
        _thisOriginal = thisOriginal;
        _thisReplacement = thisReplacement;

        _iterOriginal = iterOriginal;
        _iterReplacement = iterReplacement;
    }

    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        // should not recursively solve
        if (node.IsEquivalentTo(_thisOriginal))
            return _thisReplacement;

        // should not recursively solve
        if (node.Identifier.ValueText.StartsWith(_iterOriginal))
        {
            string substring = node.Identifier.ValueText.Substring(_iterOriginal.Length);
            return IdentifierName($"{_iterReplacement}{substring}");
        }

        return base.VisitIdentifierName(node);
    }

    public override SyntaxNode? VisitVariableDeclarator(VariableDeclaratorSyntax node)
    {
        // should not recursively solve
        if (node.Identifier.ValueText.StartsWith(_iterOriginal))
        {
            string substring = node.Identifier.ValueText.Substring(_iterOriginal.Length);
            node = node.WithIdentifier(Identifier($"{_iterReplacement}{substring}"));
        }

        return base.VisitVariableDeclarator(node);
    }

    public override SyntaxNode? VisitParameter(ParameterSyntax node)
    {
        // should not recursively solve
        if (node.Identifier.ValueText.StartsWith(_iterOriginal))
        {
            string substring = node.Identifier.ValueText.Substring(_iterOriginal.Length);
            return node.WithIdentifier(Identifier($"{_iterReplacement}{substring}"));
        }

        return base.VisitParameter(node);
    }
}