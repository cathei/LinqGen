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

    public class FirstEvaluation : Evaluation
    {
        private readonly bool OrDefault;

        public FirstEvaluation(in LinqGenExpression expression, bool orDefault) : base(expression)
        {
            OrDefault = orDefault;
            ReturnType = ElementName;
        }

        public override TypeSyntax ReturnType { get; }

        public override BlockSyntax RenderMethodBody()
        {
            return Block(UsingLocalDeclarationStatement(
                    IteratorName.Identifier, InvocationExpression(SourceName, GetEnumeratorName)),
                IfStatement(InvocationExpression(IteratorName, MoveNextName),
                    ReturnStatement(MemberAccessExpression(IteratorName, CurrentName))),
                OrDefault ? ReturnDefaultStatement() : ThrowInvalidOperationStatement());
        }
    }
}