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
        public AsEnumerableOperation(in LinqGenExpression expression, int id) : base(expression, id) { }

        public override SourceText Render()
        {
            return AsEnumerableTemplate.Render(this);
        }
    }
}