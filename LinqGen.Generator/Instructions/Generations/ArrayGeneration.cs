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

    public sealed class ArrayGeneration : Generation
    {
        public TypeSyntax SourceType { get; }

        public ArrayGeneration(in LinqGenExpression expression, int id,
            IArrayTypeSymbol arraySymbol) : base(expression, id)
        {
            // TODO generic type element
            ITypeSymbol elementSymbol = arraySymbol.ElementType;

            OutputElementType = ParseTypeName(elementSymbol);
            SourceType = ParseTypeName(arraySymbol);
        }

        public override TypeSyntax OutputElementType { get; }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Both, SourceType, VarName("source"));

            if (!isLocal)
            {
                yield return new MemberInfo(MemberKind.Enumerator, IntType, VarName("index"), LiteralExpression(-1));
            }
        }

        public override BlockSyntax RenderIteration(RenderOption option, SyntaxList<StatementSyntax> statements)
        {
            var currentName = VarName("current");

            // replace current variables of downstream
            statements = new(statements.Select(x => x.ReplaceNode(CurrentPlaceholder, currentName)));

            StatementSyntax result;

            if (option.IsLocal)
            {
                result = ForEachStatement(VarType, currentName.Identifier, VarName("source"), Block(statements));
            }
            else
            {
                statements.Insert(0, LocalDeclarationStatement(
                    currentName.Identifier, ElementAccessExpression(VarName("source"), VarName("index"))));

                result = WhileStatement(LessThanExpression(
                        CastExpression(UIntType, PreIncrementExpression(VarName("index"))),
                        CastExpression(UIntType, MemberAccessExpression(VarName("source"), LengthProperty))),
                    Block(statements));
            }

            return Block(result);
        }
    }
}