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

    public sealed class CountEvaluation : ExtensionEvaluation
    {
        private TypeSyntax? PredicateType { get; }
        private bool WithStruct { get; }

        public CountEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            if (MethodSymbol.Parameters.Length >= 1)
            {
                // Sum with a parameter uses selector
                var parameterType = MethodSymbol.Parameters[0].Type;

                PredicateType = ParseTypeName(parameterType);
                WithStruct = IsStructFunction(parameterType);
            }
            else
            {
                // and single parameter only has default value
                PredicateType = null;
                WithStruct = false;
            }
        }

        protected override TypeSyntax ReturnType => IntType;

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
                yield return new(TypeName("Predicate"), PredicateType!);
        }

        protected override IEnumerable<ParameterInfo> GetParameterInfos()
        {
            if (PredicateType != null)
            {
                yield return new ParameterInfo(
                    WithStruct ? TypeName("Predicate") : PredicateType, IdentifierName("predicate"));
            }
        }

        public override IEnumerable<MemberDeclarationSyntax> RenderExtensionMembers()
        {
            // Count method is already there
            if (PredicateType == null && Upstream.SupportCount)
                yield break;

            foreach (var member in base.RenderExtensionMembers())
                yield return member;
        }

        protected override IEnumerable<StatementSyntax> RenderInitialization()
        {
            yield return LocalDeclarationStatement(IntType, LocalName("result").Identifier, DefaultLiteral);
        }

        protected override IEnumerable<StatementSyntax> RenderAccumulation()
        {
            if (PredicateType == null)
            {
                yield return ExpressionStatement(PreIncrementExpression(LocalName("result")));
            }
            else
            {
                yield return IfStatement(InvocationExpression(MemberAccessExpression(
                        IdentifierName("predicate"), InvokeMethod), ArgumentList(CurrentPlaceholder)),
                    ExpressionStatement(PreIncrementExpression(LocalName("result"))));
            }
        }

        protected override IEnumerable<StatementSyntax> RenderReturn()
        {
            yield return ReturnStatement(LocalName("result"));
        }
    }
}