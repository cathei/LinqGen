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

        public override TypeSyntax? DummyParameterType => WithStruct && WithIndex ? BoolType : null;

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
            {
                yield return new TypeParameterInfo(TypeName("Predicate"), PredicateType);
            }
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Both,
                WithStruct ? TypeName("Predicate") : PredicateType, VarName("predicate"));

            if (WithIndex)
                yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"), LiteralExpression(-1));
        }

        public override bool SupportPartition => false;

        public override ExpressionSyntax? RenderCount()
        {
            return null;
        }

        protected override StatementSyntax RenderMoveNext()
        {
            return IfStatement(
                LogicalNotExpression(InvocationExpression(
                    MemberAccessExpression(VarName("predicate"), InvokeMethod),
                    ArgumentList(WithIndex
                        ? new ExpressionSyntax[] { CurrentPlaceholder, PreIncrementExpression(VarName("index")) }
                        : new ExpressionSyntax[] { CurrentPlaceholder }))),
                ContinueStatement());
        }
    }
}