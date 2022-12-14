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

    public sealed class EnumerableGeneration : Generation
    {
        public EnumerableGeneration(in LinqGenExpression expression, int id,
            ITypeSymbol sourceSymbol) : base(expression, id)
        {
            // TODO ICollection, ICollection<T>, IReadOnlyCollection<T> ...
            IsCollection = TryGetGenericCollectionInterface(sourceSymbol, out _);

            var enumeratorSymbol = GetEnumeratorSymbol(sourceSymbol)!.ReturnType;
            var elementSymbol = GetCurrentSymbol(enumeratorSymbol);

            SourceEnumerableType = ParseTypeName(sourceSymbol);
            SourceEnumeratorType = ParseTypeName(enumeratorSymbol);

            OutputElementSymbol = elementSymbol;

            if (elementSymbol is ITypeParameterSymbol typeParameterSymbol)
            {
                var outputElementName = TypeName("Element");

                OutputElementType = outputElementName;
                GenericElement = true;

                var rewriter = new GenericRewriter(IdentifierName(typeParameterSymbol.Name), outputElementName);
                SourceEnumerableType = (TypeSyntax)rewriter.Visit(SourceEnumerableType);
                SourceEnumeratorType = (TypeSyntax)rewriter.Visit(SourceEnumeratorType);
            }
            else
            {
                OutputElementType = ParseTypeName(elementSymbol);
                GenericElement = false;
            }
        }

        public override ITypeSymbol OutputElementSymbol { get; }
        public override TypeSyntax OutputElementType { get; }

        private TypeSyntax SourceEnumerableType { get; }
        private TypeSyntax SourceEnumeratorType { get; }

        private bool GenericElement { get; }
        private bool IsCollection { get; }

        public override bool SupportPartition => false;

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Enumerable, SourceEnumerableType, VarName("source"));

            var defaultValue = isLocal
                ? InvocationExpression(VarName("source"), GetEnumeratorMethod)
                : InvocationExpression(IdentifierName("source"), VarName("source"), GetEnumeratorMethod);

            yield return new MemberInfo(MemberKind.Enumerator, SourceEnumeratorType, VarName("iter"), defaultValue);
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (GenericElement)
                yield return new TypeParameterInfo(TypeName("Element"));
        }

        public override ExpressionSyntax? RenderCount()
        {
            return IsCollection ? MemberAccessExpression(VarName("source"), CountProperty) : null;
        }

        public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
        {
            var currentName = VarName("current");
            var currentRewriter = new PlaceholderRewriter(currentName);

            // replace current variables of downstream
            statements = currentRewriter.VisitStatementSyntaxList(statements);

            statements = statements.Insert(0, LocalDeclarationStatement(
                currentName.Identifier, MemberAccessExpression(VarName("iter"), CurrentProperty)));

            var result = WhileStatement(
                InvocationExpression(VarName("iter"), MoveNextMethod),
                Block(statements));

            return Block(result);
        }
    }
}