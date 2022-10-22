// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;

    public static class CodeGenUtils
    {
        private const string LinqGenAssemblyName = "LinqGen";
        private const string LinqGenStubExtensionsTypeName = nameof(StubExtensions);

        private const string LinqGenStubEnumerableTypeName = "Stub`2";
        private const string LinqGenBoxedStubEnumerableTypeName = "BoxedStub`2";
        private const string LinqGenStubInterfaceTypeName = "IStub`2";

        private static bool IsMethodDefinedIn(IMethodSymbol symbol,
            string assemblyName, string containingTypeName)
        {
            return symbol.ContainingAssembly.Name == assemblyName &&
                   symbol.ContainingType.MetadataName == containingTypeName;
        }

        private static bool IsMethodDefinedAs(IMethodSymbol symbol,
            string assemblyName, string containingTypeName, string methodName)
        {
            return IsMethodDefinedIn(symbol, assemblyName, containingTypeName) &&
                   symbol.MetadataName == methodName;
        }

        public static bool IsStubMethod(IMethodSymbol symbol)
        {
            return IsMethodDefinedIn(symbol, LinqGenAssemblyName, LinqGenStubExtensionsTypeName);
        }

        public static bool IsStubEnumerable(INamedTypeSymbol symbol)
        {
            return symbol.ContainingAssembly.Name == LinqGenAssemblyName &&
                   symbol.MetadataName is LinqGenStubEnumerableTypeName or LinqGenBoxedStubEnumerableTypeName;
        }

        public static bool IsStubInterface(INamedTypeSymbol symbol)
        {
            return symbol.ContainingAssembly.Name == LinqGenAssemblyName &&
                   symbol.MetadataName == LinqGenStubInterfaceTypeName;
        }

        // known predefined type names
        public static readonly PredefinedTypeSyntax IntType = PredefinedType(Token(SyntaxKind.IntKeyword));

        // known generic interface names
        public static readonly GenericNameSyntax EnumerableInterfaceName = GenericName("IEnumerable");
        public static readonly GenericNameSyntax EnumeratorInterfaceName = GenericName("IEnumerator");
        public static readonly GenericNameSyntax ListInterfaceName = GenericName("IList");

        // known method names
        public static readonly IdentifierNameSyntax InvokeName = IdentifierName("Invoke");
        public static readonly IdentifierNameSyntax MoveNextName = IdentifierName("MoveNext");
        public static readonly IdentifierNameSyntax DisposeName = IdentifierName("Dispose");
        public static readonly IdentifierNameSyntax GetEnumeratorName = IdentifierName("GetEnumerator");

        // known property names
        public static readonly IdentifierNameSyntax CurrentName = IdentifierName("Current");
        public static readonly IdentifierNameSyntax CountName = IdentifierName("Count");

        // custom variable names
        public static readonly IdentifierNameSyntax ParentName = IdentifierName("parent");
        public static readonly IdentifierNameSyntax SourceName = IdentifierName("source");
        public static readonly IdentifierNameSyntax IndexName = IdentifierName("index");
        public static readonly IdentifierNameSyntax SelectorName = IdentifierName("select");
        public static readonly IdentifierNameSyntax PredicateName = IdentifierName("predicate");

        public static readonly SyntaxTokenList ThisTokenList = TokenList(Token(SyntaxKind.ThisKeyword));
        public static readonly SyntaxTokenList PrivateTokenList = TokenList(Token(SyntaxKind.PrivateKeyword));
        public static readonly SyntaxTokenList PrivateReadOnlyTokenList =
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword));

        public static InvocationExpressionSyntax InvocationExpression(
            ExpressionSyntax expression, IdentifierNameSyntax name)
        {
            return SyntaxFactory.InvocationExpression(MemberAccessExpression(expression, name));
        }

        public static InvocationExpressionSyntax InvocationExpression(
            ExpressionSyntax expression, IdentifierNameSyntax name1, IdentifierNameSyntax name2)
        {
            return SyntaxFactory.InvocationExpression(MemberAccessExpression(expression, name1, name2));
        }

        public static MemberAccessExpressionSyntax MemberAccessExpression(
            ExpressionSyntax expression, IdentifierNameSyntax name)
        {
            return SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expression, name);
        }

        public static MemberAccessExpressionSyntax MemberAccessExpression(
            ExpressionSyntax expression, IdentifierNameSyntax name1, IdentifierNameSyntax name2)
        {
            return SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                MemberAccessExpression(expression, name1), name2);
        }

        public static AssignmentExpressionSyntax SimpleAssignmentExpression(
            ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, left, right);
        }

        public static ArgumentListSyntax ArgumentList(ExpressionSyntax expression)
        {
            return SyntaxFactory.ArgumentList(SingletonSeparatedList(Argument(expression)));
        }

        public static ArgumentListSyntax ArgumentList(IEnumerable<ArgumentSyntax> arguments)
        {
            return SyntaxFactory.ArgumentList(SeparatedList(arguments));
        }

        public static ParameterListSyntax ParameterList(IEnumerable<ParameterSyntax> parameters)
        {
            return SyntaxFactory.ParameterList(SeparatedList(parameters));
        }

        public static BracketedArgumentListSyntax BracketedArgumentList(ExpressionSyntax expression)
        {
            return SyntaxFactory.BracketedArgumentList(SingletonSeparatedList(Argument(expression)));
        }

        public static LiteralExpressionSyntax LiteralExpression(int value)
        {
            if (value < 0)
                SyntaxFactory.PrefixUnaryExpression(SyntaxKind.UnaryMinusExpression, LiteralExpression(-value));
            return SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(value));
        }

        public static PrefixUnaryExpressionSyntax PreIncrementExpression(ExpressionSyntax operand)
        {
            return SyntaxFactory.PrefixUnaryExpression(SyntaxKind.PreIncrementExpression, operand);
        }

        public static PrefixUnaryExpressionSyntax LogicalNotExpression(ExpressionSyntax operand)
        {
            return SyntaxFactory.PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, operand);
        }

        public static BinaryExpressionSyntax LessThanExpression(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.LessThanExpression, left, right);
        }

        public static ExpressionSyntax TrueExpression()
        {
            return SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression);
        }

        public static ExpressionSyntax FalseExpression()
        {
            return SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression);
        }
    }
}