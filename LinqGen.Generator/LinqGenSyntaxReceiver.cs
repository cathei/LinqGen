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

        private readonly Dictionary<INamedTypeSymbol, Node> _allNodes = new(SymbolEqualityComparer.Default);

        public readonly List<Node> Roots = new();

        public LinqGenSyntaxReceiver(StringBuilder logBuilder)
        {
            _logBuilder = logBuilder;
        }

        public void VisitSyntaxTree(SemanticModel semanticModel, SyntaxTree syntaxTree)
        {
            foreach (var node in syntaxTree.GetRoot().DescendantNodes())
                if (node is InvocationExpressionSyntax invocationSyntax)
                    VisitNode(semanticModel, invocationSyntax);
        }

        private void VisitNode(SemanticModel semanticModel, InvocationExpressionSyntax invocationSyntax)
        {
            if (!LinqGenExpression.TryParse(semanticModel, invocationSyntax, out var expression))
                return;

            if (_allNodes.ContainsKey(expression.OpSymbol))
            {
                // already registered
                return;
            }

            var node = NodeFactory.CreateNode(_logBuilder, ref expression);

            _logBuilder.AppendFormat("// Node : {0}\n", node?.GetType().Name);

            if (node == null)
            {
                // something is wrong
                return;
            }

            _allNodes.Add(expression.OpSymbol, node);
        }

        // private void AddGenerationNode(
        //     SemanticModel semanticModel, MemberAccessExpressionSyntax syntax, IMethodSymbol methodSymbol)
        // {
        //     // First type parameter is Operation type
        //     if (methodSymbol.TypeArguments[0] is not INamedTypeSymbol elementTypeSymbol)
        //     {
        //         // TODO: sorry, LinqGen does not support generic argument (yet)
        //         return;
        //     }
        //
        //     if (methodSymbol.ReturnType is not INamedTypeSymbol returnTypeSymbol ||
        //         returnTypeSymbol.TypeArguments[1] is not INamedTypeSymbol opSymbol)
        //     {
        //         // something is wrong
        //         return;
        //     }
        //
        //     if (_allNodes.ContainsKey(opSymbol))
        //     {
        //         // already added
        //         return;
        //     }
        //
        //     bool isList = methodSymbol.Parameters[0].Type.MetadataName == "IList`1";
        //     Node node = isList ? new GenListNode(elementTypeSymbol) : new GenNode(elementTypeSymbol);
        //
        //     _allNodes.Add(opSymbol, node);
        // }
        //
        // private void AddOperationNode(
        //     SemanticModel semanticModel, MemberAccessExpressionSyntax syntax, IMethodSymbol methodSymbol)
        // {
        //     var callerTypeInfo = semanticModel.GetTypeInfo(syntax.Expression);
        //
        //     if (methodSymbol.ReturnType is not INamedTypeSymbol returnTypeSymbol ||
        //         returnTypeSymbol.TypeArguments[1] is not INamedTypeSymbol opSymbol)
        //     {
        //         // something is wrong
        //         return;
        //     }
        //
        //     if (_allNodes.ContainsKey(opSymbol))
        //     {
        //         // already added
        //         return;
        //     }
        //
        //     if (callerTypeInfo.Type is not INamedTypeSymbol callerTypeSymbol ||
        //         !CodeGenUtils.IsStubEnumerable(callerTypeSymbol) ||
        //         callerTypeSymbol.TypeArguments[1] is not INamedTypeSymbol parentSymbol)
        //     {
        //         // something is wrong
        //         // currently LinqGen does not support generated LinqGen enumerable from another assembly
        //         return;
        //     }
        //
        //     // _logBuilder.AppendLine($"// caller type symbol args {callerTypeSymbol.TypeArguments[1].ToDisplayString()}");
        // }

        public void ResolveHierarchy()
        {
            foreach (var node in _allNodes.Values)
            {
                if (node.ParentSymbol != null &&
                    _allNodes.TryGetValue(node.ParentSymbol, out var parent))
                {
                    node.SetParent(parent);
                }
                else
                {
                    Roots.Add(node);
                }
            }
        }
    }
}