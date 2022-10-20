﻿// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
    public static class GenerationListFormatter
    {
        private const string SourceTemplate = @"// DO NOT EDIT
// Generated by LinqGen.Generator

using System.Collections.Generic;

namespace Cathei.LinqGen.Hidden
{{
    public readonly struct {0}_{1}
    {{
        private IEnumerable<{2}> source;

        public IEnumerator<{2}> GetEnumerator() => source.GetEnumerator();
    }}
}}

namespace Cathei.LinqGen
{{
    using Hidden;

    public static class LinqGenExtensions
    {{
        public static {0}_{1} Generate(IList<{2}> source) => new(source);
    }}
}}
";

        public static void Format(StringBuilder builder, GenerationItem item, int id)
        {
            string shortName = Constants.Op.ShortName[Constants.Op.GenList];
            string elementFullName = item.ElementTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            builder.AppendFormat(SourceTemplate, shortName, id, elementFullName);
        }
    }
}