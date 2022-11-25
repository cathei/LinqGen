// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public readonly struct ParameterInfo
    {
        public readonly TypeSyntax Type;
        public readonly IdentifierNameSyntax Name;
        public readonly ExpressionSyntax? DefaultValue;

        public ParameterInfo(TypeSyntax type, IdentifierNameSyntax name, ExpressionSyntax? defaultValue = null)
        {
            Type = type;
            Name = name;
            DefaultValue = defaultValue;
        }

        public ParameterSyntax AsParameter(bool defaultValue)
        {
            return Parameter(Type, Name.Identifier, defaultValue ? DefaultValue : null);
        }

        public ArgumentSyntax AsArgument()
        {
            return Argument(Name);
        }
    }
}