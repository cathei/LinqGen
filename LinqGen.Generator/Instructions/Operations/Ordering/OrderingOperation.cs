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
         protected bool WithSelector { get; }
         private bool WithStruct { get; }
         private bool Descending { get; }

         public OrderingOperation(in LinqGenExpression expression, int id,
             INamedTypeSymbol? selectorType, bool withStruct, bool descending) : base(expression, id)
         {
             WithStruct = withStruct;
             Descending = descending;

             if (selectorType != null)
             {
                 WithSelector = true;
                 SelectorInterfaceType = ParseTypeName(selectorType);
                 SelectorKeyType = ParseTypeName(selectorType.TypeArguments[1]);
             }
             else
             {
                 WithSelector = false;
                 SelectorInterfaceType = null;
                 SelectorKeyType = null;
             }
         }

         /// <summary>
         /// Upstream ordering chained with ThenBy.
         /// </summary>
         protected virtual OrderingOperation? UpstreamOrder => null;

         protected OrderingOperation RootOrder => UpstreamOrder?.RootOrder ?? this;

         private TypeSyntax? SelectorInterfaceType { get; }
         private TypeSyntax? SelectorKeyType { get; }

         private TypeSyntax KeyType => WithSelector ? SelectorKeyType! : OutputElementType;
         private TypeSyntax ComparerInterfaceType =>
             GenericName(Identifier("IComparer"), TypeArgumentList(KeyType));

         // public override TypeSyntax InterfaceType =>
         //     GenericName(Identifier("IInternalOrderedStub"), TypeArgumentList(OutputElementType));

         // public override void SetUpstream(Generation upstream)
         // {
         //     UpstreamOrder = upstream as OrderingOperation;
         //     base.SetUpstream(upstream);
         // }

         protected override bool ClearsUpstreamEnumerator => true;

         protected override IEnumerable<MemberInfo> GetMemberInfos(bool isLocal)
         {
             yield return new MemberInfo(MemberKind.Enumerator, PooledListType(OutputElementType), VarName("elements"));
             yield return new MemberInfo(MemberKind.Enumerator, PooledListType(IntType), VarName("indices"));

             if (WithStruct)
             {
                 if (WithSelector)
                     yield return new MemberInfo(MemberKind.Enumerable, TypeName("Selector"), VarName("selector"));

                 yield return new MemberInfo(
                     MemberKind.Enumerable, TypeName("Comparer"), VarName("comparer"));
             }
             else
             {
                 if (WithSelector)
                     yield return new MemberInfo(MemberKind.Enumerable, SelectorInterfaceType!, VarName("selector"));

                 yield return new MemberInfo(
                     MemberKind.Enumerable, ComparerInterfaceType, VarName("comparer"), NullLiteral);
             }
         }

         protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
         {
             if (WithStruct)
             {
                 if (WithSelector)
                 {
                     yield return new TypeParameterInfo(TypeName("Selector"), SelectorInterfaceType!);
                     yield return new TypeParameterInfo(TypeName("Comparer"), ComparerInterfaceType);
                 }
                 else
                 {
                     // comparer requires struct type constraint for now... may refactor overloads
                     yield return new TypeParameterInfo(TypeName("Comparer"),
                         ClassOrStructConstraint(SyntaxKind.StructConstraint), TypeConstraint(ComparerInterfaceType));
                 }
             }
         }

         public IEnumerable<ParameterSyntax> GetOrderParameters()
         {
             foreach (var member in GetOrderMemberInfos())
             {
                 if (member.SelectorType != null)
                 {
                     yield return Parameter(member.SelectorType, member.SelectorName.Identifier);
                 }

                 yield return Parameter(member.ComparerType, member.ComparerName.Identifier);
             }
         }

         public IEnumerable<ArgumentSyntax> GetOrderArguments()
         {
             foreach (var member in GetOrderMemberInfos())
             {
                 if (member.SelectorType != null)
                     yield return Argument(member.SelectorName);
                 yield return Argument(member.ComparerName);
             }
         }

         public override IEnumerable<MemberDeclarationSyntax> RenderEnumerableMembers()
         {
             foreach (var member in base.RenderEnumerableMembers())
                 yield return member;

             yield return SorterTemplate.Render(this);
         }

         public override IEnumerable<StatementSyntax> RenderInitialization(
             bool isLocal, ExpressionSyntax? skipVar, ExpressionSyntax? takeVar)
         {
             var rootUpstream = RootOrder.Upstream;

             foreach (var statement in rootUpstream.RenderInitialization(true, null, null))
                 yield return statement;

             ExpressionSyntax countExpression = SupportCount ? CountProperty : LiteralExpression(0);

             var elementsName = VarName("elements");
             var elementsCount = MemberAccessExpression(elementsName, CountProperty);

             yield return ExpressionStatement(SimpleAssignmentExpression(elementsName,
                 ObjectCreationExpression(PooledListType(OutputElementType), ArgumentList(countExpression), null)));

             var addElementStatements = SingletonList<StatementSyntax>(
                 ExpressionStatement(InvocationExpression(
                     MemberAccessExpression(VarName("elements"), AddMethod), ArgumentList(CurrentPlaceholder))));

             yield return rootUpstream.RenderIteration(true, addElementStatements);

             var indicesName = VarName("indices");

             yield return LocalDeclarationStatement(indicesName.Identifier,
                 ObjectCreationExpression(PooledListType(IntType), ArgumentList(LiteralExpression(0)), null));

             var minName = IdentifierName("min");
             var maxName = IdentifierName("max");

             yield return LocalDeclarationStatement(minName.Identifier, skipVar ?? LiteralExpression(0));

             yield return LocalDeclarationStatement(maxName.Identifier, takeVar == null
                 ? SubtractExpression(elementsCount, LiteralExpression(1))
                 : SubtractExpression(AddExpression(minName, takeVar), LiteralExpression(1)));

             List<StatementSyntax> sortBody = new List<StatementSyntax>();

             var sorterName = IdentifierName("sorter");

             sortBody.Add(ExpressionStatement(SimpleAssignmentExpression(indicesName,
                 ObjectCreationExpression(PooledListType(IntType), ArgumentList(elementsCount), null))));

             var loopVar = IdentifierName("i");

             sortBody.Add(ForStatement(loopVar, LiteralExpression(0), elementsCount, ExpressionStatement(
                 InvocationExpression(MemberAccessExpression(indicesName, AddMethod), ArgumentList(loopVar)))));

             sortBody.Add(LocalDeclarationStatement(sorterName.Identifier,
                 ObjectCreationExpression(QualifiedName(ResolvedClassName, IdentifierName("Sorter")),
                     ArgumentList(GetOrderArguments().Prepend(Argument(elementsName))), null)));

             sortBody.Add(ExpressionStatement(InvocationExpression(
                 MemberAccessExpression(sorterName, IdentifierName("PartialQuickSort")),
                 ArgumentList(MemberAccessExpression(indicesName, IdentifierName("Array")), LiteralExpression(0),
                     SubtractExpression(elementsCount, LiteralExpression(1)), minName, maxName))));

             sortBody.Add(ExpressionStatement(InvocationExpression(sorterName, DisposeMethod)));

             yield return IfStatement(GreaterOrEqualExpression(maxName, minName), Block(sortBody));
         }

         public IEnumerable<OrderMemberInfo> GetOrderMemberInfos()
         {
             if (UpstreamOrder != null)
             {
                 foreach (var member in UpstreamOrder.GetOrderMemberInfos())
                     yield return member;
             }

             if (WithStruct)
             {
                 yield return new OrderMemberInfo(
                     WithSelector ? TypeName("Selector") : null,
                     TypeName("Comparer"), KeyType, Id, Descending);
             }
             else
             {
                 yield return new OrderMemberInfo(
                     WithSelector ? SelectorInterfaceType : null,
                     ComparerInterfaceType, KeyType, Id, Descending);
             }
         }

        public override BlockSyntax RenderIteration(bool isLocal, SyntaxList<StatementSyntax> statements)
        {
            return base.RenderIteration(isLocal, statements);
        }

        protected override StatementSyntax? RenderMoveNext()
        {
            // return base.RenderMoveNext();
        }

        protected override ExpressionSyntax? RenderCurrent()
        {
            // return base.RenderCurrent();
        }


        //         private static readonly SyntaxTree SliceEnumeratorTemplate = CSharpSyntaxTree.ParseText(@"
//         {
//             using var iter = source.GetEnumerator();
//
//             // prepare temporary buffers
//             var elementBuffer = new PooledList<_Element_>(_Count_);
//
//             // load all elements
//             while (iter.MoveNext())
//                 elementBuffer.Add(iter.Current);
//
//             int min = skip;
//             int max = take == null ? elementBuffer.Count - 1 : min + take.Value - 1;
//
//             if (min > max)
//             {
//                 // in this case, it actually should not be enumerated at all
//                 elementBuffer.Dispose();
//                 return new Enumerator(new PooledList<int>(0), new PooledList<_Element_>(0), -1);
//             }
//
//             var indexBuffer = new PooledList<int>(elementBuffer.Count);
//
//             // initialize all indexes
//             for (int i = 0; i < elementBuffer.Count; ++i)
//                 indexBuffer.Add(i);
//
//             // initialize comparer with keys
//             var sorter = new Sorter(this, elementBuffer);
//
//             sorter.PartialQuickSort(indexBuffer.Array, 0, elementBuffer.Count - 1, min, max);
//
//             sorter.Dispose();
//
//             return new Enumerator(indexBuffer, elementBuffer, skip - 1);
//         }");
//
//         private static readonly SyntaxTree ComparerTemplate = CSharpSyntaxTree.ParseText(@"
//         internal struct Sorter : IComparer<int>, IDisposable
//         {
//             private _Enumerable_ source;
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public Sorter(_Enumerable_ source, PooledList<_Element_> elements)
//             {
//                 this.source = source;
//             }
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public int Compare(int x, int y)
//             {
//                 int result;
//             }
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public void Dispose()
//             {
//             }
//
//             public void PartialQuickSort(
//                 int[] indexesToSort, int left, int right, int min, int max)
//             {
//                 do
//                 {
//                     int mid = PartitionHoare(indexesToSort, left, right);
//
//                     if (left < mid && mid >= min)
//                         PartialQuickSort(indexesToSort, left, mid, min, max);
//
//                     left = mid + 1;
//
//                 } while (left < right && left <= max);
//             }
//
//             // Hoare partition scheme
//             // This implementation is faster when using struct comparer (more comparison and less copy)
//             private int PartitionHoare(int[] indexesToSort, int left, int right)
//             {
//                 // preventing overflow of the pivot
//                 int pivot = left + ((right - left) >> 1);
//                 int pivotIndex = indexesToSort[pivot];
//
//                 int i = left - 1;
//                 int j = right + 1;
//
//                 while (true)
//                 {
//                     // Move the left index to the right at least once and while the element at
//                     // the left index is less than the pivot
//                     while (Compare(indexesToSort[++i], pivotIndex) < 0) { }
//
//                     // Move the right index to the left at least once and while the element at
//                     // the right index is greater than the pivot
//                     while (Compare(indexesToSort[--j], pivotIndex) > 0) { }
//
//                     // If the indices crossed, return
//                     if (i >= j)
//                         return j;
//
//                     // Swap the elements at the left and right indices
//                     (indexesToSort[i], indexesToSort[j]) = (indexesToSort[j], indexesToSort[i]);
//                 }
//             }
//         }
// ");

//         private class SyntaxRewriter : CSharpSyntaxRewriter
//         {
//             private readonly OrderingOperation _operation;
//
//             public SyntaxRewriter(OrderingOperation operation)
//             {
//                 _operation = operation;
//             }
//
//             public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
//             {
//                 switch (node.Identifier.ValueText)
//                 {
//                     case "_Enumerable_":
//                         return _operation.ResolvedClassName;
//
//                     case "_Element_":
//                         return _operation.OutputElementType;
//
//                     case "_Count_":
//                         return _operation.IsCountable
//                             ? MemberAccessExpression(SourceVar, CountProperty)
//                             : LiteralExpression(0);
//                 }
//
//                 return base.VisitIdentifierName(node);
//             }
//
//             public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
//             {
//                 node = RenderComparerStruct(node);
//                 return base.VisitStructDeclaration(node);
//             }
//
//             public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
//             {
//                 node = RenderConstructor(node);
//                 return base.VisitConstructorDeclaration(node);
//             }
//
//             public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
//             {
//                 switch (node.Identifier.ValueText)
//                 {
//                     case "Compare":
//                         node = RenderCompareMethod(node);
//                         break;
//
//                     case "Dispose":
//                         node = RenderDisposeMethod(node);
//                         break;
//                 }
//
//                 return base.VisitMethodDeclaration(node);
//             }
//
//             private StructDeclarationSyntax RenderComparerStruct(StructDeclarationSyntax node)
//             {
//                 var list = new List<MemberDeclarationSyntax>();
//
//                 bool needElement = false;
//
//                 foreach (var member in _operation.GetOrderMemberInfos())
//                 {
//                     if (member.SelectorType == null)
//                     {
//                         needElement = true;
//                         continue;
//                     }
//
//                     list.Add(FieldDeclaration(
//                         default, PrivateTokenList, VariableDeclaration(
//                             GenericName(Identifier("PooledList"), TypeArgumentList(member.KeyType)),
//                             SingletonSeparatedList(VariableDeclarator(Identifier($"keys{member.Index}"))))));
//                 }
//
//                 if (needElement)
//                 {
//                     list.Add(FieldDeclaration(
//                         default, PrivateTokenList, VariableDeclaration(
//                             GenericName(Identifier("PooledList"), TypeArgumentList(_operation.OutputElementType)),
//                             SingletonSeparatedList(VariableDeclarator(Identifier("elements"))))));
//                 }
//
//                 return node.AddMembers(list.ToArray());
//             }
//
//             private ConstructorDeclarationSyntax RenderConstructor(ConstructorDeclarationSyntax node)
//             {
//                 var list = new List<StatementSyntax>();
//
//                 var elementIdentifier = IdentifierName("elements");
//                 var elementCount = MemberAccessExpression(elementIdentifier, CountProperty);
//
//                 bool needElement = false;
//
//                 foreach (var member in _operation.GetOrderMemberInfos())
//                 {
//                     if (member.SelectorType == null)
//                     {
//                         needElement = true;
//                         continue;
//                     }
//
//                     var keyIdentifier = IdentifierName($"keys{member.Index}");
//                     var selectorIdentifier = IdentifierName($"selector{member.Index}");
//
//                     list.Add(ExpressionStatement(SimpleAssignmentExpression(
//                         keyIdentifier, ObjectCreationExpression(
//                             GenericName(Identifier("PooledList"), TypeArgumentList(member.KeyType)),
//                             ArgumentList(elementCount), null))));
//
//                     list.Add(ForStatement(
//                         VariableDeclaration(IntType, SingletonSeparatedList(VariableDeclarator(
//                             IndexVar.Identifier, null, EqualsValueClause(LiteralExpression(0))))),
//                         default, LessThanExpression(IndexVar, elementCount),
//                         SingletonSeparatedList<ExpressionSyntax>(PreIncrementExpression(IndexVar)),
//                         ExpressionStatement(InvocationExpression(
//                             MemberAccessExpression(keyIdentifier, AddMethod),
//                             ArgumentList(InvocationExpression(
//                                 MemberAccessExpression(SourceVar, selectorIdentifier, InvokeMethod), ArgumentList(
//                                     ElementAccessExpression(elementIdentifier, IndexVar))))))));
//                 }
//
//                 if (needElement)
//                 {
//                     list.Add(ExpressionStatement(SimpleAssignmentExpression(
//                         MemberAccessExpression(ThisExpression(), elementIdentifier), elementIdentifier)));
//                 }
//
//                 return node.AddBodyStatements(list.ToArray());
//             }
//
//             private MethodDeclarationSyntax RenderCompareMethod(MethodDeclarationSyntax node)
//             {
//                 var list = new List<StatementSyntax>();
//
//                 var xVar = IdentifierName("x");
//                 var yVar = IdentifierName("y");
//                 var resultVar = IdentifierName("result");
//
//                 foreach (var member in _operation.GetOrderMemberInfos())
//                 {
//                     var keyIdentifier = member.SelectorType == null
//                         ? IdentifierName("elements")
//                         : IdentifierName($"keys{member.Index}");
//
//                     var comparerIdentifier = IdentifierName($"comparer{member.Index}");
//                     var descIdentifier = IdentifierName($"desc{member.Index}");
//
//                     list.Add(ExpressionStatement(SimpleAssignmentExpression(resultVar, InvocationExpression(
//                         MemberAccessExpression(SourceVar, comparerIdentifier, CompareMethod), ArgumentList(
//                             ElementAccessExpression(keyIdentifier, xVar),
//                             ElementAccessExpression(keyIdentifier, yVar))))));
//
//                     list.Add(IfStatement(NotEqualsExpression(resultVar, LiteralExpression(0)),
//                         ReturnStatement(ConditionalExpression(
//                             MemberAccessExpression(SourceVar, descIdentifier),
//                             MinusExpression(resultVar),
//                             resultVar))));
//                 }
//
//                 list.Add(ReturnStatement(SubtractExpression(xVar, yVar)));
//
//                 return node.AddBodyStatements(list.ToArray());
//             }
//
//             private MethodDeclarationSyntax RenderDisposeMethod(MethodDeclarationSyntax node)
//             {
//                 var list = new List<StatementSyntax>();
//
//                 foreach (var member in _operation.GetOrderMemberInfos())
//                 {
//                     if (member.SelectorType == null)
//                         continue;
//
//                     var keyIdentifier = IdentifierName($"keys{member.Index}");
//                     list.Add(ExpressionStatement(InvocationExpression(keyIdentifier, DisposeMethod)));
//                 }
//
//                 return node.AddBodyStatements(list.ToArray());
//             }
//         }
//
//
//         protected bool WithSelector { get; }
//         private bool WithStruct { get; }
//         private SyntaxRewriter Rewriter { get; }
//
//         public OrderingOperation(in LinqGenExpression expression, int id,
//             INamedTypeSymbol? selectorType, bool withStruct) : base(expression, id)
//         {
//             WithStruct = withStruct;
//
//             if (selectorType != null)
//             {
//                 WithSelector = true;
//                 SelectorInterfaceType = ParseTypeName(selectorType);
//                 SelectorKeyType = ParseTypeName(selectorType.TypeArguments[1]);
//             }
//             else
//             {
//                 WithSelector = false;
//                 SelectorInterfaceType = null;
//                 SelectorKeyType = null;
//             }
//
//             Rewriter = new SyntaxRewriter(this);
//         }
//
//         private TypeSyntax? SelectorInterfaceType { get; }
//         private TypeSyntax? SelectorKeyType { get; }
//
//         private TypeSyntax KeyType => WithSelector ? SelectorKeyType! : OutputElementType;
//         private TypeSyntax ComparerInterfaceType =>
//             GenericName(Identifier("IComparer"), TypeArgumentList(KeyType));
//
//         public override TypeSyntax InterfaceType =>
//             GenericName(Identifier("IInternalOrderedStub"), TypeArgumentList(OutputElementType));
//
//         public int Depth => UpstreamOrder == null ? 1 : UpstreamOrder.Depth + 1;
//
//         private TypeSyntax SourceEnumerableType =>
//             UpstreamOrder == null ? UpstreamResolvedClassName : UpstreamOrder.SourceEnumerableType;
//
//         public override bool IsCountable => Upstream!.IsCountable;
//         public override bool IsPartition => true;
//
//         protected OrderingOperation? UpstreamOrder { get; set; }
//
//         public override void SetUpstream(Generation upstream)
//         {
//             UpstreamOrder = upstream as OrderingOperation;
//             base.SetUpstream(upstream);
//         }
//
//         private IdentifierNameSyntax IndexBufferVar { get; } = IdentifierName("indexBuffer");
//         private IdentifierNameSyntax ElementBufferVar { get; } = IdentifierName("elementBuffer");
//
//         protected override IEnumerable<TypeParameterInfo> GetTypeParameterInfos()
//         {
//             if (WithStruct)
//             {
//                 if (WithSelector)
//                 {
//                     yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}1"),
//                         TypeConstraint(SelectorInterfaceType!));
//
//                     yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}2"),
//                         TypeConstraint(ComparerInterfaceType));
//                 }
//                 else
//                 {
//                     // here struct constraint is necessary for type inference
//                     yield return new TypeParameterInfo(IdentifierName($"{TypeParameterPrefix}2"),
//                         ClassOrStructConstraint(SyntaxKind.StructConstraint), TypeConstraint(ComparerInterfaceType));
//                 }
//             }
//         }
//
//         public override ConstructorDeclarationSyntax RenderEnumerableConstructor()
//         {
//             var syntax = base.RenderEnumerableConstructor();
//
//             if (!WithStruct)
//             {
//                 var block = syntax.Body!;
//                 var comparerVar = IdentifierName($"comparer{Depth}");
//
//                 // Comparer<T>.Default if null
//                 var statements = block.Statements.Insert(0,
//                     ExpressionStatement(SimpleAssignmentExpression(comparerVar,
//                         NullCoalesce(comparerVar, ComparerDefault(Upstream!.OutputElementType)))));
//
//                 syntax = syntax.WithBody(block.WithStatements(statements));
//             }
//
//             return syntax;
//         }
//
//         public override BlockSyntax RenderGetEnumeratorBody()
//         {
//             return Block(ReturnStatement(
//                 InvocationExpression(GetSliceEnumeratorMethod, ArgumentList(LiteralExpression(0), NullLiteral))));
//         }
//
//         public sealed override IEnumerable<MemberInfo> GetMemberInfos()
//         {
//             yield return new MemberInfo(MemberKind.Enumerable, SourceEnumerableType, SourceVar);
//
//             int depth = Depth;
//
//             foreach (var member in GetOrderMemberInfos())
//             {
//                 if (member.SelectorType != null)
//                 {
//                     yield return new MemberInfo(MemberKind.Enumerable,
//                         member.SelectorType, IdentifierName($"selector{member.Index}"));
//                 }
//
//                 yield return new MemberInfo(MemberKind.Enumerable,
//                     member.ComparerType, IdentifierName($"comparer{member.Index}"),
//                     WithStruct || depth != member.Index ? null : NullLiteral);
//
//                 yield return new MemberInfo(MemberKind.Enumerable,
//                     BoolType, IdentifierName($"desc{member.Index}"));
//             }
//
//             yield return new MemberInfo(MemberKind.Enumerator,
//                GenericName(Identifier("PooledList"), TypeArgumentList(IntType)), IndexBufferVar);
//
//             yield return new MemberInfo(MemberKind.Enumerator,
//                GenericName(Identifier("PooledList"), TypeArgumentList(OutputElementType)), ElementBufferVar);
//
//             yield return new MemberInfo(MemberKind.Enumerator, IntType, IndexVar);
//         }
//
//         protected IEnumerable<OrderMemberInfo> GetOrderMemberInfos()
//         {
//             if (UpstreamOrder != null)
//             {
//                 foreach (var member in UpstreamOrder.GetOrderMemberInfos())
//                     yield return member;
//             }
//
//             int depth = Depth;
//
//             if (WithStruct)
//             {
//                 yield return new OrderMemberInfo(
//                     WithSelector ? IdentifierName($"{TypeParameterPrefix}1") : null,
//                     IdentifierName($"{TypeParameterPrefix}2"),
//                     KeyType, depth);
//             }
//             else
//             {
//                 yield return new OrderMemberInfo(
//                     WithSelector ? SelectorInterfaceType : null,
//                     ComparerInterfaceType, KeyType, depth);
//             }
//         }
//
//         public override IEnumerable<MemberDeclarationSyntax> RenderAdditionalMembers()
//         {
//             foreach (var member in base.RenderAdditionalMembers())
//                 yield return member;
//
//             var structDeclaration =
//                 ComparerTemplate.GetRoot().DescendantNodes().OfType<StructDeclarationSyntax>().First();
//
//             yield return (StructDeclarationSyntax)Rewriter.VisitStructDeclaration(structDeclaration)!;
//         }
//
//         public override BlockSyntax RenderGetSliceEnumeratorBody()
//         {
//             var block = SliceEnumeratorTemplate.GetRoot().DescendantNodes().OfType<BlockSyntax>().First();
//             block = (BlockSyntax)Rewriter.VisitBlock(block)!;
//
//             return block;
//         }
//
//         public override ConstructorDeclarationSyntax RenderEnumeratorConstructor()
//         {
//             var parameters = GetParameters(MemberKind.Enumerator);
//             var assignments = GetAssignments(MemberKind.Enumerator);
//
//             return ConstructorDeclaration(new(AggressiveInliningAttributeList),
//                 InternalTokenList, Identifier("Enumerator"), ParameterList(parameters),
//                 ThisInitializer, Block(assignments));
//         }
//
//         public override BlockSyntax RenderMoveNextBody()
//         {
//             return Block(ReturnStatement(LessThanExpression(
//                 PreIncrementExpression(IndexVar),
//                 MemberAccessExpression(IndexBufferVar, CountProperty))));
//         }
//
//         public override BlockSyntax RenderCurrentGetBody()
//         {
//             return Block(ReturnStatement(ElementAccessExpression(
//                 ElementBufferVar, ElementAccessExpression(IndexBufferVar, IndexVar))));
//         }
//
//         public override BlockSyntax RenderDisposeBody()
//         {
//             return Block(
//                 ExpressionStatement(InvocationExpression(IndexBufferVar, DisposeMethod)),
//                 ExpressionStatement(InvocationExpression(ElementBufferVar, DisposeMethod)));
//         }
    }
}