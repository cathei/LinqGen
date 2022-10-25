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

    /// <summary>
    /// Operation take LinqGen enumerable as input, and produces another enumerable as output
    /// </summary>
    public class Operation : CompilingGeneration
    {
        public Operation(in LinqGenExpression expression) : base(expression) { }

        // upstream must exists for operations
        public override TypeSyntax OutputElementType => Upstream!.OutputElementType;

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            yield return new MemberInfo(MemberKind.Enumerable, UpstreamResolvedClassName, SourceName);

            yield return new MemberInfo(MemberKind.Enumerator,
                QualifiedName(UpstreamResolvedClassName, IdentifierName("Enumerator")), SourceName);
        }

        public override BlockSyntax RenderConstructorBody()
        {
            return Block(ExpressionStatement(AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, ThisExpression(), SourceName),
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