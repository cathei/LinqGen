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
        // public TypeSyntax SourceEnumeratorType { get; }

        public ListGeneration(in LinqGenExpression expression, int id,
            INamedTypeSymbol enumerableSymbol, INamedTypeSymbol listSymbol) : base(expression, id)
        {
            // TODO generic type element
            ITypeSymbol elementSymbol = listSymbol.TypeArguments[0];
            // ITypeSymbol enumeratorSymbol = GetEnumeratorSymbol(enumerableSymbol);

            OutputElementType = ParseTypeName(elementSymbol);
            SourceType = ParseTypeName(enumerableSymbol);
            // SourceEnumeratorType = ParseTypeName(enumeratorSymbol);
        }

        public override TypeSyntax OutputElementType { get; }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Both, SourceType, VarName("source"));
            yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"));
        }

        public override bool SupportPartition => true;

        public override ExpressionSyntax RenderCount()
        {
            return MemberAccessExpression(VarName("source"), CountProperty);
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(
            bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            if (skipVar != null)
            {
                yield return ExpressionStatement(SimpleAssignmentExpression(
                    VarName("index"), SubtractExpression(skipVar, LiteralExpression(1))));
            }
            else
            {
                yield return ExpressionStatement(SimpleAssignmentExpression(
                    VarName("index"), LiteralExpression(-1)));
            }
        }

        public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
        {
            var currentName = VarName("current");
            var currentRewriter = new PlaceholderRewriter(currentName);

            // replace current variables of downstream
            statements = currentRewriter.VisitStatementSyntaxList(statements);

            statements = statements.Insert(0, LocalDeclarationStatement(
                currentName.Identifier, ElementAccessExpression(VarName("source"), VarName("index"))));

            var result = WhileStatement(LessThanExpression(
                    CastExpression(UIntType, PreIncrementExpression(VarName("index"))),
                    CastExpression(UIntType, MemberAccessExpression(VarName("source"), CountProperty))),
                Block(statements));

            return Block(result);
        }
    }
}