// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    public readonly struct RenderOption
    {
        /// <summary>
        /// Is it rendering for local enumeration?
        /// This means we don't have to worry about versioning
        /// When we enumerating for IList
        /// </summary>
        public readonly bool IsLocal;

        public RenderOption(bool isLocal)
        {
            IsLocal = isLocal;
        }
    }
}