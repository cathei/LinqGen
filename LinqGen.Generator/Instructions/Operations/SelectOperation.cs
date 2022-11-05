// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public sealed class SelectOperation : Operation
    {
        private TypeSyntax ParameterTypeName { get; }
        private bool WithIndex { get; }
        private bool WithStruct { get; }

        public SelectOperation(in LinqGenExpression expression, int id,
            INamedTypeSymbol parameterType, bool withIndex, bool withStruct) : base(expression, id)
        {
            ParameterTypeName = ParseTypeName(parameterType);

            // Func<TIn, TOut> or IStructFunction<TIn, TOut>
            // Func<TIn, int, TOut> or IStructFunction<TIn, int, TOut>
            OutputElementType = ParseTypeName(parameterType.TypeArguments[withIndex ? 2 : 1]);

            WithIndex = withIndex;
            WithStruct = withStruct;
        }

        public override TypeSyntax OutputElementType { get; }

        // public override bool PreserveElementType => false;

        public override bool IsCountable => Upstream!.IsCountable;
        public override bool IsPartition => Upstream!.IsPartition;

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Both,
                WithStruct ? IdentifierName($"{TypeParameterPrefix}1") : ParameterTypeName, SelectorVar);

            if (WithIndex)
                yield return new MemberInfo(MemberKind.Enumerator, IntType, IndexVar);
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
                yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}1"), ParameterTypeName);
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            var syntax = base.RenderEnumeratorConstructor();

            if (WithIndex)
            {
                syntax = syntax.AddBodyStatements(
                    ExpressionStatement(SimpleAssignmentExpression(IndexVar, LiteralExpression(-1))));
            }

            return syntax;
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            if (WithIndex)
            {
                return Block(
                    IfStatement(LogicalNotExpression(InvocationExpression(SourceVar, MoveNextMethod)),
                        ReturnStatement(FalseExpression())),
                    ExpressionStatement(PreIncrementExpression(IndexVar)),
                    ReturnStatement(TrueExpression()));
            }

            return base.RenderMoveNextBody();
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(InvocationExpression(
                MemberAccessExpression(SelectorVar, InvokeMethod),
                WithIndex ?
                    ArgumentList(MemberAccessExpression(SourceVar, CurrentProperty), PreIncrementExpression(IndexVar)) :
                    ArgumentList(MemberAccessExpression(SourceVar, CurrentProperty)))));
        }
    }
}