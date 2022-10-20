// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    public static class CodeGenUtils
    {
        private const string LinqGenAssemblyName = "LinqGen";
        private const string LinqGenStubExtensionsTypeName = "StubExtensions";
        private const string LinqGenStubEnumerableTypeName = "StubEnumerable`2";
        private const string LinqGenGenerateMethodName = "Gen";

        private static bool IsMethodDefinedIn(IMethodSymbol symbol,
            string assemblyName, string containingTypeName)
        {
            return symbol.ContainingAssembly.Name == assemblyName &&
                   symbol.ContainingType.MetadataName == containingTypeName;
        }

        private static bool IsMethodDefinedAs(IMethodSymbol symbol,
            string assemblyName, string containingTypeName, string methodName)
        {
            return IsMethodDefinedIn(symbol, assemblyName, containingTypeName) &&
                   symbol.MetadataName == methodName;
        }

        public static bool IsGenerationMethod(IMethodSymbol symbol)
        {
            return IsMethodDefinedAs(symbol,
                LinqGenAssemblyName, LinqGenStubExtensionsTypeName, LinqGenGenerateMethodName);
        }

        public static bool IsOperationMethod(IMethodSymbol symbol)
        {
            return IsMethodDefinedIn(symbol, LinqGenAssemblyName, LinqGenStubExtensionsTypeName);
        }

        public static bool IsStubEnumerable(INamedTypeSymbol symbol)
        {
            return symbol.ContainingAssembly.Name == LinqGenAssemblyName &&
                   symbol.MetadataName == LinqGenStubEnumerableTypeName;
        }
    }
}