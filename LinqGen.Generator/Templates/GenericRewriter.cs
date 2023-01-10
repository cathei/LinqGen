// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

internal class GenericRewriter : CSharpSyntaxRewriter
{
    private readonly IdentifierNameSyntax _target;
    private readonly IdentifierNameSyntax _replace;

    public GenericRewriter(IdentifierNameSyntax target, IdentifierNameSyntax replace)
    {
        _target = target;
        _replace = replace;
    }

    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        if (node.Identifier.ValueText == _target.Identifier.ValueText)
            return _replace;

        return base.VisitIdentifierName(node);
    }
}