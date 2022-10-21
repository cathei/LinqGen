// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Cathei.LinqGen.Generator
{
    public class GenListNode : Node
    {
        public GenListNode(INamedTypeSymbol elementSymbol) :
            base(elementSymbol, null) { }

        public override string MethodName => nameof(StubExtensions.Gen);

        public override IEnumerable<Template.MemberInfo> GetMemberInfos()
        {
            yield return new Template.MemberInfo(
                Template.MemberKind.Both, $"IList<{ElementName}>", "source");

            yield return new Template.MemberInfo(
                Template.MemberKind.Enumerator, $"int", "index");
        }

        public override void RenderConstructorBody(StringBuilder builder)
        {
            builder.Append("index = -1;");
        }

        public override void RenderMoveNextBody(StringBuilder builder)
        {
            builder.Append("return ++index < source.Count;");
        }

        public override void RenderGetCurrentBody(StringBuilder builder)
        {
            builder.Append("return source[index];");
        }
    }
}