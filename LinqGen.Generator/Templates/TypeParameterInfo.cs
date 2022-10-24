// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    public readonly struct TypeParameterInfo
    {
        public readonly IdentifierNameSyntax Identifier;
        public readonly TypeSyntax? ConstraintType;

        public TypeParameterInfo(IdentifierNameSyntax identifier, TypeSyntax? constraintType)
        {
            Identifier = identifier;
            ConstraintType = constraintType;
        }

        public TypeParameterSyntax AsTypeParameter()
        {
            return TypeParameter(Identifier.Identifier);
        }

        public TypeSyntax AsTypeArgument()
        {
            return Identifier;
        }

        public TypeParameterConstraintClauseSyntax? AsGenericConstraint()
        {
            if (ConstraintType == null)
                return null;

            return TypeParameterConstraintClause(Identifier,
                SingletonSeparatedList((TypeParameterConstraintSyntax)TypeConstraint(ConstraintType)));
        }

    }
}