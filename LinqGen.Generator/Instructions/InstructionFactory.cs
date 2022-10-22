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
            ITypeSymbol? argumentType;

            switch (expression.SignatureSymbol!.Name)
            {
                case "Gen":
                    return new GenGeneration(expression);

                case "GenList":
                    return new GenListGeneration(expression);

                case "Select":
                case "SelectStruct":
                case "SelectAt":
                case "SelectAtStruct":
                    if (!expression.TryGetArgumentType(0, out argumentType))
                        break;
                    return new SelectOperation(expression, argumentType);

                case "Where":
                case "WhereAt":
                case "WhereStruct":
                case "WhereAtStruct":
                    if (!expression.TryGetArgumentType(0, out argumentType))
                        break;
                    return new WhereOperation(expression, argumentType);

                case "AsEnumerable":
                    return new AsEnumerableOperation(expression);
            }

            // not yet implemented
            return null;
        }

        public static Evaluation? CreateEvaluation(StringBuilder logBuilder, in LinqGenExpression expression)
        {
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
            }

            return null;
        }

    }
}