// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public sealed class ThenByOperation : OrderingOperation
    {
        public ThenByOperation(in LinqGenExpression expression, int id,
            INamedTypeSymbol? selectorType, bool withStruct, bool descending)
            : base(in expression, id, selectorType, withStruct, descending)
        {
        }

        protected override OrderingOperation? UpstreamOrder => Upstream as OrderingOperation;
    }
}
