// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public sealed class GroupByOperation : Operation
    {
        private ITypeSymbol KeySymbol { get; }
        private TypeSyntax KeyType { get; }

        private FunctionKind KeySelectorKind { get; }
        private FunctionKind ValueSelectorKind { get; }
        private FunctionKind ResultSelectorKind { get; }
        private ComparerKind ComparerKind { get; }

        public GroupByOperation(in LinqGenExpression expression, int id,
            FunctionKind keySelectorKind, FunctionKind valueSelectorKind, FunctionKind resultSelectorKind,
            ComparerKind comparerKind) : base(expression, id)
        {
            KeySymbol = expression.SignatureSymbol!.TypeArguments[1];
            KeyType = ParseTypeName(KeySymbol);

            KeySelectorKind = keySelectorKind;
            ValueSelectorKind = valueSelectorKind;
            ResultSelectorKind = resultSelectorKind;
            ComparerKind = comparerKind;
        }

        private bool IsUnmanaged => KeySymbol.IsUnmanagedType && Upstream.OutputElementSymbol.IsUnmanagedType;

        private TypeSyntax DictionaryType => PooledDictionaryType(KeyType, Upstream.OutputElementType, IsUnmanaged);

        private TypeSyntax GroupingType => GroupingType(KeyType, Upstream.OutputElementType, IsUnmanaged);

        public override ITypeSymbol OutputElementSymbol => // ...
        public override TypeSyntax OutputElementType => GroupingType;

        public override bool SupportPartition => true;

        public override ExpressionSyntax RenderCount()
        {
            return MemberAccessExpression(VarName("dict"), CountProperty);
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Enumerator, DictionaryType, VarName("dict"));
            yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"));
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(
            bool isLocal, ExpressionSyntax source, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            var elementsName = VarName("elements");
        }
    }
}