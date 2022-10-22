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
    using static CodeGenUtils;

    public class AsEnumerableOperation : Operation
    {
        public AsEnumerableOperation(in LinqGenExpression expression) : base(expression) { }

        public override SourceText Render(IdentifierNameSyntax assemblyName, int id)
        {
            ClassName = IdentifierName = IdentifierName($"{MethodName}_{id}");
            return AsEnumerableTemplate.Render(assemblyName, this);
        }
    }
}