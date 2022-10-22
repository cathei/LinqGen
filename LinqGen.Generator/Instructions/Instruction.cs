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
    /// Generation is Something => LinqGenEnumerable
    /// Operation is LinqGenEnumerable => LinqGenEnumerable
    /// Evaluation is LinqGenEnumerable => SomethingElse
    /// </summary>
    public abstract class Instruction
    {
        protected Instruction(in LinqGenExpression expression) : this(expression.UpstreamSymbol) { }

        protected Instruction(INamedTypeSymbol? upstreamSymbol)
        {
            UpstreamSymbol = upstreamSymbol;
        }

        public INamedTypeSymbol? UpstreamSymbol { get; }

        public Instruction? Upstream { get; protected set; }
        public List<Instruction>? Downstream { get; private set; }
        public Dictionary<IMethodSymbol, Evaluation>? Evaluations { get; set; }

        /// <summary>
        /// The class name cached for child class rendering
        /// </summary>
        public IdentifierNameSyntax? ClassName { get; protected set; }

        public virtual void SetUpstream(Instruction upstream)
        {
            Upstream = upstream;

            upstream.Downstream ??= new List<Instruction>();
            upstream.Downstream.Add(this);
        }

        public virtual IEnumerable<TypeParameterSyntax> GetTypeParameters()
        {
            if (Upstream == null)
                yield break;

            foreach (var syntax in Upstream.GetTypeParameters())
                yield return syntax;
        }

        public abstract SourceText Render(IdentifierNameSyntax assemblyName, int id);
    }
}