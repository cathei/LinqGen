// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;

namespace Cathei.LinqGen.Generator;

public sealed class AsEnumerableEvaluation : Evaluation
{
    private static readonly SyntaxTree TemplateSyntaxTree = CSharpSyntaxTree.ParseText(@"
        // result of AsEnumerable, doesn't need to be public
        public class BoxedEnumerable : IEnumerable<_Element_>
        {
            private _Upstream_ source;

            internal BoxedEnumerable(in _Upstream_ source)
            {
                this.source = source;
            }

            public Enumerator GetEnumerator() => source.GetEnumerator();

            IEnumerator<_Element_> IEnumerable<_Element_>.GetEnumerator() => GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
");

    private class Rewriter : CSharpSyntaxRewriter
    {
        private readonly AsEnumerableEvaluation _instruction;

        public Rewriter(AsEnumerableEvaluation instruction)
        {
            _instruction = instruction;
        }

        public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
        {
            switch (node.Identifier.ValueText)
            {
                case "_Upstream_":
                    return _instruction.Upstream.ResolvedClassName;
            }

            return base.VisitIdentifierName(node);
        }
    }

    private readonly Rewriter _rewriter;

    public AsEnumerableEvaluation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
        _rewriter = new(this);
    }

    public override void AddUpstream(Generation upstream)
    {
        upstream.HasEnumerator = true;
        base.AddUpstream(upstream);
    }

    public override IEnumerable<MemberDeclarationSyntax> RenderUpstreamMembers()
    {
        var classSyntax = TemplateSyntaxTree.GetRoot()
            .DescendantNodesAndSelf()
            .OfType<ClassDeclarationSyntax>()
            .First();

        yield return (MemberDeclarationSyntax)_rewriter.Visit(classSyntax);

        yield return MethodDeclaration(SingletonList(AggressiveInliningAttributeList), PublicTokenList,
            IdentifierName("BoxedEnumerable"), null, MethodName.Identifier, null, ParameterList(), default, null,
            ArrowExpressionClause(ObjectCreationExpression(
                IdentifierName("BoxedEnumerable"), ArgumentList(ThisExpression()), null)), SemicolonToken);
    }
}