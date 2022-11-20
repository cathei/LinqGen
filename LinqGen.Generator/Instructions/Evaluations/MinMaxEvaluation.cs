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

    public sealed class MinMaxEvaluation : LocalEvaluation
    {
        private bool IsMin { get; }
        private bool WithComparer { get; }
        private bool IsElementComparable { get; }

        public MinMaxEvaluation(in LinqGenExpression expression, int id, bool isMin) : base(expression, id)
        {
            IsMin = isMin;

            // generic Min and Max with type parameter
            WithComparer = expression.MethodSymbol.TypeParameters.Length == 1;
            IsElementComparable = TryGetComparableSelfInterface(InputElementSymbol, out _);
        }

        private TypeSyntax ReturnType => Upstream.OutputElementType;

        private TypeParameterInfo ComparerType => new(TypeName("Comparer"), ComparerInterfaceType);

        private TypeSyntax ComparerInterfaceType =>
            GenericName(Identifier("IComparer"), TypeArgumentList(Upstream.OutputElementType));

        public override IEnumerable<MemberDeclarationSyntax> RenderUpstreamMembers()
        {
            if (WithComparer)
            {
                yield return MethodDeclaration(
                    SingletonList(AggressiveInliningAttributeList), PublicTokenList,
                    ReturnType, null, MethodName.Identifier,
                    TypeParameterList(SingletonSeparatedList(ComparerType.AsTypeParameter())),
                    ParameterList(Parameter(ComparerType.Name, Identifier("comparer"))),
                    SingletonList(ComparerType.AsGenericConstraint()!),
                    RenderBody(), null);
            }
            else
            {
                yield return MethodDeclaration(
                    SingletonList(AggressiveInliningAttributeList), PublicTokenList, ReturnType, null,
                    MethodName.Identifier, null, EmptyParameterList, default, RenderBody(), null);
            }
        }

        protected override IEnumerable<StatementSyntax> RenderInitialization()
        {
            if (!WithComparer)
            {
                yield return LocalDeclarationStatement(ComparerType.Name.Identifier,
                    IsElementComparable
                        ? ObjectCreationExpression(GenericName(
                            Identifier("CompareToComparer"), TypeArgumentList(Upstream.OutputElementType)))
                        : ComparerDefault(Upstream.OutputElementType));
            }

            yield return LocalDeclarationStatement(BoolType, VarName("isSet").Identifier, FalseExpression());
            yield return LocalDeclarationStatement(ReturnType, VarName("result").Identifier, DefaultLiteral);
        }

        protected override IEnumerable<StatementSyntax> RenderAccumulation()
        {
            var expressionKind = IsMin ? SyntaxKind.LessThanExpression : SyntaxKind.GreaterThanExpression;

            var comparison = BinaryExpression(expressionKind,
                LiteralExpression(0),
                InvocationExpression(MemberAccessExpression(IdentifierName("comparer"), CompareMethod),
                    ArgumentList(VarName("result"), CurrentPlaceholder)));

            yield return IfStatement(
                LogicalOrExpression(LogicalNotExpression(VarName("isSet")), comparison), Block(
                    ExpressionStatement(SimpleAssignmentExpression(VarName("isSet"), TrueExpression())),
                    ExpressionStatement(SimpleAssignmentExpression(VarName("result"), CurrentPlaceholder))));
        }

        protected override IEnumerable<StatementSyntax> RenderReturn()
        {
            yield return IfStatement(LogicalNotExpression(VarName("isSet")), ThrowInvalidOperationStatement());
            yield return ReturnStatement(VarName("result"));
        }
    }
}
