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
        protected Generation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            ClassName = IdentifierName($"{expression.SignatureSymbol!.Name}_{id}");
        }

        public abstract TypeSyntax OutputElementType { get; }

        /// <summary>
        /// The qualified class name cached for rendering
        /// </summary>
        public NameSyntax ClassName { get; }

        // /// <summary>
        // /// If True, method will be embedded as member method
        // /// If false, method will be extension method
        // /// </summary>
        // public virtual bool ShouldBeMemberMethod => false;

        public List<Operation>? Downstream { get; private set; }
        public List<Evaluation>? Evaluations { get; private set; }

        public virtual void SetUpstream(Generation upstream)
        {
            // only operation can have upstream
            throw new NotSupportedException();
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

        public abstract SourceText Render();

        protected abstract IEnumerable<MemberInfo> GetMemberInfos();

        public abstract IEnumerable<StatementSyntax> RenderInitialization(RenderOption option);

        /// <summary>
        /// Return value is the return value of MoveNext().
        /// </summary>
        public abstract ExpressionSyntax RenderMoveNext(RenderOption option);

        /// <summary>
        /// Return value is the value of Current.
        /// Returning Null means the operation will not change the value of Upstream Current.
        /// </summary>
        public abstract ExpressionSyntax? RenderCurrent(RenderOption option);

        public abstract IEnumerable<StatementSyntax> RenderDispose(RenderOption option);



    }
}