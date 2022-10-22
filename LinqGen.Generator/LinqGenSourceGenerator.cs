// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
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

                int id = 0;
                foreach (var node in syntaxReceiver.Roots)
                    RenderNodeRecursive(node, context, ref id);
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

        private void RenderNodeRecursive(Instruction instruction, GeneratorExecutionContext context, ref int id)
        {
            var sourceText = instruction.Render(id++);
            context.AddSource($"LinqGen.{instruction.ClassName}.g.cs", sourceText);

            if (instruction.Downstream != null)
            {
                foreach (var downstream in instruction.Downstream)
                    RenderNodeRecursive(downstream, context, ref id);
            }
        }
    }
}