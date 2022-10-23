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

    public class WhereOperation : Operation
    {
        protected readonly NameSyntax ParameterTypeName;
        protected readonly bool WithIndex;
        protected readonly bool WithStruct;

        public WhereOperation(in LinqGenExpression expression, ITypeSymbol parameterType,
            bool withIndex, bool withStruct) : base(expression)
        {
            ParameterTypeName = ParseName(parameterType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
            WithIndex = withIndex;
            WithStruct = withStruct;
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Both,
                WithStruct ? IdentifierName("T1") : ParameterTypeName, PredicateName);

            if (WithIndex)
                yield return new MemberInfo(MemberKind.Enumerator, IntType, IndexName);
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
                yield return new TypeParameterInfo(ParameterTypeName);
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
            return Block(WhileStatement(TrueExpression(), Block(
                IfStatement(
                    LogicalNotExpression(InvocationExpression(SourceName, MoveNextName)),
                    ReturnStatement(FalseExpression())),
                IfStatement(
                    InvocationExpression(
                        MemberAccessExpression(PredicateName, InvokeName),
                        WithIndex
                            ? ArgumentList(MemberAccessExpression(SourceName, CurrentName),
                                PreIncrementExpression(IndexName))
                            : ArgumentList(MemberAccessExpression(SourceName, CurrentName))),
                    ReturnStatement(TrueExpression()))
            )));
        }
    }
}