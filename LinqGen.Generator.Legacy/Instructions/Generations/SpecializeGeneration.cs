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
                elementSymbol = genericInterfaceSymbol.TypeArguments[0];
            }

            // TODO ICollection, ICollection<T>, IReadOnlyCollection<T> ...
            IsCountable = TryGetGenericCollectionInterface(enumerableSymbol, out _);

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
        public override bool IsCountable { get; }
        public override bool IsPartition => false;

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            // TODO apply generic constraints from original type...
            if (GenericElement)
                yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}1"));
        }

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            yield return new MemberInfo(
                MemberKind.Enumerable, CallerEnumerableType, SourceVar);

            yield return new MemberInfo(
                MemberKind.Enumerator, CallerEnumeratorType, SourceVar);
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(
                EnumeratorType, ArgumentList(InvocationExpression(SourceVar, GetEnumeratorMethod)), null)));
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            // assignment will be automatic if parameter kind is Both
            return ConstructorDeclaration(new(AggressiveInliningAttributeList),
                InternalTokenList, Identifier("Enumerator"),
                ParameterList(Parameter(CallerEnumeratorType, SourceVar.Identifier)), ThisInitializer,
                Block(ExpressionStatement(SimpleAssignmentExpression(
                    MemberAccessExpression(ThisExpression(), SourceVar), SourceVar))));
        }

        public override BlockSyntax RenderCountGetBody()
        {
            return Block(ReturnStatement(MemberAccessExpression(SourceVar, CountProperty)));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(ReturnStatement(InvocationExpression(SourceVar, MoveNextMethod)));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(MemberAccessExpression(SourceVar, CurrentProperty)));
        }

        public override BlockSyntax RenderDisposeBody()
        {
            return Block(ExpressionStatement(InvocationExpression(SourceVar, DisposeMethod)));
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