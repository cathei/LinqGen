// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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
        private const string LinqGenStubExtensionsTypeName = "StubExtensions";

        private const string LinqGenStubEnumerableTypeName = "Stub`2";
        private const string LinqGenBoxedStubEnumerableTypeName = "BoxedStub`2";
        private const string LinqGenStubInterfaceTypeName = "IStub`2";
        private const string LinqGenOrderedStubEnumerableTypeName = "OrderedStub`2";
        private const string LinqGenOrderedStubInterfaceTypeName = "IOrderedStub`2";
        private const string LinqGenStructFunctionTypeName = "IStructFunction";

        private const string SystemNamespace = "System";
        private const string SystemCollectionsGenericNamespace = "System.Collections.Generic";

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
                   symbol.MetadataName is LinqGenStubEnumerableTypeName or
                       LinqGenBoxedStubEnumerableTypeName or
                       LinqGenOrderedStubEnumerableTypeName;
        }

        public static bool IsInputStubEnumerable(INamedTypeSymbol symbol)
        {
            // is input parameter defined for method is stub interface or stub enumerable?
            return symbol.ContainingAssembly.Name == LinqGenAssemblyName &&
                   symbol.MetadataName is LinqGenStubInterfaceTypeName or
                       LinqGenStubEnumerableTypeName or
                       LinqGenOrderedStubInterfaceTypeName;
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
        public static readonly PredefinedTypeSyntax UIntType = PredefinedType(Token(SyntaxKind.UIntKeyword));
        public static readonly PredefinedTypeSyntax BoolType = PredefinedType(Token(SyntaxKind.BoolKeyword));
        public static readonly IdentifierNameSyntax EnumeratorType = IdentifierName("Enumerator");

        // known method names
        public static readonly IdentifierNameSyntax InvokeMethod = IdentifierName("Invoke");
        public static readonly IdentifierNameSyntax MoveNextMethod = IdentifierName("MoveNext");
        public static readonly IdentifierNameSyntax DisposeMethod = IdentifierName("Dispose");
        public static readonly IdentifierNameSyntax GetEnumeratorMethod = IdentifierName("GetEnumerator");
        public static readonly IdentifierNameSyntax GetSliceEnumeratorMethod = IdentifierName("GetSliceEnumerator");
        public static readonly IdentifierNameSyntax AddMethod = IdentifierName("Add");
        public static readonly IdentifierNameSyntax AddRangeMethod = IdentifierName("AddRange");
        public static readonly IdentifierNameSyntax CompareMethod = IdentifierName("Compare");
        public static readonly IdentifierNameSyntax CompareToMethod = IdentifierName("CompareTo");

        // known property names
        public static readonly IdentifierNameSyntax CurrentProperty = IdentifierName("Current");
        public static readonly IdentifierNameSyntax CountProperty = IdentifierName("Count");
        public static readonly IdentifierNameSyntax LengthProperty = IdentifierName("Length");
        public static readonly IdentifierNameSyntax HasValueProperty = IdentifierName("HasValue");
        public static readonly IdentifierNameSyntax ValueProperty = IdentifierName("Value");

        // custom variable names
        // public static readonly IdentifierNameSyntax SourceVar = IdentifierName("source");
        // public static readonly IdentifierNameSyntax IteratorVar = IdentifierName("iter");
        // public static readonly IdentifierNameSyntax IndexVar = IdentifierName("index");
        // public static readonly IdentifierNameSyntax ResultVar = IdentifierName("result");
        // public static readonly IdentifierNameSyntax SelectorVar = IdentifierName("select");
        // public static readonly IdentifierNameSyntax PredicateVar = IdentifierName("predicate");
        // public static readonly IdentifierNameSyntax ComparerVar = IdentifierName("comparer");
        // public static readonly IdentifierNameSyntax InitialValueVar = IdentifierName("initialValue");
        // public static readonly IdentifierNameSyntax SkipVar = IdentifierName("skip");
        // public static readonly IdentifierNameSyntax TakeVar = IdentifierName("take");
        // public static readonly IdentifierNameSyntax ValueVar = IdentifierName("value");
        // public static readonly IdentifierNameSyntax HashSetVar = IdentifierName("hashSet");
        // public static readonly IdentifierNameSyntax ListVar = IdentifierName("list");

        public static readonly TypeSyntax VarType = IdentifierName("var");
        public static readonly TypeSyntax ObjectType = IdentifierName("object");

        public static readonly LiteralExpressionSyntax DefaultLiteral =
            SyntaxFactory.LiteralExpression(SyntaxKind.DefaultLiteralExpression);
        public static readonly LiteralExpressionSyntax NullLiteral =
            SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression);

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

        public static ElementAccessExpressionSyntax ElementAccessExpression(
            ExpressionSyntax expression, ExpressionSyntax index)
        {
            return SyntaxFactory.ElementAccessExpression(expression, BracketedArgumentList(index));
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

        public static ParameterSyntax Parameter(TypeSyntax type, SyntaxToken identifier,
            ExpressionSyntax? defaultValue = null)
        {
            return SyntaxFactory.Parameter(default, default, type, identifier,
                defaultValue == null ? default : EqualsValueClause(defaultValue));
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
                    IdentifierName("ExceptionUtils"),
                    IdentifierName("ThrowInvalidOperation"))),
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

        public static PrefixUnaryExpressionSyntax PreDecrementExpression(ExpressionSyntax operand)
        {
            return SyntaxFactory.PrefixUnaryExpression(SyntaxKind.PreDecrementExpression, operand);
        }

        public static PrefixUnaryExpressionSyntax LogicalNotExpression(ExpressionSyntax operand)
        {
            return SyntaxFactory.PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, operand);
        }

        public static PrefixUnaryExpressionSyntax MinusExpression(ExpressionSyntax operand)
        {
            return SyntaxFactory.PrefixUnaryExpression(SyntaxKind.UnaryMinusExpression, operand);
        }

        public static BinaryExpressionSyntax LessThanExpression(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.LessThanExpression, left, right);
        }

        public static BinaryExpressionSyntax GreaterOrEqualExpression(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.GreaterThanOrEqualExpression, left, right);
        }

        public static BinaryExpressionSyntax NotEqualsExpression(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.NotEqualsExpression, left, right);
        }

        public static BinaryExpressionSyntax AddExpression(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.AddExpression, left, right);
        }

        public static BinaryExpressionSyntax SubtractExpression(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.SubtractExpression, left, right);
        }

        public static BinaryExpressionSyntax NullCoalesce(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.CoalesceExpression, left, right);
        }

        public static BinaryExpressionSyntax IsExpression(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.IsExpression, left, right);
        }


        public static ForStatementSyntax ForStatement(
            IdentifierNameSyntax loopVariable, ExpressionSyntax start, ExpressionSyntax end, StatementSyntax body)
        {
            return SyntaxFactory.ForStatement(
                SyntaxFactory.VariableDeclaration(IntType, SingletonSeparatedList(VariableDeclarator(
                    loopVariable.Identifier, null, EqualsValueClause(start)))),
                default, LessThanExpression(loopVariable, end),
                SingletonSeparatedList<ExpressionSyntax>(PreIncrementExpression(loopVariable)),
                body);
        }

        public static ExpressionSyntax MathMin(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.InvocationExpression(
                MemberAccessExpression(IdentifierName("Math"), IdentifierName("Min")),
                ArgumentList(left, right));
        }

        public static ExpressionSyntax MathMax(ExpressionSyntax left, ExpressionSyntax right)
        {
            return SyntaxFactory.InvocationExpression(
                MemberAccessExpression(IdentifierName("Math"), IdentifierName("Max")),
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

        public static MemberAccessExpressionSyntax ComparerDefault(TypeSyntax type)
        {
            return MemberAccessExpression(
                GenericName(Identifier("Comparer"), TypeArgumentList(type)), IdentifierName("Default"));
        }

        public static MemberAccessExpressionSyntax EqualityComparerDefault(TypeSyntax type)
        {
            return MemberAccessExpression(
                GenericName(Identifier("EqualityComparer"), TypeArgumentList(type)), IdentifierName("Default"));
        }

        private static bool CompareNamespace(ITypeSymbol symbol, string name)
        {
            return symbol.ContainingNamespace.ToDisplayString() == name;
        }

        private static INamedTypeSymbol? GetInterface(
            ITypeSymbol symbol, string namespaceName, string interfaceMetadataName)
        {
            if (symbol.MetadataName == interfaceMetadataName && CompareNamespace(symbol, namespaceName))
                return (INamedTypeSymbol)symbol;

            return symbol.AllInterfaces.FirstOrDefault(x =>
                x.MetadataName == interfaceMetadataName && CompareNamespace(symbol, namespaceName));
        }

        public static bool TryGetGenericListInterface(ITypeSymbol symbol, out INamedTypeSymbol interfaceSymbol)
        {
            interfaceSymbol = GetInterface(symbol, SystemCollectionsGenericNamespace, "IList`1")!;
            return interfaceSymbol != null!;
        }

        public static bool TryGetGenericEnumerableInterface(ITypeSymbol symbol, out INamedTypeSymbol interfaceSymbol)
        {
            interfaceSymbol = GetInterface(symbol, SystemCollectionsGenericNamespace, "IEnumerable`1")!;
            return interfaceSymbol != null!;
        }

        public static bool TryGetGenericCollectionInterface(ITypeSymbol symbol, out INamedTypeSymbol interfaceSymbol)
        {
            interfaceSymbol = GetInterface(symbol, SystemCollectionsGenericNamespace, "ICollection`1")!;
            return interfaceSymbol != null!;
        }

        public static bool TryGetComparableSelfInterface(ITypeSymbol symbol, out INamedTypeSymbol interfaceSymbol)
        {
            interfaceSymbol = symbol.AllInterfaces.FirstOrDefault(x =>
                x.MetadataName == "IComparable`1" && CompareNamespace(x, SystemNamespace) &&
                SymbolEqualityComparer.Default.Equals(x.TypeArguments[0], symbol))!;

            return interfaceSymbol != null!;
        }

        public static ITypeSymbol GetEnumeratorSymbol(INamedTypeSymbol enumerableSymbol)
        {
            // find GetEnumerator with same rule as C# duck typing
            // TODO fallback to interface implementation
            return enumerableSymbol.GetMembers()
                .OfType<IMethodSymbol>()
                .First(x =>
                    x.DeclaredAccessibility == Accessibility.Public &&
                    x.Name == "GetEnumerator" && x.Parameters.Length == 0 && x.TypeParameters.Length == 0)
                .ReturnType;
        }

        public static INamedTypeSymbol NormalizeSignature(INamedTypeSymbol signature)
        {
            if (signature.Arity == 0)
                return signature;

            var typeArgs = signature.TypeArguments;
            var newTypeArgs = new ITypeSymbol[typeArgs.Length];

            var constructedFromArgs = signature.ConstructedFrom.TypeArguments;

            for (int i = 0; i < typeArgs.Length; i++)
            {
                // any ITypeParameterSymbol should be replaced
                if (typeArgs[i] is ITypeParameterSymbol)
                {
                    newTypeArgs[i] = constructedFromArgs[i];
                    continue;
                }

                if (typeArgs[i] is IArrayTypeSymbol arraySymbol &&
                    arraySymbol.ElementType is ITypeParameterSymbol)
                {
                    newTypeArgs[i] = arraySymbol.OriginalDefinition;
                    continue;
                }

                if (typeArgs[i] is INamedTypeSymbol namedTypeSymbol)
                {
                    newTypeArgs[i] = NormalizeSignature(namedTypeSymbol);
                    continue;
                }

                newTypeArgs[i] = typeArgs[i];
            }

            return signature.ConstructedFrom.Construct(newTypeArgs);
        }
    }
}