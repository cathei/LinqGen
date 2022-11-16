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

    /// <summary>
    /// Array can be enumerated with index as it has fixed size and no version check
    /// </summary>
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
        }

        public override BlockSyntax RenderCountGetBody()
        {
            return Block(ReturnStatement(
                MemberAccessExpression(SourceVar, LengthProperty)));
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType,
                ArgumentList(SourceVar, LiteralExpression(0)), null)));
        }

        public override BlockSyntax RenderGetSliceEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType,
                ArgumentList(SourceVar, SkipVar), null)));
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            return base.RenderEnumeratorConstructor()
                .AddParameterListParameters(Parameter(IntType, SkipVar.Identifier))
                .AddBodyStatements(
                    ExpressionStatement(SimpleAssignmentExpression(
                        MemberAccessExpression(ThisExpression(), IndexVar),
                        SubtractExpression(SkipVar, LiteralExpression(1)))));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(ReturnStatement(LessThanExpression(
                CastExpression(UIntType, PreIncrementExpression(IndexVar)),
                CastExpression(UIntType, MemberAccessExpression(SourceVar, LengthProperty)))));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(ElementAccessExpression(SourceVar, BracketedArgumentList(IndexVar))));
        }
    }
}