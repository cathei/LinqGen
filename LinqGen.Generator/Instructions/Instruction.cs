// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Immutable;
using System.Linq;

namespace Cathei.LinqGen.Generator;

/// <summary>
/// Instruction is all kind of method that can performed on LinqGen enumerable.
/// Generation is Value => LinqGen,
/// Operation is LinqGen => LinqGen,
/// Evaluation is LinqGen => Value.
/// </summary>
public abstract class Instruction
{
    public ImmutableArray<INamedTypeSymbol> UpstreamSignatureSymbols { get; }
    public string Id { get; }

    protected Instruction(in LinqGenExpression expression, uint id)
    {
        UpstreamSignatureSymbols = expression.UpstreamSignatureSymbols;
        Id = Base62.Encode(id);
    }

    /// <summary>
    /// Input element type that can substitute upstream output element type.
    /// </summary>
    protected virtual TypeSyntax? InputElementType => null;

    /// <summary>
    /// Defines which kind of method should instruction exposed as.
    /// </summary>
    public abstract MethodKind MethodKind { get; }

    public Generation? Upstream => Upstreams?[0];

    public List<Generation>? Upstreams { get; protected set; }

    private NameSyntax? _upstreamResolvedClassName;

    /// <summary>
    /// Upstream resolved name after type replacement.
    /// </summary>
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

            _upstreamResolvedClassName = MakeGenericName(Upstream.ClassName, GetUpstreamTypeArguments());
            return _upstreamResolvedClassName;
        }
    }

    /// <summary>
    /// Creating unique type parameter name for this instruction
    /// </summary>
    protected IdentifierNameSyntax TypeName(string identifier)
    {
        return IdentifierName($"T{identifier}_{Id}");
    }

    /// <summary>
    /// Creating unique local variable name for this instruction
    /// </summary>
    protected IdentifierNameSyntax LocalName(string identifier)
    {
        return IdentifierName($"{identifier}_{Id}");
    }

    /// <summary>
    /// Creating unique variable name for this instruction
    /// </summary>
    protected ExpressionSyntax Member(string identifier)
    {
        return MemberAccessExpression(ThisPlaceholder, LocalName(identifier));
    }

    /// <summary>
    /// Creating unique variable name for this instruction
    /// </summary>
    protected IdentifierNameSyntax Iterator(string identifier)
    {
        return IdentifierName($"{IterPlaceholder}{identifier}_{Id}");
    }

    /// <summary>
    /// Context variable, affected by upstream
    /// </summary>
    public static readonly IdentifierNameSyntax CurrentPlaceholder = IdentifierName("_current_");

    public static readonly IdentifierNameSyntax ThisPlaceholder = IdentifierName("_this_");

    public const string IterPlaceholder = "_iter_";

    protected virtual IEnumerable<TypeParameterInfo> GetTypeParameterInfos() => Array.Empty<TypeParameterInfo>();

    private List<TypeParameterInfo>? _typeParameters;

    /// <summary>
    /// Note the current instruction's parameters come first
    /// </summary>
    public IReadOnlyList<TypeParameterInfo> TypeParameters
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
                .Where(x => !x.Name.IsEquivalentTo(Upstream.OutputElementType)));
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
            if (InputElementType != null && x.Name.IsEquivalentTo(Upstream!.OutputElementType))
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