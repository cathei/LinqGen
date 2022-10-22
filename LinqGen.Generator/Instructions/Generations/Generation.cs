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
    /// Common base class for CompilingGeneration and CompiledGeneration.
    /// So they can provide metadata with same interfaces.
    /// </summary>
    public abstract class Generation : Instruction
    {
        public Generation(in LinqGenExpression expression) : base(expression) { }

        public Generation(INamedTypeSymbol? upstreamSymbol) : base(upstreamSymbol) { }

        // public virtual bool CanSlice { get; }

        public List<Generation>? Downstream { get; set; }

        public Dictionary<IMethodSymbol, Evaluation>? Evaluations { get; set; }

        public void SetUpstream(Generation upstream)
        {
            Upstream = upstream;
            Upstream.AddDownstream(this);
        }

        public void AddDownstream(Generation downstream)
        {
            Downstream ??= new List<Generation>();
            Downstream.Add(downstream);
        }

        public void AddEvaluation(Evaluation downstream)
        {
            Evaluations ??= new Dictionary<IMethodSymbol, Evaluation>(SymbolEqualityComparer.Default);
            Evaluations.Add(downstream.MethodSymbol, downstream);
        }

        public abstract SourceText Render(IdentifierNameSyntax assemblyName, int id);
    }
}