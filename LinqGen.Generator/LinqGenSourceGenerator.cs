// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

                IdentifierNameSyntax assemblyName =
                    SyntaxFactory.IdentifierName(context.Compilation.Assembly.Name.Replace('.', '_'));

                int id = 0;

                foreach (var node in syntaxReceiver.Roots)
                    RenderNodeRecursive(node, context, assemblyName, ref id);
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

        private void RenderNodeRecursive(Generation generation,
            GeneratorExecutionContext context, IdentifierNameSyntax assemblyName, ref int id)
        {
            var sourceText = generation.Render(assemblyName, id++);
            context.AddSource($"LinqGen.{id}.g.cs", sourceText);

            if (generation.Downstream != null)
            {
                foreach (var downstream in generation.Downstream)
                    RenderNodeRecursive(downstream, context, assemblyName, ref id);
            }
        }
    }
}