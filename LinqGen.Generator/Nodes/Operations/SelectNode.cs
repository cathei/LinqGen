// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
    public class SelectNode : OpNode
    {
        private string ArgumentTypeName;

        public SelectNode(INamedTypeSymbol elementSymbol, INamedTypeSymbol? parentSymbol,
            ITypeSymbol argumentSymbol) : base(elementSymbol, parentSymbol)
        {
            ArgumentTypeName = argumentSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }

        public override string MethodName => nameof(StubExtensions.Select);

        public override IEnumerable<Template.MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new Template.MemberInfo(
                Template.MemberKind.Both, ArgumentTypeName, "selector");
        }

        public override void RenderGetCurrentBody(StringBuilder builder)
        {
            builder.Append("return selector.Invoke(source.Current);");
        }
    }
}