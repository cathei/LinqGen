// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public sealed class AsEnumerableEvaluation : Evaluation
    {
        private static readonly SyntaxTree TemplateSyntaxTree = CSharpSyntaxTree.ParseText(@"
        // result of AsEnumerable, doesn't need to be public
        private class BoxedEnumerable : IEnumerable<_Element_>
        {
            private _Upstream_ source;

            internal BoxedEnumerable(in _Upstream_ source)
            {
                this.source = source;
            }

            public IEnumerator<_Element_> GetEnumerator() => source.GetEnumerator();

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

        public AsEnumerableEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            _rewriter = new(this);
        }

        public override void AddUpstream(Generation upstream)
        {
            upstream.IsEnumerator = true;
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
                EnumerableInterfaceType(Upstream.OutputElementType), null,
                MethodName.Identifier, null, ParameterList(), default, null,
                ArrowExpressionClause(ObjectCreationExpression(
                    IdentifierName("BoxedEnumerable"), ArgumentList(ThisExpression()), null)), SemicolonToken);
        }
    }
}
