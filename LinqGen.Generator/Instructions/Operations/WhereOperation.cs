// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public class WhereOperation : Operation
    {
        private TypeSyntax PredicateType { get; }
        private bool WithIndex { get; }
        private bool WithStruct { get; }

        public WhereOperation(in LinqGenExpression expression, int id,
            ITypeSymbol parameterType, bool withIndex, bool withStruct) : base(expression, id)
        {
            PredicateType = ParseTypeName(parameterType);
            WithIndex = withIndex;
            WithStruct = withStruct;
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
            {
                yield return new TypeParameterInfo(TypeName("1"), PredicateType);
            }
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            yield return new MemberInfo(MemberKind.Both,
                WithStruct ? TypeName("1") : PredicateType, VarName("predicate"));

            if (WithIndex)
                yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"));
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(RenderOption option)
        {
            if (WithIndex)
            {
                yield return ExpressionStatement(
                    SimpleAssignmentExpression(VarName("index"), LiteralExpression(-1)));
            }
        }

        public override ExpressionSyntax RenderMoveNext(RenderOption option)
        {
            return InvocationExpression(MemberAccessExpression(VarName("predicate"), InvokeMethod),
                ArgumentList(WithIndex
                    ? new ExpressionSyntax[] { CurrentVar, PreIncrementExpression(VarName("index")) }
                    : new ExpressionSyntax[] { CurrentVar }));
        }
    }
}