// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public readonly struct MemberInfo
{
    public readonly MemberKind Kind;
    public readonly TypeSyntax Type;
    public readonly IdentifierNameSyntax Name;
    public readonly ExpressionSyntax? DefaultValue;

    public MemberInfo(MemberKind kind,
        TypeSyntax type, IdentifierNameSyntax name, ExpressionSyntax? defaultValue = null)
    {
        Kind = kind;
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