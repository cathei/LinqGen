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

        private readonly TypeSyntax _concreteOutputElementType;

        public SelectOperation(in LinqGenExpression expression,
            ITypeSymbol parameterType, bool withIndex, bool withStruct) : base(expression)
        {
            ParameterTypeName = ParseTypeName(parameterType
                .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

            WithIndex = withIndex;
            WithStruct = withStruct;

            _concreteOutputElementType = ParseTypeName(expression.ElementSymbol
                .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        }

        /// <summary>
        /// When Select use struct function selector, the output type must be fixed.
        /// TODO: It might be possible to just have overloads of methods, but not enumerable type?
        /// </summary>
        public sealed override bool SupportGenericElementOutput => base.SupportGenericElementOutput && !WithStruct;

        public override TypeSyntax OutputElementType =>
            SupportGenericElementOutput ? base.OutputElementType : _concreteOutputElementType;

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Both,
                WithStruct ? IdentifierName("T1") : ParameterTypeName, SelectorName);

            if (WithIndex)
                yield return new MemberInfo(MemberKind.Enumerator, IntType, IndexName);
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
                yield return new TypeParameterInfo(IdentifierName("T1"), ParameterTypeName);
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