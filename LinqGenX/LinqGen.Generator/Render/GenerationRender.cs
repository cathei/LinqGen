using System;
using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public class GenerationRender : LinqGenRender
{
    private static readonly SyntaxTree Template = CSharpSyntaxTree.ParseText("""
namespace Cathei.LinqGen.Hidden
{
    // Enumerable should be considered as anonymous type, thus it will be internal
    internal readonly partial struct _Enumerable_
    {
        private readonly _Source_ source;
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal _Enumerable_(in _Source_ source) : this()
        {
            this.source = source;
        }
    }
}

namespace Cathei.LinqGen
{
    // Extension class needs to be internal to prevent ambiguous resolution
    internal static partial class _Extensions_
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static _Enumerable_ _Method_(this _Source_ source)
        {
            return new _Enumerable_(input);
        }
    }
}
""");

    public class Rewriter : CSharpSyntaxRewriter
    {
        private readonly IdentifierNameSyntax _methodName;
        private readonly IdentifierNameSyntax _enumerableName;
        private readonly TypeSyntax _sourceType;
        private readonly TypeSyntax _interfaceType;

        public Rewriter(
            IdentifierNameSyntax methodName,
            IdentifierNameSyntax enumerableName,
            TypeSyntax sourceType,
            TypeSyntax interfaceType)
        {
            _methodName = methodName;
            _enumerableName = enumerableName;
            _sourceType = sourceType;
            _interfaceType = interfaceType;
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
                case "_Source_":
                    return _sourceType;

                case "_Enumerable_":
                    return _enumerableName;
            }

            return base.VisitIdentifierName(node);
        }

        private ClassDeclarationSyntax? RewriteExtensionClass(ClassDeclarationSyntax node)
        {
            return node
                .WithIdentifier(Identifier($"LinqGenExtensions_{_enumerableName.Identifier.ValueText}"))
                .WithBaseList(BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(_interfaceType))));
        }

        private StructDeclarationSyntax RewriteEnumerableStruct(StructDeclarationSyntax node)
        {
            node = node.WithIdentifier(_enumerableName.Identifier);
                // .WithTypeParameterList(_instruction.GetTypeParameters())
                // .WithConstraintClauses(_instruction.GetGenericConstraints())
                // .AddMembers(_instruction.RenderEnumerableMembers().ToArray())
                // .AddMembers(_instruction.GetFieldDeclarations(MemberKind.Enumerable).ToArray());

            return node;
        }

        private ConstructorDeclarationSyntax? RewriteEnumerableConstructor(ConstructorDeclarationSyntax node)
        {
            return node.WithIdentifier(_enumerableName.Identifier);
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
            return node.WithIdentifier(_methodName.Identifier);
        }
    }

    public GenerationRender(LinqGenInstruction upstream, IdentifierNameSyntax methodName)
        : base(upstream, methodName)
    {}

    public override CompilationUnitSyntax Render()
    {
        var instruction = (GenerationInstruction)Upstream!;
        var enumerableName = IdentifierName($"{MethodName.Identifier.ValueText}_{UniqueId}");

        var rewriter = new Rewriter(MethodName, enumerableName, instruction.SourceType, instruction.InterfaceType);
        return (CompilationUnitSyntax)rewriter.Visit(Template.GetCompilationUnitRoot());
    }
}
