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

    // public class ConcatOperation : Operation
    // {
    //     public ConcatOperation(in LinqGenExpression expression, int id) : base(expression, id)
    //     {
    //     }
    //
    //     protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
    //     {
    //     }
    //
    //     public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal, ExpressionSyntax source,
    //         ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    //     {
    //     }
    //
    //     public override ExpressionSyntax? RenderCount()
    //     {
    //     }
    //
    //     protected override StatementSyntax? RenderMoveNext()
    //     {
    //     }
    // }
}