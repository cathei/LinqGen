// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public abstract class PredefinedGeneration : Generation
    {
        protected PredefinedGeneration(in LinqGenExpression expression, int id) : base(expression, id)
        {
        }

        public override bool SupportPartition => true;

        protected override ParameterListSyntax GetExtensionMethodParameters()
        {
            var parameters = GetParameters(true);

            parameters = parameters.Prepend(
                Parameter(IdentifierName("GenerationStub"), Identifier("stub")).WithModifiers(ThisTokenList));

            return ParameterList(parameters);
        }
    }
}