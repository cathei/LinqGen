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
        protected CompilingGeneration(in LinqGenExpression expression) : base(expression)
        {
            MethodName = IdentifierName(expression.MethodSymbol.Name);
        }

        public IdentifierNameSyntax MethodName { get; }

        /// <summary>
        /// Non-qualified class name, Only used for current file rendering
        /// </summary>
        public IdentifierNameSyntax? IdentifierName { get; protected set; }

        protected abstract IEnumerable<MemberInfo> GetMemberInfos();

        public override SourceText Render(IdentifierNameSyntax assemblyName, int id)
        {
            ClassName = IdentifierName = IdentifierName($"{MethodName}_{id}");
            return GenerationTemplate.Render(assemblyName, this);
        }

        public virtual BlockSyntax RenderConstructorBody() => SyntaxFactory.Block();

        public abstract BlockSyntax RenderMoveNextBody();

        public abstract BlockSyntax RenderCurrentGetBody();

        public virtual BlockSyntax RenderDisposeBody() => SyntaxFactory.Block();

        private MemberInfo[]? _memberInfos;

        private MemberInfo[] MemberInfos => _memberInfos ??= GetMemberInfos().ToArray();

        public IEnumerable<ParameterSyntax> GetParameters(MemberKind kind, bool firstThisParam)
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
