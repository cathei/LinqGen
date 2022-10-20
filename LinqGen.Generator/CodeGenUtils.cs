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
        private const string LinqGenExtensionsTypeName = "LinqGenExtensions";
        private const string LinqGenStubTypeName = "StubEnumerable`1";
        private const string LinqGenGenerateMethodName = "Generate`1";

        private static bool IsMethodDefinedIn(IMethodSymbol symbol,
            string assemblyName, string containingTypeName)
        {
            return symbol.ContainingAssembly.Name == assemblyName &&
                   symbol.ContainingSymbol.MetadataName == containingTypeName;
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
                LinqGenAssemblyName, LinqGenExtensionsTypeName, LinqGenGenerateMethodName);
        }

        public static bool IsOperationMethod(IMethodSymbol symbol)
        {
            return IsMethodDefinedIn(symbol, LinqGenAssemblyName, LinqGenStubTypeName);
        }
    }
}