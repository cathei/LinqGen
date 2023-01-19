// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

namespace Cathei.LinqGen.Generator;

public sealed class CountEvaluation : ExtensionEvaluation
{
    private FunctionKind PredicatorKind { get;  }

    public CountEvaluation(in LinqGenExpression expression, int id) : base(expression, id)
    {
        if (MethodSymbol.Parameters.Length >= 1)
        {
            // Sum with a parameter uses selector
            var parameterType = MethodSymbol.Parameters[0].Type;
            PredicatorKind = IsStructFunction(parameterType) ? FunctionKind.Struct : FunctionKind.Delegate;
        }
        else
        {
            // and single parameter only has default value
            PredicatorKind = FunctionKind.Default;
        }
    }

    private TypeSyntax? _predicateType;

    private TypeSyntax PredicateType
    {
        get
        {
            if (_predicateType != null)
                return _predicateType;

            TypeSyntax[] typeArguments = { InputElementType, BoolType };

            return _predicateType = PredicatorKind == FunctionKind.Struct
                ? StructFunctionInterfaceType(typeArguments)
                : FuncDelegateType(typeArguments);
        }
    }

    protected override TypeSyntax ReturnType => IntType;

    protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
    {
        if (PredicatorKind == FunctionKind.Struct)
            yield return new(TypeName("Predicate"), PredicateType!);
    }

    protected override IEnumerable<ParameterInfo> GetParameterInfos()
    {
        if (PredicatorKind == FunctionKind.Struct)
        {
            yield return new ParameterInfo(TypeName("Predicate"), IdentifierName("predicate"));
        }
        else if (PredicatorKind == FunctionKind.Delegate)
        {
            yield return new ParameterInfo(PredicateType, IdentifierName("predicate"));
        }
    }

    public override IEnumerable<MemberDeclarationSyntax> RenderExtensionMembers()
    {
        // Count method is already there
        if (PredicatorKind == FunctionKind.Default && Upstream.SupportCount)
            yield break;

        foreach (var member in base.RenderExtensionMembers())
            yield return member;
    }

    protected override IEnumerable<StatementSyntax> RenderInitialization()
    {
        yield return LocalDeclarationStatement(IntType, LocalName("result").Identifier, DefaultLiteral);
    }

    protected override IEnumerable<StatementSyntax> RenderAccumulation()
    {
        if (PredicatorKind == FunctionKind.Default)
        {
            yield return ExpressionStatement(PreIncrementExpression(LocalName("result")));
        }
        else
        {
            yield return IfStatement(InvocationExpression(MemberAccessExpression(
                    IdentifierName("predicate"), InvokeMethod), ArgumentList(CurrentPlaceholder)),
                ExpressionStatement(PreIncrementExpression(LocalName("result"))));
        }
    }

    protected override IEnumerable<StatementSyntax> RenderReturn()
    {
        yield return ReturnStatement(LocalName("result"));
    }
}