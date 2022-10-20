// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    public static class CodeGenUtils
    {
        public static bool MethodDefinedIn(IMethodSymbol symbol,
            string assemblyName, string containingTypeName)
        {
            return symbol.ContainingAssembly.Name == assemblyName &&
                   symbol.ContainingSymbol.MetadataName == containingTypeName;
        }

        public static bool MethodDefinedAs(IMethodSymbol symbol,
            string assemblyName, string containingTypeName, string methodName)
        {
            return MethodDefinedIn(symbol, assemblyName, containingTypeName) &&
                   symbol.MetadataName == methodName;
        }

        public static bool IsGenerationMethod(IMethodSymbol symbol, )
        {

        }
    }
}