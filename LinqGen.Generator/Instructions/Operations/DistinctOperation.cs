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
        private bool WithComparerParameter { get; }

        public DistinctOperation(in LinqGenExpression expression, int id, bool withComparer) : base(expression, id)
        {
            WithComparerParameter = withComparer;
        }

        private bool IsElementEquatable => TryGetEquatableSelfInterface(OutputElementSymbol, out _);

        private TypeSyntax ComparerType
        {
            get
            {
                if (WithComparerParameter)
                    return TypeName("Comparer");

                if (IsElementEquatable)
                    return GenericName(Identifier("EquatableComparer"), TypeArgumentList(OutputElementType));

                return ComparerInterfaceType;
            }
        }

        private TypeSyntax ComparerInterfaceType =>
            GenericName(Identifier("IEqualityComparer"), TypeArgumentList(OutputElementType));

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithComparerParameter)
            {
                yield return new TypeParameterInfo(TypeName("Comparer"), ComparerInterfaceType);
            }
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            ExpressionSyntax comparerExpression;

            if (WithComparerParameter)
            {
                comparerExpression = VarName("comparer");
                yield return new MemberInfo(MemberKind.Both, ComparerType, VarName("comparer"));
            }
            else
            {
                comparerExpression = IsElementEquatable
                    ? ObjectCreationExpression(ComparerType, EmptyArgumentList, null)
                    : EqualityComparerDefault(OutputElementType);
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