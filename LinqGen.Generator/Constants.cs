// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cathei.LinqGen.Operations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    public static class Constants
    {
        public static class Op
        {
            public static readonly IReadOnlyDictionary<Type, string> ShortName = new Dictionary<Type, string>
            {
                { typeof(Gen<>), "Ge" },
                { typeof(GenList<>), "Gl" },
                { typeof(Select<,>), "Se" },
                { typeof(Where<>), "Wh" },
            };
        }
    }
}