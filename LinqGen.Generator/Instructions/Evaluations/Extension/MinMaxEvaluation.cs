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

    public sealed class MinMaxEvaluation : ExtensionEvaluation
    {
        private bool IsMin { get; }

        private ComparerKind ComparerKind { get; }

        public MinMaxEvaluation(in LinqGenExpression expression, int id, bool isMin) : base(expression, id)
        {
            IsMin = isMin;

            if (expression.MethodSymbol.Parameters.Length == 1)
            {
                // Min and Max with parameter
                ComparerKind = ComparerKind.Struct;
            }
            else
            {
                ComparerKind = ComparerKind.Default;
            }
        }

        protected override TypeSyntax ReturnType => InputElementType;

        private TypeSyntax ComparerInterfaceType =>
            GenericName(Identifier("IComparer"), TypeArgumentList(InputElementType));

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (ComparerKind == ComparerKind.Struct)
                yield return new(TypeName("Comparer"), ComparerInterfaceType);
        }

        protected override IEnumerable<ParameterInfo> GetParameterInfos()
        {
            if (ComparerKind == ComparerKind.Struct)
                yield return new(TypeName("Comparer"), IdentifierName("comparer"));
        }

        protected override IEnumerable<StatementSyntax> RenderInitialization()
        {
            if (ComparerKind == ComparerKind.Default)
            {
                yield return LocalDeclarationStatement(ComparerDefaultType(InputElementType, InputElementSymbol),
                    IdentifierName("comparer").Identifier, ComparerDefault(InputElementType, InputElementSymbol));
            }

            yield return LocalDeclarationStatement(BoolType, LocalName("isSet").Identifier, DefaultLiteral);
            yield return LocalDeclarationStatement(ReturnType, LocalName("result").Identifier, DefaultLiteral);
        }

        protected override IEnumerable<StatementSyntax> RenderAccumulation()
        {
            var expressionKind = IsMin ? SyntaxKind.LessThanExpression : SyntaxKind.GreaterThanExpression;

            var comparison = BinaryExpression(expressionKind,
                LiteralExpression(0),
                InvocationExpression(MemberAccessExpression(IdentifierName("comparer"), CompareMethod),
                    ArgumentList(LocalName("result"), CurrentPlaceholder)));

            yield return IfStatement(
                LogicalOrExpression(LogicalNotExpression(LocalName("isSet")), comparison), Block(
                    ExpressionStatement(SimpleAssignmentExpression(LocalName("isSet"), TrueExpression())),
                    ExpressionStatement(SimpleAssignmentExpression(LocalName("result"), CurrentPlaceholder))));
        }

        protected override IEnumerable<StatementSyntax> RenderReturn()
        {
            yield return IfStatement(LogicalNotExpression(LocalName("isSet")), ThrowInvalidOperationStatement());
            yield return ReturnStatement(LocalName("result"));
        }
    }
}