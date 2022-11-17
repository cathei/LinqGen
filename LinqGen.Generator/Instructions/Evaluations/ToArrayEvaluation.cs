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

    public sealed class ToArrayEvaluation : Evaluation
    {
        private readonly RenderOption _renderOption;

        private TypeSyntax ReturnType { get; }

        public ToArrayEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            _renderOption = new(true);

            ReturnType = ParseTypeName(expression.MethodSymbol.ReturnType);
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderMembers()
        {
            yield return MethodDeclaration(SingletonList(AggressiveInliningAttributeList), PublicTokenList,
                ReturnType, null, Identifier("ToArray"), null, ParameterList(), default, RenderBody(), null, default);
        }

        private BlockSyntax RenderBody()
        {
            var initialStatements =
                Upstream.GetLocalDeclarations(MemberKind.Enumerator)
                    .Concat(Upstream.GetLocalAssignments(MemberKind.Both));

            var disposeStatements = Upstream.RenderDispose(_renderOption);

            var successStatements = new StatementSyntax[]
            {
                ExpressionStatement(SimpleAssignmentExpression(
                    ElementAccessExpression(IdentifierName("array"), PreIncrementExpression(IdentifierName("index"))),
                    CurrentPlaceholder)),
            };

            var returnStatement = ReturnStatement(IdentifierName("array"));

            var body = Upstream.RenderIteration(_renderOption, new(successStatements));
            var statements = body.Statements;

            statements = statements.InsertRange(0, initialStatements);
            statements = statements.AddRange(disposeStatements);
            statements = statements.Add(returnStatement);

            return body.WithStatements(statements);
        }
    }
}
