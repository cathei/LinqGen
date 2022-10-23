﻿// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

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
        private static readonly SyntaxTree TemplateSyntaxTree = CSharpSyntaxTree.ParseText(@"// DO NOT EDIT
// Generated by LinqGen.Generator

using System;
using System.Collections;
using System.Collections.Generic;
using Cathei.LinqGen;
using Cathei.LinqGen.Hidden;
using Cathei.LinqGen.Hidden._Assembly_;

namespace Cathei.LinqGen.Hidden._Assembly_
{
    // Enumerable is always readonly
    // Can be public as LinqGen support predefined enumerable
    public readonly struct _Enumerable_ : IStub<_Element_, Compiled>
    {
        internal _Enumerable_()
        {

        }

        public Enumerator GetEnumerator() => new Enumerator(this);

        public struct Enumerator : IEnumerator<_Element_>
        {
            internal Enumerator(in _Enumerable_ parent)
            {

            }

            public bool MoveNext()
            {
            }

            public TReturn Current
            {
                get
                {

                }
            }

            object IEnumerator.Current => Current;

            void IEnumerator.Reset() => throw new NotSupportedException();

            public void Dispose()
            {

            }
        }
    }
}

namespace Cathei.LinqGen
{
    // Extensions needs to be internal to prevent ambiguous resolution
    internal static partial class _Extensions_
    {
        public static _Enumerable_ _ExtensionMethod_()
        {
            return new _Enumerable_();
        }
    }
}
");

        private class Rewriter : CSharpSyntaxRewriter
        {
            private readonly IdentifierNameSyntax _assemblyName;
            private readonly CompilingGeneration _instruction;

            public Rewriter(IdentifierNameSyntax assemblyName, CompilingGeneration instruction)
            {
                _assemblyName = assemblyName;
                _instruction = instruction;
            }

            public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "_Enumerable_":
                        node = RewriteEnumerableStruct(node);
                        break;

                    case "Enumerator":
                        node = RewriteEnumeratorStruct(node);
                        break;
                }

                return base.VisitStructDeclaration(node);
            }

            public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "_Extensions_":
                        node = RewriteExtensionClass(node);
                        break;
                }

                return base.VisitClassDeclaration(node);
            }

            public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "_Enumerable_":
                        node = RewriteEnumerableConstructor(node);
                        break;

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

                    case "_ExtensionMethod_":
                        node = RewriteExtensionMethod(node);
                        break;
                }

                return base.VisitMethodDeclaration(node);
            }

            public override SyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "Current":
                        if (node.ExplicitInterfaceSpecifier == null)
                            node = RewriteEnumeratorCurrent(node);
                        break;
                }

                return base.VisitPropertyDeclaration(node);
            }

            public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "_Enumerable_":
                        if (_instruction.Arity == 0)
                            return _instruction.IdentifierName;
                        return GenericName(_instruction.IdentifierName!.Identifier, _instruction.GetTypeArguments()!);

                    case "_Element_":
                        return _instruction.ElementName;

                    case "_Assembly_":
                        return _assemblyName;
                }

                return base.VisitIdentifierName(node);
            }

            private StructDeclarationSyntax RewriteEnumerableStruct(StructDeclarationSyntax node)
            {
                return node.WithIdentifier(_instruction.IdentifierName!.Identifier)
                    .WithTypeParameterList(_instruction.GetTypeParameters())
                    .WithConstraintClauses(_instruction.GetGenericConstraints())
                    .AddMembers(_instruction.GetFieldDeclarations(MemberKind.Enumerable, true).ToArray());
            }

            private StructDeclarationSyntax RewriteEnumeratorStruct(StructDeclarationSyntax node)
            {
                return node.AddMembers(_instruction.GetFieldDeclarations(MemberKind.Enumerator, false).ToArray());
            }

            private ClassDeclarationSyntax RewriteExtensionClass(ClassDeclarationSyntax node)
            {
                return node.WithIdentifier(Identifier($"LinqGenExtensions_{_assemblyName.Identifier.ValueText}"))
                    .AddMembers(GetExtensionMethods().ToArray());
            }

            private ConstructorDeclarationSyntax RewriteEnumerableConstructor(ConstructorDeclarationSyntax node)
            {
                return node.WithIdentifier(_instruction.IdentifierName!.Identifier)
                    .WithParameterList(ParameterList(_instruction.GetParameters(MemberKind.Enumerable, false)))
                    .WithBody(Block(_instruction.GetAssignments(MemberKind.Enumerable)));
            }

            private ConstructorDeclarationSyntax RewriteEnumeratorConstructor(ConstructorDeclarationSyntax node)
            {
                // assignment will be automatic if parameter kind is Both
                return node.WithBody(Block(_instruction.RenderConstructorBody()
                    .Statements.InsertRange(0, _instruction.GetAssignments(MemberKind.Both, ParentName))));
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
                return node.WithType(_instruction.ElementName)
                    .WithAccessorList(AccessorList(SingletonList(AccessorDeclaration(
                        SyntaxKind.GetAccessorDeclaration, _instruction.RenderCurrentGetBody()))));
            }

            private MethodDeclarationSyntax RewriteExtensionMethod(MethodDeclarationSyntax node)
            {
                // keep identifier name here so it can be visited later
                var body = Block(ReturnStatement(
                    ObjectCreationExpression(IdentifierName("_Enumerable_"),
                        ArgumentList(_instruction.GetArguments(MemberKind.Enumerable)), default)));

                return MethodDeclaration(
                    node.AttributeLists, node.Modifiers, node.ReturnType, node.ExplicitInterfaceSpecifier,
                    _instruction.MethodName.Identifier, _instruction.GetTypeParameters(),
                    ParameterList(_instruction.GetParameters(MemberKind.Enumerable, true)),
                    _instruction.GetGenericConstraints(), body, default, default);
            }

            private IEnumerable<MemberDeclarationSyntax> GetExtensionMethods()
            {
                if (_instruction.Evaluations == null)
                {
                    // nothing to evaluate
                    // downstream will have separated files
                    yield break;
                }

                foreach (var evaluation in _instruction.Evaluations.Values)
                {
                    yield return MethodDeclaration(default, PublicStaticTokenList, evaluation.ReturnType,
                        default, evaluation.MethodName.Identifier, evaluation.GetTypeParameters(),
                        ParameterList(evaluation.GetParameters()), evaluation.GetGenericConstraints(),
                        evaluation.RenderMethodBody(), default, default);
                }
            }
        }

        public static SourceText Render(IdentifierNameSyntax assemblyName, CompilingGeneration instruction)
        {
            var root = TemplateSyntaxTree.GetRoot();

            var rewriter = new Rewriter(assemblyName, instruction);
            root = rewriter.Visit(root);

            return root.NormalizeWhitespace().GetText(Encoding.UTF8);
        }
    }
}