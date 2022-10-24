// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    public readonly struct TypeParameterInfo
    {
        public readonly TypeSyntax? ConstraintType;

        public TypeParameterInfo(TypeSyntax? constraintType)
        {
            ConstraintType = constraintType;
        }

        public TypeParameterSyntax AsTypeParameter(int index)
        {
            return TypeParameter(Identifier($"T{index}"));
        }

        public TypeSyntax AsTypeArgument(int index)
        {
            return IdentifierName($"T{index}");
        }

        public TypeParameterConstraintClauseSyntax? AsGenericConstraint(int index)
        {
            if (ConstraintType == null)
                return null;

            return TypeParameterConstraintClause(IdentifierName($"T{index}"),
                SingletonSeparatedList((TypeParameterConstraintSyntax)TypeConstraint(ConstraintType)));
        }

    }
}