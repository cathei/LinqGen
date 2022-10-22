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

    public class GenGeneration : CompilingGeneration
    {
        public GenGeneration(in LinqGenExpression expression) : base(expression) { }

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            var typeList = TypeArgumentList(SingletonSeparatedList((TypeSyntax)ElementName));

            yield return new MemberInfo(
                MemberKind.Enumerable, EnumerableInterfaceName.WithTypeArgumentList(typeList), SourceName);

            yield return new MemberInfo(
                MemberKind.Enumerator, EnumeratorInterfaceName.WithTypeArgumentList(typeList), SourceName);
        }

        public override BlockSyntax RenderConstructorBody()
        {
            return Block(ExpressionStatement(SimpleAssignmentExpression(
                MemberAccessExpression(ThisExpression(), SourceName),
                InvocationExpression(ParentName, SourceName, GetEnumeratorName))));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(ReturnStatement(InvocationExpression(SourceName, MoveNextName)));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(MemberAccessExpression(SourceName, CurrentName)));
        }

        public override BlockSyntax RenderDisposeBody()
        {
            return Block(ExpressionStatement(InvocationExpression(SourceName, DisposeName)));
        }
    }
}