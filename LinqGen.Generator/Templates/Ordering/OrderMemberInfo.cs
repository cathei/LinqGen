// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public readonly struct OrderMemberInfo
    {
        public readonly TypeSyntax? SelectorType;
        public readonly TypeSyntax ComparerType;
        public readonly TypeSyntax KeyType;
        public readonly int Id;
        public readonly bool Desc;

        public OrderMemberInfo(TypeSyntax? selectorType, TypeSyntax comparerType, TypeSyntax keyType, int id, bool desc)
        {
            SelectorType = selectorType;
            ComparerType = comparerType;
            KeyType = keyType;
            Id = id;
            Desc = desc;
        }

        public IdentifierNameSyntax SelectorName => IdentifierName($"selector_{Id}");
        public IdentifierNameSyntax ComparerName => IdentifierName($"comparer_{Id}");
        public IdentifierNameSyntax KeysName => IdentifierName($"keys_{Id}");
    }
}