// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
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
            context.AddSource("Log.g.cs", "/* Started */");


            var syntaxReceiver = new LinqGenSyntaxReceiver();

            foreach (var syntaxTree in context.Compilation.SyntaxTrees)
            {
                var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);
                syntaxReceiver.VisitSyntaxTree(semanticModel, syntaxTree);
            }

            StringBuilder builder = new();
            int id = 0;

            foreach (var item in syntaxReceiver.Generations)
            {
                builder.Clear();

                if (item.IsList)
                {
                    GenerationListFormatter.Format(builder, item, id);
                }
                else
                {
                    GenerationFormatter.Format(builder, item, id);
                }

                context.AddSource($"LinqGen.Generations.{id}.g.cs", builder.ToString());
                id++;
            }

            foreach (var item in syntaxReceiver.Operations)
            {
                context.AddSource($"LinqGen.Generations.{id}.g.cs", "/* Empty */");
                id++;

            }
        }
    }
}