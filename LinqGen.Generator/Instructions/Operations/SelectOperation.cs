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

    public class SelectOperation : Operation
    {
        private TypeSyntax ParameterTypeName { get; }
        private bool WithIndex { get; }
        private bool WithStruct { get; }

        public SelectOperation(in LinqGenExpression expression, int id,
            INamedTypeSymbol parameterType, bool withIndex, bool withStruct) : base(expression, id)
        {
            ParameterTypeName = ParseTypeName(parameterType
                .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

            // Func<TIn, TOut> or IStructFunction<TIn, TOut>
            // Func<TIn, int, TOut> or IStructFunction<TIn, int, TOut>
            OutputElementType = ParseTypeName(parameterType.TypeArguments[withIndex ? 2 : 1]
                .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

            WithIndex = withIndex;
            WithStruct = withStruct;
        }

        public override TypeSyntax OutputElementType { get; }

        // public override bool PreserveElementType => false;

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Both,
                WithStruct ? IdentifierName($"{TypeParameterPrefix}1") : ParameterTypeName, SelectorName);

            if (WithIndex)
                yield return new MemberInfo(MemberKind.Enumerator, IntType, IndexName);
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
                yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}1"), ParameterTypeName);
        }

        public override BlockSyntax RenderConstructorBody()
        {
            if (WithIndex)
            {
                return base.RenderConstructorBody()
                    .AddStatements(ExpressionStatement(SimpleAssignmentExpression(IndexName, LiteralExpression(-1))));
            }

            return base.RenderConstructorBody();
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            if (WithIndex)
            {
                return Block(
                    IfStatement(LogicalNotExpression(InvocationExpression(SourceName, MoveNextName)),
                        ReturnStatement(FalseExpression())),
                    ExpressionStatement(PreIncrementExpression(IndexName)),
                    ReturnStatement(TrueExpression()));
            }

            return base.RenderMoveNextBody();
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(InvocationExpression(
                MemberAccessExpression(SelectorName, InvokeName),
                WithIndex ?
                    ArgumentList(MemberAccessExpression(SourceName, CurrentName), PreIncrementExpression(IndexName)) :
                    ArgumentList(MemberAccessExpression(SourceName, CurrentName)))));
        }
    }
}