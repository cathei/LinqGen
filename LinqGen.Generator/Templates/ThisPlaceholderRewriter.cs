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

    public class ThisPlaceholderRewriter : CSharpSyntaxRewriter
    {
        private readonly ExpressionSyntax _this;
        private readonly ExpressionSyntax _context;

        public ThisPlaceholderRewriter(ExpressionSyntax @this, ExpressionSyntax context)
        {
            _this = @this;
            _context = context;
        }

        public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
        {
            // should not recursively solve
            if (node.IsEquivalentTo(Instruction.ThisPlaceholder))
                return _this;

            // should not recursively solve
            if (node.IsEquivalentTo(Instruction.IterPlaceholder))
                return _context;

            return base.VisitIdentifierName(node);
        }

        public SyntaxList<StatementSyntax> VisitStatementSyntaxList(SyntaxList<StatementSyntax> nodes)
        {
            return List(nodes.Select(node => (StatementSyntax)Visit(node)));
        }
    }
}