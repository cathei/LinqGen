// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cathei.LinqGen.Hidden;
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
        private const string LinqGenStructFunctionTypeName = "IStructFunction";

        public static bool IsStubMethod(IMethodSymbol symbol)
        {
            // is it member of extension class or member of stub enumerable?
            return symbol.ContainingAssembly.Name == LinqGenAssemblyName &&
                   symbol.ContainingType.MetadataName is LinqGenStubExtensionsTypeName or LinqGenStubEnumerableTypeName;
        }

        public static bool IsOutputStubEnumerable(INamedTypeSymbol symbol)
        {
            // is return type defined for method is stub enumerable or boxed IEnumerable?
            return symbol.ContainingAssembly.Name == LinqGenAssemblyName &&
                   symbol.MetadataName is LinqGenStubEnumerableTypeName or LinqGenBoxedStubEnumerableTypeName;
        }

        public static bool IsInputStubEnumerable(INamedTypeSymbol symbol)
        {
            // is input parameter defined for method is stub interface or stub enumerable?
            return symbol.ContainingAssembly.Name == LinqGenAssemblyName &&
                   symbol.MetadataName is LinqGenStubInterfaceTypeName or LinqGenStubEnumerableTypeName;
        }

        public static bool IsCountable(INamedTypeSymbol symbol)
        {
            return symbol.ContainingAssembly.Name == LinqGenAssemblyName &&
                   symbol.MetadataName == "ICountable";
        }

        public static bool IsPartition(INamedTypeSymbol symbol)
        {
            return symbol.ContainingAssembly.Name == LinqGenAssemblyName &&
                   symbol.MetadataName == "IPartition`1";
        }

        public static bool IsStructFunction(ITypeSymbol symbol)
        {
            return symbol.ContainingAssembly.Name == LinqGenAssemblyName &&
                   symbol is INamedTypeSymbol { Name: LinqGenStructFunctionTypeName };
        }

        public static bool TryParseStubInterface(INamedTypeSymbol symbol,
            out ITypeSymbol inputElementSymbol, out INamedTypeSymbol signatureSymbol)
        {
            // generic signature type should not be allowed
            // receiver type is: IStub<IContent<T>, TSignature>
            if (symbol.TypeArguments.Length < 2 ||
                symbol.TypeArguments[0] is not INamedTypeSymbol contentTypeSymbol ||
                symbol.TypeArguments[1] is not INamedTypeSymbol resultSignatureSymbol ||
                contentTypeSymbol.TypeArguments.Length < 1)
            {
                inputElementSymbol = default!;
                signatureSymbol = default!;
                return false;
            }

            inputElementSymbol = contentTypeSymbol.TypeArguments[0];
            signatureSymbol = resultSignatureSymbol;
            return true;
        }

        // known type names
        public static readonly PredefinedTypeSyntax IntType = PredefinedType(Token(SyntaxKind.IntKeyword));
        public static readonly IdentifierNameSyntax EnumeratorName = IdentifierName("Enumerator");

        // known method names
        public static readonly IdentifierNameSyntax InvokeName = IdentifierName("Invoke");
        public static readonly IdentifierNameSyntax MoveNextName = IdentifierName("MoveNext");
        public static readonly IdentifierNameSyntax DisposeName = IdentifierName("Dispose");
        public static readonly IdentifierNameSyntax GetEnumeratorName = IdentifierName("GetEnumerator");
        public static readonly IdentifierNameSyntax GetSliceEnumeratorName = IdentifierName("GetSliceEnumerator");

        // known property names
        public static readonly IdentifierNameSyntax CurrentName = IdentifierName("Current");
        public static readonly IdentifierNameSyntax CountName = IdentifierName("Count");
        public static readonly IdentifierNameSyntax LengthName = IdentifierName("Length");

        // custom variable names
        public static readonly IdentifierNameSyntax ParentName = IdentifierName("parent");
        public static readonly IdentifierNameSyntax SourceName = IdentifierName("source");
        public static readonly IdentifierNameSyntax IteratorName = IdentifierName("iter");
        public static readonly IdentifierNameSyntax IndexName = IdentifierName("index");
        public static readonly IdentifierNameSyntax SelectorName = IdentifierName("select");
        public static readonly IdentifierNameSyntax PredicateName = IdentifierName("predicate");
        public static readonly IdentifierNameSyntax InitialValueName = IdentifierName("initialValue");
        public static readonly IdentifierNameSyntax SkipName = IdentifierName("skip");
        public static readonly IdentifierNameSyntax TakeName = IdentifierName("take");

        public static readonly TypeSyntax VarType = IdentifierName("var");
        public static readonly TypeSyntax ObjectType = IdentifierName("object");

        public static readonly LiteralExpressionSyntax DefaultLiteral =
            SyntaxFactory.LiteralExpression(SyntaxKind.DefaultLiteralExpression);

        public static readonly SyntaxToken UsingKeywordToken = Token(SyntaxKind.UsingKeyword);
        public static readonly SyntaxToken SemicolonToken = Token(SyntaxKind.SemicolonToken);

        public static readonly SyntaxTokenList ThisTokenList = TokenList(Token(SyntaxKind.ThisKeyword));
        public static readonly SyntaxTokenList PrivateTokenList = TokenList(Token(SyntaxKind.PrivateKeyword));
        public static readonly SyntaxTokenList PublicTokenList = TokenList(Token(SyntaxKind.PublicKeyword));
        public static readonly SyntaxTokenList InternalTokenList = TokenList(Token(SyntaxKind.InternalKeyword));

        public static readonly SyntaxTokenList PrivateReadOnlyTokenList =
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword));
        public static readonly SyntaxTokenList PublicStaticTokenList =
            TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword));

        public static readonly ArgumentListSyntax EmptyArgumentList = SyntaxFactory.ArgumentList();

        public static readonly ConstructorInitializerSyntax ThisInitializer =
            ConstructorInitializer(SyntaxKind.ThisConstructorInitializer);

        public static readonly AttributeListSyntax AggressiveInliningAttributeList =
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("MethodImpl"),
                    AttributeArgumentList(SingletonSeparatedList(AttributeArgument(
                        MemberAccessExpression(IdentifierName("MethodImplOptions"),
                            IdentifierName("AggressiveInlining"))))))));

        public static TypeSyntax ParseTypeName(ITypeSymbol typeSymbol)
        {
            return SyntaxFactory.ParseTypeName(typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        }

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

        public static ArgumentListSyntax ArgumentList(params ExpressionSyntax[] expression)
        {
            return SyntaxFactory.ArgumentList(SeparatedList(expression.Select(Argument)));
        }

        public static ArgumentListSyntax ArgumentList(IEnumerable<ArgumentSyntax> arguments)
        {
            return SyntaxFactory.ArgumentList(SeparatedList(arguments));
        }

        public static ParameterListSyntax ParameterList(IEnumerable<ParameterSyntax> parameters)
        {
            return SyntaxFactory.ParameterList(SeparatedList(parameters));
        }

        public static ParameterListSyntax ParameterList(params ParameterSyntax[] parameters)
        {
            return SyntaxFactory.ParameterList(SeparatedList(parameters));
        }

        public static ParameterSyntax Parameter(TypeSyntax type, SyntaxToken identifier)
        {
            return SyntaxFactory.Parameter(default, default, type, identifier, default);
        }

        public static BracketedArgumentListSyntax BracketedArgumentList(ExpressionSyntax expression)
        {
            return SyntaxFactory.BracketedArgumentList(SingletonSeparatedList(Argument(expression)));
        }

        public static TypeArgumentListSyntax TypeArgumentList(params TypeSyntax[] arguments)
        {
            return SyntaxFactory.TypeArgumentList(SeparatedList(arguments));
        }

        public static NameSyntax MakeGenericName(NameSyntax name, TypeArgumentListSyntax arguments)
        {
            return name switch
            {
                QualifiedNameSyntax qualifiedName =>
                    qualifiedName.WithRight(GenericName(qualifiedName.Right.Identifier, arguments)),
                GenericNameSyntax genericName => genericName.WithTypeArgumentList(arguments),
                SimpleNameSyntax simpleName => GenericName(simpleName.Identifier, arguments),
                _ => name
            };
        }

        public static LocalDeclarationStatementSyntax UsingLocalDeclarationStatement(
            SyntaxToken identifier, ExpressionSyntax initialValue)
        {
            return SyntaxFactory.LocalDeclarationStatement(default, UsingKeywordToken, default,
                VariableDeclaration(identifier, initialValue), SemicolonToken);
        }

        public static LocalDeclarationStatementSyntax LocalDeclarationStatement(
            SyntaxToken identifier, ExpressionSyntax initialValue)
        {
            return SyntaxFactory.LocalDeclarationStatement(
                VariableDeclaration(identifier, initialValue));
        }

        public static VariableDeclarationSyntax VariableDeclaration(
            SyntaxToken identifier, ExpressionSyntax initialValue)
        {
            return SyntaxFactory.VariableDeclaration(VarType, SingletonSeparatedList(
                VariableDeclarator(identifier, default, EqualsValueClause(initialValue))));
        }

        public static ReturnStatementSyntax ReturnDefaultStatement()
        {
            return SyntaxFactory.ReturnStatement(DefaultLiteral);
        }

        public static StatementSyntax ThrowInvalidOperationStatement()
        {
            return Block(
                ExpressionStatement(InvocationExpression(
                    IdentifierName(nameof(ExceptionUtils)),
                    IdentifierName(nameof(ExceptionUtils.ThrowInvalidOperation)))),
                ReturnDefaultStatement());
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

        public static BinaryExpressionSyntax GreaterOrEqualExpression(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.GreaterThanOrEqualExpression, left, right);
        }

        public static BinaryExpressionSyntax SubtractExpression(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.SubtractExpression, left, right);
        }

        public static BinaryExpressionSyntax IsExpression(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.IsExpression, left, right);
        }

        public static ExpressionSyntax MathMin(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.InvocationExpression(
                MemberAccessExpression(IdentifierName("Math"), IdentifierName("Min")),
                ArgumentList(left, right));
        }

        public static ExpressionSyntax TrueExpression()
        {
            return SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression);
        }

        public static ExpressionSyntax FalseExpression()
        {
            return SyntaxFactory.LiteralExpression(SyntaxKind.FalseLiteralExpression);
        }

        private static INamedTypeSymbol? GetInterface(
            ITypeSymbol symbol, string assemblyName, string interfaceMetadataName)
        {
            // check symbol itself first
            if (symbol.ContainingAssembly?.Name == assemblyName && symbol.MetadataName == interfaceMetadataName)
                return (INamedTypeSymbol)symbol;

            return symbol.AllInterfaces.FirstOrDefault(x =>
                x.ContainingAssembly.Name == assemblyName && x.MetadataName == interfaceMetadataName);
        }

        public static bool TryGetGenericListInterface(ITypeSymbol symbol, out INamedTypeSymbol interfaceSymbol)
        {
            interfaceSymbol = GetInterface(symbol, "System.Runtime", "IList`1")!;
            return interfaceSymbol != null!;
        }

        public static bool TryGetGenericEnumerableInterface(ITypeSymbol symbol, out INamedTypeSymbol interfaceSymbol)
        {
            interfaceSymbol = GetInterface(symbol, "System.Runtime", "IEnumerable`1")!;
            return interfaceSymbol != null!;
        }

        public static bool TryGetGenericCollectionInterface(ITypeSymbol symbol, out INamedTypeSymbol interfaceSymbol)
        {
            interfaceSymbol = GetInterface(symbol, "System.Runtime", "ICollection`1")!;
            return interfaceSymbol != null!;
        }
    }
}