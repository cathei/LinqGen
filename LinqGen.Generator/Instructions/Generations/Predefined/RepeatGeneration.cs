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

    public sealed class RepeatGeneration : PredefinedGeneration
    {
        public RepeatGeneration(in LinqGenExpression expression, int id) : base(expression, id)
        {
        }

        public override TypeSyntax OutputElementType => TypeName("Element");

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Both, OutputElementType, VarName("element"));
            yield return new MemberInfo(MemberKind.Both, IntType, VarName("count"));

            yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"));
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(RenderOption option)
        {
            yield return ExpressionStatement(SimpleAssignmentExpression(VarName("index"), LiteralExpression(-1)));
        }

        public override BlockSyntax RenderIteration(RenderOption option, SyntaxList<StatementSyntax> statements)
        {
            var currentName = VarName("element");
            var currentRewriter = new CurrentRewriter(currentName);

            // replace current variables of downstream
            statements = currentRewriter.VisitStatementSyntaxList(statements);

            return Block(WhileStatement(
                LessThanExpression(PreIncrementExpression(VarName("index")), VarName("count")),
                Block(statements)));
        }
    }
}