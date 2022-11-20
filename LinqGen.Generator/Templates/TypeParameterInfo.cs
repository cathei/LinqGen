// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    public readonly struct TypeParameterInfo
    {
        public readonly IdentifierNameSyntax Name;
        // public readonly TypeSyntax? ConstraintType;
        public readonly TypeParameterConstraintClauseSyntax? GenericConstraint;

        public TypeParameterInfo(IdentifierNameSyntax name)
        {
            Name = name;
            GenericConstraint = null;
        }

        public TypeParameterInfo(IdentifierNameSyntax name, TypeSyntax constraintType)
        {
            Name = name;
            GenericConstraint = TypeParameterConstraintClause(name,
                SingletonSeparatedList<TypeParameterConstraintSyntax>(TypeConstraint(constraintType)));
        }

        public TypeParameterInfo(IdentifierNameSyntax name, params TypeParameterConstraintSyntax[] constraints)
        {
            Name = name;
            GenericConstraint = TypeParameterConstraintClause(name, SeparatedList(constraints));
        }

        public TypeParameterSyntax AsTypeParameter()
        {
            return TypeParameter(Name.Identifier);
        }

        public TypeSyntax AsTypeArgument()
        {
            return Name;
        }

        public TypeParameterConstraintClauseSyntax? AsGenericConstraint()
        {
            return GenericConstraint;
        }

    }
}