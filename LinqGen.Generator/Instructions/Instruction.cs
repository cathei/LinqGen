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

        // protected Instruction(INamedTypeSymbol? upstreamSignatureSymbol)
        // {
        //     UpstreamSignatureSymbol = upstreamSignatureSymbol;
        // }

        public virtual string TypeParameterPrefix { get; } = "T";

        public INamedTypeSymbol? UpstreamSignatureSymbol { get; }

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
                    Upstream.ClassName, GetTypeArguments(true)!);

                return _upstreamResolvedClassName;
            }
        }

        // public virtual bool SupportGenericElementOutput => Upstream?.SupportGenericElementOutput ?? true;
        //
        // /// <summary>
        // /// Is input element type will be same as output element type?
        // /// </summary>
        // public virtual bool PreserveElementType => true;
        //
        // private static readonly IdentifierNameSyntax GenericOutputElementName = IdentifierName("TElement");
        //
        // /// <summary>
        // /// If generic output is not supported, this should be overriden as well
        // /// </summary>
        // public virtual TypeSyntax OutputElementType =>
        //     SupportGenericElementOutput ? GenericOutputElementName : Upstream!.OutputElementType;

        // public TypeArgumentListSyntax? CallerTypeArguments { get; }

        protected virtual IEnumerable<TypeParameterInfo> GetTypeParameterInfos() => Array.Empty<TypeParameterInfo>();

        private List<TypeParameterInfo>? _typeParameters;
        private IReadOnlyList<TypeParameterInfo>? _upstreamTypeParameters;

        /// <summary>
        /// Note the current instruction's parameters come first
        /// </summary>
        private IReadOnlyList<TypeParameterInfo> TypeParameters
        {
            get
            {
                EnsureLoadTypeParameters();
                return _typeParameters!;
            }
        }

        public int Arity => TypeParameters.Count;

        private void EnsureLoadTypeParameters()
        {
            if (_typeParameters != null)
                return;

            _typeParameters = new List<TypeParameterInfo>();
            _upstreamTypeParameters = new List<TypeParameterInfo>();

            // if (SupportGenericElementOutput)
            //     _typeParameters.Add(new TypeParameterInfo(GenericOutputElementName, null));

            _typeParameters.AddRange(GetTypeParameterInfos());

            if (Upstream == null)
                return;

            _upstreamTypeParameters = Upstream.TypeParameters;
            _typeParameters.AddRange(Upstream.TypeParameters);

            // var upstreamOriginalParameters = Upstream.TypeParameters;

            // int index = 0;

            // if (Upstream.SupportGenericElementOutput && PreserveElementType)
            // {
            //     _upstreamTypeParameters.Add(new TypeParameterInfo(GenericOutputElementName, null));
            //     index = 1;
            // }

            // // NOTE: Always carry over upstream type arguments
            // while (index < upstreamOriginalParameters.Count)
            // {
            //     // normalize the name
            //     var info = new TypeParameterInfo(IdentifierName($"TUp{index + 1}"),
            //         upstreamOriginalParameters[index].ConstraintType);
            //
            //     _typeParameters.Add(info);
            //     _upstreamTypeParameters.Add(info);
            //
            //     index++;
            // }
        }

        public TypeParameterListSyntax? GetTypeParameters(int? take = null)
        {
            EnsureLoadTypeParameters();

            var parameters = TypeParameters;

            take ??= parameters.Count;

            if (take == 0)
                return null;

            return TypeParameterList(SeparatedList(parameters
                .Select((x) => x.AsTypeParameter())
                .Take(take.Value)));
        }

        public TypeArgumentListSyntax? GetTypeArguments(bool upstream = false)
        {
            EnsureLoadTypeParameters();

            var parameters = upstream ? _upstreamTypeParameters : _typeParameters;

            if (parameters!.Count == 0)
                return null;

            return TypeArgumentList(SeparatedList(parameters
                .Select((x) => x.AsTypeArgument())));
        }

        public SyntaxList<TypeParameterConstraintClauseSyntax> GetGenericConstraints(int? take = null)
        {
            // var parameters = upstream ? _upstreamTypeParameters : _typeParameters;
            var parameters = TypeParameters;

            take ??= parameters.Count;

            return new SyntaxList<TypeParameterConstraintClauseSyntax>(
                parameters.Select((x) => x.AsGenericConstraint()!)
                    .Take(take.Value)
                    .Where(x => x != null));
        }
    }
}