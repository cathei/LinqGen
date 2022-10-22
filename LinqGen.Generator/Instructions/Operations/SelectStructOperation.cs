// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public class SelectStructOperation : SelectOperation
    {
        public SelectStructOperation(in LinqGenExpression expression, ITypeSymbol argumentSymbol)
            : base(expression, argumentSymbol)
        { }
    }
}