// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Cathei.LinqGen.Hidden;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    public static class NodeFactory
    {
        public static Node? CreateNode(StringBuilder logBuilder, ref LinqGenExpression expression)
        {
            ITypeSymbol? argumentType;

            switch (expression.OpSymbol.MetadataName)
            {
                case "Gen`1":
                    return new GenNode(expression.ElementSymbol);

                case "GenList`1":
                    return new GenListNode(expression.ElementSymbol);

                case "Select`2":
                    if (!expression.TryGetArgumentType(0, out argumentType))
                        break;
                    return new SelectNode(expression.ElementSymbol, expression.ParentSymbol, argumentType);

                case "Where`1":
                    if (!expression.TryGetArgumentType(0, out argumentType))
                        break;
                    return new WhereNode(expression.ElementSymbol, expression.ParentSymbol, argumentType);
            }

            // not yet implemented
            return null;
        }
    }
}