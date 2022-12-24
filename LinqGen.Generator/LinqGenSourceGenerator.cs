// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.IO;
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

                foreach (var node in syntaxReceiver.Roots)
                {
                    var members = RenderNodeRecursive(node);
                    context.AddSource(node.FileName, FileTemplate.Render(members));
                }
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

        private IEnumerable<MemberDeclarationSyntax> RenderNodeRecursive(Generation generation)
        {
            foreach (var member in generation.Render())
                yield return member;

            if (generation.Downstream != null)
            {
                foreach (var downstream in generation.Downstream)
                {
                    foreach (var member in RenderNodeRecursive(downstream))
                        yield return member;
                }
            }
        }
    }
}