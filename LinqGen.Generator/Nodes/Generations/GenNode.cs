// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
    public class GenNode : Node
    {
        public GenNode(INamedTypeSymbol elementSymbol) :
            base(elementSymbol, null) { }

        public override string MethodName => nameof(StubExtensions.Gen);

        public override IEnumerable<Template.MemberInfo> GetMemberInfos()
        {
            yield return new Template.MemberInfo(
                Template.MemberKind.Enumerable, $"IEnumerable<{ElementName}>", "source");

            yield return new Template.MemberInfo(
                Template.MemberKind.Enumerator, $"IEnumerator<{ElementName}>", "source");
        }

        public override void RenderConstructorBody(StringBuilder builder)
        {
            builder.Append("this.source = e.source.GetEnumerator();");
        }

        public override void RenderMoveNextBody(StringBuilder builder)
        {
            builder.Append("return source.MoveNext();");
        }

        public override void RenderGetCurrentBody(StringBuilder builder)
        {
            builder.Append("return source.Current;");
        }
    }
}