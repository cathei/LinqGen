using System;
using System.Collections.Immutable;
using System.Linq;

namespace Cathei.LinqGen.Generator;

public class GenerationRender : LinqGenRender
{
    private static readonly SyntaxTree Template = CSharpSyntaxTree.ParseText("""
namespace Cathei.LinqGen.Hidden
{
    // Enumerable should be considered as anonymous type, thus it will be internal
    internal readonly partial struct _Enumerable_
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal _Enumerable_() : this() {}
    }
}

namespace Cathei.LinqGen
{
    // Extension class needs to be internal to prevent ambiguous resolution
    internal static partial class _Extensions_
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static _Enumerable_ _Method_() {}
    }
}
""");

    private class Rewriter : CSharpSyntaxRewriter
    {
        private readonly IdentifierNameSyntax _methodName;
        private readonly IdentifierNameSyntax _enumerableName;
        private readonly GenerationInstruction _instruction;

        public Rewriter(
            IdentifierNameSyntax methodName,
            IdentifierNameSyntax enumerableName,
            GenerationInstruction instruction)
        {
            _methodName = methodName;
            _enumerableName = enumerableName;
            _instruction = instruction;
        }

        public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax? node)
        {
            switch (node!.Identifier.ValueText)
            {
                case "_Extensions_":
                    node = RewriteExtensionClass(node);
                    break;
            }

            return node == null ? null : base.VisitClassDeclaration(node);
        }

        public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax? node)
        {
            switch (node?.Identifier.ValueText)
            {
                case "_Enumerable_":
                    node = RewriteEnumerableStruct(node);
                    break;
            }

            return node == null ? null : base.VisitStructDeclaration(node);
        }

        public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax? node)
        {
            switch (node!.Identifier.ValueText)
            {
                case "_Enumerable_":
                    node = RewriteEnumerableConstructor(node);
                    break;
            }

            return node == null ? null : base.VisitConstructorDeclaration(node);
        }

        public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax? node)
        {
            switch (node!.Identifier.ValueText)
            {
                case "_Method_":
                    node = RewriteExtensionMethod(node);
                    break;
            }

            return node == null ? null : base.VisitMethodDeclaration(node);
        }

        public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
        {
            switch (node.Identifier.ValueText)
            {
                case "_Enumerable_":
                    return _enumerableName;
            }

            return base.VisitIdentifierName(node);
        }

        private ClassDeclarationSyntax? RewriteExtensionClass(ClassDeclarationSyntax node)
        {
            return node
                .WithIdentifier(Identifier($"LinqGenExtensions_{_enumerableName.Identifier.ValueText}"))
                .WithBaseList(BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(_instruction.InterfaceType))));
        }

        private StructDeclarationSyntax RewriteEnumerableStruct(StructDeclarationSyntax node)
        {
            using (ListPool.Rent(out List<StatementSyntax> statements))
            {
                node = node.WithIdentifier(_enumerableName.Identifier)
                    .AddMembers();





                return node;
            }
                // .WithTypeParameterList(_instruction.GetTypeParameters())
                // .WithConstraintClauses(_instruction.GetGenericConstraints())
                // .AddMembers(_instruction.RenderEnumerableMembers().ToArray())
                // .AddMembers(_instruction.GetFieldDeclarations(MemberKind.Enumerable).ToArray());
        }

        private ConstructorDeclarationSyntax? RewriteEnumerableConstructor(ConstructorDeclarationSyntax node)
        {
            using (ListPool.Rent(out List<ParameterSyntax> parameters))
            using (ListPool.Rent(out List<StatementSyntax> statements))
            {
                node = node.WithIdentifier(_enumerableName.Identifier);

                _instruction.GetConstructorParameters(parameters);
                parameters[0] = parameters[0].AddModifiers(InToken);
                node = node.WithParameterList(ParameterList(parameters));

                return node;
            }

            //
            // var parameters = _instruction.GetParameters();
            // var assignments = _instruction.GetFieldAssignments(MemberKind.Enumerable, false);
            //
            // if (_instruction.Upstream != null)
            // {
            //     var sourceName = IdentifierName("source");
            //
            //     parameters = parameters.Prepend(Parameter(default, InTokenList,
            //         _instruction.UpstreamResolvedClassName, sourceName.Identifier, null));
            //
            //     assignments = _instruction.Upstream.GetFieldAssignments(MemberKind.Enumerable, true, sourceName)
            //         .Concat(assignments);
            // }
            //
            // var parameterList = ParameterList(parameters);
            //
            // if (parameterList.Parameters.Count == 0)
            //     return null;
            //
            // return node.WithIdentifier(_instruction.ClassName.Identifier)
            //     .WithParameterList(parameterList)
            //     .WithBody(Block(assignments));
        }

        private MethodDeclarationSyntax? RewriteExtensionMethod(MethodDeclarationSyntax node)
        {
            using (ListPool.Rent(out List<ParameterSyntax> parameters))
            {
                node = node.WithIdentifier(_methodName.Identifier);

                _instruction.GetConstructorParameters(parameters);
                parameters[0] = parameters[0].AddModifiers(ThisToken, InToken);
                node = node.WithParameterList(ParameterList(parameters));

                node = node.WithExpressionBody(ArrowExpressionClause(ObjectCreationExpression(
                    _enumerableName,
                    ArgumentList(parameters.Select(x => Argument(IdentifierName(x.Identifier)))),
                    null)));

                return node;
            }
        }
    }

    public GenerationRender(LinqGenInstruction upstream, IdentifierNameSyntax methodName)
        : base(upstream, methodName)
    {}

    public override CompilationUnitSyntax Render()
    {
        var instruction = (GenerationInstruction)Upstream!;
        var enumerableName = IdentifierName($"{MethodName.Identifier.ValueText}_{UniqueId}");

        var rewriter = new Rewriter(MethodName, enumerableName, instruction);
        return (CompilationUnitSyntax)rewriter.Visit(Template.GetCompilationUnitRoot());
    }
}
