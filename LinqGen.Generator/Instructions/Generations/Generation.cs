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
        protected Generation(in LinqGenExpression expression) : base(expression) { }

        // protected Generation(INamedTypeSymbol? upstreamSignatureSymbol) : base(upstreamSignatureSymbol) { }

        // public virtual bool CanSlice { get; }
        public abstract TypeSyntax OutputElementType { get; }

        /// <summary>
        /// The qualified class name cached for child class rendering
        /// </summary>
        public abstract NameSyntax ClassName { get; }

        /// <summary>
        /// Non-qualified class name, Only used for current file rendering
        /// </summary>
        public abstract IdentifierNameSyntax IdentifierName { get; }

        public List<Operation>? Downstream { get; private set; }

        public Dictionary<IMethodSymbol, Evaluation>? Evaluations { get; private set; }

        public virtual void SetUpstream(Generation upstream)
        {
            // only operation can have upstream
            throw new NotImplementedException();
        }

        public void AddDownstream(Operation downstream)
        {
            Downstream ??= new List<Operation>();
            Downstream.Add(downstream);
        }

        public void AddEvaluation(Evaluation downstream)
        {
            Evaluations ??= new Dictionary<IMethodSymbol, Evaluation>(SymbolEqualityComparer.Default);
            Evaluations.Add(downstream.MethodSymbol, downstream);
        }

        // public HashSet<INamedTypeSymbol>? InputSymbols { get; private set; }
        // public void AddInputSymbol(INamedTypeSymbol inputSymbol)
        // {
        //     InputSymbols ??= new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        //     InputSymbols.Add(inputSymbol);
        // }

        public abstract SourceText Render();
    }
}