// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Immutable;
using System.Linq;

namespace Cathei.LinqGen.Generator;

[Generator(LanguageNames.CSharp)]
public class LinqGenIncrementalGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // *Caveat* Even though we are specifying tracking name,
        // CodeAnalysis 4.1.0 left out (modified, added) state from single to many.
        // So we cannot support GeneratorDriverOptions.TrackIncrementalGeneratorSteps at this time.
        // See https://github.com/dotnet/roslyn/pull/61308

        var signatures = context.SyntaxProvider
            .CreateSyntaxProvider(
                static (node, _) => LinqGenAnalyzer.ShouldAnalyze(node),
                static (ctx, token) => LinqGenAnalyzer.Analyze(ctx.SemanticModel, ctx.Node, token))
            .SelectMany(static (x, _) => x)
            .WithTrackingName("Signatures");

        context.RegisterSourceOutput(signatures, RenderSourceOutput);
    }

    private void RenderSourceOutput(SourceProductionContext ctx, LinqGenRender render)
    {
        var source = FileRender.Render(render.Render());
        ctx.AddSource($"LinqGen_{render.MethodName.Identifier.ValueText}_{render.UniqueId}.g.cs", source);
    }
}
