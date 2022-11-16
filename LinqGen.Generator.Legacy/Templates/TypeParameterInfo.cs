// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    public readonly struct TypeParameterInfo
    {
        public readonly IdentifierNameSyntax Identifier;
        // public readonly TypeSyntax? ConstraintType;
        public readonly TypeParameterConstraintClauseSyntax? GenericConstraint;

        public TypeParameterInfo(IdentifierNameSyntax identifier)
        {
            Identifier = identifier;
            GenericConstraint = null;
        }

        public TypeParameterInfo(IdentifierNameSyntax identifier, TypeSyntax constraintType)
        {
            Identifier = identifier;
            GenericConstraint = TypeParameterConstraintClause(identifier,
                SingletonSeparatedList<TypeParameterConstraintSyntax>(TypeConstraint(constraintType)));
        }

        public TypeParameterInfo(IdentifierNameSyntax identifier, params TypeParameterConstraintSyntax[] constraints)
        {
            Identifier = identifier;
            GenericConstraint = TypeParameterConstraintClause(identifier, SeparatedList(constraints));
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
            return GenericConstraint;
        }

    }
}