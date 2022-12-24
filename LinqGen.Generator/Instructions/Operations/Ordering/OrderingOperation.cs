// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private FunctionKind SelectorKind { get; }
        private ComparerKind ComparerKind { get; }
        public bool Descending { get; }

        public OrderingOperation(in LinqGenExpression expression, int id,
            FunctionKind selectorKind, ComparerKind comparerKind, bool descending) : base(expression, id)
        {
            SelectorKind = selectorKind;
            ComparerKind = comparerKind;
            Descending = descending;

            if (selectorKind != FunctionKind.Default)
            {
                expression.TryGetNamedParameterType(0, out var selectorType);

                SelectorInterfaceType = ParseTypeName(selectorType);
                SelectorKeySymbol = selectorType.TypeArguments[1];
                SelectorKeyType = ParseTypeName(SelectorKeySymbol);
            }
            else
            {
                SelectorInterfaceType = null;
                SelectorKeySymbol = null;
                SelectorKeyType = null;
            }
        }

        /// <summary>
        /// Upstream ordering chained with ThenBy.
        /// </summary>
        protected virtual OrderingOperation? UpstreamOrder => null;

        protected OrderingOperation RootOrder => UpstreamOrder?.RootOrder ?? this;

        private TypeSyntax? SelectorInterfaceType { get; }
        private ITypeSymbol? SelectorKeySymbol { get; }
        private TypeSyntax? SelectorKeyType { get; }

        private TypeSyntax KeyType =>
            SelectorKind == FunctionKind.Default ? OutputElementType : SelectorKeyType!;

        private ITypeSymbol KeySymbol =>
            SelectorKind == FunctionKind.Default ? OutputElementSymbol : SelectorKeySymbol!;

        private TypeSyntax ComparerInterfaceType =>
            GenericName(Identifier("IComparer"), TypeArgumentList(KeyType));

        public override TypeSyntax EnumerableInterfaceType =>
            GenericName(Identifier("IInternalOrderedStub"), TypeArgumentList(OutputElementType));

        public TypeSyntax ElementListType => PooledListType(OutputElementType, OutputElementSymbol.IsUnmanagedType);

        public TypeSyntax IndexListType => PooledListType(IntType, true);

        public override TypeSyntax? DummyParameterType
        {
            get
            {
                // parameter can collide in this case
                if (SelectorKind == FunctionKind.Struct)
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
            yield return new MemberInfo(MemberKind.Enumerator, ElementListType, VarName("elements"),
                ObjectCreationExpression(ElementListType, ArgumentList(LiteralExpression(0)), null));

            yield return new MemberInfo(MemberKind.Enumerator, IndexListType, VarName("indices"),
                ObjectCreationExpression(IndexListType, ArgumentList(LiteralExpression(0)), null));

            yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"));

            switch (SelectorKind)
            {
                case FunctionKind.Delegate:
                    yield return new MemberInfo(MemberKind.Enumerable, SelectorInterfaceType!, VarName("selector"));
                    break;

                case FunctionKind.Struct:
                    yield return new MemberInfo(MemberKind.Enumerable, TypeName("Selector"), VarName("selector"));
                    break;
            }

            switch (ComparerKind)
            {
                case ComparerKind.Interface:
                    yield return new MemberInfo(MemberKind.Enumerable, ComparerInterfaceType, VarName("comparer"));
                    break;

                case ComparerKind.Struct:
                    yield return new MemberInfo(MemberKind.Enumerable, TypeName("Comparer"), VarName("comparer"));
                    break;
            }
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (SelectorKind == FunctionKind.Struct)
            {
                yield return new TypeParameterInfo(
                    TypeName("Selector"), SelectorInterfaceType!);
            }

            if (ComparerKind == ComparerKind.Struct)
            {
                yield return new TypeParameterInfo(
                    TypeName("Comparer"), StructConstraint, TypeConstraint(ComparerInterfaceType));
            }
        }

        public IEnumerable<ParameterSyntax> GetOrderParameters()
        {
            foreach (var member in GetOrderMemberInfos())
            {
                if (member.SelectorType != null)
                    yield return Parameter(member.SelectorType, member.SelectorName.Identifier);
                if (member.ComparerType != null)
                    yield return Parameter(member.ComparerType, member.ComparerName.Identifier);
            }
        }

        public IEnumerable<ArgumentSyntax> GetOrderArguments(ExpressionSyntax sourceName)
        {
            foreach (var member in GetOrderMemberInfos())
            {
                if (member.SelectorType != null)
                    yield return Argument(MemberAccessExpression(sourceName, member.SelectorName));
                if (member.ComparerType != null)
                    yield return Argument(MemberAccessExpression(sourceName, member.ComparerName));
            }
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderEnumerableMembers()
        {
            foreach (var member in base.RenderEnumerableMembers())
                yield return member;

            yield return SorterTemplate.Render(this);
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal, ExpressionSyntax source,
            ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            var rootUpstream = RootOrder.Upstream;
            ExpressionSyntax sourceName = isLocal ? ThisExpression() : IdentifierName("source");

            foreach (var statement in rootUpstream.GetLocalDeclarations(MemberKind.Enumerator))
                yield return statement;

            foreach (var statement in rootUpstream.GetLocalAssignments(MemberKind.Both, sourceName))
                yield return statement;

            foreach (var statement in rootUpstream.RenderInitialization(true, source, null, null))
                yield return statement;

            ExpressionSyntax countExpression = rootUpstream.RenderCount() ?? LiteralExpression(0);

            var elementsName = VarName("elements");
            var elementsCount = MemberAccessExpression(elementsName, CountProperty);

            yield return ExpressionStatement(SimpleAssignmentExpression(elementsName,
                ObjectCreationExpression(ElementListType, ArgumentList(countExpression), null)));

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
                : SubtractExpression(MathMin(elementsCount, AddExpression(minName, takeVar)), LiteralExpression(1)));

            yield return ExpressionStatement(SimpleAssignmentExpression(
                VarName("index"), SubtractExpression(minName, LiteralExpression(1))));

            var sortBody = new List<StatementSyntax>();

            var sorterName = VarName("sorter");

            sortBody.Add(ExpressionStatement(SimpleAssignmentExpression(indicesName,
                ObjectCreationExpression(IndexListType, ArgumentList(elementsCount), null))));

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

            TypeSyntax? selectorType;
            TypeSyntax? comparerType;

            switch (SelectorKind)
            {
                case FunctionKind.Default:
                    selectorType = null;
                    break;

                case FunctionKind.Delegate:
                    selectorType = SelectorInterfaceType;
                    break;

                case FunctionKind.Struct:
                    selectorType = TypeName("Selector");
                    break;

                default:
                    throw new InvalidOperationException();
            }

            switch (ComparerKind)
            {
                case ComparerKind.Default:
                    comparerType = null;
                    break;

                case ComparerKind.Interface:
                    comparerType = ComparerInterfaceType;
                    break;

                case ComparerKind.Struct:
                    comparerType = TypeName("Comparer");
                    break;

                default:
                    throw new InvalidOperationException();
            }

            yield return new OrderMemberInfo(this, selectorType, comparerType, KeyType, KeySymbol);
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