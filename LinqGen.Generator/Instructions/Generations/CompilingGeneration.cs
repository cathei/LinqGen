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

        public virtual TypeSyntax InterfaceType =>
            GenericName(Identifier("IInternalStub"), TypeArgumentList(OutputElementType));

        public abstract IEnumerable<MemberInfo> GetMemberInfos();

        public override SourceText Render()
        {
            return GenerationTemplate.Render(this);
        }

        public virtual ConstructorDeclarationSyntax RenderEnumerableConstructor()
        {
            return ConstructorDeclaration(new(AggressiveInliningAttributeList),
                InternalTokenList, IdentifierName.Identifier, ParameterList(GetParameters(MemberKind.Enumerable)),
                ThisInitializer, Block(GetAssignments(MemberKind.Enumerable)));
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
            Block(ReturnStatement(MemberAccessExpression(SourceVar, CountProperty)));

        private MemberInfo[]? _memberInfos;

        private MemberInfo[] MemberInfos => _memberInfos ??= GetMemberInfos().ToArray();

        public TypeSyntax ResolvedClassName
        {
            get
            {
                if (Arity == 0)
                    return IdentifierName;
                return GenericName(IdentifierName.Identifier, GetTypeArguments()!);
            }
        }

        public virtual BlockSyntax RenderGetEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(
                EnumeratorType, ArgumentList(GetArguments(MemberKind.Both)), null)));
        }

        public virtual BlockSyntax RenderGetSliceEnumeratorBody() => throw new NotImplementedException();

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

            if (IsCountable)
            {
                yield return SimpleBaseType(IdentifierName("ICountable"));
            }

            if (IsPartition)
            {
                yield return SimpleBaseType(GenericName(Identifier("IPartition"),
                    TypeArgumentList(QualifiedName(genericClassName, IdentifierName("Enumerator")))));
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

        public IEnumerable<MemberDeclarationSyntax> RenderExtensionMethods()
        {
            if (ShouldBeMemberMethod)
                yield break;

            var body = Block(ReturnStatement(
                ObjectCreationExpression(ResolvedClassName,
                    ArgumentList(GetArguments(MemberKind.Enumerable)), default)));

            yield return MethodDeclaration(
                new(AggressiveInliningAttributeList), PublicStaticTokenList, ResolvedClassName, null,
                MethodName.Identifier, GetTypeParameters(), ParameterList(GetParameters(MemberKind.Enumerable, true)),
                GetGenericConstraints(), body, default, default);
        }

        public virtual IEnumerable<MemberDeclarationSyntax> RenderAdditionalMembers()
        {
            if (Downstream == null)
            {
                // nothing to operate
                yield break;
            }

            foreach (var downstream in Downstream)
            {
                foreach (var method in downstream.RenderUpstreamMemberMethods())
                    yield return method;
            }
        }

        public IEnumerable<MemberDeclarationSyntax> RenderUpstreamMemberMethods()
        {
            if (!ShouldBeMemberMethod)
                yield break;

            int arityDiff = Arity - Upstream!.Arity;

            var typeParameters = GetTypeParameters(take: arityDiff);
            var genericConstraints = GetGenericConstraints(take: arityDiff);

            // swap first argument with this
            var argumentList = ArgumentList(
                GetArguments(MemberKind.Enumerable).Skip(1).Prepend(Argument(ThisExpression())));

            var parameterList = ParameterList(GetParameters(MemberKind.Enumerable).Skip(1));

            var body = Block(ReturnStatement(
                ObjectCreationExpression(ResolvedClassName, argumentList, default)));

            yield return MethodDeclaration(new(AggressiveInliningAttributeList),
                PublicTokenList, ResolvedClassName, default, MethodName.Identifier, typeParameters,
                parameterList, genericConstraints, body, default, default);
        }
    }
}
