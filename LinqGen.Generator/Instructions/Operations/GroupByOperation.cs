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
            KeySelectorKind = keySelectorKind;
            ValueSelectorKind = valueSelectorKind;
            ResultSelectorKind = resultSelectorKind;
            ComparerKind = comparerKind;

            int paramIndex = -1;

            // NOTE: key selector should always exist
            if (true) // KeySelectorKind != FunctionKind.Default)
            {
                var param = expression.GetNamedParameterType(++paramIndex);
                // Func<TIn, TOut> or IStructFunction<TIn, TOut>
                KeySymbol = param.TypeArguments[1];
                KeyType = ParseTypeName(KeySymbol);
            }

            if (ValueSelectorKind != FunctionKind.Default)
            {
                var param = expression.GetNamedParameterType(++paramIndex);
                // Func<TIn, TOut> or IStructFunction<TIn, TOut>
                _explicitValueSymbol = param.TypeArguments[1];
                _explicitValueType = ParseTypeName(_explicitValueSymbol);
            }

            if (ResultSelectorKind != FunctionKind.Default)
            {
                var param = expression.GetNamedParameterType(++paramIndex);
                // Func<TIn, TOut> or IStructFunction<TIn, TOut>
                _explicitResultSymbol = param.TypeArguments[1];
                _explicitResultType = ParseTypeName(_explicitResultSymbol);
            }

            // IStub<IEnumerable<Element>, ...>
            var returnType = (INamedTypeSymbol)expression.MethodSymbol.ReturnType;
            var enumerableType = (INamedTypeSymbol)returnType.TypeArguments[0];
            OutputElementSymbol = enumerableType.TypeArguments[0];
        }

        private bool IsUnmanaged => KeySymbol.IsUnmanagedType && ValueSymbol.IsUnmanagedType;

        private TypeSyntax ComparerType => ComparerKind switch
        {
            ComparerKind.Default => EqualityComparerDefaultType(KeyType, KeySymbol),
            ComparerKind.Interface => EqualityComparerInterfaceType(KeyType),
            ComparerKind.Struct => TypeName("Comparer"),
            _ => throw new ArgumentOutOfRangeException()
        };


        private readonly ITypeSymbol? _explicitValueSymbol;
        private ITypeSymbol ValueSymbol => ValueSelectorKind switch
        {
            FunctionKind.Default => Upstream.OutputElementSymbol,
            FunctionKind.Delegate => _explicitValueSymbol!,
            FunctionKind.Struct => _explicitValueSymbol!,
            _ => throw new ArgumentOutOfRangeException()
        };

        private readonly TypeSyntax? _explicitValueType;
        private TypeSyntax ValueType => ValueSelectorKind switch
        {
            FunctionKind.Default => Upstream.OutputElementType,
            FunctionKind.Delegate => _explicitValueType!,
            FunctionKind.Struct => _explicitValueType!,
            _ => throw new ArgumentOutOfRangeException()
        };

        private readonly ITypeSymbol? _explicitResultSymbol;
        private ITypeSymbol ResultSymbol => ResultSelectorKind switch
        {
            FunctionKind.Default => Upstream.OutputElementSymbol,
            FunctionKind.Delegate => _explicitResultSymbol!,
            FunctionKind.Struct => _explicitResultSymbol!,
            _ => throw new ArgumentOutOfRangeException()
        };

        private readonly TypeSyntax? _explicitResultType;
        private TypeSyntax ResultType => ResultSelectorKind switch
        {
            FunctionKind.Default => Upstream.OutputElementType,
            FunctionKind.Delegate => _explicitResultType!,
            FunctionKind.Struct => _explicitResultType!,
            _ => throw new ArgumentOutOfRangeException()
        };

        private TypeSyntax DictionaryType => PooledDictionaryType(
            KeyType, Upstream.OutputElementType, ComparerType, IsUnmanaged);

        private TypeSyntax PooledListType => PooledListType(ValueType, IsUnmanaged);

        private TypeSyntax GroupingType => GroupingType(KeyType, ValueType, IsUnmanaged);

        public override ITypeSymbol OutputElementSymbol { get; }

        public override TypeSyntax OutputElementType => ResultSelectorKind switch
        {
            FunctionKind.Default => GroupingType,
            FunctionKind.Delegate => ResultType,
            FunctionKind.Struct => ResultType,
            _ => throw new ArgumentOutOfRangeException()
        };

        public override bool SupportPartition => true;

        // cannot decide count unless enumerated
        public override ExpressionSyntax? RenderCount() => null;

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Enumerator, DictionaryType, VarName("dict"));
            yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"));

            if (KeySelectorKind == FunctionKind.Delegate)
            {
                yield return new MemberInfo(MemberKind.Enumerable,
                    FuncDelegateType(Upstream.OutputElementType, KeyType), VarName("keySelector"));
            }
            else if (KeySelectorKind == FunctionKind.Struct)
            {
                yield return new MemberInfo(MemberKind.Enumerable, TypeName("KeySelector"), VarName("keySelector"));
            }

            if (ValueSelectorKind == FunctionKind.Delegate)
            {
                yield return new MemberInfo(MemberKind.Enumerable,
                    FuncDelegateType(Upstream.OutputElementType, ValueType), VarName("valueSelector"));
            }
            else if (ValueSelectorKind == FunctionKind.Struct)
            {
                yield return new MemberInfo(MemberKind.Enumerable, TypeName("ValueSelector"), VarName("valueSelector"));
            }

            if (ResultSelectorKind == FunctionKind.Delegate)
            {
                yield return new MemberInfo(MemberKind.Both,
                    FuncDelegateType(GroupingType, ResultType), VarName("keySelector"));
            }
            else if (ResultSelectorKind == FunctionKind.Struct)
            {
                yield return new MemberInfo(MemberKind.Both, TypeName("ResultSelector"), VarName("resultSelector"));
            }

            if (ComparerKind != ComparerKind.Default)
            {
                yield return new MemberInfo(MemberKind.Enumerable, ComparerType, VarName("comparer"));
            }
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (KeySelectorKind == FunctionKind.Struct)
            {
                yield return new TypeParameterInfo(TypeName("KeySelector"),
                    StructFunctionInterfaceType(Upstream.OutputElementType, KeyType));
            }

            if (ValueSelectorKind == FunctionKind.Struct)
            {
                yield return new TypeParameterInfo(TypeName("ValueSelector"),
                    StructFunctionInterfaceType(Upstream.OutputElementType, ValueType));
            }

            if (ResultSelectorKind == FunctionKind.Struct)
            {
                yield return new TypeParameterInfo(TypeName("ResultSelector"),
                    StructFunctionInterfaceType(GroupingType, ResultType));
            }

            if (ComparerKind == ComparerKind.Struct)
            {
                yield return new TypeParameterInfo(TypeName("Comparer"),
                    EqualityComparerInterfaceType(KeyType));
            }
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

            if (ComparerKind == ComparerKind.Default)
            {
                yield return LocalDeclarationStatement(ComparerType, VarName("comparer").Identifier,
                    ComparerDefault(KeyType, KeySymbol));
            }

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

        public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
        {
            var slotName = VarName("slot");
            var groupingName = VarName("grouping");
            var currentName = VarName("current");
            var currentRewriter = new PlaceholderRewriter(currentName);

            // replace current variables of downstream
            statements = currentRewriter.VisitStatementSyntaxList(statements);

            // should this be ref?
            var currentGetStatements = new StatementSyntax[]
            {
                LocalDeclarationStatement(
                    slotName.Identifier, ElementAccessExpression(
                        MemberAccessExpression(VarName("dict"), IdentifierName("Slots")), VarName("index"))),
                LocalDeclarationStatement(
                    groupingName.Identifier, ObjectCreationExpression(
                        GroupingType, ArgumentList(
                            MemberAccessExpression(slotName, IdentifierName("Key")),
                            MemberAccessExpression(slotName, IdentifierName("Value"))), default)),
                ResultSelectorKind == FunctionKind.Default
                    ? LocalDeclarationStatement(currentName.Identifier, groupingName)
                    : LocalDeclarationStatement(currentName.Identifier,
                        InvocationExpression(VarName("resultSelector"), ArgumentList(groupingName)))
            };

            statements = statements.InsertRange(0, currentGetStatements);

            var result = WhileStatement(LessThanExpression(
                    CastExpression(UIntType, PreIncrementExpression(VarName("index"))),
                    CastExpression(UIntType, MemberAccessExpression(VarName("dict"), CountProperty))),
                Block(statements));

            return Block(result);
        }
    }
}