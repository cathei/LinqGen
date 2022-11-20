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

    public sealed class SumEvaluation : LocalEvaluation
    {
        public SumEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
        }

        protected override TypeSyntax ReturnType => Upstream.OutputElementType;

        protected override IEnumerable<StatementSyntax> RenderInitialization()
        {
            yield return LocalDeclarationStatement(ReturnType, VarName("result").Identifier, DefaultLiteral);
        }

        protected override IEnumerable<StatementSyntax> RenderAccumulation()
        {
            yield return ExpressionStatement(AddAssignmentExpression(VarName("result"), CurrentPlaceholder));
        }

        protected override IEnumerable<StatementSyntax> RenderReturn()
        {
            yield return ReturnStatement(VarName("result"));
        }
    }
}
