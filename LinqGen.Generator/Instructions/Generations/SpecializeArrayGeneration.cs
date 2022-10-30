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
            // TODO prevent generic type element?
            ITypeSymbol elementSymbol = arraySymbol.ElementType;

            OutputElementType = ParseTypeName(elementSymbol);
            CallerEnumerableType = ParseTypeName(arraySymbol);
        }

        public override TypeSyntax OutputElementType { get; }

        public override bool IsCountable => true;
        public override bool IsPartition => true;

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            yield return new MemberInfo(
                MemberKind.Both, CallerEnumerableType, SourceName);

            yield return new MemberInfo(
                MemberKind.Enumerator, IntType, IndexName);

            yield return new MemberInfo(
                MemberKind.Enumerator, IntType, TakeName);
        }

        public override BlockSyntax RenderCountGetBody()
        {
            return Block(ReturnStatement(
                MemberAccessExpression(SourceName, LengthName)));
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorName,
                ArgumentList(SourceName, LiteralExpression(0), CountName), null)));
        }

        public override BlockSyntax RenderGetSliceEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorName,
                ArgumentList(SourceName, SkipName, MathMin(SubtractExpression(CountName, SkipName), TakeName)), null)));
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            return base.RenderEnumeratorConstructor()
                .AddParameterListParameters(
                    Parameter(IntType, SkipName.Identifier), Parameter(IntType, TakeName.Identifier))
                .AddBodyStatements(
                    ExpressionStatement(SimpleAssignmentExpression(
                        MemberAccessExpression(ThisExpression(), IndexName),
                        SubtractExpression(SkipName, LiteralExpression(1)))),
                    ExpressionStatement(SimpleAssignmentExpression(
                        MemberAccessExpression(ThisExpression(), TakeName), TakeName)));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(ReturnStatement(LessThanExpression(PreIncrementExpression(IndexName), TakeName)));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(ElementAccessExpression(SourceName, BracketedArgumentList(IndexName))));
        }
    }
}