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
        public struct GenerationItem
        {

        }

        public struct OperationItem
        {

        }

        public List<GenerationItem> Generations;
        public List<OperationItem> Operations;

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is not MemberAccessExpressionSyntax node)
                return;

            var memberSymbolInfo = context.SemanticModel.GetSymbolInfo(node.Name);

            if (memberSymbolInfo.Symbol is not IMethodSymbol methodSymbol)
                return;

            if (CodeGenUtils.IsGenerationMethod(methodSymbol))
            {
                AddGenerationItem(context, node, methodSymbol);
                return;
            }

            if (CodeGenUtils.IsOperationMethod(methodSymbol))
            {
                AddOperationItem(context, node, methodSymbol);
                return;
            }



            // context.SemanticModel.GetSymbolInfo(node.Expression).Symbol as I
            // node.Name


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

        private void AddGenerationItem(
            GeneratorSyntaxContext context, MemberAccessExpressionSyntax node, IMethodSymbol methodSymbol)
        {
            var callerTypeInfo = context.SemanticModel.GetTypeInfo(node.Expression);

            if (callerTypeInfo.Type is null)
                return;
        }

        private void AddOperationItem(
            GeneratorSyntaxContext context, MemberAccessExpressionSyntax node, IMethodSymbol methodSymbol)
        {
            var callerTypeInfo = context.SemanticModel.GetTypeInfo(node.Expression);

            if (callerTypeInfo.Type is null)
                return;

        }

    }
}