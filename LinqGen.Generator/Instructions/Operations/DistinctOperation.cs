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
        private bool WithStruct { get; }

        public DistinctOperation(in LinqGenExpression expression, int id, bool withStruct) : base(expression, id)
        {
            WithStruct = withStruct;
        }

        public override bool IsCountable => false;
        public override bool IsPartition => false;

        private TypeSyntax ComparerInterfaceName =>
            GenericName(Identifier("IEqualityComparer"), TypeArgumentList(Upstream!.OutputElementType));

        private GenericNameSyntax HashSetType =>
            GenericName(Identifier("PooledSet"), TypeArgumentList(Upstream!.OutputElementType, ComparerType));

        private TypeSyntax ComparerType =>
            WithStruct ? IdentifierName($"{TypeParameterPrefix}1") : ComparerInterfaceName;

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Enumerable,
                ComparerType, ComparerVar, WithStruct ? null : NullLiteral);

            yield return new MemberInfo(MemberKind.Enumerator, HashSetType, HashSetVar);
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
            {
                yield return new TypeParameterInfo(
                    IdentifierName($"{TypeParameterPrefix}1"), TypeConstraint(ComparerInterfaceName));
            }
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

        public override ConstructorDeclarationSyntax RenderEnumerableConstructor()
        {
            var syntax = base.RenderEnumerableConstructor();

            if (!WithStruct)
            {
                var block = syntax.Body!;

                // EqualityComparer<T>.Default if null
                var statements = block.Statements.Insert(0,
                    ExpressionStatement(SimpleAssignmentExpression(ComparerVar,
                        NullCoalesce(ComparerVar, MemberAccessExpression(
                            GenericName(Identifier("EqualityComparer"), TypeArgumentList(Upstream!.OutputElementType)),
                            IdentifierName("Default"))))));

                syntax = syntax.WithBody(block.WithStatements(statements));
            }

            return syntax;
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
    }
}