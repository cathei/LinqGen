// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Immutable;
using System.Linq;

namespace Cathei.LinqGen.Generator;

// Not supported in current Unity version
[Generator(LanguageNames.CSharp)]
public class LinqGenIncrementalGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // *Caveat* Even though we are specifying tracking name,
        // CodeAnalysis 4.1.0 left out (modified, added) state from single to many.
        // So we cannot support GeneratorDriverOptions.TrackIncrementalGeneratorSteps at this time.
        // See https://github.com/dotnet/roslyn/pull/61308

        var expressions = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => ExpressionPredicate(node),
                static (ctx, _) => ExpressionTransform(ctx.SemanticModel, ctx.Node))
            .Where(static x => x != null)
            .Select(static (x, _) => x!.Value)
            .WithTrackingName("Expressions");

        var generations = expressions
            .Where(static x => x.IsCompilingGeneration())
            .Collect()
            .Select(static (x, _) => new HashSet<LinqGenExpression>(x)) // Considered Immutable
            .WithComparer(SetEqualComparer<LinqGenExpression>.Default)
            .WithTrackingName("Generations");

        var evaluations = expressions
            .Where(static x => !x.IsCompilingGeneration())
            .Collect()
            .Select(static (x, _) => new HashSet<LinqGenExpression>(x)) // Considered Immutable
            .WithComparer(SetEqualComparer<LinqGenExpression>.Default)
            .WithTrackingName("Evaluations");

        var dependencies = generations.Combine(evaluations)
            .SelectMany(static (x, _) => CreateDependencies(x.Left, x.Right))
            .WithTrackingName("Dependencies");

        var diagnostics = generations.Combine(evaluations);

        context.RegisterSourceOutput(diagnostics, ReportDiagnostics);
        context.RegisterSourceOutput(dependencies, Render);
    }

    private static uint GenerateStableId(in LinqGenExpression expr)
    {
        unchecked
        {
            string str = expr.IsCompilingGeneration()
                ? expr.GenerationKey.ToString()
                : expr.EvaluationKey.ToString();

            int hash = 0;

            foreach (var c in str)
                hash = HashCombine(hash, c);

            return (uint)hash;
        }
    }

    private static bool ExpressionPredicate(SyntaxNode node)
    {
        if (node.SyntaxTree.FilePath.StartsWith("LinqGen.Generator"))
            return false;

        return node is
            InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax } or
            CommonForEachStatementSyntax;
    }

    private static LinqGenExpression? ExpressionTransform(SemanticModel model, SyntaxNode node)
    {
        if (node is InvocationExpressionSyntax invocationSyntax)
        {
            if (LinqGenExpression.TryParse(model, invocationSyntax, out var expression))
            {
                return expression;
            }
        }
        else if (node is CommonForEachStatementSyntax forEachSyntax)
        {
            if (LinqGenExpression.TryParse(model, forEachSyntax, out var expression))
            {
                return expression;
            }
        }

        return null;
    }

    private static ImmutableDictionary<SymbolKey, LinqGenExpression> CreateGenerationDictionary(
        HashSet<LinqGenExpression> expressions)
    {
        return expressions.ToImmutableDictionary(static x => x.GenerationKey);
    }

    private static ImmutableDictionary<EvaluationKey, LinqGenExpression> CreateEvaluationDictionary(
        HashSet<LinqGenExpression> expressions)
    {
        return expressions.ToImmutableDictionary(static x => x.EvaluationKey);
    }

    private static Dictionary<SymbolKey, (LinqGenExpression, List<LinqGenExpression>)> CreateDownstreamDictionary(
        HashSet<LinqGenExpression> generations,
        HashSet<LinqGenExpression> evaluations)
    {
        var downstream = generations.ToDictionary(
            static x => x.GenerationKey,
            static x => (Expr: x, List: new List<LinqGenExpression>()));

        foreach (var generation in generations)
        {
            if (generation.UpstreamSignatureSymbols.IsEmpty)
                continue;

            // only first upstream depends on downstream
            if (downstream.TryGetValue(new(generation.UpstreamSignatureSymbols[0]), out var tuple))
                tuple.List.Add(generation);
        }

        foreach (var evaluation in evaluations)
        {
            // only first upstream depends on downstream
            if (downstream.TryGetValue(new(evaluation.UpstreamSignatureSymbols[0]), out var tuple))
                tuple.List.Add(evaluation);
        }

        return downstream;
    }

    private static ImmutableArray<LinqGenExpressionDependency> CreateDependencies(
        HashSet<LinqGenExpression> generations,
        HashSet<LinqGenExpression> evaluations)
    {
        var downstream = CreateDownstreamDictionary(generations, evaluations);

        var hashSet = new HashSet<LinqGenExpression>();
        var builder = ImmutableArray.CreateBuilder<LinqGenExpressionDependency>(generations.Count);

        foreach (var pair in downstream)
        {
            var (expr, list) = pair.Value;

            hashSet.Clear();
            hashSet.Add(expr);
            CollectUpwardDependencies(expr, downstream, hashSet);

            foreach (var child in list)
            {
                hashSet.Add(child);
                CollectUpwardDependencies(child, downstream, hashSet);
            }

            builder.Add(new LinqGenExpressionDependency(expr, hashSet.ToImmutableArray()));
        }

        return builder.MoveToImmutable();
    }

    private static void CollectUpwardDependencies(
        in LinqGenExpression current,
        Dictionary<SymbolKey, (LinqGenExpression, List<LinqGenExpression>)> downstream,
        HashSet<LinqGenExpression> result)
    {
        foreach (var symbol in current.UpstreamSignatureSymbols)
        {
            // failed to find upstream
            if (!downstream.TryGetValue(new(symbol), out var tuple))
                continue;

            var (upstream, _) = tuple;

            // result already been added
            if (!result.Add(upstream))
                continue;

            // no more upstream to traverse
            if (upstream.UpstreamSignatureSymbols.IsEmpty)
                continue;

            CollectUpwardDependencies(upstream, downstream, result);
        }
    }

    private static readonly DiagnosticDescriptor GenerationTriggerDiagnostic = new(
        "LQ0001", "LinqGen Incremental Generation Triggered",
        "Found Generation: {0}, Evaluation: {1}", "LinqGen",
        DiagnosticSeverity.Info, true);

    private static void ReportDiagnostics(SourceProductionContext context,
        (HashSet<LinqGenExpression>, HashSet<LinqGenExpression>) pair)
    {
        var (generations, evaluations) = pair;

        context.ReportDiagnostic(Diagnostic.Create(
            GenerationTriggerDiagnostic, null, generations.Count, evaluations.Count));
    }

    private static void Render(SourceProductionContext context, LinqGenExpressionDependency dependency)
    {
        var generations = new Dictionary<SymbolKey, Generation>();

        foreach (var dep in dependency.Dependencies)
        {
            if (!dep.IsCompilingGeneration())
                continue;

            var generation = InstructionFactory.CreateGeneration(dep, GenerateStableId(dep));

            // Something went wrong?
            if (generation == null)
                continue;

            generations.Add(dep.GenerationKey, generation);
        }

        foreach (var generation in generations.Values)
        {
            foreach (var upstreamSymbol in generation.UpstreamSignatureSymbols)
            {
                if (!generations.TryGetValue(new(upstreamSymbol), out var upstream))
                    continue;

                generation.AddUpstream(upstream);
            }
        }

        foreach (var dep in dependency.Dependencies)
        {
            if (dep.IsCompilingGeneration())
                continue;

            var evaluation = InstructionFactory.CreateEvaluation(dep, GenerateStableId(dep));

            // Something went wrong?
            if (evaluation == null)
                continue;

            foreach (var upstreamSymbol in evaluation.UpstreamSignatureSymbols)
            {
                if (!generations.TryGetValue(new(upstreamSymbol), out var upstream))
                    continue;

                evaluation.AddUpstream(upstream);
            }
        }

        var generationToRender = generations[dependency.Expression.GenerationKey];
        var sourceText = FileTemplate.Render(generationToRender.Render());

        context.AddSource($"LinqGen.{generationToRender.FileName}", sourceText);
    }
}
