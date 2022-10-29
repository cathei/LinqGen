// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public sealed class SpecializeGeneration : CompilingGeneration
    {
        private TypeSyntax CallerEnumerableType { get; }
        private TypeSyntax CallerEnumeratorType { get; }

        private bool GenericElement { get; }

        public SpecializeGeneration(in LinqGenExpression expression, int id, INamedTypeSymbol enumerableSymbol)
            : base(expression, id)
        {
            // TODO prevent generic type element?
            ITypeSymbol? elementSymbol = null;

            if (TryGetGenericEnumerableInterface(enumerableSymbol, out var genericInterfaceSymbol))
            {
                // IEnumerable<T>
                elementSymbol = enumerableSymbol.TypeArguments[0];
            }

            // find GetEnumerator with same rule as C# duck typing
            // TODO also find with interface implementation
            ITypeSymbol enumeratorSymbol = enumerableSymbol.GetMembers()
                .OfType<IMethodSymbol>()
                .First(x =>
                    x.DeclaredAccessibility == Accessibility.Public &&
                    x.Name == "GetEnumerator" && x.Parameters.Length == 0 && x.TypeParameters.Length == 0)
                .ReturnType;

            CallerEnumerableType = ParseTypeName(enumerableSymbol);
            CallerEnumeratorType = ParseTypeName(enumeratorSymbol);

            if (elementSymbol is ITypeParameterSymbol typeParameterSymbol)
            {
                var outputElementName = IdentifierName($"{TypeParameterPrefix}1");

                OutputElementType = outputElementName;
                GenericElement = true;

                var rewriter = new GenericRewriter(IdentifierName(typeParameterSymbol.Name), outputElementName);
                CallerEnumerableType = (TypeSyntax)rewriter.Visit(CallerEnumerableType);
                CallerEnumeratorType = (TypeSyntax)rewriter.Visit(CallerEnumeratorType);
            }
            else if (elementSymbol != null)
            {
                OutputElementType = ParseTypeName(elementSymbol);
                GenericElement = false;
            }
            else
            {
                // if element symbol is not found, use object type
                OutputElementType = ObjectType;
                GenericElement = false;
            }
        }

        public override TypeSyntax OutputElementType { get; }
        public override bool IsCollection => false;
        public override bool IsPartition => false;

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            // TODO apply generic constraints from original type...
            if (GenericElement)
                yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}1"), null);
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            yield return new MemberInfo(
                MemberKind.Enumerable, CallerEnumerableType, SourceName);

            yield return new MemberInfo(
                MemberKind.Enumerator, CallerEnumeratorType, SourceName);
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(
                EnumeratorName, ArgumentList(InvocationExpression(SourceName, GetEnumeratorName)), null)));
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            // assignment will be automatic if parameter kind is Both
            return ConstructorDeclaration(new(AggressiveInliningAttributeList),
                InternalTokenList, Identifier("Enumerator"),
                ParameterList(Parameter(CallerEnumeratorType, SourceName.Identifier)), ThisInitializer,
                Block(ExpressionStatement(SimpleAssignmentExpression(
                    MemberAccessExpression(ThisExpression(), SourceName), SourceName))));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(ReturnStatement(InvocationExpression(SourceName, MoveNextName)));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(MemberAccessExpression(SourceName, CurrentName)));
        }

        public override BlockSyntax RenderDisposeBody()
        {
            return Block(ExpressionStatement(InvocationExpression(SourceName, DisposeName)));
        }

        private class GenericRewriter : CSharpSyntaxRewriter
        {
            private readonly IdentifierNameSyntax _target;
            private readonly IdentifierNameSyntax _replace;

            public GenericRewriter(IdentifierNameSyntax target, IdentifierNameSyntax replace)
            {
                _target = target;
                _replace = replace;
            }

            public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
            {
                if (node.Identifier.ValueText == _target.Identifier.ValueText)
                    return _replace;

                return base.VisitIdentifierName(node);
            }
        }
    }
}