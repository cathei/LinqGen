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
                switch (expression.SignatureSymbol.MetadataName)
                {
                    case "Gen`1":
                        return new GenGeneration(expression);

                    case "GenList`1":
                        return new GenListGeneration(expression);

                    case "Select`2":
                    case "SelectStruct`2":
                    case "SelectAt`2":
                    case "SelectAtStruct`2":
                        if (!expression.TryGetArgumentType(0, out argumentType))
                            break;
                        return new SelectOperation(expression, argumentType);

                    case "Where`1":
                    case "WhereAt`1":
                    case "WhereStruct`1":
                    case "WhereAtStruct`1":
                        if (!expression.TryGetArgumentType(0, out argumentType))
                            break;
                        return new WhereOperation(expression, argumentType);
                }
            }
            else
            {
                // switch (expression.UpstreamSymbol.met)
                // {
                //
                // }
            }

            // not yet implemented
            return null;
        }
    }
}