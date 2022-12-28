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
        private readonly string _context;

        public ThisPlaceholderRewriter(ExpressionSyntax @this, string context)
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
            if (node.Identifier.ValueText.StartsWith(Instruction.IterPlaceholder))
            {
                string substring = node.Identifier.ValueText.Substring(Instruction.IterPlaceholder.Length);
                return IdentifierName($"{_context}{substring}");
            }

            return base.VisitIdentifierName(node);
        }

        public override SyntaxNode? VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            // should not recursively solve
            if (node.Identifier.ValueText.StartsWith(Instruction.IterPlaceholder))
            {
                string substring = node.Identifier.ValueText.Substring(Instruction.IterPlaceholder.Length);
                node = node.WithIdentifier(Identifier($"{_context}{substring}"));
            }

            return base.VisitVariableDeclarator(node);
        }

        public SyntaxList<StatementSyntax> VisitStatementSyntaxList(SyntaxList<StatementSyntax> nodes)
        {
            return List(nodes.Select(node => (StatementSyntax)Visit(node)));
        }
    }
}