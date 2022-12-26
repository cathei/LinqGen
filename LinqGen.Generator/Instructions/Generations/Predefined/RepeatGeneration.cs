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
            OutputElementSymbol = expression.MethodSymbol.ConstructedFrom.TypeParameters[0];
        }

        public override ITypeSymbol OutputElementSymbol { get; }
        public override TypeSyntax OutputElementType => TypeName("Element");

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            yield return new TypeParameterInfo(TypeName("Element"));
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Both, OutputElementType, LocalName("element"));
            yield return new MemberInfo(MemberKind.Both, IntType, LocalName("count"));

            yield return new MemberInfo(MemberKind.Enumerator, IntType, LocalName("index"));
        }

        public override ExpressionSyntax RenderCount()
        {
            return MemberName("count");
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            ExpressionSyntax initialValue = LiteralExpression(-1);

            if (skipVar != null)
                initialValue = SubtractExpression(skipVar, LiteralExpression(1));

            yield return ExpressionStatement(SimpleAssignmentExpression(LocalName("index"), initialValue));
        }

        public override BlockSyntax RenderIteration(bool isLocal,
            SyntaxList<StatementSyntax> statements)
        {
            var currentName = MemberName("element");
            var currentRewriter = new CurrentPlaceholderRewriter(currentName);

            // replace current variables of downstream
            statements = currentRewriter.VisitStatementSyntaxList(statements);

            return Block(WhileStatement(
                LessThanExpression(PreIncrementExpression(LocalName("index")), MemberName("count")),
                Block(statements)));
        }
    }
}