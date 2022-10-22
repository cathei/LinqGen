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

        private readonly Dictionary<INamedTypeSymbol, Instruction> _instructions = new(SymbolEqualityComparer.Default);

        public readonly List<Instruction> Roots = new();

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

            if (_instructions.ContainsKey(expression.OpSymbol))
            {
                // already registered
                return;
            }

            var instruction = InstructionFactory.Create(_logBuilder, ref expression);

            if (instruction == null)
            {
                // something is wrong
                _logBuilder.AppendFormat("/* Instruction Failed to generate : {0} */\n", expression.OpSymbol.Name);
                return;
            }

            _logBuilder.AppendFormat("/* Instruction : {0} {1} */\n", instruction.GetType().Name, expression.OpSymbol.Name);

            _instructions.Add(expression.OpSymbol, instruction);
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
            foreach (var node in _instructions.Values)
            {
                if (node.ParentSymbol != null &&
                    _instructions.TryGetValue(node.ParentSymbol, out var parent))
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