// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    /// <summary>
    /// Instruction is all kind of method that can performed on LinqGen enumerable
    /// Generation is Value => Enumerable
    /// Operation is Enumerable => Enumerable
    /// Evaluation is Enumerable => Non-Enumerable
    /// </summary>
    public abstract class Instruction
    {
        protected Instruction(in LinqGenExpression expression) : this(expression.UpstreamSymbol) { }

        protected Instruction(INamedTypeSymbol? upstreamSymbol)
        {
            UpstreamSymbol = upstreamSymbol;
        }

        public INamedTypeSymbol? UpstreamSymbol { get; }

        public Generation? Upstream { get; protected set; }

        /// <summary>
        /// The qualified class name cached for child class rendering
        /// </summary>
        protected NameSyntax? ClassName { get; set; }

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

                int arityDiff = Arity - Upstream.Arity;
                var upstreamArguments = Upstream.GetTypeArguments(arityDiff)!;

                _upstreamResolvedClassName = Upstream.ClassName switch
                {
                    QualifiedNameSyntax qualifiedName =>
                        qualifiedName.WithRight(GenericName(qualifiedName.Right.Identifier, upstreamArguments)),
                    GenericNameSyntax genericName => genericName.WithTypeArgumentList(upstreamArguments),
                    SimpleNameSyntax simpleName => GenericName(simpleName.Identifier, upstreamArguments),
                    _ => Upstream.ClassName
                };

                return _upstreamResolvedClassName;
            }
        }

        protected virtual IEnumerable<TypeParameterInfo> GetTypeParameterInfos() => Array.Empty<TypeParameterInfo>();

        private List<TypeParameterInfo>? _typeParameters;

        /// <summary>
        /// Note that parameter order
        /// </summary>
        private List<TypeParameterInfo> TypeParameters
        {
            get
            {
                if (_typeParameters != null)
                    return _typeParameters;

                var inst = this;
                var result = new List<TypeParameterInfo>();

                while (inst != null)
                {
                    result.AddRange(inst.GetTypeParameterInfos());
                    inst = inst.Upstream;
                }

                return _typeParameters = result;
            }
        }

        public int Arity => TypeParameters.Count;

        public TypeParameterListSyntax? GetTypeParameters(int indexStart = 0)
        {
            if (Arity == 0)
                return null;

            return TypeParameterList(SeparatedList(TypeParameters
                .Select((x, i) => x.AsTypeParameter(i + indexStart + 1))));
        }

        public TypeArgumentListSyntax? GetTypeArguments(int indexStart = 0)
        {
            if (Arity == 0)
                return null;

            return TypeArgumentList(SeparatedList(TypeParameters
                .Select((x, i) => x.AsTypeArgument(i + indexStart + 1))));
        }

        public SyntaxList<TypeParameterConstraintClauseSyntax> GetGenericConstraints(int indexStart = 0)
        {
            if (Arity == 0)
                return default;

            return new SyntaxList<TypeParameterConstraintClauseSyntax>(
                TypeParameters.Select((x, i) => x.AsGenericConstraint(i + indexStart + 1)!)
                    .Where(x => x != null));
        }
    }
}