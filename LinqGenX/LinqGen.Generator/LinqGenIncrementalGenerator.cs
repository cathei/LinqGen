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
                static (node, _) => LinqGenAnalyzer.ShouldAnalyze(node),
                static (ctx, token) => LinqGenAnalyzer.Analyze(ctx.SemanticModel, ctx.Node, token))
            .SelectMany(static (x, _) =>
                x.HasValue ? ImmutableArray.Create(x.Value) : ImmutableArray<LinqGenSignature>.Empty)
            .WithTrackingName("Expressions");

    }

    private static uint GenerateStableId(in LinqGenSignature expr)
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


}
