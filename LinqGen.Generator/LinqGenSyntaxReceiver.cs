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

        private readonly Dictionary<LinqGenExpression.Key, Instruction> _allInstructions = new();

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

            if (_allInstructions.ContainsKey(expression.GetKey()))
            {
                // already registered
                return;
            }

            var instruction = InstructionFactory.Create(_logBuilder, expression);

            if (instruction == null)
            {
                // something is wrong
                _logBuilder.AppendFormat("/* Instruction failed to generate : {0} */\n", expression.MethodSymbol.Name);
                return;
            }

            _logBuilder.AppendFormat("/* Instruction : {0} {1} */\n", instruction.GetType().Name, expression.MethodSymbol.Name);

            _allInstructions.Add(expression.GetKey(), instruction);
        }

        public void ResolveHierarchy()
        {
            var compiledGenerations = new Dictionary<LinqGenExpression.Key, Instruction>();

            foreach (var node in _allInstructions.Values)
            {
                if (node.UpstreamSymbol == null)
                {
                    Roots.Add(node);
                    continue;
                }

                var upstreamKey = new LinqGenExpression.Key(node.UpstreamSymbol);

                if (!_allInstructions.TryGetValue(upstreamKey, out var upstream) &&
                    !compiledGenerations.TryGetValue(upstreamKey, out upstream))
                {
                    // okay we will need create compiled symbol here
                    upstream = new CompiledGeneration(node.UpstreamSymbol);
                    compiledGenerations.Add(upstreamKey, upstream);
                }

                node.SetUpstream(upstream);
            }
        }
    }
}