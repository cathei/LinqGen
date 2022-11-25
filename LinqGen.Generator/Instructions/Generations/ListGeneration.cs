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

    public sealed class ListGeneration : Generation
    {
        public TypeSyntax SourceType { get; }
        public TypeSyntax SourceEnumeratorType { get; }

        public ListGeneration(in LinqGenExpression expression, int id,
            INamedTypeSymbol enumerableSymbol, INamedTypeSymbol listSymbol) : base(expression, id)
        {
            // TODO generic type element
            ITypeSymbol elementSymbol = listSymbol.TypeArguments[0];
            ITypeSymbol enumeratorSymbol = GetEnumeratorSymbol(enumerableSymbol);

            OutputElementType = ParseTypeName(elementSymbol);
            SourceType = ParseTypeName(enumerableSymbol);
            SourceEnumeratorType = ParseTypeName(enumeratorSymbol);
        }

        public override TypeSyntax OutputElementType { get; }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Both, SourceType, VarName("source"));

            if (!isLocal)
            {
                yield return new MemberInfo(MemberKind.Enumerator, SourceType, VarName("iter"));
            }
        }

        public override ExpressionSyntax RenderCount()
        {
            return MemberAccessExpression(VarName("source"), CountProperty);
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(RenderOption option)
        {
            if (!option.IsLocal)
            {
                yield return ExpressionStatement(SimpleAssignmentExpression(
                    VarName("iter"), InvocationExpression(VarName("source"), GetEnumeratorMethod)));
            }
        }

        public override BlockSyntax RenderIteration(RenderOption option, SyntaxList<StatementSyntax> statements)
        {
            var currentName = VarName("current");
            var currentRewriter = new CurrentRewriter(currentName);

            // replace current variables of downstream
            statements = currentRewriter.VisitStatementSyntaxList(statements);

            StatementSyntax result;

            if (option.IsLocal)
            {
                // In local loop we don't have to worry about collection changing during iteration.
                statements = statements.Insert(0, LocalDeclarationStatement(
                    currentName.Identifier, ElementAccessExpression(VarName("source"), VarName("index"))));

                result = ForStatement(
                    VarName("index"), LiteralExpression(0), MemberAccessExpression(VarName("source"), CountProperty),
                    Block(statements));
            }
            else
            {
                statements = statements.Insert(0, LocalDeclarationStatement(
                    currentName.Identifier, MemberAccessExpression(VarName("source"), CurrentProperty)));

                result = WhileStatement(InvocationExpression(VarName("iter"), MoveNextMethod), Block(statements));
            }

            return Block(result);
        }
    }
}