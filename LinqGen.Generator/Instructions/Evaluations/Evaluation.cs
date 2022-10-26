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
    using static CodeGenUtils;

    /// <summary>
    /// Evaluation takes LinqGen enumerable as input, but output is not LinqGen enumerable.
    /// </summary>
    public abstract class Evaluation : Instruction
    {
        protected Evaluation(in LinqGenExpression expression) : base(expression)
        {
            MethodSymbol = expression.MethodSymbol;
            MethodName = IdentifierName(MethodSymbol.Name);

            InputElementType = ParseTypeName(expression.InputElementSymbol!
                .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        }

        public IMethodSymbol MethodSymbol { get; }
        public IdentifierNameSyntax MethodName { get; }

        /// <summary>
        /// Evaluation should not rendered individually. Instead it will be rendered with upstream.
        /// </summary>
        public void SetUpstream(Generation upstream)
        {
            Upstream = upstream;
            Upstream.AddEvaluation(this);
        }

        public abstract TypeSyntax ReturnType { get; }

        public override TypeSyntax? InputElementType { get; }

        public virtual IEnumerable<ParameterSyntax> GetParameters()
        {
            yield return Parameter(default, ThisTokenList,
                UpstreamResolvedClassName, SourceName.Identifier, default);
        }

        public abstract BlockSyntax RenderMethodBody();
    }
}