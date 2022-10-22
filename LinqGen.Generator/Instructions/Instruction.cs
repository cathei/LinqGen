// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    // When generic expression is
    // StubEnumerable<T, ResultOp> Extension(this StubEnumerable<T, SourceOp> x)
    // ResultOp is a child of SourceOp
    public abstract class Instruction
    {
        // known predefined type names
        public static readonly PredefinedTypeSyntax IntType = PredefinedType(Token(SyntaxKind.IntKeyword));

        // known generic interface names
        public static readonly GenericNameSyntax EnumerableInterfaceName = GenericName("IEnumerable");
        public static readonly GenericNameSyntax EnumeratorInterfaceName = GenericName("IEnumerator");
        public static readonly GenericNameSyntax ListInterfaceName = GenericName("IList");

        // known method names
        public static readonly IdentifierNameSyntax InvokeName = IdentifierName("Invoke");
        public static readonly IdentifierNameSyntax MoveNextName = IdentifierName("MoveNext");
        public static readonly IdentifierNameSyntax DisposeName = IdentifierName("Dispose");
        public static readonly IdentifierNameSyntax GetEnumeratorName = IdentifierName("GetEnumerator");

        // known property names
        public static readonly IdentifierNameSyntax CurrentName = IdentifierName("Current");
        public static readonly IdentifierNameSyntax CountName = IdentifierName("Count");

        // custom variable names
        public static readonly IdentifierNameSyntax ParentName = IdentifierName("parent");
        public static readonly IdentifierNameSyntax SourceName = IdentifierName("source");
        public static readonly IdentifierNameSyntax IndexName = IdentifierName("index");
        public static readonly IdentifierNameSyntax SelectorName = IdentifierName("select");
        public static readonly IdentifierNameSyntax PredicateName = IdentifierName("predicate");

        protected Instruction(INamedTypeSymbol elementSymbol, INamedTypeSymbol? parentSymbol)
        {
            ElementName = ParseName(elementSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
            ParentSymbol = parentSymbol;
        }

        public INamedTypeSymbol? ParentSymbol { get; }

        public Instruction? Upstream { get; private set; }

        public List<Instruction>? Downstream { get; private set; }

        public void SetParent(Instruction parent)
        {
            Upstream = parent;

            Upstream.Downstream ??= new List<Instruction>();
            Upstream.Downstream.Add(this);
        }

        /// <summary>
        /// The class name cached for child class rendering
        /// </summary>
        public IdentifierNameSyntax? ClassName { get; private set; }

        public NameSyntax ElementName { get; }

        public abstract IdentifierNameSyntax MethodName { get; }

        public abstract IEnumerable<MemberInfo> GetMemberInfos();

        public SourceText Render(int id)
        {
            ClassName = IdentifierName($"{MethodName}_{id}");
            return Template.Render(this);
        }

        public virtual BlockSyntax RenderConstructorBody() => SyntaxFactory.Block();

        public abstract BlockSyntax RenderMoveNextBody();

        public abstract BlockSyntax RenderCurrentGetBody();

        public virtual BlockSyntax RenderDisposeBody() => SyntaxFactory.Block();
    }
}