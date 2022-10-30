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
                MemberKind.Both, CallerEnumerableType, SourceField);

            yield return new MemberInfo(
                MemberKind.Enumerator, IntType, IndexField);

            yield return new MemberInfo(
                MemberKind.Enumerator, IntType, TakeField);
        }

        public override BlockSyntax RenderCountGetBody()
        {
            return Block(ReturnStatement(
                MemberAccessExpression(SourceField, LengthProperty)));
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType,
                ArgumentList(SourceField, LiteralExpression(0), CountProperty), null)));
        }

        public override BlockSyntax RenderGetSliceEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType,
                ArgumentList(SourceField, SkipField, ConditionalExpression(
                    MemberAccessExpression(TakeField, HasValueProperty),
                    MathMin(
                        SubtractExpression(CountProperty, SkipField),
                        MemberAccessExpression(TakeField, ValueProperty)),
                    SubtractExpression(CountProperty, SkipField))), null)));
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            return base.RenderEnumeratorConstructor()
                .AddParameterListParameters(
                    Parameter(IntType, SkipField.Identifier), Parameter(IntType, TakeField.Identifier))
                .AddBodyStatements(
                    ExpressionStatement(SimpleAssignmentExpression(
                        MemberAccessExpression(ThisExpression(), IndexField),
                        SubtractExpression(SkipField, LiteralExpression(1)))),
                    ExpressionStatement(SimpleAssignmentExpression(
                        MemberAccessExpression(ThisExpression(), TakeField), TakeField)));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(ReturnStatement(LessThanExpression(PreIncrementExpression(IndexField), TakeField)));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(ElementAccessExpression(SourceField, BracketedArgumentList(IndexField))));
        }
    }
}