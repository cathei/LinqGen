// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static CodeGenUtils;

    public static class InstructionFactory
    {
        /// <summary>
        /// The Instruction instance must be unique per signature (per generic arguments combination).
        /// </summary>
        public static Generation? CreateGeneration(StringBuilder logBuilder, in LinqGenExpression expression, int id)
        {
            INamedTypeSymbol? typeSymbol;

            switch (expression.SignatureSymbol!.Name)
            {
                case "Specialize":
                {
                    ITypeSymbol typeArgument = expression.SignatureSymbol!.TypeArguments[0];

                    if (typeArgument is IArrayTypeSymbol arraySymbol)
                    {
                        if (arraySymbol.Rank == 1)
                        {
                            return new ArrayOrListGeneration(expression, id,
                                arraySymbol, arraySymbol.ElementType, LengthProperty, false);
                        }

                        // return new SpecializeArrayMultiGeneration(expression, id, arraySymbol);
                        return new EnumerableGeneration(expression, id, arraySymbol);
                    }

                    if (typeArgument is INamedTypeSymbol namedTypeSymbol)
                    {
                        if (TryGetGenericListInterface(namedTypeSymbol, out var listSymbol))
                        {
                            return new ArrayOrListGeneration(expression, id,
                                namedTypeSymbol, listSymbol.TypeArguments[0], CountProperty, false);
                        }

                        if (IsUnityNativeArrayOrSlice(namedTypeSymbol))
                        {
                            return new ArrayOrListGeneration(expression, id,
                                namedTypeSymbol, namedTypeSymbol.TypeArguments[0], LengthProperty, false);
                        }

                        return new EnumerableGeneration(expression, id, namedTypeSymbol);
                    }

                    break;
                }

                case "SpecializeList":
                {
                    if (expression.MethodSymbol.ReceiverType is not INamedTypeSymbol listSymbol)
                        break;

                    return new ArrayOrListGeneration(expression, id,
                        listSymbol, listSymbol.TypeArguments[0], CountProperty, true);
                }

                case "SpecializeStruct":
                {
                    if (expression.MethodSymbol.ReceiverType is not INamedTypeSymbol sourceSymbol)
                        break;

                    return new StructEnumerableGeneration(expression, id, sourceSymbol);
                }

                case "Range":
                    return new RangeGeneration(expression, id);

                case "Repeat":
                    return new RepeatGeneration(expression, id);

                case "Empty":
                    return new EmptyGeneration(expression, id);

                case "Select":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new SelectOperation(expression, id, typeSymbol, false, false);

                case "SelectStruct":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new SelectOperation(expression, id, typeSymbol, false, true);

                case "SelectAt":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new SelectOperation(expression, id, typeSymbol, true, false);

                case "SelectAtStruct":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new SelectOperation(expression, id, typeSymbol, true, true);

                case "Where":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new WhereOperation(expression, id, typeSymbol, false, false);

                case "WhereAt":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new WhereOperation(expression, id, typeSymbol, true, false);

                case "WhereStruct":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new WhereOperation(expression, id, typeSymbol, false, true);

                case "WhereAtStruct":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new WhereOperation(expression, id, typeSymbol, true, true);

                case "Cast":
                    return new CastOperation(expression, id, false);

                case "OfType":
                    return new CastOperation(expression, id, true);

                case "Skip":
                    return new SkipOperation(expression, id);

                case "Take":
                    return new TakeOperation(expression, id);

                case "Distinct":
                    return new DistinctOperation(expression, id, ComparerKind.Default);

                case "DistinctComparer":
                    return new DistinctOperation(expression, id, ComparerKind.Interface);

                case "DistinctStruct":
                    return new DistinctOperation(expression, id, ComparerKind.Struct);

                case "OrderBy":
                    return new OrderByOperation(expression, id, FunctionKind.Delegate, ComparerKind.Default, false);

                case "OrderByKey":
                    return new OrderByOperation(expression, id, FunctionKind.Struct, ComparerKind.Default, false);

                case "OrderByComparer":
                    return new OrderByOperation(expression, id, FunctionKind.Delegate, ComparerKind.Interface, false);

                case "OrderByStruct":
                    return new OrderByOperation(expression, id, FunctionKind.Struct, ComparerKind.Struct, false);

                case "OrderBySelf":
                    return new OrderByOperation(expression, id, FunctionKind.Default, ComparerKind.Default, false);

                case "OrderBySelfComparer":
                    return new OrderByOperation(expression, id, FunctionKind.Default, ComparerKind.Interface, false);

                case "OrderBySelfStruct":
                    return new OrderByOperation(expression, id, FunctionKind.Default, ComparerKind.Struct, false);

                case "ThenBy":
                    return new ThenByOperation(expression, id, FunctionKind.Delegate, ComparerKind.Default, false);

                case "ThenByKey":
                    return new ThenByOperation(expression, id, FunctionKind.Struct, ComparerKind.Default, false);

                case "ThenByComparer":
                    return new ThenByOperation(expression, id, FunctionKind.Delegate, ComparerKind.Interface, false);

                case "ThenByStruct":
                    return new ThenByOperation(expression, id, FunctionKind.Struct, ComparerKind.Struct, false);

                case "ThenBySelf":
                    return new ThenByOperation(expression, id, FunctionKind.Default, ComparerKind.Default, false);

                case "ThenBySelfComparer":
                    return new ThenByOperation(expression, id, FunctionKind.Default, ComparerKind.Interface, false);

                case "ThenBySelfStruct":
                    return new ThenByOperation(expression, id, FunctionKind.Default, ComparerKind.Struct, false);

                case "OrderByDesc":
                    return new OrderByOperation(expression, id, FunctionKind.Delegate, ComparerKind.Default, true);

                case "OrderByDescKey":
                    return new OrderByOperation(expression, id, FunctionKind.Struct, ComparerKind.Default, true);

                case "OrderByDescComparer":
                    return new OrderByOperation(expression, id, FunctionKind.Delegate, ComparerKind.Interface, true);

                case "OrderByDescStruct":
                    return new OrderByOperation(expression, id, FunctionKind.Struct, ComparerKind.Struct, true);

                case "OrderByDescSelf":
                    return new OrderByOperation(expression, id, FunctionKind.Default, ComparerKind.Default, true);

                case "OrderByDescSelfComparer":
                    return new OrderByOperation(expression, id, FunctionKind.Default, ComparerKind.Interface, true);

                case "OrderByDescSelfStruct":
                    return new OrderByOperation(expression, id, FunctionKind.Default, ComparerKind.Struct, true);

                case "ThenByDesc":
                    return new ThenByOperation(expression, id, FunctionKind.Delegate, ComparerKind.Default, true);

                case "ThenByDescKey":
                    return new ThenByOperation(expression, id, FunctionKind.Struct, ComparerKind.Default, true);

                case "ThenByDescComparer":
                    return new ThenByOperation(expression, id, FunctionKind.Delegate, ComparerKind.Interface, true);

                case "ThenByDescStruct":
                    return new ThenByOperation(expression, id, FunctionKind.Struct, ComparerKind.Struct, true);

                case "ThenByDescSelf":
                    return new ThenByOperation(expression, id, FunctionKind.Default, ComparerKind.Default, true);

                case "ThenByDescSelfComparer":
                    return new ThenByOperation(expression, id, FunctionKind.Default, ComparerKind.Interface, true);

                case "ThenByDescSelfStruct":
                    return new ThenByOperation(expression, id, FunctionKind.Default, ComparerKind.Struct, true);
            }

            // not yet implemented
            return null;
        }

        public static Evaluation? CreateEvaluation(StringBuilder logBuilder, in LinqGenExpression expression, int id)
        {
            switch (expression.MethodSymbol.Name)
            {
                case "GetEnumerator":
                    return new GetEnumeratorEvaluation(expression, id);

                case "AsEnumerable":
                    return new AsEnumerableEvaluation(expression, id);

                case "First":
                    return new FirstEvaluation(expression, id, false);

                case "FirstOrDefault":
                    return new FirstEvaluation(expression, id, true);

                case "Last":
                    return new LastEvaluation(expression, id, false);

                case "LastOrDefault":
                    return new LastEvaluation(expression, id, true);

                // case "Single":
                //     break;
                //
                case "Sum":
                    return new SumEvaluation(expression, id);

                case "Min":
                    return new MinMaxEvaluation(expression, id, true);

                case "Max":
                    return new MinMaxEvaluation(expression, id, false);

                case "Count":
                    return new CountEvaluation(expression, id);

                case "ToArray":
                    return new ToArrayEvaluation(expression, id);

                case "ToList":
                    return new ToListEvaluation(expression, id);
            }

            return null;
        }

    }
}