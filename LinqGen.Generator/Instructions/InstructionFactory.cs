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
                            return new ArrayGeneration(expression, id, arraySymbol);
                        // return new SpecializeArrayMultiGeneration(expression, id, arraySymbol);
                    }
                    else if (typeArgument is INamedTypeSymbol namedTypeSymbol)
                    {
                        if (TryGetGenericListInterface(namedTypeSymbol, out var listSymbol))
                            return new ListGeneration(expression, id, namedTypeSymbol, listSymbol);
                    }

                    // if (typeArgument is INamedTypeSymbol namedTypeSymbol)
                    //     return new SpecializeGeneration(expression, id, namedTypeSymbol);

                    break;
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
                //
                // case "AsEnumerable":
                //     return new AsEnumerableOperation(expression, id);
                //
                // case "Cast":
                //     return new CastOperation(expression, id, false);
                //
                // case "OfType":
                //     return new CastOperation(expression, id, true);
                //
                case "Skip":
                    return new SkipOperation(expression, id);

                case "Take":
                    return new TakeOperation(expression, id);

                // case "Distinct":
                //     return new DistinctOperation(expression, id, false);
                //
                // case "DistinctStruct":
                //     return new DistinctOperation(expression, id, true);
                //
                // case "OrderBy":
                //     if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                //         break;
                //     return new OrderByOperation(expression, id, typeSymbol, false);
                //
                // case "OrderByStruct":
                //     if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                //         break;
                //     return new OrderByOperation(expression, id, typeSymbol, true);
                //
                // case "OrderBySelf":
                //     return new OrderByOperation(expression, id, null, false);
                //
                // case "OrderBySelfStruct":
                //     return new OrderByOperation(expression, id, null, true);
                //
                // case "ThenBy":
                //     if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                //         break;
                //     return new ThenByOperation(expression, id, typeSymbol, false);
                //
                // case "ThenByStruct":
                //     if (!expression.TryGetNamedParameterType(0, out typeSymbol))
                //         break;
                //     return new ThenByOperation(expression, id, typeSymbol, true);
                //
                // case "ThenBySelf":
                //     return new ThenByOperation(expression, id, null, false);
                //
                // case "ThenBySelfStruct":
                //     return new ThenByOperation(expression, id, null, true);
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

                // case "First":
                //     return new FirstEvaluation(expression, false);
                //
                // case "FirstOrDefault":
                //     return new FirstEvaluation(expression, true);
                //
                // case "Last":
                //     return new LastEvaluation(expression, false);
                //
                // case "LastOrDefault":
                //     return new LastEvaluation(expression, true);
                //
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