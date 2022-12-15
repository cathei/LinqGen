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
                                arraySymbol, arraySymbol.ElementType, LengthProperty);
                        }

                        // return new SpecializeArrayMultiGeneration(expression, id, arraySymbol);
                        return new EnumerableGeneration(expression, id, arraySymbol);
                    }

                    if (typeArgument is INamedTypeSymbol namedTypeSymbol)
                    {
                        if (TryGetGenericListInterface(namedTypeSymbol, out var listSymbol))
                        {
                            return new ArrayOrListGeneration(expression, id,
                                namedTypeSymbol, listSymbol.TypeArguments[0], CountProperty);
                        }

                        if (IsUnityNativeArrayOrSlice(namedTypeSymbol))
                        {
                            return new ArrayOrListGeneration(expression, id,
                                namedTypeSymbol, namedTypeSymbol.TypeArguments[0], LengthProperty);
                        }

                        return new EnumerableGeneration(expression, id, namedTypeSymbol);
                    }

                    break;
                }

                // case "SpecializeStruct":
                //     break;

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
                    return new DistinctOperation(expression, id, false);

                case "DistinctComparer":
                    return new DistinctOperation(expression, id, true);

                case "OrderBy":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new OrderByOperation(expression, id, typeSymbol, false, false);

                case "OrderByStruct":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new OrderByOperation(expression, id, typeSymbol, true, false);

                case "OrderBySelf":
                    return new OrderByOperation(expression, id, null, false, false);

                case "OrderBySelfStruct":
                    return new OrderByOperation(expression, id, null, true, false);

                case "ThenBy":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new ThenByOperation(expression, id, typeSymbol, false, false);

                case "ThenByStruct":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new ThenByOperation(expression, id, typeSymbol, true, false);

                case "ThenBySelf":
                    return new ThenByOperation(expression, id, null, false, false);

                case "ThenBySelfStruct":
                    return new ThenByOperation(expression, id, null, true, false);

                case "OrderByDesc":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new OrderByOperation(expression, id, typeSymbol, false, true);

                case "OrderByDescStruct":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new OrderByOperation(expression, id, typeSymbol, true, true);

                case "OrderByDescSelf":
                    return new OrderByOperation(expression, id, null, false, true);

                case "OrderByDescSelfStruct":
                    return new OrderByOperation(expression, id, null, true, true);

                case "ThenByDesc":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new ThenByOperation(expression, id, typeSymbol, false, true);

                case "ThenByDescStruct":
                    if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                        break;
                    return new ThenByOperation(expression, id, typeSymbol, true, true);

                case "ThenByDescSelf":
                    return new ThenByOperation(expression, id, null, false, true);

                case "ThenByDescSelfStruct":
                    return new ThenByOperation(expression, id, null, true, true);
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