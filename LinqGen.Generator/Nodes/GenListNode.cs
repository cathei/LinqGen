// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
    public class GenListNode : Node
    {
        public readonly INamedTypeSymbol ElementTypeSymbol;
        public readonly bool IsList;

        public GenListNode(INamedTypeSymbol elementTypeSymbol, bool isList)
        {
            ElementTypeSymbol = elementTypeSymbol;
            IsList = isList;
        }

        public bool Equals(GenerationItem other)
        {
            return IsList == other.IsList &&
                   SymbolEqualityComparer.Default.Equals(ElementTypeSymbol, other.ElementTypeSymbol);
        }

        public override bool Equals(object obj)
        {
            return obj is GenerationItem other && Equals(other);
        }

        public override int GetHashCode()
        {
            return IsList.GetHashCode() ^
                   SymbolEqualityComparer.Default.GetHashCode(ElementTypeSymbol);
        }
    }
}