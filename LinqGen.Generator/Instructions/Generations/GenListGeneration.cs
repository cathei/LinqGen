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

    public class GenListGeneration : Generation
    {
        public GenListGeneration(in LinqGenExpression expression) : base(expression) { }

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            var typeList = TypeArgumentList(SingletonSeparatedList((TypeSyntax)ElementName));

            yield return new MemberInfo(
                MemberKind.Both, ListInterfaceName.WithTypeArgumentList(typeList), SourceName);

            yield return new MemberInfo(
                MemberKind.Enumerator, IntType, IndexName);
        }

        public override BlockSyntax RenderConstructorBody()
        {
            return Block(ExpressionStatement(
                SimpleAssignmentExpression(IndexName, LiteralExpression(-1))));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(ReturnStatement(LessThanExpression(
                PreIncrementExpression(IndexName),
                MemberAccessExpression(SourceName, CountName))));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(
                ElementAccessExpression(SourceName, BracketedArgumentList(IndexName))));
        }
    }
}