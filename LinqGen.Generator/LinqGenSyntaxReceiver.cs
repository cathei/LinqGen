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

        private readonly Dictionary<INamedTypeSymbol, Generation> _generations = new(SymbolEqualityComparer.Default);
        private readonly Dictionary<EvaluationKey, Evaluation> _evaluations = new();

        public readonly List<Generation> Roots = new();

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

            if (expression.IsCompilingGeneration())
            {
                if (_generations.ContainsKey(expression.SignatureSymbol!))
                {
                    // already registered
                    return;
                }

                var generation = InstructionFactory.CreateGeneration(_logBuilder, expression);

                if (generation == null)
                {
                    // something is wrong
                    _logBuilder.AppendFormat("/* Generation failed to create : {0} */\n",
                        expression.SignatureSymbol!.Name);
                    return;
                }

                _logBuilder.AppendFormat("/* Generation : {0} {1} */\n",
                    generation.GetType().Name, expression.SignatureSymbol!.Name);

                _generations.Add(expression.SignatureSymbol!, generation);
            }
            else
            {
                var key = new EvaluationKey(expression.UpstreamSymbol!, expression.MethodSymbol);

                if (_evaluations.ContainsKey(key))
                {
                    // already registered
                    return;
                }

                var evaluation = InstructionFactory.CreateEvaluation(_logBuilder, expression);

                if (evaluation == null)
                {
                    // something is wrong
                    _logBuilder.AppendFormat("/* Evaluation failed to create : {0} {1} */\n",
                        expression.UpstreamSymbol!.Name, expression.MethodSymbol.Name);
                    return;
                }

                _logBuilder.AppendFormat("/* Evaluation : {0} {1} */\n",
                    evaluation.GetType().Name, expression.MethodSymbol.Name);

                _evaluations.Add(key, evaluation);
            }
        }

        public void ResolveHierarchy()
        {
            var compiledGenerations = new Dictionary<INamedTypeSymbol, Generation>(SymbolEqualityComparer.Default);

            foreach (var generation in _generations.Values)
            {
                var upstreamSymbol = generation.UpstreamSymbol;

                if (upstreamSymbol == null)
                {
                    Roots.Add(generation);
                    continue;
                }

                if (!_generations.TryGetValue(upstreamSymbol, out var upstream) &&
                    !compiledGenerations.TryGetValue(upstreamSymbol, out upstream))
                {
                    // okay we will need create compiled symbol here
                    upstream = new CompiledGeneration(upstreamSymbol);
                    compiledGenerations.Add(upstreamSymbol, upstream);
                }

                generation.SetUpstream(upstream);
            }

            foreach (var evaluation in _evaluations.Values)
            {
                var upstreamSymbol = evaluation.UpstreamSymbol;

                if (upstreamSymbol == null)
                {
                    // this should never happen
                    continue;
                }

                if (!_generations.TryGetValue(upstreamSymbol, out var upstream) &&
                    !compiledGenerations.TryGetValue(upstreamSymbol, out upstream))
                {
                    // okay we will need create compiled symbol here
                    upstream = new CompiledGeneration(upstreamSymbol);
                    compiledGenerations.Add(upstreamSymbol, upstream);
                }

                evaluation.SetUpstream(upstream);
            }

            // compiled generations are always root
            Roots.AddRange(compiledGenerations.Values);
        }
    }
}