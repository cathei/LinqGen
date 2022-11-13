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

    public sealed class MinMaxEvaluation : Evaluation
    {
        private bool IsMin { get; }
        private bool WithComparer { get; }

        public MinMaxEvaluation(in LinqGenExpression expression, bool isMin) : base(expression)
        {
            IsMin = isMin;
            // max with a parameter uses comparer
            WithComparer = MethodSymbol.Parameters.Length == 1;
        }

        public override TypeSyntax ReturnType => Upstream!.OutputElementType;

        private TypeSyntax ComparerInterfaceType =>
            GenericName(Identifier("IComparer"), TypeArgumentList(Upstream!.OutputElementType));

        public override IEnumerable<ParameterSyntax> GetParameters()
        {
            foreach (var info in base.GetParameters())
                yield return info;

            if (WithComparer)
            {
                yield return Parameter(default, default,
                    IdentifierName($"{TypeParameterPrefix}1"), ComparerVar.Identifier, default);
            }
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithComparer)
                yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}1"), ComparerInterfaceType);
        }

        public override BlockSyntax RenderMethodBody()
        {
            List<StatementSyntax> statements = new List<StatementSyntax>();

            if (!WithComparer)
            {
                statements.Add(LocalDeclarationStatement(
                    ComparerVar.Identifier, ComparerDefault(Upstream!.OutputElementType)));
            }

            statements.Add(UsingLocalDeclarationStatement(
                IteratorVar.Identifier, InvocationExpression(SourceVar, GetEnumeratorMethod)));

            statements.Add(
                IfStatement(LogicalNotExpression(InvocationExpression(IteratorVar, MoveNextMethod)),
                    ThrowInvalidOperationStatement()));

            statements.Add(
                LocalDeclarationStatement(ResultVar.Identifier,
                    MemberAccessExpression(IteratorVar, CurrentProperty)));

            var expressionKind = IsMin ? SyntaxKind.LessThanExpression : SyntaxKind.GreaterThanExpression;

            statements.Add(WhileStatement(InvocationExpression(IteratorVar, MoveNextMethod), Block(
                LocalDeclarationStatement(ValueVar.Identifier, MemberAccessExpression(IteratorVar, CurrentProperty)),
                IfStatement(
                    BinaryExpression(expressionKind, LiteralExpression(0), InvocationExpression(
                        MemberAccessExpression(ComparerVar, CompareMethod),
                        ArgumentList(ResultVar, ValueVar))),
                    ExpressionStatement(SimpleAssignmentExpression(ResultVar, ValueVar))))));

            statements.Add(ReturnStatement(ResultVar));

            return Block(statements);
        }
    }
}
