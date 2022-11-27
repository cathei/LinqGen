// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public abstract class LocalEvaluation : Evaluation
    {
        public LocalEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
        }

        protected abstract IEnumerable<StatementSyntax> RenderAccumulation();
        protected abstract ExpressionSyntax RenderResult();

        protected virtual IEnumerable<StatementSyntax> RenderInitialization()
        {
            yield break;
        }

        protected virtual IEnumerable<StatementSyntax> RenderDispose()
        {
            yield break;
        }

        protected abstract TypeSyntax ReturnType { get; }

        protected virtual IEnumerable<ParameterInfo> GetParameterInfos()
        {
            yield break;
        }

        protected ParameterListSyntax GetParameters()
        {
            return ParameterList(GetParameterInfos().Select(x => x.AsParameter(true)));
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderUpstreamMembers()
        {
            var arityDiff = Arity - Upstream.Arity;

            yield return MethodDeclaration(
                SingletonList(AggressiveInliningAttributeList), PublicTokenList,
                ReturnType, null, MethodName.Identifier, GetTypeParameters(arityDiff), GetParameters(),
                GetGenericConstraints(arityDiff), RenderBody(), null, default);
        }

        private BlockSyntax RenderBody()
        {
            var initialDeclarations = Upstream.GetLocalDeclarations(MemberKind.Enumerator);

            var initialAssignments = Upstream.GetLocalAssignments(MemberKind.Both)
                .Concat(Upstream.RenderInitialization(true, null, null))
                .Concat(RenderInitialization());

            var accumulationStatements = RenderAccumulation();

            var iterationBlock = Upstream.RenderIteration(true, List(accumulationStatements));

            var disposeBlock = Block(Upstream.RenderDispose(true).Concat(RenderDispose()));

            var iterationStatements = iterationBlock.Statements;
            iterationStatements = iterationStatements.InsertRange(0, initialAssignments);
            iterationStatements = iterationStatements.Add(ReturnStatement(RenderResult()));

            BlockSyntax body;

            if (disposeBlock.Statements.Count == 0)
            {
                body = Block(initialDeclarations.Concat(iterationStatements));
            }
            else
            {
                StatementSyntax tryStatement = TryStatement(
                    Block(iterationStatements), default, FinallyClause(disposeBlock));

                body = Block(initialDeclarations.Append(tryStatement));
            }

            return body;
        }
    }
}
