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

    public sealed class GetEnumeratorEvaluation : Evaluation
    {
        private static readonly SyntaxTree EnumeratorTemplate = CSharpSyntaxTree.ParseText(@"// DO NOT EDIT
        public struct Enumerator : IEnumerator<_Element_>
        {
            private _Element_ current;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal Enumerator(in _Enumerable_ source) : this()
            {

            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
            }

            public _Element_ Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => current;
            }

            object IEnumerator.Current => Current;

            void IEnumerator.Reset() => throw new NotSupportedException();

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Dispose()
            {

            }
        }
");

        private class EnumeratorRewriter : CSharpSyntaxRewriter
        {
            private readonly GetEnumeratorEvaluation _instruction;

            public EnumeratorRewriter(GetEnumeratorEvaluation instruction)
            {
                _instruction = instruction;
            }

            public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "Enumerator":
                        node = RewriteEnumeratorStruct(node);
                        break;
                }

                return base.VisitStructDeclaration(node);
            }

            public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "Enumerator":
                        node = RewriteEnumeratorConstructor(node);
                        break;
                }

                return base.VisitConstructorDeclaration(node);
            }

            public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "MoveNext":
                        node = RewriteEnumeratorMoveNext(node);
                        break;

                    case "Dispose":
                        node = RewriteEnumeratorDispose(node);
                        break;
                }

                return base.VisitMethodDeclaration(node);
            }

            private StructDeclarationSyntax RewriteEnumeratorStruct(StructDeclarationSyntax node)
            {
                return node.AddMembers(_instruction.GetFieldDeclarations().ToArray());
            }

            private ConstructorDeclarationSyntax RewriteEnumeratorConstructor(ConstructorDeclarationSyntax node)
            {
                return node.WithBody(_instruction.RenderConstructorBody());
            }

            private MethodDeclarationSyntax RewriteEnumeratorMoveNext(MethodDeclarationSyntax node)
            {
                return node.WithBody(_instruction.RenderMoveNextBody());
            }

            private MethodDeclarationSyntax RewriteEnumeratorDispose(MethodDeclarationSyntax node)
            {
                return node.WithBody(_instruction.RenderDisposeBody());
            }

            private PropertyDeclarationSyntax RewriteEnumeratorCurrent(PropertyDeclarationSyntax node)
            {
                var getAccessor = node.AccessorList!.Accessors[0].WithBody(_instruction.RenderCurrentGetBody());
                return node.WithAccessorList(AccessorList(SingletonList(getAccessor)));
            }
        }

        private readonly EnumeratorRewriter _rewriter;
        private readonly RenderOption _renderOption;

        public GetEnumeratorEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            _rewriter = new(this);
            _renderOption = new(false);
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderMembers()
        {
            var structSyntax = EnumeratorTemplate.GetRoot()
                .DescendantNodesAndSelf()
                .OfType<StructDeclarationSyntax>()
                .First();

            yield return (MemberDeclarationSyntax)_rewriter.Visit(structSyntax);

            yield return MethodDeclaration(SingletonList(AggressiveInliningAttributeList), PublicTokenList,
                IdentifierName("Enumerator"), null, Identifier("GetEnumerator"), null, ParameterList(), default, null,
                ArrowExpressionClause(ObjectCreationExpression(
                    IdentifierName("Enumerator"), ArgumentList(ThisExpression()), null)), default);
        }

        private BlockSyntax RenderConstructorBody()
        {
            var assignments = Upstream.GetAssignments(MemberKind.Both, SourceVar);
            var initialization = Upstream.RenderInitialization(_renderOption);

            return Block(assignments.Concat(initialization));
        }

        private IEnumerable<MemberDeclarationSyntax> GetFieldDeclarations()
        {
            return Upstream.GetFieldDeclarations(MemberKind.Enumerator, false);
        }

        private BlockSyntax RenderMoveNextBody()
        {
            return Upstream.RenderIteration(_renderOption);
        }

        private BlockSyntax RenderDisposeBody()
        {
            return Block(Upstream.RenderDispose(_renderOption));
        }

    }
}
