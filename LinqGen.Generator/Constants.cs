// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    public static class Constants
    {
        public static class Op
        {
            public const string Gen = "Gen`1";
            public const string GenList = "GenList`1";

            public const string Select = "Select`2";
            public const string Where = "GenList`1";

            public static readonly IReadOnlyDictionary<string, string> ShortName = new Dictionary<string, string>
            {
                { Gen, "Ge" },
                { GenList, "Gl" },
                { Select, "Se" },
                { Where, "Wh" },
            };
        }
    }
}