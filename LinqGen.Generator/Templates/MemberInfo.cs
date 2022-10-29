// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public readonly struct MemberInfo
    {
        public readonly MemberKind Kind;
        public readonly TypeSyntax Type;
        public readonly IdentifierNameSyntax Name;

        public MemberInfo(MemberKind kind, TypeSyntax type, IdentifierNameSyntax name)
        {
            Kind = kind;
            Type = type;
            Name = name;
        }

        public ParameterSyntax AsParameter()
        {
            return Parameter(Type, Name.Identifier);
        }

        public ArgumentSyntax AsArgument()
        {
            return Argument(Name);
        }
    }
}