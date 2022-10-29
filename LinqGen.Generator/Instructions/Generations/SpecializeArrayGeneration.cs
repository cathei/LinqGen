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

    public sealed class SpecializeArrayGeneration : CompilingGeneration
    {
        private TypeSyntax CallerEnumerableType { get; }

        public SpecializeArrayGeneration(in LinqGenExpression expression, int id,
            IArrayTypeSymbol arraySymbol) : base(expression, id)
        {
            OutputElementType = ParseTypeName(arraySymbol.ElementType);
            CallerEnumerableType = ParseTypeName(arraySymbol);
        }

        public override TypeSyntax OutputElementType { get; }

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            yield return new MemberInfo(
                MemberKind.Both, CallerEnumerableType, SourceName);

            yield return new MemberInfo(
                MemberKind.Enumerator, IntType, IndexName);
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            return base.RenderEnumeratorConstructor().AddBodyStatements(
                ExpressionStatement(SimpleAssignmentExpression(IndexName, LiteralExpression(-1))));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(ReturnStatement(LessThanExpression(
                PreIncrementExpression(IndexName), MemberAccessExpression(SourceName, IdentifierName("Length")))));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(ElementAccessExpression(SourceName, BracketedArgumentList(IndexName))));
        }
    }
}