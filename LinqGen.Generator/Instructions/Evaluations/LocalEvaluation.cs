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
        private readonly RenderOption _renderOption;

        protected abstract TypeSyntax ReturnType { get; }

        public LocalEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            _renderOption = new(true);
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderUpstreamMembers()
        {
            yield return MethodDeclaration(
                SingletonList(AggressiveInliningAttributeList), PublicTokenList,
                ReturnType, null, MethodName.Identifier, null,
                ParameterList(), default, RenderBody(), null, default);
        }

        protected abstract IEnumerable<StatementSyntax> RenderInitialization();
        protected abstract IEnumerable<StatementSyntax> RenderAccumulation();
        protected abstract IEnumerable<StatementSyntax> RenderReturn();

        private BlockSyntax RenderBody()
        {
            var initialStatements =
                Upstream.GetLocalDeclarations(MemberKind.Enumerator)
                    .Concat(Upstream.GetLocalAssignments(MemberKind.Both))
                    .Concat(RenderInitialization());

            var disposeStatements = Upstream.RenderDispose(_renderOption);

            var body = Upstream.RenderIteration(_renderOption, new(RenderAccumulation()));
            var statements = body.Statements;

            statements = statements.InsertRange(0, initialStatements);
            statements = statements.AddRange(disposeStatements);
            statements = statements.AddRange(RenderReturn());

            return body.WithStatements(statements);
        }
    }
}
