// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Text;

namespace Cathei.LinqGen.Generator;

/// <summary>
/// For Unity support, 3.8 doesn't have ISyntaxContextReceiver
/// </summary>
public class LinqGenSyntaxReceiver // : ISyntaxContextReceiver
{
    private readonly StringBuilder _logBuilder;

    private readonly Dictionary<INamedTypeSymbol, Generation> _generations = new(SymbolEqualityComparer.Default);
    private readonly Dictionary<EvaluationKey, Evaluation> _evaluations = new();

    private int _idCounter;

    public readonly List<Generation> Roots = new();

    public LinqGenSyntaxReceiver(StringBuilder logBuilder)
    {
        _logBuilder = logBuilder;
    }

    public void VisitSyntaxTree(SemanticModel semanticModel, SyntaxTree syntaxTree)
    {
        foreach (var node in syntaxTree.GetRoot().DescendantNodes())
        {
            if (node is InvocationExpressionSyntax invocationSyntax)
                VisitNode(semanticModel, invocationSyntax);
            else if (node is CommonForEachStatementSyntax forEachSyntax)
                VisitNode(semanticModel, forEachSyntax);
        }
    }

    private void VisitNode(SemanticModel semanticModel, InvocationExpressionSyntax invocationSyntax)
    {
        if (!LinqGenExpression.TryParse(semanticModel, invocationSyntax, out var expression))
            return;

        if (expression.IsCompilingGeneration())
        {
            AddGeneration(expression);
        }
        else
        {
            AddEvaluation(expression);
        }
    }

    private void VisitNode(SemanticModel semanticModel, CommonForEachStatementSyntax forEachSyntax)
    {
        if (!LinqGenExpression.TryParse(semanticModel, forEachSyntax, out var expression))
            return;

        AddEvaluation(expression);
    }

    private void AddGeneration(in LinqGenExpression expression)
    {
        if (_generations.ContainsKey(expression.SignatureSymbol!))
        {
            // already registered
            return;
        }

        var generation = InstructionFactory.CreateGeneration(_logBuilder, expression, ++_idCounter);

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

    private void AddEvaluation(in LinqGenExpression expression)
    {
        var key = new EvaluationKey(
            expression.UpstreamSignatureSymbols![0], expression.MethodSymbol, expression.InputElementSymbol!);

        if (_evaluations.ContainsKey(key))
        {
            // already registered
            return;
        }

        var evaluation = InstructionFactory.CreateEvaluation(_logBuilder, expression, ++_idCounter);

        if (evaluation == null)
        {
            // something is wrong
            _logBuilder.AppendFormat("/* Evaluation failed to create : {0} {1} */\n",
                key.SignatureSymbol.Name, expression.MethodSymbol.Name);
            return;
        }

        _logBuilder.AppendFormat("/* Evaluation : {0} {1} */\n",
            evaluation.GetType().Name, expression.MethodSymbol.Name);

        _evaluations.Add(key, evaluation);
    }

    public void ResolveHierarchy()
    {
        foreach (var generation in _generations.Values)
        {
            var upstreamSymbols = generation.UpstreamSignatureSymbols;

            if (upstreamSymbols == null)
            {
                Roots.Add(generation);
                continue;
            }

            foreach (var upstreamSymbol in upstreamSymbols)
            {
                if (!_generations.TryGetValue(upstreamSymbol, out var upstream))
                    continue;

                generation.AddUpstream(upstream);
            }
        }

        foreach (var evaluation in _evaluations.Values)
        {
            var upstreamSymbols = evaluation.UpstreamSignatureSymbols!;

            foreach (var upstreamSymbol in upstreamSymbols)
            {
                if (!_generations.TryGetValue(upstreamSymbol, out var upstream))
                    continue;

                evaluation.AddUpstream(upstream);
            }
        }
    }
}