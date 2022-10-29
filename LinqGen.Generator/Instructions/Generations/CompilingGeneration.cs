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
    using static CodeGenUtils;

    /// <summary>
    /// Generation is any instruction that produces enumerable as output.
    /// </summary>
    public abstract class CompilingGeneration : Generation
    {
        protected CompilingGeneration(in LinqGenExpression expression, int id) : base(expression)
        {
            MethodName = IdentifierName(expression.MethodSymbol.Name);
            ClassName = IdentifierName = IdentifierName($"{MethodName}_{id}");

            // To make sure type parameter has unique id, easier to make downstream methods
            TypeParameterPrefix = $"T{id}_";
        }

        public override string TypeParameterPrefix { get; }

        public IdentifierNameSyntax MethodName { get; }

        public override NameSyntax ClassName { get; }

        public override IdentifierNameSyntax IdentifierName { get; }

        protected abstract IEnumerable<MemberInfo> GetMemberInfos();

        public override SourceText Render()
        {
            return GenerationTemplate.Render(this);
        }

        public virtual ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            // assignment will be automatic if parameter kind is Both
            return ConstructorDeclaration(new(AggressiveInliningAttributeList),
                InternalTokenList, Identifier("Enumerator"), ParameterList(GetParameters(MemberKind.Both)),
                ThisInitializer, Block(GetAssignments(MemberKind.Both)));
        }

        public abstract BlockSyntax RenderMoveNextBody();

        public abstract BlockSyntax RenderCurrentGetBody();

        public virtual BlockSyntax RenderDisposeBody() => Block();

        public virtual BlockSyntax RenderCountGetBody() =>
            Block(ReturnStatement(MemberAccessExpression(SourceName, CountName)));

        private MemberInfo[]? _memberInfos;

        private MemberInfo[] MemberInfos => _memberInfos ??= GetMemberInfos().ToArray();

        public IEnumerable<ParameterSyntax> GetParameters(MemberKind kind, bool firstThisParam = false)
        {
            foreach (var member in MemberInfos)
            {
                if ((member.Kind & kind) != kind)
                    continue;

                var param = member.AsParameter();

                if (firstThisParam)
                {
                    param = param.WithModifiers(ThisTokenList);
                    firstThisParam = false;
                }

                yield return param;
            }
        }

        public IEnumerable<ArgumentSyntax> GetArguments(MemberKind kind)
        {
            foreach (var member in MemberInfos)
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return member.AsArgument();
            }
        }

        public IEnumerable<BaseTypeSyntax> GetBaseInterfaces()
        {
            NameSyntax genericClassName = IdentifierName;

            if (Arity > 0)
                genericClassName = GenericName(IdentifierName.Identifier, GetTypeArguments()!);

            if (IsCollection)
            {
                yield return SimpleBaseType(GenericName(Identifier("IStructCollection"),
                    TypeArgumentList(OutputElementType,
                        QualifiedName(genericClassName, IdentifierName("Enumerator")))));
            }
            else if (IsPartition)
            {
                yield return SimpleBaseType(GenericName(Identifier("IStructPartition"),
                    TypeArgumentList(OutputElementType,
                        QualifiedName(genericClassName, IdentifierName("Enumerator")))));
            }
        }

        public IEnumerable<MemberDeclarationSyntax> GetFieldDeclarations(MemberKind kind, bool isReadOnly)
        {
            foreach (var member in MemberInfos)
            {
                if ((member.Kind & kind) != kind)
                    continue;

                var tokenList = isReadOnly ? PrivateReadOnlyTokenList : PrivateTokenList;
                yield return FieldDeclaration(default, tokenList, VariableDeclaration(
                    member.Type, SingletonSeparatedList(VariableDeclarator(member.Name.Identifier))));
            }
        }

        public IEnumerable<StatementSyntax> GetAssignments(MemberKind kind, IdentifierNameSyntax? source = null)
        {
            foreach (var member in MemberInfos)
            {
                if ((member.Kind & kind) != kind)
                    continue;

                yield return ExpressionStatement(SimpleAssignmentExpression(
                    MemberAccessExpression(ThisExpression(), member.Name),
                    source == null ? member.Name : MemberAccessExpression(source, member.Name)));
            }
        }
    }
}
