// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
    public class WhereNode : OpNode
    {
        private readonly string ArgumentTypeName;

        public WhereNode(INamedTypeSymbol elementSymbol, INamedTypeSymbol? parentSymbol,
            ITypeSymbol argumentSymbol) : base(elementSymbol, parentSymbol)
        {
            ArgumentTypeName = argumentSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }

        public override string MethodName => nameof(StubExtensions.Where);

        public override IEnumerable<Template.MemberInfo> GetMemberInfos()
        {
            foreach (var member in base.GetMemberInfos())
                yield return member;

            yield return new Template.MemberInfo(
                Template.MemberKind.Both, ArgumentTypeName, "predicate");
        }

        public override void RenderMoveNextBody(StringBuilder builder)
        {
            builder.Append(@"
            while (true)
            {
                if (!source.MoveNext())
                    return false;

                if (predicate.Invoke(source.Current))
                    return true;
            }
");
        }
    }
}