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

            int paramIndex = -1;

            if (KeySelectorKind != FunctionKind.Default)
            {
                var param = expression.GetNamedParameterType(++paramIndex);
                // Func<TIn, TOut> or IStructFunction<TIn, TOut>
                param.TypeArguments[1];

            }

            if (ValueSelectorKind != FunctionKind.Default)
            {
                var param = expression.GetNamedParameterType(++paramIndex);

            }

            if (ResultSelectorKind != FunctionKind.Default)
            {
                var param = expression.GetNamedParameterType(++paramIndex);

            }
        }

        private bool IsUnmanaged => KeySymbol.IsUnmanagedType && ValueSymbol.IsUnmanagedType;

        private TypeSyntax ComparerType => ComparerKind switch
        {
            ComparerKind.Default => ComparerDefaultType(KeyType, KeySymbol),
            ComparerKind.Interface => ComparerInterfaceType(KeyType),
            ComparerKind.Struct => TypeName("Comparer"),
            _ => throw new ArgumentOutOfRangeException()
        };

        private ITypeSymbol ValueSymbol => ValueSelectorKind switch
        {
            FunctionKind.Default => Upstream.OutputElementSymbol,
            FunctionKind.Delegate => expr,
            FunctionKind.Struct => expr,
            _ => throw new ArgumentOutOfRangeException()
        };

        private TypeSyntax ValueType => ValueSelectorKind switch
        {
            FunctionKind.Default => Upstream.OutputElementType,
            FunctionKind.Delegate => expr,
            FunctionKind.Struct => expr,
            _ => throw new ArgumentOutOfRangeException()
        };

        private TypeSyntax DictionaryType => PooledDictionaryType(
            KeyType, Upstream.OutputElementType, ComparerType, IsUnmanaged);

        private TypeSyntax PooledListType => PooledListType(ValueType, IsUnmanaged);

        private TypeSyntax GroupingType => GroupingType(KeyType, ValueType, IsUnmanaged);

        // public override ITypeSymbol OutputElementSymbol => // ...
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

            if (KeySelectorKind != FunctionKind.Default)
                yield return new MemberInfo(MemberKind.Enumerable, , VarName("index"));
        }

        public override IEnumerable<StatementSyntax> RenderInitialization(
            bool isLocal, ExpressionSyntax source, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        {
            var dictName = VarName("dict");
            var keyName = VarName("key");
            var valueName = VarName("value");
            var listName = VarName("list");

            ExpressionSyntax sourceName = isLocal ? ThisExpression() : IdentifierName("source");

            foreach (var statement in Upstream.GetLocalDeclarations(MemberKind.Enumerator))
                yield return statement;

            foreach (var statement in Upstream.GetLocalAssignments(MemberKind.Both, sourceName))
                yield return statement;

            foreach (var statement in Upstream.RenderInitialization(true, source, null, null))
                yield return statement;

            yield return ExpressionStatement(SimpleAssignmentExpression(dictName,
                ObjectCreationExpression(DictionaryType, ArgumentList(
                    LiteralExpression(0), VarName("comparer")), null)));

            ExpressionSyntax keySelectExpression = KeySelectorKind == FunctionKind.Default
                ? CurrentPlaceholder
                : InvocationExpression(MemberAccessExpression(
                    VarName("keySelector"), InvokeMethod), ArgumentList(CurrentPlaceholder));

            ExpressionSyntax valueSelectExpression = KeySelectorKind == FunctionKind.Default
                ? CurrentPlaceholder
                : InvocationExpression(MemberAccessExpression(
                    VarName("valueSelector"), InvokeMethod), ArgumentList(CurrentPlaceholder));

            var addStatements = new StatementSyntax[]
            {
                // select key
                LocalDeclarationStatement(keyName.Identifier, keySelectExpression),
                // get or create list from dict
                LocalDeclarationStatement(default, RefTokenList, VariableDeclaration(listName.Identifier,
                    InvocationExpression(MemberAccessExpression(dictName, GetOrDefaultMethod), ArgumentList(keyName)))),
                // if count is 0 here it means that the list is not initialized
                IfStatement(
                    GreaterOrEqualExpression(LiteralExpression(0), MemberAccessExpression(listName, CountProperty)),
                    // initialize list
                    ExpressionStatement(SimpleAssignmentExpression(listName,
                        ObjectCreationExpression(PooledListType, ArgumentList(LiteralExpression(0)), null)))),
                // select value
                LocalDeclarationStatement(valueName.Identifier, valueSelectExpression),
                // add value to the list
                ExpressionStatement(
                    InvocationExpression(MemberAccessExpression(listName, AddMethod), ArgumentList(valueName)))
            };

            yield return Upstream.RenderIteration(isLocal, List(addStatements));

            yield return ExpressionStatement(SimpleAssignmentExpression(VarName("index"),
                skipVar != null ? SubtractExpression(skipVar, LiteralExpression(1)) : LiteralExpression(-1)));
        }
    }
}