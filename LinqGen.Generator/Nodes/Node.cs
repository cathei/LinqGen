// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
    // When generic expression is
    // StubEnumerable<T, ResultOp> Extension(this StubEnumerable<T, SourceOp> x)
    // ResultOp is a child of SourceOp
    public abstract class Node
    {
        protected Node(INamedTypeSymbol elementSymbol, INamedTypeSymbol? parentSymbol)
        {
            ElementName = elementSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            ParentSymbol = parentSymbol;
        }

        public INamedTypeSymbol? ParentSymbol { get; }

        public Node? Parent { get; private set; }

        public List<Node>? Children { get; private set; }

        public void SetParent(Node parent)
        {
            Parent = parent;

            if (Parent.Children == null)
                Parent.Children = new List<Node>();

            Parent.Children.Add(this);
        }

        /// <summary>
        /// The class name cached for child class rendering
        /// </summary>
        public string? ClassName { get; private set; }

        public string ElementName { get; }

        public abstract string MethodName { get; }

        public abstract IEnumerable<Template.MemberInfo> GetMemberInfos();

        public void Render(StringBuilder builder, int id)
        {
            ClassName = $"{MethodName}_{id}";
            Template.Render(builder, this);
        }

        public virtual void RenderConstructorBody(StringBuilder builder) { }

        public abstract void RenderMoveNextBody(StringBuilder builder);

        public abstract void RenderGetCurrentBody(StringBuilder builder);

        public virtual void RenderDisposeBody(StringBuilder builder) { }
    }
}