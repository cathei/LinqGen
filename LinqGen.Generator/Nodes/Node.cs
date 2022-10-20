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
        public readonly Dictionary<INamedTypeSymbol, Node> Children = new(SymbolEqualityComparer.Default);

        public abstract void Render(StringBuilder builder, int id);
    }
}