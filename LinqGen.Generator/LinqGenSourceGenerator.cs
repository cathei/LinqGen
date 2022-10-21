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

            logBuilder.AppendLine("// Started");

            var syntaxReceiver = new LinqGenSyntaxReceiver(logBuilder);

            foreach (var syntaxTree in context.Compilation.SyntaxTrees)
            {
                var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);
                syntaxReceiver.VisitSyntaxTree(semanticModel, syntaxTree);
            }

            StringBuilder builder = new();
            int id = 0;

            syntaxReceiver.ResolveHierarchy();

            foreach (var node in syntaxReceiver.Roots)
                RenderNodeRecursive(node, context, builder, ref id);

            logBuilder.AppendLine("// Ended");

            context.AddSource("Log.g.cs", logBuilder.ToString());
        }

        private void RenderNodeRecursive(Node node, GeneratorExecutionContext context, StringBuilder builder, ref int id)
        {
            node.Render(builder, id++);
            context.AddSource($"LinqGen.{node.ClassName}.g.cs", builder.ToString());

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                    RenderNodeRecursive(child, context, builder, ref id);
            }
        }
    }
}