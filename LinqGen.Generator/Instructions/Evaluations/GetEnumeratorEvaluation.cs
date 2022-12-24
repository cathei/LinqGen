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

    public sealed class GetEnumeratorEvaluation : Evaluation
    {
        public GetEnumeratorEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
        }

        public override void AddUpstream(Generation upstream)
        {
            upstream.IsEnumerator = true;
            base.AddUpstream(upstream);
        }
    }
}
