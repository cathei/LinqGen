// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public class SelectOperation : Operation
    {
        protected readonly NameSyntax ArgumentTypeName;

        public SelectOperation(INamedTypeSymbol elementSymbol, INamedTypeSymbol? parentSymbol,
            ITypeSymbol argumentSymbol) : base(elementSymbol, parentSymbol)
        {
            ArgumentTypeName = ParseName(argumentSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        }

        public override IdentifierNameSyntax MethodName { get; } = IdentifierName(nameof(StubExtensions.Select));

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new MemberInfo(MemberKind.Both, ArgumentTypeName, SelectorName);
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(InvocationExpression(
                MemberAccessExpression(SelectorName, InvokeName),
                ArgumentList(MemberAccessExpression(SourceName, CurrentName)))));
        }
    }
}