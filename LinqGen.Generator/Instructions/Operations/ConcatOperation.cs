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

    public class ConcatOperation : Operation
    {
        private readonly ThisPlaceholderRewriter SecondRewriter;

        public ConcatOperation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            SecondRewriter = new ThisPlaceholderRewriter(Member("second"));
        }

        public Generation Second => Upstreams[1];

        private NameSyntax? _secondResolvedName;
        public TypeSyntax SecondResolvedName
        {
            get
            {
                if (_secondResolvedName != null)
                    return _secondResolvedName;

                _secondResolvedName = MakeGenericName(Second.ClassName, GetSecondTypeArguments());
                return _secondResolvedName;
            }
        }

        public TypeArgumentListSyntax? GetSecondTypeArguments()
        {
            var parameters = Second.TypeParameters;

            if (parameters.Count == 0)
                return null;

            return TypeArgumentList(SeparatedList(parameters.Select(x =>
            {
                // replace with upstream type
                if (x.Name.IsEquivalentTo(Second.OutputElementType))
                    return Upstream.OutputElementType;
                return x.AsTypeArgument();
            })));
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
        {
            yield return new MemberInfo(MemberKind.Enumerable, SecondResolvedName, LocalName("second"));
            yield return new MemberInfo(MemberKind.Enumerator, ByteType, LocalName("state"));
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            foreach (var type in Second.TypeParameters)
            {
                if (type.Name.IsEquivalentTo(Second.OutputElementType))
                    continue;

                yield return new TypeParameterInfo(type.Name, type.GenericConstraint?.Constraints.ToArray());
            }
        }

        // public override IEnumerable<StatementSyntax> RenderInitialization(
        //     bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
        // {
        // }

        public override ExpressionSyntax? RenderCount()
        {
            var firstCount = Upstream.RenderCount();
            var secondCount = Second.RenderCount();

            if (firstCount == null || secondCount == null)
                return null;

            secondCount = (ExpressionSyntax)SecondRewriter.Visit(secondCount);

            return AddExpression(ParenthesizedExpression(firstCount), ParenthesizedExpression(secondCount));
        }

        public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
        {
            // TODO: partition optimization (skip, take)
            var first = Upstream.RenderIteration(isLocal, statements);

            var secondInit = Block(Second.GetFieldDefaultAssignments(MemberKind.Enumerator)
                .Concat(Second.RenderInitialization(isLocal, null, null)));

            var second = Second.RenderIteration(isLocal, statements);

            first = first.AddStatements(
                ExpressionStatement(SimpleAssignmentExpression(LocalName("state"), LiteralExpression(1))),
                GotoStatement(SyntaxKind.GotoCaseStatement, Token(SyntaxKind.CaseKeyword), LiteralExpression(1)));

            secondInit = secondInit.AddStatements(
                ExpressionStatement(SimpleAssignmentExpression(LocalName("state"), LiteralExpression(2))),
                GotoStatement(SyntaxKind.GotoCaseStatement, Token(SyntaxKind.CaseKeyword), LiteralExpression(2)));
            secondInit = (BlockSyntax)SecondRewriter.Visit(secondInit);

            second = second.AddStatements(BreakStatement());
            second = (BlockSyntax)SecondRewriter.Visit(second);

            return Block(SwitchStatement(LocalName("state"), List(new[]
            {
                SwitchSection(
                    SingletonList<SwitchLabelSyntax>(CaseSwitchLabel(LiteralExpression(0))),
                    SingletonList<StatementSyntax>(first)),
                SwitchSection(
                    SingletonList<SwitchLabelSyntax>(CaseSwitchLabel(LiteralExpression(1))),
                    SingletonList<StatementSyntax>(secondInit)),
                SwitchSection(
                    SingletonList<SwitchLabelSyntax>(CaseSwitchLabel(LiteralExpression(2))),
                    SingletonList<StatementSyntax>(second)),
            })));
        }

        // protected override StatementSyntax? RenderMoveNext()
        // {
        //
        //
        // }
    }
}