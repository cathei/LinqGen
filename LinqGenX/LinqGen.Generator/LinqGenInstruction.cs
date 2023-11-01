using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cathei.LinqGen.Generator;

/// <summary>
/// Instructions correspond to unique LinqGen hierarchy.
/// Single node can be expanded to multiple instructions.
/// Identical list of instructions produces identical source code.
/// Instructions does not contain any symbol information.
/// </summary>
public abstract class LinqGenInstruction : IEquatable<LinqGenInstruction>
{
    public readonly LinqGenInstruction? Upstream;
    public readonly ImmutableArray<TypeSyntax> Identity;

    protected LinqGenInstruction(LinqGenInstruction? upstream, in ImmutableArray<TypeSyntax> identity)
    {
        Upstream = upstream;
        Identity = identity;
    }

    public bool Equals(LinqGenInstruction? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (other.GetType() != GetType())
            return false;

        if (Identity.Length != other.Identity.Length)
            return false;

        for (int i = 0; i < Identity.Length; ++i)
        {
            if (!Identity[i].IsEquivalentTo(other.Identity[i]))
                return false;
        }

        if (Upstream != null)
            return Upstream.Equals(other.Upstream);

        return other.Upstream == null;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as LinqGenInstruction);
    }

    public override int GetHashCode()
    {
        return (int)UniqueId;
    }

    private uint? _uniqueId = null;

    public uint UniqueId
    {
        get
        {
            if (_uniqueId.HasValue)
                return _uniqueId.Value;

            int id = StableHashCode(GetType().Name);

            if (Upstream != null)
                id = HashCombine(id, Upstream.GetHashCode());

            foreach (var type in Identity)
                id = HashCombine(id, StableHashCode(type.ToFullString()));

            _uniqueId = (uint)id;
            return _uniqueId.Value;
        }
    }

    protected class MemberInfo
    {
        public delegate ExpressionSyntax DefaultValueDelegate(in IterationContext ctx);

        public readonly TypeSyntax Type;
        public readonly string Name;
        public readonly DefaultValueDelegate? DefaultValue;
        public readonly bool ShouldDispose;

        public MemberInfo(
            TypeSyntax type,
            string name,
            DefaultValueDelegate? defaultValue = null,
            bool shouldDispose = false)
        {
            Type = type;
            Name = name;
            DefaultValue = defaultValue;
            ShouldDispose = shouldDispose;
        }

        public MemberInfo(TypeSyntax type, string name, ExpressionSyntax defaultValue, bool shouldDispose = false)
            : this(type, name, (in IterationContext _) => defaultValue, shouldDispose)
        {}

        public StatementSyntax? AsMemberAssignment(LinqGenInstruction parent, in IterationContext ctx)
        {
            if (DefaultValue == null)
                return null;

            return ExpressionStatement(SimpleAssignmentExpression(
                ctx.Member($"{Name}_{parent.UniqueId}"), DefaultValue(ctx)));
        }
    }

    /// <summary>
    /// If true, invalidates upstream members and statements.
    /// </summary>
    protected bool ClearUpstream => false;

    /// <summary>
    /// Will be used as method parameters and enumerable member.
    /// </summary>
    protected abstract IEnumerable<MemberInfo> Parameters();

    /// <summary>
    /// Will be added as Enumerator member.
    /// </summary>
    protected virtual IEnumerable<MemberInfo> EnumeratorMembers(in IterationContext ctx)
        => LocalMembers(ctx);

    /// <summary>
    /// Will be added as local evaluation member.
    /// </summary>
    protected abstract IEnumerable<MemberInfo> LocalMembers(in IterationContext ctx);

    public readonly struct ScanContext
    {
        public readonly ExpressionSyntax? Context;

        public ScanContext(ExpressionSyntax? context)
        {
            Context = context;
        }

        public ExpressionSyntax Member(string value)
        {
            if (Context == null)
                return IdentifierName(value);
            return MemberAccessExpression(Context, IdentifierName(value));
        }
    }

    public readonly struct IterationContext
    {
        public readonly ScanContext Context;
        public readonly ExpressionSyntax? SkipVar;
        public readonly ExpressionSyntax? TakeVar;
        public readonly ExpressionSyntax CurrentVar;
        public readonly ExpressionSyntax? CountVar;

        public IterationContext(
            in ScanContext context,
            ExpressionSyntax? skipVar,
            ExpressionSyntax? takeVar,
            ExpressionSyntax currentVar,
            ExpressionSyntax? countVar)
        {
            Context = context;
            SkipVar = skipVar;
            TakeVar = takeVar;
            CurrentVar = currentVar;
            CountVar = countVar;
        }

        public ExpressionSyntax Member(string value) => Context.Member(value);

        public IterationContext Adapt(LinqGenInstruction current, string? context = null)
        {
            ScanContext newContext = context != null ? new ScanContext(Member(context)) : Context;

            return new IterationContext(
                newContext,
                current.GetSkip(this),
                current.GetTake(this),
                current.Upstream!.GetCurrent(Context),
                current.Upstream!.GetCount(Context));
        }
    }

    /// <summary>
    /// Apply statements after enumerator initialization.
    /// </summary>
    protected virtual IEnumerable<StatementSyntax> EnumeratorPreparation(in IterationContext ctx)
        => LocalPreparation(ctx);

    /// <summary>
    /// Apply statements after local initialization.
    /// </summary>
    protected virtual IEnumerable<StatementSyntax> LocalPreparation(in IterationContext ctx)
        => Enumerable.Empty<StatementSyntax>();

    /// <summary>
    /// Statements for enumerator MoveNext.
    /// </summary>
    protected virtual IEnumerable<StatementSyntax> EnumeratorStep(in IterationContext ctx)
        => LocalStep(ctx);

    /// <summary>
    /// Statements for local MoveNext.
    /// </summary>
    protected abstract IEnumerable<StatementSyntax> LocalStep(in IterationContext ctx);

    /// <summary>
    /// Get variable represents current value.
    /// </summary>
    public abstract ExpressionSyntax GetCurrent(in ScanContext ctx);

    /// <summary>
    /// Does this instruction support partitioning?
    /// </summary>
    public abstract bool SupportsPartition { get; }

    /// <summary>
    /// Modify skip variable, only called if partition is supported.
    /// </summary>
    protected virtual ExpressionSyntax? GetSkip(in IterationContext ctx)
    {
        return ctx.SkipVar;
    }

    /// <summary>
    /// Modify skip variable, only called if partition is supported.
    /// </summary>
    protected virtual ExpressionSyntax? GetTake(in IterationContext ctx)
    {
        return ctx.TakeVar;
    }

    /// <summary>
    /// Does this instruction support count?
    /// </summary>
    public abstract bool SupportsCount { get; }

    /// <summary>
    /// Modify count variable, only called if count is supported.
    /// </summary>
    public abstract ExpressionSyntax? GetCount(in ScanContext ctx);

    /// <summary>
    /// Recursively resolve preparation statements.
    /// </summary>
    public void GetEnumeratorInitStatements(IterationContext ctx, List<StatementSyntax> statements)
    {
        if (Upstream != null && !ClearUpstream)
        {
            Upstream.GetEnumeratorInitStatements(ctx.Adapt(this), statements);
        }

        statements.AddRange(EnumeratorInit(ctx));
        statements.AddRange(EnumeratorPreparation(ctx));
    }

    /// <summary>
    /// Recursively resolve preparation statements.
    /// </summary>
    public void GetLocalInitStatements(IterationContext ctx, List<StatementSyntax> statements)
    {
        if (Upstream != null && !ClearUpstream)
        {
            Upstream.GetLocalInitStatements(ctx.Adapt(this), statements);
        }

        statements.AddRange(LocalInit(ctx));
        statements.AddRange(LocalPreparation(ctx));
    }

    /// <summary>
    /// Recursively resolve iteration statements.
    /// </summary>
    public void GetEnumeratorStepStatements(in IterationContext ctx, List<StatementSyntax> statements)
    {
        if (Upstream != null && !ClearUpstream)
        {
            Upstream.GetEnumeratorStepStatements(ctx.Adapt(this), statements);
        }

        statements.AddRange(EnumeratorStep(ctx));
    }

    /// <summary>
    /// Recursively resolve iteration statements.
    /// </summary>
    public void GetLocalStepStatements(IterationContext ctx, List<StatementSyntax> statements)
    {
        if (Upstream != null && !ClearUpstream)
        {
            Upstream.GetLocalStepStatements(ctx.Adapt(this), statements);
        }

        statements.AddRange(LocalStep(ctx));
    }

    private IEnumerable<StatementSyntax> EnumeratorInit(IterationContext ctx)
    {
        return EnumeratorMembers(ctx)
            .Select(x => x.AsMemberAssignment(this, ctx))
            .Where(x => x != null)!;
    }

    private IEnumerable<StatementSyntax> LocalInit(IterationContext ctx)
    {
        return LocalMembers(ctx)
            .Select(x => x.AsMemberAssignment(this, ctx))
            .Where(x => x != null)!;
    }
}
