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
    /// Instruction is all kind of method that can performed on LinqGen enumerable
    /// Generation is Value => Enumerable
    /// Operation is Enumerable => Enumerable
    /// Evaluation is Enumerable => Non-Enumerable
    /// </summary>
    public abstract class Instruction
    {
        protected Instruction(in LinqGenExpression expression) : this(expression.UpstreamSymbol) { }

        protected Instruction(INamedTypeSymbol? upstreamSymbol)
        {
            UpstreamSymbol = upstreamSymbol;
        }

        public INamedTypeSymbol? UpstreamSymbol { get; }

        public Generation? Upstream { get; protected set; }

        /// <summary>
        /// The qualified class name cached for child class rendering
        /// </summary>
        public NameSyntax? ClassName { get; protected set; }

        /// <summary>
        /// Note that derived class's type parameter info should precedence.
        /// So we can easily indicate it with T1, T2...
        /// </summary>
        public virtual IEnumerable<TypeParameterInfo> GetTypeParameterInfos() => Array.Empty<TypeParameterInfo>();
    }
}