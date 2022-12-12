// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    public static class SorterTemplate
    {
        private static readonly SyntaxTree TemplateSyntaxTree = CSharpSyntaxTree.ParseText(@"
        internal struct Sorter : IComparer<int>, IDisposable
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Sorter(PooledList<_Element_> elements)
            {
                this.source = source;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Compare(int x, int y)
            {
                int result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Dispose()
            {
            }

            public void PartialQuickSort(
                int[] indexesToSort, int left, int right, int min, int max)
            {
                do
                {
                    int mid = PartitionHoare(indexesToSort, left, right);

                    if (left < mid && mid >= min)
                        PartialQuickSort(indexesToSort, left, mid, min, max);

                    left = mid + 1;

                } while (left < right && left <= max);
            }

            // Hoare partition scheme
            // This implementation is faster when using struct comparer (more comparison and less copy)
            private int PartitionHoare(int[] indexesToSort, int left, int right)
            {
                // preventing overflow of the pivot
                int pivot = left + ((right - left) >> 1);
                int pivotIndex = indexesToSort[pivot];

                int i = left - 1;
                int j = right + 1;

                while (true)
                {
                    // Move the left index to the right at least once and while the element at
                    // the left index is less than the pivot
                    while (Compare(indexesToSort[++i], pivotIndex) < 0) { }

                    // Move the right index to the left at least once and while the element at
                    // the right index is greater than the pivot
                    while (Compare(indexesToSort[--j], pivotIndex) > 0) { }

                    // If the indices crossed, return
                    if (i >= j)
                        return j;

                    // Swap the elements at the left and right indices
                    (indexesToSort[i], indexesToSort[j]) = (indexesToSort[j], indexesToSort[i]);
                }
            }
        }
");

        private class Rewriter : CSharpSyntaxRewriter
        {
            private readonly OrderingOperation _operation;

            public Rewriter(OrderingOperation operation)
            {
                _operation = operation;
            }

            public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "_Element_":
                        return _operation.OutputElementType;
                }

                return base.VisitIdentifierName(node);
            }

            public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
            {
                node = RenderComparerStruct(node);
                return base.VisitStructDeclaration(node);
            }

            public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
            {
                node = RenderConstructor(node);
                return base.VisitConstructorDeclaration(node);
            }

            public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "Compare":
                        node = RenderCompareMethod(node);
                        break;

                    case "Dispose":
                        node = RenderDisposeMethod(node);
                        break;
                }

                return base.VisitMethodDeclaration(node);
            }

            private StructDeclarationSyntax RenderComparerStruct(StructDeclarationSyntax node)
            {
                var list = new List<MemberDeclarationSyntax>();

                bool needElement = false;

                foreach (var member in _operation.GetOrderMemberInfos())
                {
                    list.Add(FieldDeclaration(
                        PrivateTokenList, member.ComparerType, member.ComparerName.Identifier));

                    if (member.SelectorType == null)
                    {
                        needElement = true;
                        continue;
                    }

                    list.Add(FieldDeclaration(
                        PrivateTokenList, PooledListType(member.KeyType), member.KeysName.Identifier));
                }

                if (needElement)
                {
                    list.Add(FieldDeclaration(
                        PrivateTokenList, PooledListType(_operation.OutputElementType), Identifier("elements")));
                }

                return node.AddMembers(list.ToArray());
            }

            private ConstructorDeclarationSyntax RenderConstructor(ConstructorDeclarationSyntax node)
            {
                var list = new List<StatementSyntax>();

                var elementName = IdentifierName("elements");
                var elementCount = MemberAccessExpression(elementName, CountProperty);

                bool needElement = false;

                foreach (var member in _operation.GetOrderMemberInfos())
                {
                    // assign to field
                    list.Add(ExpressionStatement(SimpleAssignmentExpression(
                        MemberAccessExpression(ThisExpression(), member.ComparerName), member.ComparerName)));

                    if (member.SelectorType == null)
                    {
                        needElement = true;
                        continue;
                    }

                    list.Add(ExpressionStatement(SimpleAssignmentExpression(member.KeysName,
                        ObjectCreationExpression(PooledListType(member.KeyType), ArgumentList(elementCount), null))));

                    var indexName = IdentifierName("i");

                    var forStatement = ForStatement(indexName, LiteralExpression(0), elementCount,
                        ExpressionStatement(InvocationExpression(
                            MemberAccessExpression(member.KeysName, AddMethod),
                            ArgumentList(InvocationExpression(
                                MemberAccessExpression(member.SelectorName, InvokeMethod),
                                ArgumentList(ElementAccessExpression(elementName, indexName)))))));

                    list.Add(forStatement);
                }

                if (needElement)
                {
                    list.Add(ExpressionStatement(SimpleAssignmentExpression(
                        MemberAccessExpression(ThisExpression(), elementName), elementName)));
                }

                return node
                    .AddParameterListParameters(_operation.GetOrderParameters().ToArray())
                    .AddBodyStatements(list.ToArray());
            }

            private MethodDeclarationSyntax RenderCompareMethod(MethodDeclarationSyntax node)
            {
                var list = new List<StatementSyntax>();

                var xVar = IdentifierName("x");
                var yVar = IdentifierName("y");
                var resultVar = IdentifierName("result");

                foreach (var member in _operation.GetOrderMemberInfos())
                {
                    var keyIdentifier = member.SelectorType == null ? IdentifierName("elements") : member.KeysName;

                    list.Add(ExpressionStatement(SimpleAssignmentExpression(resultVar, InvocationExpression(
                        MemberAccessExpression(member.ComparerName, CompareMethod), ArgumentList(
                            ElementAccessExpression(keyIdentifier, xVar),
                            ElementAccessExpression(keyIdentifier, yVar))))));

                    list.Add(IfStatement(NotEqualsExpression(resultVar, LiteralExpression(0)),
                        ReturnStatement(member.Desc ? MinusExpression(resultVar) : resultVar)));
                }

                list.Add(ReturnStatement(SubtractExpression(xVar, yVar)));

                return node.AddBodyStatements(list.ToArray());
            }

            private MethodDeclarationSyntax RenderDisposeMethod(MethodDeclarationSyntax node)
            {
                var list = new List<StatementSyntax>();

                foreach (var member in _operation.GetOrderMemberInfos())
                {
                    if (member.SelectorType == null)
                        continue;

                    list.Add(ExpressionStatement(InvocationExpression(member.KeysName, DisposeMethod)));
                }

                return node.AddBodyStatements(list.ToArray());
            }
        }

        public static StructDeclarationSyntax Render(OrderingOperation operation)
        {
            var structSyntax = TemplateSyntaxTree.GetRoot()
                .DescendantNodesAndSelf()
                .OfType<StructDeclarationSyntax>()
                .First();

            var rewriter = new Rewriter(operation);
            return (StructDeclarationSyntax)rewriter.Visit(structSyntax);
        }
    }
}