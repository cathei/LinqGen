// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public sealed class SpecializeArrayMultiGeneration : CompilingGeneration
    {
        private TypeSyntax CallerEnumerableType { get; }

        private IArrayTypeSymbol ArrayTypeSymbol { get; }

        private int Rank { get; }

        public SpecializeArrayMultiGeneration(in LinqGenExpression expression, int id,
            IArrayTypeSymbol arraySymbol) : base(expression, id)
        {
            ArrayTypeSymbol = arraySymbol;

            OutputElementType = ParseTypeName(arraySymbol.ElementType);
            CallerEnumerableType = ParseTypeName(arraySymbol);

            Rank = arraySymbol.Rank;
        }

        public override TypeSyntax OutputElementType { get; }

        public override bool IsCollection => false;
        public override bool IsPartition => false;

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            yield return new MemberInfo(
                MemberKind.Both, CallerEnumerableType, SourceName);

            for (int i = 0; i < Rank; ++i)
            {
                yield return new MemberInfo(
                    MemberKind.Enumerator, IntType, IdentifierName($"index{i}"));
            }
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            var syntax = base.RenderEnumeratorConstructor();

            static StatementSyntax setIndexToLength(int i)
            {
                return ExpressionStatement(SimpleAssignmentExpression(IdentifierName($"index{i}"),
                    InvocationExpression(MemberAccessExpression(SourceName, IdentifierName("GetLength")),
                        ArgumentList(LiteralExpression(i)))));
            }

            var lengthZeroCheck = IfStatement(
                GreaterOrEqualExpression(LiteralExpression(0),
                    MemberAccessExpression(SourceName, IdentifierName("Length"))),
                Block(Enumerable.Range(0, Rank).Select(setIndexToLength).Append(ReturnStatement())));

            var statements = syntax.Body!.Statements.AddRange(
                Enumerable.Range(0, Rank)
                    .Select(i => ExpressionStatement(SimpleAssignmentExpression(IdentifierName($"index{i}"),
                        LiteralExpression(i == Rank - 1 ? -1 : 0))))
                    .Prepend<StatementSyntax>(lengthZeroCheck));

            return syntax.WithBody(Block(statements));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(Enumerable.Range(0, Rank).Reverse().Select(i => IfStatement(
                LessThanExpression(PreIncrementExpression(IdentifierName($"index{i}")),
                    InvocationExpression(MemberAccessExpression(SourceName, IdentifierName("GetLength")),
                        ArgumentList(LiteralExpression(i)))),
                Block(Enumerable.Range(i + 1, Rank - i - 1).Select(j => ExpressionStatement(
                        SimpleAssignmentExpression(IdentifierName($"index{j}"), LiteralExpression(0))))
                    .Append<StatementSyntax>(ReturnStatement(TrueExpression())))))
                .Append<StatementSyntax>(ReturnStatement(FalseExpression())));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(ElementAccessExpression(SourceName,
                BracketedArgumentList(SeparatedList(Enumerable.Range(0, Rank)
                    .Select(i => Argument(IdentifierName($"index{i}"))))))));
        }
    }
}