// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

/// <summary>
/// Evaluation takes LinqGen enumerable as input, but output is not LinqGen enumerable.
/// </summary>
public abstract class Evaluation : Instruction
{
    protected Evaluation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
        MethodSymbol = expression.MethodSymbol;
        MethodName = IdentifierName(MethodSymbol.Name);
    }

    public IMethodSymbol MethodSymbol { get; }
    public IdentifierNameSyntax MethodName { get; }

    /// <summary>
    /// Evaluations are exposed as enumerable member by default.
    /// </summary>
    public override MethodKind MethodKind => MethodKind.Enumerable;

    /// <summary>
    /// Evaluation should not rendered individually. Instead it will be rendered with upstream.
    /// </summary>
    public virtual void AddUpstream(Generation upstream)
    {
        base.Upstreams ??= new List<Generation>();
        base.Upstreams.Add(upstream);

        // only first upstream
        if (base.Upstreams.Count == 1)
            Upstream.AddEvaluation(this);
    }

    /// <summary>
    /// Upstream must be assigned for Evaluations
    /// </summary>
    public new Generation Upstream => base.Upstream!;

    /// <summary>
    /// Upstreams must be assigned for Evaluations
    /// </summary>
    public new List<Generation> Upstreams => base.Upstreams!;

    public virtual IEnumerable<MemberDeclarationSyntax> RenderUpstreamMembers()
    {
        yield break;
    }

    public virtual IEnumerable<MemberDeclarationSyntax> RenderExtensionMembers()
    {
        yield break;
    }
}