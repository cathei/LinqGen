// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;

namespace Cathei.LinqGen.Generator;

/// <summary>
/// Common base class for CompilingGeneration and CompiledGeneration.
/// So they can provide metadata with same interfaces.
/// </summary>
public abstract class Generation : Instruction
{
    public IdentifierNameSyntax MethodName { get; }

    protected Generation(in LinqGenExpression expression, uint id) : base(expression, id)
    {
        // File name has to use integer id
        FileName = $"{expression.MethodSymbol.Name}_{id}.g.cs";

        MethodName = IdentifierName(expression.MethodSymbol.Name);
        ClassName = IdentifierName($"{expression.SignatureSymbol!.Name}_{Id}");
    }

    public abstract ITypeSymbol OutputElementSymbol { get; }
    public abstract TypeSyntax OutputElementType { get; }

    public string FileName { get; }

    /// <summary>
    /// The qualified class name cached for rendering
    /// </summary>
    public IdentifierNameSyntax ClassName { get; }

    public NameSyntax ResolvedClassName
    {
        get
        {
            if (Arity == 0)
                return ClassName;
            return GenericName(ClassName.Identifier, GetTypeArguments()!);
        }
    }

    /// <summary>
    /// Non-operation generations has to be exposed as extension method.
    /// </summary>
    public override MethodKind MethodKind => MethodKind.Extension;

    public List<Operation>? Downstream { get; private set; }
    public List<Evaluation>? Evaluations { get; private set; }

    public virtual void AddUpstream(Generation upstream)
    {
        // only operation can have upstream
        throw new NotSupportedException();
    }

    public void AddDownstream(Operation downstream)
    {
        Downstream ??= new List<Operation>();
        Downstream.Add(downstream);
    }

    public void AddEvaluation(Evaluation downstream)
    {
        Evaluations ??= new List<Evaluation>();
        Evaluations.Add(downstream);
    }

    public IEnumerable<MemberDeclarationSyntax> Render()
    {
        return GenerationTemplate.Render(this);
    }

    public virtual IEnumerable<MemberDeclarationSyntax> RenderEnumerableMembers()
    {
        var countExpression = RenderCount();

        if (countExpression != null)
        {
            // count expression cannot use enumerator members
            var thisRewriter = new ThisPlaceholderRewriter(ThisExpression(), IterPlaceholder);
            countExpression = (ExpressionSyntax)thisRewriter.Visit(countExpression);

            yield return MethodDeclaration(SingletonList(AggressiveInliningAttributeList), PublicTokenList,
                IntType, null, CountMethod.Identifier, null, EmptyParameterList, default, null,
                ArrowExpressionClause(countExpression), SemicolonToken);
        }

        if (HasEnumerator)
        {
            foreach (var member in RenderGetEnumerator())
                yield return member;
        }

        if (Downstream != null)
        {
            foreach (var operation in Downstream)
            {
                foreach (var member in operation.RenderUpstreamMembers())
                    yield return member;
            }
        }

        if (Evaluations != null)
        {
            foreach (var evaluation in Evaluations)
            {
                foreach (var member in evaluation.RenderUpstreamMembers())
                    yield return member;
            }
        }
    }

    protected virtual ParameterListSyntax GetExtensionMethodParameters()
    {
        var parameters = GetParameters(true).ToList();

        parameters[0] = parameters[0].WithModifiers(Upstream != null ? ThisInTokenList : ThisTokenList);

        return ParameterList(parameters);
    }

    public IEnumerable<MemberDeclarationSyntax> RenderExtensionClassMembers()
    {
        if (MethodKind == MethodKind.Extension)
        {
            var expression = ObjectCreationExpression(ResolvedClassName, ArgumentList(GetArguments()), null);

            yield return MethodDeclaration(new(AggressiveInliningAttributeList), PublicStaticTokenList,
                ResolvedClassName, null, MethodName.Identifier, GetTypeParameters(), GetExtensionMethodParameters(),
                GetGenericConstraints(), null, ArrowExpressionClause(expression), SemicolonToken);
        }

        if (Evaluations != null)
        {
            foreach (var evaluation in Evaluations)
            {
                foreach (var member in evaluation.RenderExtensionMembers())
                    yield return member;
            }
        }
    }

    public virtual TypeSyntax EnumerableInterfaceType =>
        GenericName(Identifier("IInternalStub"), TypeArgumentList(OutputElementType));

    public IEnumerable<BaseTypeSyntax> InterfaceTypes
    {
        get { yield return SimpleBaseType(EnumerableInterfaceType); }
    }

    protected abstract IEnumerable<MemberInfo> GetMemberInfos(bool isLocal);

    public abstract bool SupportPartition { get; }

    public bool SupportCount => RenderCount() != null;

    /// <summary>
    /// This means the operation will locally run to the end on timing of initialization.
    /// For example, OrderBy needs to run to end first to sort the upstream.
    /// </summary>
    protected virtual bool ClearsUpstreamEnumerator => false;

    private bool ShouldIgnoreUpstream(MemberKind kind)
    {
        if ((kind & MemberKind.Enumerator) == 0)
            return false;
        return ClearsUpstreamEnumerator;
    }

    /// <summary>
    /// Returns null if cannot get count without iteration.
    /// </summary>
    public abstract ExpressionSyntax? RenderCount();

    /// <summary>
    /// Additional initialization statements.
    /// </summary>
    public virtual IEnumerable<StatementSyntax> RenderInitialization(
        bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
    {
        yield break;
    }

    /// <summary>
    /// Dispose statements if needed.
    /// </summary>
    public virtual IEnumerable<StatementSyntax> RenderDispose(bool isLocal)
    {
        yield break;
    }

    /// <summary>
    /// Writes full body of iteration. Can be overriden to change behaviour.
    /// </summary>
    public abstract BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements);

    public IEnumerable<MemberInfo> GetMemberInfos(MemberKind kind, bool isLocal)
    {
        foreach (var member in GetMemberInfos(isLocal))
        {
            if ((member.Kind & kind) != kind)
                continue;

            yield return member;
        }
    }

    public IEnumerable<MemberInfo> GetAllMemberInfos(MemberKind kind, bool isLocal)
    {
        if (Upstream != null && !ShouldIgnoreUpstream(kind))
        {
            foreach (var member in Upstream.GetAllMemberInfos(kind, isLocal))
                yield return member;
        }

        foreach (var member in GetMemberInfos(kind, isLocal))
            yield return member;
    }

    public IEnumerable<ParameterSyntax> GetParameters(bool defaultValue = false)
    {
        foreach (var member in GetMemberInfos(false))
        {
            if ((member.Kind & MemberKind.Enumerable) == 0)
                continue;

            yield return member.AsParameter(defaultValue);
        }
    }

    protected IEnumerable<ArgumentSyntax> GetArguments()
    {
        foreach (var member in GetMemberInfos(false))
        {
            if ((member.Kind & MemberKind.Enumerable) == 0)
                continue;

            yield return member.AsArgument();
        }
    }

    public IEnumerable<StatementSyntax> GetLocalDeclarations(MemberKind kind)
    {
        foreach (var member in GetAllMemberInfos(kind, true))
        {
            yield return LocalDeclarationStatement(member.Type,
                Identifier($"{IterPlaceholder}{member.Name.Identifier.ValueText}"),
                member.DefaultValue ?? DefaultLiteral);
        }
    }

    public IEnumerable<MemberDeclarationSyntax> GetFieldDeclarations(MemberKind kind)
    {
        foreach (var member in GetAllMemberInfos(kind, false))
        {
            yield return FieldDeclaration(SingletonList(EditorBrowsableNeverAttributeList),
                InternalTokenList, member.Type, member.Name.Identifier);
        }
    }

    public IEnumerable<StatementSyntax> GetFieldAssignments(
        MemberKind kind, bool includeUpstream, IdentifierNameSyntax? source = null)
    {
        var memberInfos = includeUpstream
            ? GetAllMemberInfos(kind, false)
            : GetMemberInfos(kind, false);

        foreach (var member in memberInfos)
        {
            yield return ExpressionStatement(SimpleAssignmentExpression(
                MemberAccessExpression(ThisExpression(), member.Name),
                source == null ? member.Name : MemberAccessExpression(source, member.Name)));
        }
    }

    public IEnumerable<StatementSyntax> GetFieldDefaultAssignments(MemberKind kind)
    {
        foreach (var member in GetAllMemberInfos(kind, false))
        {
            if (member.DefaultValue == null)
                continue;

            yield return ExpressionStatement(SimpleAssignmentExpression(
                MemberAccessExpression(ThisExpression(), member.Name), member.DefaultValue));
        }
    }

    public bool HasEnumerator { get; set; }

    private IEnumerable<MemberDeclarationSyntax> RenderGetEnumerator()
    {
        yield return EnumeratorTemplate.Render(this);

        yield return MethodDeclaration(SingletonList(AggressiveInliningAttributeList), PublicTokenList,
            IdentifierName("Enumerator"), null, GetEnumeratorMethod.Identifier, null, ParameterList(),
            default, null, ArrowExpressionClause(ObjectCreationExpression(
                IdentifierName("Enumerator"), ArgumentList(ThisExpression()), null)), SemicolonToken);
    }
}