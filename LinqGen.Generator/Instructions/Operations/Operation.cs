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

    /// <summary>
    /// Operation take LinqGen enumerable as input, and produces another enumerable as output
    /// </summary>
    public abstract class Operation : Generation
    {
        protected Operation(in LinqGenExpression expression, int id) : base(expression, id) { }

        public override void SetUpstream(Generation upstream)
        {
            base.Upstream = upstream;
            upstream.AddDownstream(this);
        }

        /// <summary>
        /// Upstream must be assigned for Operations
        /// </summary>
        public new Generation Upstream => base.Upstream!;

        public override ITypeSymbol OutputElementSymbol => Upstream.OutputElementSymbol;
        public override TypeSyntax OutputElementType => Upstream.OutputElementType;

        /// <summary>
        /// Operations are exposed as enumerable member by default.
        /// </summary>
        public override MethodKind MethodKind => MethodKind.Enumerable;

        public override bool SupportPartition => Upstream.SupportPartition;

        public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal, ExpressionSyntax source,
            ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            if (!Upstream.SupportPartition)
            {
                skipVar = null;
                takeVar = null;
            }

            return Upstream.RenderInitialization(isLocal, source, skipVar, takeVar);
        }

        protected virtual StatementSyntax? RenderMoveNext()
        {
            return null;
        }

        protected virtual ExpressionSyntax? RenderCurrent()
        {
            return null;
        }

        public override IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
        {
            return Upstream.RenderDispose(isLocal);
        }

        public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
        {
            // note that we are adding statements in reversed order
            var getCurrent = RenderCurrent();

            if (getCurrent != null)
            {
                var currentName = VarName("current");
                var currentRewriter = new PlaceholderRewriter(currentName);

                // replace current variables of downstream
                statements = currentRewriter.VisitStatementSyntaxList(statements);

                // define current variable
                statements = statements.Insert(0, LocalDeclarationStatement(currentName.Identifier, getCurrent));
            }

            // MoveNext should be passed to get current
            var moveNext = RenderMoveNext();

            if (moveNext != null)
                statements = statements.Insert(0, moveNext);

            return Upstream.RenderIteration(isLocal, statements);
        }

        /// <summary>
        /// "Dummy" parameter to prevent signature conflict.
        /// This also can be solved by using extension method, but for now all operation is enumerable method.
        /// </summary>
        public virtual TypeSyntax? DummyParameterType => null;

        public IEnumerable<MemberDeclarationSyntax> RenderUpstreamMembers()
        {
            if (MethodKind == MethodKind.Enumerable)
            {
                int arityDiff = Arity - Upstream.Arity;

                var parameters = GetParameters(MemberKind.Enumerable, false, true);

                if (DummyParameterType != null)
                    parameters = parameters.Append(Parameter(DummyParameterType, Identifier("unused"), DefaultLiteral));

                yield return MethodDeclaration(new(AggressiveInliningAttributeList), PublicTokenList,
                    ResolvedClassName, null, MethodName.Identifier, GetTypeParameters(arityDiff),
                    ParameterList(parameters),
                    GetGenericConstraints(arityDiff), null,
                    ArrowExpressionClause(ObjectCreationExpression(
                        ResolvedClassName, ArgumentList(GetArguments(MemberKind.Enumerable, true)), null)),
                    SemicolonToken);
            }
        }
    }
}