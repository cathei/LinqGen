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
        public struct Enumerator : IEnumerator<_Element_>
        {
            [EditorBrowsable(EditorBrowsableState.Never)]
            private _Element_ current;

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

        private class Rewriter : CSharpSyntaxRewriter
        {
            private readonly Generation _instruction;

            public Rewriter(Generation instruction)
            {
                _instruction = instruction;
            }

            // public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
            // {
            //     switch (node.Identifier.ValueText)
            //     {
            //         case "Enumerator":
            //             node = RewriteEnumeratorStruct(node);
            //             break;
            //     }
            //
            //     return base.VisitStructDeclaration(node);
            // }

            // public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
            // {
            //     switch (node.Identifier.ValueText)
            //     {
            //         case "Enumerator":
            //             node = RewriteEnumeratorConstructor(node);
            //             break;
            //     }
            //
            //     return base.VisitConstructorDeclaration(node);
            // }

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

            // private StructDeclarationSyntax RewriteEnumeratorStruct(StructDeclarationSyntax node)
            // {
            //     var fields = _instruction.GetFieldDeclarations(MemberKind.Enumerator);
            //     return node.AddMembers(fields.ToArray());
            // }

            // private ConstructorDeclarationSyntax RewriteEnumeratorConstructor(ConstructorDeclarationSyntax node)
            // {
            //     IEnumerable<StatementSyntax> statements;
            //
            //     statements = _instruction.GetFieldAssignments(MemberKind.Any, IdentifierName("source"))
            //         .Concat(_instruction.GetFieldDefaultAssignments(MemberKind.Enumerator))
            //         .Concat(_instruction.RenderInitialization(false, IdentifierName("source"), null, null));
            //
            //     return node.WithBody(Block(statements));
            // }

            private MethodDeclarationSyntax RewriteEnumeratorMoveNext(MethodDeclarationSyntax node)
            {
                var successStatements = new StatementSyntax[]
                {
                    ExpressionStatement(SimpleAssignmentExpression(
                        IdentifierName("current"), Instruction.CurrentPlaceholder)),
                    ReturnStatement(TrueExpression())
                };

                var failStatement = ReturnStatement(FalseExpression());

                var body = _instruction.RenderIteration(false, new(successStatements))
                    .AddStatements(failStatement);

                return node.WithBody(body);
            }

            private MethodDeclarationSyntax RewriteEnumeratorDispose(MethodDeclarationSyntax node)
            {
                var body = Block(_instruction.RenderDispose(false));
                return node.WithBody(body);
            }
        }

        public static IEnumerable<MemberDeclarationSyntax> Render(Generation instruction)
        {
            var structSyntax = TemplateSyntaxTree.GetRoot()
                .DescendantNodesAndSelf()
                .OfType<StructDeclarationSyntax>()
                .First();

            var rewriter = new Rewriter(instruction);
            structSyntax = (StructDeclarationSyntax)rewriter.Visit(structSyntax);

            return structSyntax.DescendantNodes().OfType<MemberDeclarationSyntax>();
        }
    }
}