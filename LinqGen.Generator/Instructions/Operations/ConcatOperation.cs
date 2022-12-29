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
        private ThisPlaceholderRewriter SecondRewriter { get; }
        private ThisPlaceholderRewriter TempRewriter { get; }
        private ThisPlaceholderRewriter TempRevertRewriter { get; }

        public ConcatOperation(in LinqGenExpression expression, int id) : base(expression, id)
        {
            SecondRewriter = new ThisPlaceholderRewriter(Member("second"), $"{IterPlaceholder}s{Id}_");

            var tempThisPlaceholder = IdentifierName($"_concat_this_{Id}_");
            var tempIterPlaceholder = $"_concat_iter_{Id}_";

            TempRewriter = new(tempThisPlaceholder, tempIterPlaceholder);
            TempRevertRewriter = new(tempThisPlaceholder, ThisPlaceholder, tempIterPlaceholder, IterPlaceholder);
        }

        public Generation Second => Upstreams[1];

        private NameSyntax? _secondResolvedName;

        public NameSyntax SecondResolvedName
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
            yield return new MemberInfo(MemberKind.Enumerator, ByteType, LocalName("state"), LiteralExpression(0));

            foreach (var member in Second.GetAllMemberInfos(MemberKind.Enumerator, isLocal))
            {
                var overridenName = IdentifierName($"s{Id}_{member.Name.Identifier.ValueText}");
                yield return new(member.Kind, member.Type, overridenName, member.DefaultValue);
            }
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
            statements = TempRewriter.VisitList(statements);

            // TODO: partition optimization (skip, take)
            var first = Upstream.RenderIteration(isLocal, statements);

            var secondInit = Block(Second.RenderInitialization(isLocal, null, null));

            var second = Second.RenderIteration(isLocal, statements);

            first = first.AddStatements(
                ExpressionStatement(SimpleAssignmentExpression(Iterator("state"), LiteralExpression(1))),
                GotoStatement(SyntaxKind.GotoCaseStatement, Token(SyntaxKind.CaseKeyword), LiteralExpression(1)));

            secondInit = (BlockSyntax)SecondRewriter.Visit(secondInit);
            secondInit = secondInit.AddStatements(
                ExpressionStatement(SimpleAssignmentExpression(Iterator("state"), LiteralExpression(2))),
                GotoStatement(SyntaxKind.GotoCaseStatement, Token(SyntaxKind.CaseKeyword), LiteralExpression(2)));

            second = (BlockSyntax)SecondRewriter.Visit(second);
            second = second.AddStatements(BreakStatement());

            var result = Block(SwitchStatement(Iterator("state"), List(new[]
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

            return (BlockSyntax)TempRevertRewriter.Visit(result);
        }

        // protected override StatementSyntax? RenderMoveNext()
        // {
        //
        //
        // }
    }
}