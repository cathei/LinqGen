// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;
using System.Text;

namespace Cathei.LinqGen.Generator;

[Generator]
public class LinqGenSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        StringBuilder logBuilder = new();

        logBuilder.AppendLine("/* Started */");

        try
        {
            var syntaxReceiver = new LinqGenSyntaxReceiver(logBuilder);

            foreach (var syntaxTree in context.Compilation.SyntaxTrees)
            {
                var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);
                syntaxReceiver.VisitSyntaxTree(semanticModel, syntaxTree);
            }

            syntaxReceiver.ResolveHierarchy();

            var buffer = new List<MemberDeclarationSyntax>();
            int batch = 1;
            int count = 0;

            foreach (var result in syntaxReceiver.Roots.SelectMany(RenderNodeRecursive))
            {
                buffer.AddRange(result);

                if (count++ > 10)
                {
                    context.AddSource($"LinqGen.{batch}.cs", FileTemplate.Render(buffer));
                    batch++;
                    count = 0;
                    buffer.Clear();
                }
            }

            // last batch
            if (buffer.Count > 0)
                context.AddSource($"LinqGen.{batch}.cs", FileTemplate.Render(buffer));
        }
        catch (Exception ex)
        {
            logBuilder.AppendFormat("/* Exception found: {0} */\n", ex);
#if !DEBUG
                throw;
#endif
        }
        finally
        {
            logBuilder.AppendLine("/* Ended */");

            context.AddSource("Log.g.cs", logBuilder.ToString());
        }
    }

    private IEnumerable<IEnumerable<MemberDeclarationSyntax>> RenderNodeRecursive(Generation generation)
    {
        yield return generation.Render();

        if (generation.Downstream != null)
        {
            foreach (var downstream in generation.Downstream)
            {
                foreach (var result in RenderNodeRecursive(downstream))
                    yield return result;
            }
        }
    }
}