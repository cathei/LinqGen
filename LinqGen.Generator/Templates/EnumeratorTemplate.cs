// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public static class EnumeratorTemplate
    {
        private static readonly SyntaxTree TemplateSyntaxTree = CSharpSyntaxTree.ParseText(@"
        internal struct Enumerator : IEnumerator<_Element_>
        {
            private _Enumerable_ parent;
            private int state;
            private _Element_ current;

            internal Enumerator(in _Enumerable_ parent) : this()
            {
                this.parent = parent;
            }

            private void InitState()
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

        private class Rewriter : ThisPlaceholderRewriter
        {
            private readonly Generation _instruction;

            public Rewriter(Generation instruction) : base(IdentifierName("parent"))
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

            private StructDeclarationSyntax RewriteEnumeratorStruct(StructDeclarationSyntax node)
            {
                var fields = _instruction.GetFieldDeclarations(MemberKind.Enumerator);
                return node.AddMembers(fields.ToArray());
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

            private ConstructorDeclarationSyntax RewriteEnumeratorConstructor(ConstructorDeclarationSyntax node)
            {
                return node.AddBodyStatements(
                    _instruction.GetFieldDefaultAssignments(MemberKind.Enumerator).ToArray());
            }

            public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "InitState":
                        node = RewriteEnumeratorInitState(node);
                        break;

                    case "MoveNext":
                        node = RewriteEnumeratorMoveNext(node);
                        break;

                    case "Dispose":
                        node = RewriteEnumeratorDispose(node);
                        break;
                }

                return base.VisitMethodDeclaration(node);
            }

            public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "_Enumerable_":
                        return _instruction.ResolvedClassName;
                }

                return base.VisitIdentifierName(node);
            }

            private MethodDeclarationSyntax RewriteEnumeratorInitState(MethodDeclarationSyntax node)
            {
                return node.WithBody(Block(
                    _instruction.RenderInitialization(false, null, null)));
            }

            private MethodDeclarationSyntax RewriteEnumeratorMoveNext(MethodDeclarationSyntax node)
            {
                var initStatement = IfStatement(LessThanExpression(IdentifierName("state"), LiteralExpression(1)),
                    Block(ExpressionStatement(InvocationExpression(IdentifierName("InitState"))),
                        ExpressionStatement(PreIncrementExpression(IdentifierName("state")))));

                var successStatements = new StatementSyntax[]
                {
                    ExpressionStatement(SimpleAssignmentExpression(
                        IdentifierName("current"), Instruction.CurrentPlaceholder)),
                    ReturnStatement(TrueExpression())
                };

                var failStatement = ReturnStatement(FalseExpression());

                var body = Block(initStatement)
                    .AddStatements(_instruction.RenderIteration(false, new(successStatements)).Statements.ToArray())
                    .AddStatements(failStatement);

                return node.WithBody(body);
            }

            private MethodDeclarationSyntax RewriteEnumeratorDispose(MethodDeclarationSyntax node)
            {
                var body = Block(_instruction.RenderDispose(false));
                return node.WithBody(body);
            }
        }

        public static MemberDeclarationSyntax Render(Generation instruction)
        {
            var structSyntax = TemplateSyntaxTree.GetRoot()
                .DescendantNodesAndSelf()
                .OfType<StructDeclarationSyntax>()
                .First();

            var rewriter = new Rewriter(instruction);
            return (StructDeclarationSyntax)rewriter.Visit(structSyntax);
        }
    }
}