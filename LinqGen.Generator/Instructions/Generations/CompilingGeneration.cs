// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    /// <summary>
    /// Generation is any instruction that produces enumerable as output.
    /// </summary>
    public abstract class CompilingGeneration : Generation
    {
        protected CompilingGeneration(in LinqGenExpression expression) : base(expression)
        {
            ElementName = ParseName(expression.ElementSymbol
                .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

            MethodName = IdentifierName(expression.MethodSymbol.Name);
        }

        public NameSyntax ElementName { get; }

        public IdentifierNameSyntax MethodName { get; }

        /// <summary>
        /// Non-qualified class name, Only used for current file rendering
        /// </summary>
        public IdentifierNameSyntax? IdentifierName { get; protected set; }

        public abstract IEnumerable<MemberInfo> GetMemberInfos();

        public override SourceText Render(IdentifierNameSyntax assemblyName, int id)
        {
            ClassName = IdentifierName = IdentifierName($"{MethodName}_{id}");
            return GenerationTemplate.Render(assemblyName, this);
        }

        public virtual BlockSyntax RenderConstructorBody() => SyntaxFactory.Block();

        public abstract BlockSyntax RenderMoveNextBody();

        public abstract BlockSyntax RenderCurrentGetBody();

        public virtual BlockSyntax RenderDisposeBody() => SyntaxFactory.Block();
    }
}