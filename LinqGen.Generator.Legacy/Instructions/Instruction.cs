// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    /// <summary>
    /// Instruction is all kind of method that can performed on LinqGen enumerable.
    /// Generation is Value => Enumerable,
    /// Operation is Enumerable => Enumerable,
    /// Evaluation is Enumerable => Non-Enumerable.
    /// </summary>
    public abstract class Instruction
    {
        protected Instruction(in LinqGenExpression expression)
        {
            UpstreamSignatureSymbol = expression.UpstreamSignatureSymbol;
        }

        protected Instruction(bool generated)
        {
            UpstreamSignatureSymbol = null;
        }

        public virtual string TypeParameterPrefix { get; } = "T";

        public INamedTypeSymbol? UpstreamSignatureSymbol { get; }

        /// <summary>
        /// Input element type that can substitute upstream output element type.
        /// Null means undetermined, possibly generic input type.
        /// </summary>
        public virtual TypeSyntax? InputElementType => null;

        public Generation? Upstream { get; protected set; }

        private NameSyntax? _upstreamResolvedClassName;

        public NameSyntax UpstreamResolvedClassName
        {
            get
            {
                if (_upstreamResolvedClassName != null)
                    return _upstreamResolvedClassName;

                if (Upstream?.ClassName == null)
                {
                    // should not be called
                    _upstreamResolvedClassName = IdentifierName("_Unknown_");
                    return _upstreamResolvedClassName;
                }

                if (Upstream.Arity == 0)
                {
                    _upstreamResolvedClassName = Upstream.ClassName;
                    return _upstreamResolvedClassName;
                }

                _upstreamResolvedClassName = MakeGenericName(
                    Upstream.ClassName, GetUpstreamTypeArguments()!);

                return _upstreamResolvedClassName;
            }
        }

        protected virtual IEnumerable<TypeParameterInfo> GetTypeParameterInfos() => Array.Empty<TypeParameterInfo>();

        private List<TypeParameterInfo>? _typeParameters;

        /// <summary>
        /// Note the current instruction's parameters come first
        /// </summary>
        private IReadOnlyList<TypeParameterInfo> TypeParameters
            => _typeParameters ??= CreateTypeParameters();

        public int Arity => TypeParameters.Count;

        private List<TypeParameterInfo> CreateTypeParameters()
        {
            var result = new List<TypeParameterInfo>();

            result.AddRange(GetTypeParameterInfos());

            if (Upstream == null)
                return result;

            if (InputElementType == null)
            {
                result.AddRange(Upstream.TypeParameters);
            }
            else
            {
                // generic output element type will be replace with input element type
                result.AddRange(Upstream.TypeParameters
                    .Where(x => !x.Identifier.IsEquivalentTo(Upstream.OutputElementType)));
            }

            return result;
        }

        public TypeParameterListSyntax? GetTypeParameters(int take = -1)
        {
            var parameters = TypeParameters;

            if (take < 0)
                take = parameters.Count;

            if (take <= 0)
                return null;

            return TypeParameterList(SeparatedList(parameters
                .Take(take)
                .Select(x => x.AsTypeParameter())));
        }

        public TypeArgumentListSyntax? GetTypeArguments(int take = -1)
        {
            var parameters = TypeParameters;

            if (take < 0)
                take = parameters.Count;

            if (take <= 0)
                return null;

            return TypeArgumentList(SeparatedList(parameters
                .Take(take)
                .Select(x => x.AsTypeArgument())));
        }

        public TypeArgumentListSyntax? GetUpstreamTypeArguments()
        {
            var parameters = Upstream?.TypeParameters;

            if (parameters == null || parameters.Count == 0)
                return null;

            return TypeArgumentList(SeparatedList(parameters.Select((x) =>
            {
                if (InputElementType != null && x.Identifier.IsEquivalentTo(Upstream!.OutputElementType))
                    return InputElementType;
                return x.AsTypeArgument();
            })));
        }

        public SyntaxList<TypeParameterConstraintClauseSyntax> GetGenericConstraints(int take = -1)
        {
            var parameters = TypeParameters;

            if (take < 0)
                take = parameters.Count;

            return new SyntaxList<TypeParameterConstraintClauseSyntax>(
                parameters
                    .Take(take)
                    .Select(x => x.AsGenericConstraint()!)
                    .Where(x => x != null));
        }
    }
}