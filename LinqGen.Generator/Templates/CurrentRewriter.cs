// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    public class CurrentRewriter : CSharpSyntaxRewriter
    {
        private readonly IdentifierNameSyntax _current;

        public CurrentRewriter(IdentifierNameSyntax current)
        {
            _current = current;
        }

        public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (node.IsEquivalentTo(Instruction.CurrentPlaceholder))
            {
                node = _current;
            }

            return base.VisitIdentifierName(node);
        }

        public SyntaxList<StatementSyntax> VisitStatementSyntaxList(SyntaxList<StatementSyntax> nodes)
        {
            return new(nodes.Select(node => (StatementSyntax)Visit(node)));
        }
    }
}