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

    public sealed class CountEvaluation : Evaluation
    {
        private TypeSyntax? FunctionType { get; }
        private bool WithStruct { get; }

        public CountEvaluation(in LinqGenExpression expression) : base(expression)
        {
            if (MethodSymbol.Parameters.Length == 1)
            {
                // bit hard coding but Count with a parameter uses predicate
                var parameterType = MethodSymbol.Parameters[0].Type;

                FunctionType = ParseTypeName(parameterType);
                WithStruct = IsStructFunction(parameterType);
            }
            else
            {
                // and no parameter just counts
                FunctionType = null;
                WithStruct = false;
            }
        }

        public override TypeSyntax ReturnType => IntType;

        public override IEnumerable<ParameterSyntax> GetParameters()
        {
            foreach (var info in base.GetParameters())
                yield return info;

            if (FunctionType != null)
            {
                yield return Parameter(default, default, WithStruct
                        ? IdentifierName($"{TypeParameterPrefix}1")
                        : FunctionType,
                    PredicateField.Identifier, default);
            }
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
                yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}1"), FunctionType!);
        }

        public override BlockSyntax RenderMethodBody()
        {
            if (FunctionType == null && Upstream!.IsCountable)
            {
                return Block(ReturnStatement(MemberAccessExpression(SourceField, CountProperty)));
            }

            var localCountName = IdentifierName("count");

            return Block(UsingLocalDeclarationStatement(
                    IteratorField.Identifier, InvocationExpression(SourceField, GetEnumeratorMethod)),
                LocalDeclarationStatement(localCountName.Identifier, LiteralExpression(0)),
                WhileStatement(InvocationExpression(IteratorField, MoveNextMethod),
                    FunctionType == null
                        ? ExpressionStatement(PreIncrementExpression(localCountName))
                        : IfStatement(InvocationExpression(MemberAccessExpression(PredicateField, InvokeMethod),
                                ArgumentList(MemberAccessExpression(IteratorField, CurrentProperty))),
                            ExpressionStatement(PreIncrementExpression(localCountName)))),
                ReturnStatement(localCountName));
        }
    }
}
