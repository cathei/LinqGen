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

    public class SumEvaluation : Evaluation
    {
        private readonly TypeSyntax? FunctionType;
        private readonly bool WithStruct;

        public SumEvaluation(in LinqGenExpression expression, ITypeSymbol parameterType) : base(expression)
        {
            ReturnType = ParseTypeName(expression.MethodSymbol.ReturnType
                .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

            if (MethodSymbol.Parameters.Length == 2)
            {
                // bit hard coding but double parameter uses selector
                FunctionType = ParseTypeName(parameterType
                    .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
                WithStruct = IsStructFunction(parameterType);
            }
            else
            {
                // and single parameter only has default value
                FunctionType = null;
                WithStruct = false;
            }
        }

        public override TypeSyntax ReturnType { get; }

        public override IEnumerable<ParameterSyntax> GetParameters()
        {
            foreach (var info in base.GetParameters())
                yield return info;

            if (FunctionType != null)
            {
                yield return Parameter(default, default,
                    WithStruct ? IdentifierName("T1") : ReturnType, SelectorName.Identifier, default);
            }

            yield return Parameter(default, default,
                ReturnType, InitialValueName.Identifier, EqualsValueClause(DefaultLiteral));
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
                yield return new TypeParameterInfo(FunctionType!);
        }

        public override BlockSyntax RenderMethodBody()
        {
            return Block(UsingLocalDeclarationStatement(
                    IteratorName.Identifier, InvocationExpression(SourceName, GetEnumeratorName)),
                WhileStatement(InvocationExpression(IteratorName, MoveNextName),
                    ExpressionStatement(
                        AssignmentExpression(SyntaxKind.AddAssignmentExpression, InitialValueName,
                            FunctionType == null
                                ? MemberAccessExpression(IteratorName, CurrentName)
                                : InvocationExpression(MemberAccessExpression(SelectorName, InvokeName),
                                    ArgumentList(MemberAccessExpression(IteratorName, CurrentName)))))),
                ReturnStatement(InitialValueName));
        }
    }
}
