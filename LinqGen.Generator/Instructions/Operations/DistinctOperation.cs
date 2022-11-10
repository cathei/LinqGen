// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public sealed class DistinctOperation : Operation
    {
        public DistinctOperation(in LinqGenExpression expression, int id) : base(expression, id)
        {
        }

        public override bool IsCountable => false;
        public override bool IsPartition => false;

        private TypeSyntax ComparerInterfaceName =>
            GenericName(Identifier("IEqualityComparer"), TypeArgumentList(Upstream!.OutputElementType));

        private GenericNameSyntax HashSetType =>
            GenericName(Identifier("PooledSet"),
                TypeArgumentList(Upstream!.OutputElementType, IdentifierName($"{TypeParameterPrefix}1")));

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Enumerable,
                IdentifierName($"{TypeParameterPrefix}1"), ComparerVar);

            yield return new MemberInfo(MemberKind.Enumerator, HashSetType, HashSetVar);
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            yield return new TypeParameterInfo(
                IdentifierName($"{TypeParameterPrefix}1"), TypeConstraint(ComparerInterfaceName));
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            var hashSetCreation = ObjectCreationExpression(HashSetType,
                ArgumentList(Upstream!.IsCountable
                        ? MemberAccessExpression(SourceVar, CountProperty)
                        : LiteralExpression(0),
                    ComparerVar), default);

            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType,
                ArgumentList(GetArguments(MemberKind.Both)
                    .Prepend(Argument(InvocationExpression(SourceVar, GetEnumeratorMethod)))
                    .Append(Argument(hashSetCreation))), null)));
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            return base.RenderEnumeratorConstructor()
                .AddParameterListParameters(Parameter(HashSetType, HashSetVar.Identifier))
                .AddBodyStatements(ExpressionStatement(SimpleAssignmentExpression(
                    MemberAccessExpression(ThisExpression(), HashSetVar), HashSetVar)));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(
                WhileStatement(InvocationExpression(SourceVar, MoveNextMethod),
                    Block(IfStatement(InvocationExpression(
                            MemberAccessExpression(HashSetVar, AddMethod),
                            ArgumentList(MemberAccessExpression(SourceVar, CurrentProperty))),
                        ReturnStatement(TrueExpression())))),
                ReturnStatement(FalseExpression()));
        }

        public override BlockSyntax RenderDisposeBody()
        {
            return Block(ExpressionStatement(
                InvocationExpression(MemberAccessExpression(HashSetVar, DisposeMethod))));
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderExtensionMethods()
        {
            var returnStatement = ReturnStatement(
                ObjectCreationExpression(ResolvedClassName,
                    ArgumentList(GetArguments(MemberKind.Enumerable)), default));

            var parameters = ParameterList(GetParameters(MemberKind.Enumerable, true));

            var methodWithComparer = MethodDeclaration(
                new(AggressiveInliningAttributeList), PublicStaticTokenList, ResolvedClassName, null,
                MethodName.Identifier, GetTypeParameters(), parameters, GetGenericConstraints(),
                Block(returnStatement), default, default);

            var parametersWithoutComparer = ParameterList(
                parameters.Parameters.RemoveAt(parameters.Parameters.Count - 1));

            var typeArguments = GetTypeArguments(skip: 1) ?? TypeArgumentList();

            var typeArgumentsWithDefault =
                TypeArgumentList(typeArguments.Arguments.Insert(0, ComparerInterfaceName));

            var resolvedNameWithDefault = GenericName(IdentifierName.Identifier, typeArgumentsWithDefault);

            // EqualityComparer<T>.Default
            var defaultComparerStatement = LocalDeclarationStatement(ComparerVar.Identifier,
                MemberAccessExpression(
                    GenericName(Identifier("EqualityComparer"), TypeArgumentList(Upstream!.OutputElementType)),
                    IdentifierName("Default")));

            var returnStatementWithDefault = ReturnStatement(
                ObjectCreationExpression(resolvedNameWithDefault,
                    ArgumentList(GetArguments(MemberKind.Enumerable)), default));

            var methodWithoutComparer = MethodDeclaration(
                new(AggressiveInliningAttributeList), PublicStaticTokenList, resolvedNameWithDefault, null,
                MethodName.Identifier, GetTypeParameters(skip: 1),
                parametersWithoutComparer, GetGenericConstraints(skip: 1),
                Block(defaultComparerStatement, returnStatementWithDefault), default, default);

            yield return methodWithComparer;
            yield return methodWithoutComparer;
        }
    }
}