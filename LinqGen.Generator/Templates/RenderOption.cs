// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    public struct RenderOption
    {
        /// <summary>
        /// Is it rendering for local enumeration?
        /// This means we don't have to worry about versioning, When we enumerating for IList.
        /// As well as we can use foreach enumeration for Array (faster).
        /// </summary>
        public bool IsLocal;

        /// <summary>
        /// Skip variable, can be overriden if Upstream supports partitioning.
        /// </summary>
        public IdentifierNameSyntax? SkipVar;

        /// <summary>
        /// Take variable, can be overriden if Upstream supports partitioning.
        /// </summary>
        public IdentifierNameSyntax? TakeVar;

        public RenderOption(bool isLocal) : this()
        {
            IsLocal = isLocal;
        }

        public RenderOption WithSkip(IdentifierNameSyntax varName)
        {
            var copy = this;
            copy.SkipVar = varName;
            return copy;
        }

        public RenderOption WithTake(IdentifierNameSyntax varName)
        {
            var copy = this;
            copy.TakeVar = varName;
            return copy;
        }
    }
}