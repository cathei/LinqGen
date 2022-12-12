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

    public abstract class OrderingOperation : Operation
    {
        private bool WithSelector { get; }
        public bool WithStruct { get; }
        public bool Descending { get; }

        public OrderingOperation(in LinqGenExpression expression, int id,
            INamedTypeSymbol? selectorType, bool withStruct, bool descending) : base(expression, id)
        {
            WithStruct = withStruct;
            Descending = descending;

            if (selectorType != null)
            {
                WithSelector = true;
                SelectorInterfaceType = ParseTypeName(selectorType);
                SelectorKeyType = ParseTypeName(selectorType.TypeArguments[1]);
            }
            else
            {
                WithSelector = false;
                SelectorInterfaceType = null;
                SelectorKeyType = null;
            }
        }

        /// <summary>
        /// Upstream ordering chained with ThenBy.
        /// </summary>
        protected virtual OrderingOperation? UpstreamOrder => null;

        protected OrderingOperation RootOrder => UpstreamOrder?.RootOrder ?? this;

        private TypeSyntax? SelectorInterfaceType { get; }
        private TypeSyntax? SelectorKeyType { get; }

        private TypeSyntax KeyType => WithSelector ? SelectorKeyType! : OutputElementType;

        private TypeSyntax ComparerInterfaceType =>
            GenericName(Identifier("IComparer"), TypeArgumentList(KeyType));

        public override TypeSyntax EnumerableInterfaceType =>
            GenericName(Identifier("IInternalOrderedStub"), TypeArgumentList(OutputElementType));

        public override TypeSyntax? DummyParameterType
        {
            get
            {
                // parameter can collide in this case
                if (WithSelector && WithStruct)
                    return SelectorKeyType;
                return null;
            }
        }

        protected override bool ClearsUpstreamEnumerator => true;

        public override bool SupportPartition => true;

        public override ExpressionSyntax? RenderCount()
        {
            return Upstream.RenderCount();
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            var elementsType = PooledListType(OutputElementType);

            yield return new MemberInfo(MemberKind.Enumerator, elementsType, VarName("elements"),
                ObjectCreationExpression(elementsType, ArgumentList(LiteralExpression(0)), null));

            var indicesType = PooledListType(IntType);

            yield return new MemberInfo(MemberKind.Enumerator, PooledListType(IntType), VarName("indices"),
                ObjectCreationExpression(indicesType, ArgumentList(LiteralExpression(0)), null));

            yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"), LiteralExpression(-1));

            if (WithStruct)
            {
                if (WithSelector)
                    yield return new MemberInfo(MemberKind.Enumerable, TypeName("Selector"), VarName("selector"));

                yield return new MemberInfo(
                    MemberKind.Enumerable, TypeName("Comparer"), VarName("comparer"));
            }
            else
            {
                if (WithSelector)
                    yield return new MemberInfo(MemberKind.Enumerable, SelectorInterfaceType!, VarName("selector"));

                yield return new MemberInfo(
                    MemberKind.Enumerable, ComparerInterfaceType, VarName("comparer"), NullLiteral);
            }
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
            {
                if (WithSelector)
                {
                    yield return new TypeParameterInfo(TypeName("Selector"), SelectorInterfaceType!);
                    yield return new TypeParameterInfo(TypeName("Comparer"), ComparerInterfaceType);
                }
                else
                {
                    // comparer requires struct type constraint for now... may refactor overloads
                    yield return new TypeParameterInfo(TypeName("Comparer"),
                        ClassOrStructConstraint(SyntaxKind.StructConstraint), TypeConstraint(ComparerInterfaceType));
                }
            }
        }

        public IEnumerable<ParameterSyntax> GetOrderParameters()
        {
            foreach (var member in GetOrderMemberInfos())
            {
                if (member.SelectorType != null)
                    yield return Parameter(member.SelectorType, member.SelectorName.Identifier);
                yield return Parameter(member.ComparerType, member.ComparerName.Identifier);
            }
        }

        public IEnumerable<ArgumentSyntax> GetOrderArguments(ExpressionSyntax sourceName)
        {
            foreach (var member in GetOrderMemberInfos())
            {
                if (member.SelectorType != null)
                    yield return Argument(MemberAccessExpression(sourceName, member.SelectorName));
                yield return Argument(MemberAccessExpression(sourceName, member.ComparerName));
            }
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderEnumerableMembers()
        {
            foreach (var member in base.RenderEnumerableMembers())
                yield return member;

            yield return SorterTemplate.Render(this);
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(
            bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            var rootUpstream = RootOrder.Upstream;
            ExpressionSyntax sourceName = isLocal ? ThisExpression() : IdentifierName("source");

            foreach (var statement in rootUpstream.GetLocalDeclarations(MemberKind.Enumerator))
                yield return statement;

            foreach (var statement in rootUpstream.GetLocalAssignments(MemberKind.Both, sourceName))
                yield return statement;

            foreach (var statement in rootUpstream.RenderInitialization(true, null, null))
                yield return statement;

            ExpressionSyntax countExpression = rootUpstream.RenderCount() ?? LiteralExpression(0);

            var elementsName = VarName("elements");
            var elementsCount = MemberAccessExpression(elementsName, CountProperty);

            yield return ExpressionStatement(SimpleAssignmentExpression(elementsName,
                ObjectCreationExpression(PooledListType(OutputElementType), ArgumentList(countExpression), null)));

            var addElementStatements = SingletonList<StatementSyntax>(
                ExpressionStatement(InvocationExpression(
                    MemberAccessExpression(VarName("elements"), AddMethod), ArgumentList(CurrentPlaceholder))));

            yield return rootUpstream.RenderIteration(true, addElementStatements);

            // TODO try block
            foreach (var statement in rootUpstream.RenderDispose(true))
                yield return statement;

            var indicesName = VarName("indices");

            var minName = IdentifierName("min");
            var maxName = IdentifierName("max");

            yield return LocalDeclarationStatement(minName.Identifier, skipVar ?? LiteralExpression(0));

            yield return LocalDeclarationStatement(maxName.Identifier, takeVar == null
                ? SubtractExpression(elementsCount, LiteralExpression(1))
                : SubtractExpression(AddExpression(minName, takeVar), LiteralExpression(1)));

            var sortBody = new List<StatementSyntax>();

            var sorterName = IdentifierName("sorter");

            sortBody.Add(ExpressionStatement(SimpleAssignmentExpression(indicesName,
                ObjectCreationExpression(PooledListType(IntType), ArgumentList(elementsCount), null))));

            var loopVar = IdentifierName("i");

            sortBody.Add(ForStatement(loopVar, LiteralExpression(0), elementsCount, ExpressionStatement(
                InvocationExpression(MemberAccessExpression(indicesName, AddMethod), ArgumentList(loopVar)))));

            sortBody.Add(LocalDeclarationStatement(sorterName.Identifier,
                ObjectCreationExpression(QualifiedName(ResolvedClassName, IdentifierName("Sorter")),
                    ArgumentList(GetOrderArguments(sourceName).Prepend(Argument(elementsName))), null)));

            sortBody.Add(ExpressionStatement(InvocationExpression(
                MemberAccessExpression(sorterName, IdentifierName("PartialQuickSort")),
                ArgumentList(MemberAccessExpression(indicesName, IdentifierName("Array")), LiteralExpression(0),
                    SubtractExpression(elementsCount, LiteralExpression(1)), minName, maxName))));

            sortBody.Add(ExpressionStatement(InvocationExpression(sorterName, DisposeMethod)));

            var elseBody = new List<StatementSyntax>();

            elseBody.Add(ExpressionStatement(InvocationExpression(elementsName, DisposeMethod)));

            yield return IfStatement(GreaterOrEqualExpression(maxName, minName),
                Block(sortBody), ElseClause(Block(elseBody)));
        }

        public override IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
        {
            yield return ExpressionStatement(InvocationExpression(VarName("elements"), DisposeMethod));
            yield return ExpressionStatement(InvocationExpression(VarName("indices"), DisposeMethod));
        }

        public IEnumerable<OrderMemberInfo> GetOrderMemberInfos()
        {
            if (UpstreamOrder != null)
            {
                foreach (var member in UpstreamOrder.GetOrderMemberInfos())
                    yield return member;
            }

            if (WithStruct)
            {
                yield return new OrderMemberInfo(this,
                    WithSelector ? TypeName("Selector") : null, TypeName("Comparer"), KeyType);
            }
            else
            {
                yield return new OrderMemberInfo(this,
                    WithSelector ? SelectorInterfaceType : null, ComparerInterfaceType, KeyType);
            }
        }

        public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
        {
            var currentName = VarName("current");
            var currentRewriter = new PlaceholderRewriter(currentName);

            // replace current variables of downstream
            statements = currentRewriter.VisitStatementSyntaxList(statements);

            statements = statements.Insert(0, LocalDeclarationStatement(
                currentName.Identifier, ElementAccessExpression(VarName("elements"),
                    ElementAccessExpression(VarName("indices"), VarName("index")))));

            var result = WhileStatement(LessThanExpression(
                    CastExpression(UIntType, PreIncrementExpression(VarName("index"))),
                    CastExpression(UIntType, MemberAccessExpression(VarName("elements"), CountProperty))),
                Block(statements));

            return Block(result);
        }
    }
}