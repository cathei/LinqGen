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
            ExpressionSyntax comparerExpression;

            if (ComparerKind == ComparerKind.Default)
            {
                comparerExpression = EqualityComparerDefault(OutputElementType, OutputElementSymbol);
            }
            else
            {
                comparerExpression = VarName("comparer");
                yield return new MemberInfo(MemberKind.Both, ComparerType, VarName("comparer"));
            }

            var countExpression = Upstream.RenderCount() ?? LiteralExpression(0);

            var pooledSetType = PooledSetType(OutputElementType, ComparerType, OutputElementSymbol.IsUnmanagedType);
            var pooledSetCreation = ObjectCreationExpression(
                pooledSetType, ArgumentList(countExpression, comparerExpression), null);

            yield return new MemberInfo(MemberKind.Enumerator, pooledSetType, VarName("hashSet"), pooledSetCreation);
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