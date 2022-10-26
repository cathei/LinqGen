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

    public sealed class SpecializeArrayGeneration : CompilingGeneration
    {
        private TypeSyntax CallerEnumerableType { get; }

        private IArrayTypeSymbol ArrayTypeSymbol { get; }

        public SpecializeArrayGeneration(in LinqGenExpression expression, int id,
            IArrayTypeSymbol arraySymbol) : base(expression, id)
        {
            ArrayTypeSymbol = arraySymbol;
            OutputElementType = ParseTypeName(arraySymbol.ElementType
                .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

            CallerEnumerableType =
                ParseTypeName(arraySymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

            // // find GetEnumerator with same rule as C# duck typing
            // ITypeSymbol enumeratorSymbol = enumerableSymbol.GetMembers()
            //     .OfType<IMethodSymbol>()
            //     .First(x => x.DeclaredAccessibility == Accessibility.Public && x.Name == "GetEnumerator")
            //     .ReturnType;
            //
            // CallerEnumerableType =
            //     ParseTypeName(enumerableSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
            //
            // CallerEnumeratorType =
            //     ParseTypeName(enumeratorSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        }

        public override TypeSyntax OutputElementType { get; }

        protected override IEnumerable<MemberInfo> GetMemberInfos()
        {
            yield return new MemberInfo(
                MemberKind.Both, CallerEnumerableType, SourceName);

            yield return new MemberInfo(
                MemberKind.Enumerator, IntType, IndexName);
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
    }
}