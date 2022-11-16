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

        public override bool IsCountable => false;
        public override bool IsPartition => false;

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Both,
                WithStruct ? IdentifierName($"{TypeParameterPrefix}1") : ParameterTypeName, PredicateVar);

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
            return Block(WhileStatement(InvocationExpression(SourceVar, MoveNextMethod), Block(
                    IfStatement(
                        InvocationExpression(
                            MemberAccessExpression(PredicateVar, InvokeMethod),
                            WithIndex
                                ? ArgumentList(MemberAccessExpression(SourceVar, CurrentProperty),
                                    PreIncrementExpression(IndexVar))
                                : ArgumentList(MemberAccessExpression(SourceVar, CurrentProperty))),
                        ReturnStatement(TrueExpression())))),
                ReturnStatement(FalseExpression()));
        }
    }
}