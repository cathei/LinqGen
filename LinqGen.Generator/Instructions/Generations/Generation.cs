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
    /// Common base class for CompilingGeneration and CompiledGeneration.
    /// So they can provide metadata with same interfaces.
    /// </summary>
    public abstract class Generation : Instruction
    {
        protected Generation(in LinqGenExpression expression) : base(expression) { }

        protected Generation(bool generated) : base(generated) { }

        public abstract TypeSyntax OutputElementType { get; }

        /// <summary>
        /// The qualified class name cached for child class rendering
        /// </summary>
        public abstract NameSyntax ClassName { get; }

        /// <summary>
        /// Non-qualified class name, Only used for current file rendering
        /// </summary>
        public abstract IdentifierNameSyntax IdentifierName { get; }

        /// <summary>
        /// If True, method will be embedded as member method
        /// If false, method will be extension method
        /// </summary>
        public virtual bool ShouldBeMemberMethod => false;

        public List<Operation>? Downstream { get; private set; }
        public List<Evaluation>? Evaluations { get; private set; }

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
            Evaluations ??= new List<Evaluation>();
            Evaluations.Add(downstream);
        }

        public abstract bool IsCountable { get; }

        public abstract bool IsPartition { get; }

        public abstract SourceText Render();
    }
}