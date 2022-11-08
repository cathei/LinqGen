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

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            yield return new MemberInfo(
                MemberKind.Both, CallerEnumerableType, SourceVar);

            yield return new MemberInfo(
                MemberKind.Enumerator, IntType, IndexVar);

            yield return new MemberInfo(
                MemberKind.Enumerator, IntType, TakeVar);
        }

        public override BlockSyntax RenderCountGetBody()
        {
            return Block(ReturnStatement(
                MemberAccessExpression(SourceVar, LengthProperty)));
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType,
                ArgumentList(SourceVar, LiteralExpression(0), CountProperty), null)));
        }

        public override BlockSyntax RenderGetSliceEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType,
                ArgumentList(SourceVar, SkipVar, ConditionalExpression(
                    MemberAccessExpression(TakeVar, HasValueProperty),
                    MathMin(
                        SubtractExpression(CountProperty, SkipVar),
                        MemberAccessExpression(TakeVar, ValueProperty)),
                    SubtractExpression(CountProperty, SkipVar))), null)));
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            return base.RenderEnumeratorConstructor()
                .AddParameterListParameters(
                    Parameter(IntType, SkipVar.Identifier), Parameter(IntType, TakeVar.Identifier))
                .AddBodyStatements(
                    ExpressionStatement(SimpleAssignmentExpression(
                        MemberAccessExpression(ThisExpression(), IndexVar),
                        SubtractExpression(SkipVar, LiteralExpression(1)))),
                    ExpressionStatement(SimpleAssignmentExpression(
                        MemberAccessExpression(ThisExpression(), TakeVar), TakeVar)));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(ReturnStatement(LessThanExpression(PreIncrementExpression(IndexVar), TakeVar)));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(ElementAccessExpression(SourceVar, BracketedArgumentList(IndexVar))));
        }
    }
}