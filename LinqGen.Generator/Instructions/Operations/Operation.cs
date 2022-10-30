// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    /// <summary>
    /// Operation take LinqGen enumerable as input, and produces another enumerable as output
    /// </summary>
    public abstract class Operation : CompilingGeneration
    {
        protected Operation(in LinqGenExpression expression, int id) : base(expression, id) { }

        // upstream must exists for operations
        public override TypeSyntax OutputElementType => Upstream!.OutputElementType;

        public override void SetUpstream(Generation upstream)
        {
            Upstream = upstream;
            upstream.AddDownstream(this);
        }

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            yield return new MemberInfo(MemberKind.Enumerable, UpstreamResolvedClassName, SourceField);

            yield return new MemberInfo(MemberKind.Enumerator,
                QualifiedName(UpstreamResolvedClassName, IdentifierName("Enumerator")), SourceField);
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType,
                ArgumentList(GetArguments(MemberKind.Both).Prepend(
                    Argument(InvocationExpression(SourceField, GetEnumeratorMethod)))), null)));
        }

        public override BlockSyntax RenderGetSliceEnumeratorBody()
        {
            return Block(ReturnStatement(ObjectCreationExpression(EnumeratorType,
                ArgumentList(GetArguments(MemberKind.Both).Prepend(
                    Argument(InvocationExpression(
                        MemberAccessExpression(SourceField, GetSliceEnumeratorMethod),
                        ArgumentList(SkipField, TakeField))))), null)));
        }

        public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
        {
            var parameters = GetParameters(MemberKind.Both)
                .Prepend(Parameter(
                    QualifiedName(UpstreamResolvedClassName, IdentifierName("Enumerator")),
                    SourceField.Identifier));

            var assignments = GetAssignments(MemberKind.Both)
                .Prepend(ExpressionStatement(SimpleAssignmentExpression(
                    MemberAccessExpression(ThisExpression(), SourceField), SourceField)));

            // assignment will be automatic if parameter kind is Both
            return ConstructorDeclaration(new(AggressiveInliningAttributeList),
                InternalTokenList, Identifier("Enumerator"), ParameterList(parameters),
                ThisInitializer, Block(assignments));
        }

        public override BlockSyntax RenderMoveNextBody()
        {
            return Block(ReturnStatement(InvocationExpression(SourceField, MoveNextMethod)));
        }

        public override BlockSyntax RenderCurrentGetBody()
        {
            return Block(ReturnStatement(MemberAccessExpression(SourceField, CurrentProperty)));
        }

        public override BlockSyntax RenderDisposeBody()
        {
            return Block(ExpressionStatement(InvocationExpression(SourceField, DisposeMethod)));
        }
    }
}