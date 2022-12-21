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

    public class DistinctOperation : Operation
    {
        private ComparerKind ComparerKind { get; }

        public DistinctOperation(in LinqGenExpression expression, int id, ComparerKind comparerKind)
            : base(expression, id)
        {
            ComparerKind = comparerKind;
        }

        private TypeSyntax ComparerType
        {
            get
            {
                switch (ComparerKind)
                {
                    case ComparerKind.Default:
                        return EqualityComparerDefaultType(OutputElementType, OutputElementSymbol);

                    case ComparerKind.Interface:
                        return EqualityComparerInterfaceType(OutputElementType);

                    case ComparerKind.Struct:
                        return TypeName("Comparer");
                }

                throw new InvalidOperationException();
            }
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (ComparerKind == ComparerKind.Struct)
            {
                yield return new TypeParameterInfo(TypeName("Comparer"),
                    StructConstraint, TypeConstraint(EqualityComparerInterfaceType(OutputElementType)));
            }
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            if (ComparerKind != ComparerKind.Default)
                yield return new MemberInfo(MemberKind.Enumerable, ComparerType, VarName("comparer"));

            var pooledSetType = PooledSetType(OutputElementType, ComparerType, OutputElementSymbol.IsUnmanagedType);
            yield return new MemberInfo(MemberKind.Enumerator, pooledSetType, VarName("hashSet"));
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(
            bool isLocal, ExpressionSyntax source, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            var comparerExpression = ComparerKind == ComparerKind.Default
                ? EqualityComparerDefault(OutputElementType, OutputElementSymbol)
                : MemberAccessExpression(source, VarName("comparer"));

            var countExpression = Upstream.RenderCount() ?? LiteralExpression(0);

            var pooledSetType = PooledSetType(OutputElementType, ComparerType, OutputElementSymbol.IsUnmanagedType);

            var pooledSetCreation = ObjectCreationExpression(
                pooledSetType, ArgumentList(countExpression, comparerExpression), null);

            yield return ExpressionStatement(SimpleAssignmentExpression(VarName("hashSet"), pooledSetCreation));

            foreach (var statement in base.RenderInitialization(isLocal, source, skipVar, takeVar))
                yield return statement;
        }

        public override bool SupportPartition => false;

        public override ExpressionSyntax? RenderCount() => null;

        protected override StatementSyntax? RenderMoveNext()
        {
            return IfStatement(
                LogicalNotExpression(InvocationExpression(
                    MemberAccessExpression(VarName("hashSet"), AddMethod), ArgumentList(CurrentPlaceholder))),
                ContinueStatement());
        }

        public override IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
        {
            foreach (var statement in base.RenderDispose(isLocal))
                yield return statement;

            yield return ExpressionStatement(InvocationExpression(VarName("hashSet"), DisposeMethod));
        }
    }
}