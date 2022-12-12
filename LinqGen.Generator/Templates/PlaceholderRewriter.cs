// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public class PlaceholderRewriter : CSharpSyntaxRewriter
    {
        private readonly ExpressionSyntax _current;

        public PlaceholderRewriter(ExpressionSyntax current)
        {
            _current = current;
        }

        public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (node.IsEquivalentTo(Instruction.CurrentPlaceholder))
                return _current;

            return base.VisitIdentifierName(node);
        }

        public SyntaxList<StatementSyntax> VisitStatementSyntaxList(SyntaxList<StatementSyntax> nodes)
        {
            return List(nodes.Select(node => (StatementSyntax)Visit(node)));
        }
    }
}