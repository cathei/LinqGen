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
        public readonly struct GenerationItem : IEquatable<GenerationItem>
        {
            public readonly INamedTypeSymbol ElementTypeSymbol;
            public readonly bool IsList;

            public GenerationItem(INamedTypeSymbol elementTypeSymbol, bool isList)
            {
                ElementTypeSymbol = elementTypeSymbol;
                IsList = isList;
            }

            public bool Equals(GenerationItem other)
            {
                return SymbolEqualityComparer.Default.Equals(ElementTypeSymbol, other.ElementTypeSymbol);
            }

            public override bool Equals(object obj)
            {
                return obj is GenerationItem other && Equals(other);
            }

            public override int GetHashCode()
            {
                return SymbolEqualityComparer.Default.GetHashCode(ElementTypeSymbol);
            }
        }

        public readonly struct OperationItem
        {
            public readonly INamespaceSymbol OpTypeSymbol;

            public OperationItem(INamespaceSymbol opTypeSymbol)
            {
                OpTypeSymbol = opTypeSymbol;
            }
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
            }
            else if (CodeGenUtils.IsOperationMethod(methodSymbol))
            {
                AddOperationItem(context, node, methodSymbol);
            }
        }

        private void AddGenerationItem(
            GeneratorSyntaxContext context, MemberAccessExpressionSyntax node, IMethodSymbol methodSymbol)
        {
            if (methodSymbol.TypeArguments[0] is not INamedTypeSymbol elementTypeSymbol)
            {
                // TODO: sorry, LinqGen does not support generic argument (yet)
                return;
            }

            if (methodSymbol.Parameters[0].Type.MetadataName == "IList`1")
            {
                // IList

            }
            else
            {
                // IEnumerable

            }




            // var methodTypeInfo = context.SemanticModel.GetTypeInfo(node.Name);
            //
            // if (methodTypeInfo.Type is not INamedTypeSymbol callerTypeSymbol)
            //     return;

            // Generations.Add(new GenerationItem(callerTypeSymbol));
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