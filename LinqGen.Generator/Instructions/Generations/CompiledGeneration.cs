// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    /// <summary>
    /// Mock class for already compiled generation.
    /// Evaluation methods can still be added to it.
    /// </summary>
    public class CompiledGeneration : Instruction
    {
        public CompiledGeneration() : base(null)
        {

        }

        public override SourceText Render(IdentifierNameSyntax assemblyName, int id)
        {
            // ClassName = IdentifierName($"{MethodName}_{id}");
            // return GenerationTemplate.Render(assemblyName, this);
            return null;
        }
    }
}