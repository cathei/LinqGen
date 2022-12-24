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

    public static class GenerationTemplate
    {
        private static readonly SyntaxTree TemplateSyntaxTree = CSharpSyntaxTree.ParseText(@"
namespace Cathei.LinqGen.Hidden
{
    // Non-exported Enumerable should consider anonymous type, thus it will be internal
    internal struct _Enumerable_ : _Interface_
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal _Enumerable_() : this()
        {

        }
    }
}

namespace Cathei.LinqGen
{
    // Extension class needs to be internal to prevent ambiguous resolution
    internal static partial class _Extensions_
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

            public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax? node)
            {
                switch (node!.Identifier.ValueText)
                {
                    case "_Extensions_":
                        node = RewriteExtensionClass(node);
                        break;
                }

                return node == null ? null : base.VisitClassDeclaration(node);
            }

            public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "_Enumerable_":
                        node = RewriteEnumerableStruct(node);
                        break;
                }

                return base.VisitStructDeclaration(node);
            }

            public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax? node)
            {
                switch (node!.Identifier.ValueText)
                {
                    case "_Enumerable_":
                        node = RewriteEnumerableConstructor(node);
                        break;
                }

                return node == null ? null : base.VisitConstructorDeclaration(node);
            }

            public override SyntaxNode? VisitBaseList(BaseListSyntax node)
            {
                if (node.Types[0].Type is IdentifierNameSyntax { Identifier: { ValueText: "_Interface_" } })
                    node = BaseList(SeparatedList(_instruction.InterfaceTypes));

                return base.VisitBaseList(node);
            }

            public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "_Element_":
                        return _instruction.OutputElementType;

                    case "_Enumerable_":
                        return _instruction.ResolvedClassName;
                }

                return base.VisitIdentifierName(node);
            }

            private ClassDeclarationSyntax? RewriteExtensionClass(ClassDeclarationSyntax node)
            {
                var extensionMethods = _instruction.RenderExtensionClassMembers().ToArray();

                if (extensionMethods.Length == 0)
                    return null;

                return node
                    .WithIdentifier(Identifier($"LinqGenExtensions_{_instruction.ClassName.Identifier.ValueText}"))
                    .AddMembers(extensionMethods);
            }

            private StructDeclarationSyntax RewriteEnumerableStruct(StructDeclarationSyntax node)
            {
                node = node.WithIdentifier(_instruction.ClassName.Identifier)
                    .WithTypeParameterList(_instruction.GetTypeParameters())
                    .WithConstraintClauses(_instruction.GetGenericConstraints())
                    .AddMembers(_instruction.RenderEnumerableMembers().ToArray())
                    .AddMembers(_instruction.GetFieldDeclarations(_instruction.IsEnumerator).ToArray());

                return node;
            }

            private ConstructorDeclarationSyntax? RewriteEnumerableConstructor(ConstructorDeclarationSyntax node)
            {
                var parameters = _instruction.GetParameters(MemberKind.Enumerable, false);
                var assignments = _instruction.GetFieldAssignments(MemberKind.Enumerable, false);

                if (_instruction.Upstream != null)
                {
                    var sourceName = IdentifierName("source");

                    parameters = parameters.Prepend(Parameter(default, InTokenList,
                        _instruction.UpstreamResolvedClassName, sourceName.Identifier, null));

                    assignments = _instruction.Upstream.GetFieldAssignments(MemberKind.Enumerable, true, sourceName)
                        .Concat(assignments);
                }

                var parameterList = ParameterList(parameters);

                if (parameterList.Parameters.Count == 0)
                    return null;

                if (_instruction.IsEnumerator)
                    assignments = assignments.Concat(_instruction.GetFieldDefaultAssignments(MemberKind.Enumerator));

                return node.WithIdentifier(_instruction.ClassName.Identifier)
                    .WithParameterList(parameterList)
                    .WithBody(Block(assignments));
            }
        }

        public static IEnumerable<MemberDeclarationSyntax> Render(Generation instruction)
        {
            var root = TemplateSyntaxTree.GetCompilationUnitRoot();

            var rewriter = new Rewriter(instruction);
            root = (CompilationUnitSyntax)rewriter.Visit(root);

            return root.Members;
        }
    }
}