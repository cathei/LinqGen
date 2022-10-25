// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Cathei.LinqGen.Hidden;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    public static class InstructionFactory
    {
        /// <summary>
        /// The Instruction instance must be unique per signature (per generic arguments combination).
        /// </summary>
        public static Generation? CreateGeneration(StringBuilder logBuilder, in LinqGenExpression expression)
        {
            INamedTypeSymbol? typeSymbol;

            switch (expression.SignatureSymbol!.Name)
            {
                case "Specialize":
                    return new SpecializeGeneration(expression);

                case "Select":
                    if (!expression.TryGetParameterType(0, out typeSymbol))
                        break;
                    return new SelectOperation(expression, typeSymbol, false, false);

                case "SelectStruct":
                    if (!expression.TryGetParameterType(0, out typeSymbol))
                        break;
                    return new SelectOperation(expression, typeSymbol, false, true);

                case "SelectAt":
                    if (!expression.TryGetParameterType(0, out typeSymbol))
                        break;
                    return new SelectOperation(expression, typeSymbol, true, false);

                case "SelectAtStruct":
                    if (!expression.TryGetParameterType(0, out typeSymbol))
                        break;
                    return new SelectOperation(expression, typeSymbol, true, true);

                case "Where":
                    if (!expression.TryGetParameterType(0, out typeSymbol))
                        break;
                    return new WhereOperation(expression, typeSymbol, false, false);

                case "WhereAt":
                    if (!expression.TryGetParameterType(0, out typeSymbol))
                        break;
                    return new WhereOperation(expression, typeSymbol, true, false);

                case "WhereStruct":
                    if (!expression.TryGetParameterType(0, out typeSymbol))
                        break;
                    return new WhereOperation(expression, typeSymbol, false, true);

                case "WhereAtStruct":
                    if (!expression.TryGetParameterType(0, out typeSymbol))
                        break;
                    return new WhereOperation(expression, typeSymbol, true, true);

                case "AsEnumerable":
                    return new AsEnumerableOperation(expression);

                case "Cast":
                    return new CastOperation(expression, false);

                case "OfType":
                    return new CastOperation(expression, true);
            }

            // not yet implemented
            return null;
        }

        public static Evaluation? CreateEvaluation(StringBuilder logBuilder, in LinqGenExpression expression)
        {
            INamedTypeSymbol? typeSymbol;

            switch (expression.MethodSymbol.Name)
            {
                case "First":
                    return new FirstEvaluation(expression, false);

                case "FirstOrDefault":
                    return new FirstEvaluation(expression, true);

                case "Last":
                case "LastOrDefault":
                    break;

                case "Single":
                    break;

                case "Sum":
                    if (!expression.TryGetParameterType(0, out typeSymbol))
                        break;
                    return new SumEvaluation(expression, typeSymbol);
            }

            return null;
        }

    }
}