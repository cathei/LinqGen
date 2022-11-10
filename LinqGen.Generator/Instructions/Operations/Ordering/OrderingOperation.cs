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

    public abstract class OrderingOperation : Operation
    {
        private static readonly SyntaxTree SliceEnumeratorTemplate = CSharpSyntaxTree.ParseText(@"
        {
            using var iter = source.GetEnumerator();

            // prepare temporary buffers
            var elementBuffer = new PooledList<_Element_>(_Count_);

            // load all elements
            while (iter.MoveNext())
                elementBuffer.Add(iter.Current);

            int min = skip;
            int max = take < 0 ? elementBuffer.Count - 1 : min + take - 1;

            if (min > max)
            {
                // in this case, it actually should not be enumerated at all
                elementBuffer.Dispose();
                return new Enumerator(new PooledList<_Element_>(0), new PooledList<int>(0), 0);
            }

            var indexBuffer = new PooledList<int>(elementBuffer.Count);

            // initialize all indexes
            for (int i = 0; i < elementBuffer.Count; ++i)
                indexBuffer.Add(i);

            // initialize comparer with keys
            var comparer = new Comparer(elementBuffer);

            OrderByUtils<T, TComparer>.PartialQuickSort(
                indexBuffer.Array, comparer, 0, elementBuffer.Count - 1, min, max);

            comparerCopy.Dispose();

            return new Enumerator(indexBuffer, elementBuffer, skip);
        }");

        private static readonly SyntaxTree ComparerTemplate = CSharpSyntaxTree.ParseText(@"
        internal struct Comparer : IComparer<int>, IDisposable
        {
            private PooledList<_Element_> source;

            public Comparer(PooledList<_Element_> source)
            {
                this.source = source;
            }

            public int Compare(int x, int y)
            {
            }

            public void Dispose()
            {
            }
        }
");

        private class SyntaxRewriter : CSharpSyntaxRewriter
        {
            private readonly OrderingOperation _operation;

            public SyntaxRewriter(OrderingOperation operation)
            {
                _operation = operation;
            }

            public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
            {
                switch (node.Identifier.ValueText)
                {
                    case "_Element_":
                        return _operation.Upstream!.OutputElementType;

                    case "_Count_":
                        return _operation.IsCountable
                            ? MemberAccessExpression(SourceVar, CountProperty)
                            : LiteralExpression(0);
                }

                return base.VisitIdentifierName(node);
            }

            public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
            {
                return base.VisitMethodDeclaration(node);
            }
        }

        protected TypeSyntax SelectorTypeName { get; }
        protected bool WithStruct { get; }
        private SyntaxRewriter Rewriter { get; }

        public OrderingOperation(in LinqGenExpression expression, int id,
            INamedTypeSymbol selectorType, bool withStruct) : base(expression, id)
        {
            WithStruct = withStruct;
            SelectorTypeName = ParseTypeName(selectorType);
            Rewriter = new SyntaxRewriter(this);
        }

        public override bool IsCountable => Upstream!.IsCountable;
        public override bool IsPartition => true;

        protected TypeSyntax ComparerTypeName =>
            GenericName(Identifier("IComparer"), TypeArgumentList(Upstream!.OutputElementType));

        private OrderingOperation? UpstreamOrder { get; set; }

        public override void SetUpstream(Generation upstream)
        {
            UpstreamOrder = upstream as OrderingOperation;
            base.SetUpstream(upstream);
        }

        protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
        {
            if (WithStruct)
            {
                yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}1"),
                    TypeConstraint(SelectorTypeName));

                yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}2"),
                    ClassOrStructConstraint(SyntaxKind.StructConstraint), TypeConstraint(ComparerTypeName));
            }
        }

        public override BlockSyntax RenderGetEnumeratorBody()
        {
            return Block(ReturnStatement(
                InvocationExpression(GetSliceEnumeratorMethod,
                    ArgumentList(LiteralExpression(0), LiteralExpression(-1)))));
        }

        public override IEnumerable<MemberInfo> GetMemberInfos()
        {
            return base.GetMemberInfos();
        }




        public override BlockSyntax RenderGetSliceEnumeratorBody()
        {
            var block = SliceEnumeratorTemplate.GetRoot().DescendantNodes().OfType<BlockSyntax>().First();
            block = (BlockSyntax)Rewriter.VisitBlock(block)!;

            return block;
        }
    }
}