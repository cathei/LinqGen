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
        public static Instruction? Create(StringBuilder logBuilder, in LinqGenExpression expression)
        {
            ITypeSymbol? argumentType;

            if (expression.SignatureSymbol != null)
            {
                switch (expression.SignatureSymbol.Name)
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
                }
            }
            else
            {
                switch (expression.MethodSymbol.Name)
                {
                    case "AsEnumerable":
                        break;

                    case "First":
                    case "FirstOrDefault":
                        break;

                    case "Last":
                    case "LastOrDefault":
                        break;

                    case "Single":
                        break;
                }
            }

            // not yet implemented
            return null;
        }
    }
}