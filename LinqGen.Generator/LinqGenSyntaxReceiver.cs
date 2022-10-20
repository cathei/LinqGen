// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    public class LinqGenSyntaxReceiver : ISyntaxContextReceiver
    {
        // private const string LinqGenAssemblyName = "LinqGen";
        // private const string LinqGenExtensionsTypeName = "LinqGenExtensions";
        // private const string LinqGenStubTypeName = "StubEnumerable`1";
        // private const string LinqGenGenerateMethodName = "Generate`1";

        public struct GenerationItem
        {


        }

        // public List<>

        public struct InvocationItem
        {

        }

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (!(context.Node is MemberAccessExpressionSyntax node))
                return;

            node.





            // if (!(context.Node is InvocationExpressionSyntax node))
            //     return;
            //
            // // var arguments = node.ArgumentList.Arguments;
            //
            // var methodSymbol = context.SemanticModel.GetSymbolInfo(node.Expression).Symbol as IMethodSymbol;
            //
            // if (CodeGenUtils.IsGenerationMethod())
            // {
            //     // it's a call to generate method
            //     methodSymbol
            //
            //
            //
            //     return;
            // }
            //
            // if (CodeGenUtils.MethodDefinedIn(methodSymbol, LinqGenAssemblyName, LinqGenStubTypeName))
            // {
            //     return;
            // }


            // design note 1.
            // all the stub enumerable will be replaced

            // design note 2.
            // stub enumerable itself will be recursively solved by another



            // context.SemanticModel.
            // throw new NotImplementedException();
        }
    }
}