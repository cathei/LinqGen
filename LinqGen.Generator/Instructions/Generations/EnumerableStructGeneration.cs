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

    public sealed class EnumerableStructGeneration : Generation
    {
        public EnumerableStructGeneration(in LinqGenExpression expression, int id,
            bool isCollection) : base(expression, id)
        {
            IsCollection = isCollection;

            SourceEnumerableType = TypeName("Enumerable");
            SourceEnumeratorType = TypeName("Enumerator");

            OutputElementSymbol = expression.InputElementSymbol!;

            if (OutputElementSymbol is ITypeParameterSymbol typeParameterSymbol)
            {
                var outputElementName = TypeName("Element");

                OutputElementType = outputElementName;
                GenericElement = true;

                var rewriter = new GenericRewriter(IdentifierName(typeParameterSymbol.Name), outputElementName);
                SourceEnumerableType = (IdentifierNameSyntax)rewriter.Visit(SourceEnumerableType);
                SourceEnumeratorType = (IdentifierNameSyntax)rewriter.Visit(SourceEnumeratorType);
            }
            else
            {
                OutputElementType = ParseTypeName(OutputElementSymbol);
                GenericElement = false;
            }
        }

        public override ITypeSymbol OutputElementSymbol { get; }
        public override TypeSyntax OutputElementType { get; }

        private IdentifierNameSyntax SourceEnumerableType { get; }
        private IdentifierNameSyntax SourceEnumeratorType { get; }

        private bool GenericElement { get; }
        private bool IsCollection { get; }

        public override bool SupportPartition => false;

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Enumerable, SourceEnumerableType, VarName("source"));
            yield return new MemberInfo(MemberKind.Enumerable, TypeName("Selector"), VarName("selector"));

            var selectorInvoke = isLocal
                ? MemberAccessExpression(VarName("selector"), InvokeMethod)
                : MemberAccessExpression(IdentifierName("source"), VarName("selector"), InvokeMethod);

            var defaultValue = InvocationExpression(selectorInvoke, ArgumentList(VarName("source")));

            yield return new MemberInfo(MemberKind.Enumerator, SourceEnumeratorType, VarName("iter"), defaultValue);
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (GenericElement)
                yield return new TypeParameterInfo(TypeName("Element"));

            yield return new TypeParameterInfo(SourceEnumerableType, EnumerableInterfaceType(OutputElementType));
            yield return new TypeParameterInfo(SourceEnumeratorType, EnumeratorInterfaceType(OutputElementType));

            yield return new TypeParameterInfo(
                TypeName("Selector"), StructFunctionInterfaceType(SourceEnumerableType, SourceEnumeratorType));
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