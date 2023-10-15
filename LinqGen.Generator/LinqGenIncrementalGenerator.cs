// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Immutable;
using System.Linq;

namespace Cathei.LinqGen.Generator;

// Not supported in current Unity version
[Generator(LanguageNames.CSharp)]
public class LinqGenIncrementalGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var expressions = context.SyntaxProvider.CreateSyntaxProvider(
                static (node, _) => ExpressionPredicate(node),
                static (ctx, _) => ExpressionTransform(ctx.SemanticModel, ctx.Node))
            .Where(static x => x != null)
            .Select(static (x, _) => x!.Value);

        var generations = expressions
            .Where(static x => x.IsCompilingGeneration())
            .Collect()
            .Select(static (x, _) => CreateGenerationDictionary(x));

        var evaluations = expressions
            .Where(static x => !x.IsCompilingGeneration())
            .Collect()
            .Select(static (x, _) => CreateEvaluationDictionary(x));

        var downstream = generations.Combine(evaluations)
            .Select(static (x, _) => CreateDownstreamDictionary(x.Left, x.Right));

        var dependencies = generations.Combine(downstream)
            .SelectMany(static (x, _) => CreateDependencies(x.Left, x.Right));

        context.RegisterSourceOutput(dependencies, Render);
    }

    public readonly struct LinqGenExpressionDependency
    {
        public readonly LinqGenExpression Expression;
        public readonly ImmutableArray<LinqGenExpression> Dependencies;

        /// logic
        /// consider all nested upstreams
        /// consider direct downstream and its all additional nested upstreams

        public LinqGenExpressionDependency(
            in LinqGenExpression expression,
            in ImmutableArray<LinqGenExpression> dependencies)
        {
            Expression = expression;
            Dependencies = dependencies;
        }
    }

    private static uint GenerateStableId(in LinqGenExpression expr)
    {
        return unchecked((uint)SymbolEqualityComparer.Default.GetHashCode(expr.SignatureSymbol));
    }

    private static bool ExpressionPredicate(SyntaxNode node)
    {
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

    private static ImmutableDictionary<ISymbol, LinqGenExpression> CreateGenerationDictionary(
        in ImmutableArray<LinqGenExpression> expressions)
    {
        var builder = ImmutableDictionary.CreateBuilder<ISymbol, LinqGenExpression>(SymbolEqualityComparer.Default);

        foreach (var expr in expressions)
        {
            if (builder.ContainsKey(expr.SignatureSymbol!))
            {
                // already registered
                continue;
            }

            builder.Add(expr.SignatureSymbol!, expr);
        }

        return builder.ToImmutable();
    }

    private static ImmutableDictionary<EvaluationKey, LinqGenExpression> CreateEvaluationDictionary(
        in ImmutableArray<LinqGenExpression> expressions)
    {
        var builder = ImmutableDictionary.CreateBuilder<EvaluationKey, LinqGenExpression>();

        foreach (var expr in expressions)
        {
            var key = new EvaluationKey(expr.UpstreamSignatureSymbols[0], expr.MethodSymbol, expr.InputElementSymbol!);

            if (builder.ContainsKey(key))
            {
                // already registered
                continue;
            }

            builder.Add(key, expr);
        }

        return builder.ToImmutable();
    }

    private static ImmutableDictionary<ISymbol, ImmutableArray<LinqGenExpression>> CreateDownstreamDictionary(
        ImmutableDictionary<ISymbol, LinqGenExpression> generations,
        ImmutableDictionary<EvaluationKey, LinqGenExpression> evaluations)
    {
        var downstream = generations.ToDictionary(
            static x => x.Key,
            static _ => ImmutableArray.CreateBuilder<LinqGenExpression>(),
            generations.KeyComparer);

        foreach (var generation in generations.Values)
        {
            if (generation.UpstreamSignatureSymbols.IsEmpty)
                continue;

            // only first upstream depends on downstream
            if (downstream.TryGetValue(generation.UpstreamSignatureSymbols[0], out var builder))
                builder.Add(generation);
        }

        foreach (var evaluation in evaluations.Values)
        {
            // only first upstream depends on downstream
            if (downstream.TryGetValue(evaluation.UpstreamSignatureSymbols[0], out var builder))
                builder.Add(evaluation);
        }

        return downstream.ToImmutableDictionary(
            x => x.Key,
            x => x.Value.ToImmutable(),
            downstream.Comparer);
    }

    private static IEnumerable<LinqGenExpressionDependency> CreateDependencies(
        ImmutableDictionary<ISymbol, LinqGenExpression> generations,
        ImmutableDictionary<ISymbol, ImmutableArray<LinqGenExpression>> downstream)
    {
        var hashSet = new HashSet<LinqGenExpression>();

        foreach (var pair in generations)
        {
            hashSet.Clear();
            hashSet.Add(pair.Value);
            CollectUpwardDependencies(pair.Value, generations, hashSet);

            foreach (var expression in downstream[pair.Key])
            {
                hashSet.Add(expression);
                CollectUpwardDependencies(expression, generations, hashSet);
            }

            yield return new LinqGenExpressionDependency(pair.Value, hashSet.ToImmutableArray());
        }
    }

    private static void CollectUpwardDependencies(
        in LinqGenExpression current,
        ImmutableDictionary<ISymbol, LinqGenExpression> generations,
        HashSet<LinqGenExpression> result)
    {
        foreach (var symbol in current.UpstreamSignatureSymbols)
        {
            // failed to find upstream
            if (!generations.TryGetValue(symbol, out var upstream))
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
        var generations = new Dictionary<ISymbol, Generation>(SymbolEqualityComparer.Default);

        foreach (var dep in dependency.Dependencies)
        {
            if (!dep.IsCompilingGeneration())
                continue;

            var generation = InstructionFactory.CreateGeneration(dep, GenerateStableId(dep));

            // Something went wrong?
            if (generation == null)
                continue;

            generations.Add(dep.SignatureSymbol!, generation);
        }

        foreach (var generation in generations.Values)
        {
            foreach (var upstreamSymbol in generation.UpstreamSignatureSymbols)
            {
                if (!generations.TryGetValue(upstreamSymbol, out var upstream))
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
                if (!generations.TryGetValue(upstreamSymbol, out var upstream))
                    continue;

                evaluation.AddUpstream(upstream);
            }
        }

        var generationToRender = generations[dependency.Expression.SignatureSymbol!];
        var sourceText = FileTemplate.Render(generationToRender.Render());

        context.AddSource($"LinqGen.{generationToRender.FileName}", sourceText);
    }
}
