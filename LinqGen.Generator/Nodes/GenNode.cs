// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
    public class GenNode : Node
    {
        public readonly INamedTypeSymbol ElementTypeSymbol;

        public GenNode(INamedTypeSymbol elementTypeSymbol)
        {
            ElementTypeSymbol = elementTypeSymbol;
        }

        public override Template GetTemplate()
        {
            return new GenTemplate();
        }
    }
}