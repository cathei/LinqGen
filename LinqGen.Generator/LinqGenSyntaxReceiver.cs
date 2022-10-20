// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    /// <summary>
    /// For Unity support, 3.8 doesn't have ISyntaxContextReceiver
    /// </summary>
    public class LinqGenSyntaxReceiver // : ISyntaxContextReceiver
    {
        private readonly StringBuilder _logBuilder;

        public readonly HashSet<GenerationItem> Generations = new();
        public readonly HashSet<OperationItem> Operations = new();

        public LinqGenSyntaxReceiver(StringBuilder logBuilder)
        {
            _logBuilder = logBuilder;
        }

        public void VisitSyntaxTree(SemanticModel semanticModel, SyntaxTree syntaxTree)
        {
            foreach (var node in syntaxTree.GetRoot().DescendantNodes())
                VisitNode(semanticModel, node);
        }

        private void VisitNode(SemanticModel semanticModel, SyntaxNode syntaxNode)
        {
            if (syntaxNode is not MemberAccessExpressionSyntax node)
                return;

            var memberSymbolInfo = semanticModel.GetSymbolInfo(node.Name);

            if (memberSymbolInfo.Symbol is not IMethodSymbol methodSymbol)
                return;

            _logBuilder.AppendLine($"// symbol info {methodSymbol.ToDisplayString()}");
            _logBuilder.AppendLine($"// symbol containing type {methodSymbol.ContainingType.MetadataName}");
            _logBuilder.AppendLine($"// symbol containing asm {methodSymbol.ContainingAssembly.Name}");
            _logBuilder.AppendLine($"// symbol containing asm {methodSymbol.MetadataName}");
            _logBuilder.AppendLine($"// symbol is generic method {CodeGenUtils.IsGenerationMethod(methodSymbol)}");

            if (CodeGenUtils.IsGenerationMethod(methodSymbol))
            {
                AddGenerationItem(semanticModel, node, methodSymbol);
            }
            else if (CodeGenUtils.IsOperationMethod(methodSymbol))
            {
                AddOperationItem(semanticModel, node, methodSymbol);
            }
        }

        private void AddGenerationItem(
            SemanticModel semanticModel, MemberAccessExpressionSyntax node, IMethodSymbol methodSymbol)
        {
            // First type parameter is Operation type
            if (methodSymbol.TypeArguments[0] is not INamedTypeSymbol elementTypeSymbol)
            {
                // TODO: sorry, LinqGen does not support generic argument (yet)
                return;
            }

            bool isList = methodSymbol.Parameters[0].Type.MetadataName == "IList`1";

            Generations.Add(new(elementTypeSymbol, isList));
        }

        private void AddOperationItem(
            SemanticModel semanticModel, MemberAccessExpressionSyntax node, IMethodSymbol methodSymbol)
        {
            var callerTypeInfo = semanticModel.GetTypeInfo(node.Expression);

            _logBuilder.AppendLine($"// caller type symbol {callerTypeInfo.Type.ToDisplayString()}");

            if (callerTypeInfo.Type is not INamedTypeSymbol callerTypeSymbol)
                return;

            _logBuilder.AppendLine($"// caller type symbol args {callerTypeSymbol.TypeArguments[1].ToDisplayString()}");

            if (!CodeGenUtils.IsStubEnumerable(callerTypeSymbol))
                return;

            if (callerTypeSymbol.TypeArguments[1] is not INamedTypeSymbol opTypeSymbol)
                return;

            Operations.Add(new(methodSymbol, opTypeSymbol));
        }
    }
}