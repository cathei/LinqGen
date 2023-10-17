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
        var compilation = context.CompilationProvider
            .Select(static (comp, _) => new CompilationCache(comp))
            .WithTrackingName("Compilation");

        var nodes = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => ExpressionPredicate(node), static (ctx, _) => ctx.Node)
            .WithTrackingName("Nodes");

        var expressions = nodes.Combine(compilation)
            .Select(static (x, _) => ExpressionTransform(x.Right, x.Left))
            .Collect()
            .SelectMany(static (x, _) => DistinctExpression(x))
            .WithTrackingName("Expressions");

        var generations = expressions
            .Where(static x => x.IsCompilingGeneration())
            .Collect()
            .WithComparer(ImmutableArrayComparer<LinqGenExpression>.Default)
            .Select(static (x, _) => CreateGenerationDictionary(x))
            .WithComparer(ImmutableDictionaryComparer<SymbolKey, LinqGenExpression>.Default)
            .WithTrackingName("Generations");

        var evaluations = expressions
            .Where(static x => !x.IsCompilingGeneration())
            .Collect()
            .WithComparer(ImmutableArrayComparer<LinqGenExpression>.Default)
            .Select(static (x, _) => CreateEvaluationDictionary(x))
            .WithComparer(ImmutableDictionaryComparer<EvaluationKey, LinqGenExpression>.Default)
            .WithTrackingName("Evaluations");

        var downstream = generations.Combine(evaluations)
            .Select(static (x, _) => CreateDownstreamDictionary(x.Left, x.Right))
            .WithComparer(new ImmutableDictionaryComparer<SymbolKey, ImmutableArray<LinqGenExpression>>(
                EqualityComparer<SymbolKey>.Default, ImmutableArrayComparer<LinqGenExpression>.Default))
            .WithTrackingName("Downstream");

        // var dependencies = generations.Combine(downstream)
        //     .SelectMany(static (x, _) => CreateDependencies(x.Left, x.Right))
        //     .WithTrackingName("Dependencies");

        // This is bit dumb but CodeAnalysis 4.1.0 left out (modified, added) state.
        // See https://github.com/dotnet/roslyn/pull/61308
        var dependencies = expressions
            .Where(x => x.IsCompilingGeneration())
            .Combine(generations)
            .Combine(downstream)
            .Select(static (x, _) => CreateDependency(x.Left.Left, x.Left.Right, x.Right))
            .WithTrackingName("Dependencies");

        context.RegisterSourceOutput(dependencies, Render);
    }

    private static uint GenerateStableId(in LinqGenExpression expr)
    {
        unchecked
        {
            string str;

            if (expr.IsCompilingGeneration())
            {
                str = expr.SignatureSymbol!
                    .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            }
            else
            {
                str = expr.UpstreamSignatureSymbols[0]
                    .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            }

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

    private static LinqGenExpression? ExpressionTransform(in CompilationCache compilation, SyntaxNode node)
    {
        var model = compilation.GetSemanticModel(node.SyntaxTree);

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

    private static IEnumerable<LinqGenExpression> DistinctExpression(in ImmutableArray<LinqGenExpression?> expressions)
    {
        return expressions.Where(x => x != null).Select(x => x!.Value).Distinct();
    }

    private static ImmutableDictionary<SymbolKey, LinqGenExpression> CreateGenerationDictionary(
        in ImmutableArray<LinqGenExpression> expressions)
    {
        var builder = ImmutableDictionary.CreateBuilder<SymbolKey, LinqGenExpression>();

        foreach (var expr in expressions)
        {
            if (builder.ContainsKey(expr.GenerationKey))
            {
                // already registered
                continue;
            }

            builder.Add(expr.GenerationKey, expr);
        }

        return builder.ToImmutable();
    }

    private static ImmutableDictionary<EvaluationKey, LinqGenExpression> CreateEvaluationDictionary(
        in ImmutableArray<LinqGenExpression> expressions)
    {
        var builder = ImmutableDictionary.CreateBuilder<EvaluationKey, LinqGenExpression>();

        foreach (var expr in expressions)
        {
            var key = expr.EvaluationKey;

            if (builder.ContainsKey(key))
            {
                // already registered
                continue;
            }

            builder.Add(key, expr);
        }

        return builder.ToImmutable();
    }

    private static ImmutableDictionary<SymbolKey, ImmutableArray<LinqGenExpression>> CreateDownstreamDictionary(
        ImmutableDictionary<SymbolKey, LinqGenExpression> generations,
        ImmutableDictionary<EvaluationKey, LinqGenExpression> evaluations)
    {
        var downstream = generations.ToDictionary(
            static x => x.Key,
            static _ => ImmutableArray.CreateBuilder<LinqGenExpression>());

        foreach (var generation in generations.Values)
        {
            if (generation.UpstreamSignatureSymbols.IsEmpty)
                continue;

            // only first upstream depends on downstream
            if (downstream.TryGetValue(new(generation.UpstreamSignatureSymbols[0]), out var builder))
                builder.Add(generation);
        }

        foreach (var evaluation in evaluations.Values)
        {
            // only first upstream depends on downstream
            if (downstream.TryGetValue(new(evaluation.UpstreamSignatureSymbols[0]), out var builder))
                builder.Add(evaluation);
        }

        return downstream.ToImmutableDictionary(
            x => x.Key,
            x => x.Value.ToImmutable());
    }

    // private static IEnumerable<LinqGenExpressionDependency> CreateDependencies(
    //     ImmutableDictionary<SymbolKey, LinqGenExpression> generations,
    //     ImmutableDictionary<SymbolKey, ImmutableArray<LinqGenExpression>> downstream)
    // {
    //     var hashSet = new HashSet<LinqGenExpression>();
    //
    //     foreach (var pair in generations)
    //     {
    //         hashSet.Clear();
    //         hashSet.Add(pair.Value);
    //         CollectUpwardDependencies(pair.Value, generations, hashSet);
    //
    //         foreach (var expression in downstream[pair.Key])
    //         {
    //             hashSet.Add(expression);
    //             CollectUpwardDependencies(expression, generations, hashSet);
    //         }
    //
    //         yield return new LinqGenExpressionDependency(pair.Value, hashSet.ToImmutableArray());
    //     }
    // }

    private static LinqGenExpressionDependency CreateDependency(
        in LinqGenExpression generation,
        ImmutableDictionary<SymbolKey, LinqGenExpression> generations,
        ImmutableDictionary<SymbolKey, ImmutableArray<LinqGenExpression>> downstream)
    {
        var hashSet = new HashSet<LinqGenExpression>();

        hashSet.Clear();
        hashSet.Add(generation);
        CollectUpwardDependencies(generation, generations, hashSet);

        foreach (var expression in downstream[generation.GenerationKey])
        {
            hashSet.Add(expression);
            CollectUpwardDependencies(expression, generations, hashSet);
        }

        return new LinqGenExpressionDependency(generation, hashSet.ToImmutableArray());
    }

    private static void CollectUpwardDependencies(
        in LinqGenExpression current,
        ImmutableDictionary<SymbolKey, LinqGenExpression> generations,
        HashSet<LinqGenExpression> result)
    {
        foreach (var symbol in current.UpstreamSignatureSymbols)
        {
            // failed to find upstream
            if (!generations.TryGetValue(new(symbol), out var upstream))
                continue;

            // result already been added
            if (!result.Add(upstream))
                continue;

            // no more upstream to traverse
            if (upstream.UpstreamSignatureSymbols.IsEmpty)
                continue;

            CollectUpwardDependencies(upstream, generations, result);
        }
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

        context.ReportDiagnostic(Diagnostic.Create(
            new DiagnosticDescriptor("AAA", $"{generationToRender.FileName}",
                $"I am generating {dependency.Expression.SignatureSymbol?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}",
                "LinqGen.Generation",
                DiagnosticSeverity.Error,
                true), null));

        // try
        // {
        context.AddSource($"LinqGen.{generationToRender.FileName}", sourceText);
        // }
        // catch (Exception ex)
        // {
        //     throw new Exception($"I am generating {dependency.Expression.SignatureSymbol?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}" +
        //                         $"-> {GenerateStableId(dependency.Expression)} -> {ex.Message}",
        //         ex);
        // }
    }
}
