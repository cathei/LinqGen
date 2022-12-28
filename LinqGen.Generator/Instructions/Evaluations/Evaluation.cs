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
        protected Evaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            MethodSymbol = expression.MethodSymbol;
            MethodName = IdentifierName(MethodSymbol.Name);

            InputElementSymbol = expression.InputElementSymbol!;
            InputElementType = ParseTypeName(InputElementSymbol);
        }

        public IMethodSymbol MethodSymbol { get; }
        public IdentifierNameSyntax MethodName { get; }

        public ITypeSymbol InputElementSymbol { get; }
        protected override TypeSyntax InputElementType { get; }

        /// <summary>
        /// Evaluations are exposed as enumerable member by default.
        /// </summary>
        public override MethodKind MethodKind => MethodKind.Enumerable;

        /// <summary>
        /// Evaluation should not rendered individually. Instead it will be rendered with upstream.
        /// </summary>
        public virtual void AddUpstream(Generation upstream)
        {
            base.Upstreams ??= new List<Generation>();
            base.Upstreams.Add(upstream);

            // only first upstream
            if (base.Upstreams.Count == 1)
                Upstream.AddEvaluation(this);
        }

        /// <summary>
        /// Upstream must be assigned for Evaluations
        /// </summary>
        public new Generation Upstream => base.Upstream!;

        /// <summary>
        /// Upstreams must be assigned for Evaluations
        /// </summary>
        public new List<Generation> Upstreams => base.Upstreams!;

        public virtual IEnumerable<MemberDeclarationSyntax> RenderUpstreamMembers()
        {
            yield break;
        }

        public virtual IEnumerable<MemberDeclarationSyntax> RenderExtensionMembers()
        {
            yield break;
        }
    }
}