// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public class WhereOperation : Operation
    {
        protected readonly NameSyntax ArgumentTypeName;

        public WhereOperation(INamedTypeSymbol elementSymbol, INamedTypeSymbol? parentSymbol,
            ITypeSymbol argumentSymbol) : base(elementSymbol, parentSymbol)
        {
            ArgumentTypeName = ParseName(argumentSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        }

        public override IdentifierNameSyntax MethodName { get; } = IdentifierName(nameof(StubExtensions.Where));

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Both, ArgumentTypeName, PredicateName);
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(WhileStatement(TrueExpression(), Block(
                IfStatement(
                    LogicalNotExpression(InvocationExpression(SourceName, MoveNextName)),
                    ReturnStatement(FalseExpression())),
                IfStatement(
                    InvocationExpression(
                        MemberAccessExpression(PredicateName, InvokeName),
                        ArgumentList(MemberAccessExpression(SourceName, CurrentName))),
                    ReturnStatement(TrueExpression()))
            )));
        }
    }
}