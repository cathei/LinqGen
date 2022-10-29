// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public sealed class WhereOperation : Operation
    {
        private TypeSyntax ParameterTypeName { get; }
        private bool WithIndex { get; }
        private bool WithStruct { get; }

        public WhereOperation(in LinqGenExpression expression, int id,
            ITypeSymbol parameterType, bool withIndex, bool withStruct) : base(expression, id)
        {
            ParameterTypeName = ParseTypeName(parameterType);
            WithIndex = withIndex;
            WithStruct = withStruct;
        }

        public override bool IsCollection => false;
        public override bool IsPartition => false;

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Both,
                WithStruct ? IdentifierName($"{TypeParameterPrefix}1") : ParameterTypeName, PredicateName);

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
            return Block(WhileStatement(InvocationExpression(SourceName, MoveNextName), Block(
                    IfStatement(
                        InvocationExpression(
                            MemberAccessExpression(PredicateName, InvokeName),
                            WithIndex
                                ? ArgumentList(MemberAccessExpression(SourceName, CurrentName),
                                    PreIncrementExpression(IndexName))
                                : ArgumentList(MemberAccessExpression(SourceName, CurrentName))),
                        ReturnStatement(TrueExpression())))),
                ReturnStatement(FalseExpression()));
        }
    }
}