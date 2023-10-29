// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.Operations;

namespace Cathei.LinqGen.Generator;

public static class LinqGenAnalyzer
{
    public static bool ShouldAnalyze(SyntaxNode node)
    {
        if (node is not InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax memberAccessSyntax })
        {
            // not a method invocation
            return false;
        }

        return RootMethodNames.Contains(memberAccessSyntax.Name.Identifier.ValueText);
    }

    public static ImmutableArray<LinqGenSignature> Analyze(SemanticModel model, SyntaxNode node, CancellationToken cancellationToken)
    {
        if (node is not InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax memberAccessSyntax })
        {
            // not a method invocation
            return ImmutableArray<LinqGenSignature>.Empty;
        }

        var memberSymbolInfo = model.GetSymbolInfo(memberAccessSyntax);

        if (memberSymbolInfo.Symbol is not IMethodSymbol methodSymbol || !IsRootMethod(methodSymbol))
        {
            // not a root method
            return ImmutableArray<LinqGenSignature>.Empty;
        }

        var operation = model.GetOperation(memberAccessSyntax.Expression, cancellationToken);

        if (operation == null)
        {
            // failed to find operation
            return ImmutableArray<LinqGenSignature>.Empty;
        }

        // Collection phase
        var dict = FindSignatures(operation, cancellationToken);

        // Expansion phase



        // // create chain signatures
        // return FindSignatures(operation, cancellationToken)
        //     .Select(static x => new LinqGenSignature(x))
        //     .ToImmutableArray();
    }

    private readonly struct SignaturePair
    {
        public readonly ISymbol Symbol;
        public readonly ImmutableList<LinqGenNode> Signature;

        public SignaturePair(ISymbol symbol, ImmutableList<LinqGenNode> signature)
        {
            Symbol = symbol;
            Signature = signature;
        }
    }

    private static IEnumerable<SignaturePair> FindSignatures(
        IInvocationOperation operation, CancellationToken cancellationToken)
    {
        var upstream = ImmutableList.Create<LinqGenNode>(new SpecializeNode());

        // return upstream itself first
        yield return new(operation.TargetMethod, upstream);

        // Trace result usage
        foreach (var list in FindSignaturesFromUsage(upstream, operation.Parent, cancellationToken))
            yield return list;
    }

    private static IEnumerable<SignaturePair> FindSignaturesFromUsage(
        ImmutableList<LinqGenNode> upstream, IOperation? operation, CancellationToken cancellationToken)
    {
        while (operation != null)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (operation is IVariableDeclaratorOperation varDeclOperation)
            {
                // The operation saved as local variable
                return FindSignaturesFromLocal(upstream, varDeclOperation, cancellationToken);
            }

            if (operation is IArgumentOperation argumentOperation &&
                argumentOperation.Parent is IInvocationOperation invocationOperation)
            {
                if (!invocationOperation.TargetMethod.IsExtensionMethod)
                {
                    // Illegal for using with non-extension method.
                    // TODO: Emit error
                    break;
                }

                if (invocationOperation.Arguments[0] == argumentOperation)
                {
                    // The operation used as caller
                    return FindSignaturesFromInvocation(upstream, invocationOperation, cancellationToken);
                }

                // The operation used as an argument
                // This chain will be analyzed later
                // For now, treat same as being enumerated
                return GetEnumeratorSignature(upstream, operation);
            }

            if (operation.Parent is IForEachLoopOperation forEachOperation &&
                forEachOperation.Collection == operation)
            {
                // The operation is being enumerated
                return GetEnumeratorSignature(upstream, operation);
            }

            operation = operation.Parent;
        }

        return Enumerable.Empty<SignaturePair>();
    }

    private static IEnumerable<SignaturePair> FindSignaturesFromLocal(
        ImmutableList<LinqGenNode> upstream, IVariableDeclaratorOperation operation, CancellationToken cancellationToken)
    {
        var symbol = operation.Symbol;
        var symbolComparer = SymbolEqualityComparer.Default;

        IOperation root = operation;
        while (root.Parent != null)
            root = root.Parent;

        // Find signatures per ILocalReferenceOperation in current method
        var usages = root.Descendants()
            .OfType<ILocalReferenceOperation>()
            .Where(x => symbolComparer.Equals(x.Local, symbol));

        return usages.SelectMany(x => FindSignaturesFromUsage(upstream, x, cancellationToken));
    }

    private static IEnumerable<SignaturePair> FindSignaturesFromInvocation(
        ImmutableList<LinqGenNode> upstream, IInvocationOperation operation, CancellationToken cancellationToken)
    {
        // operation.TargetMethod

    }

    private static IEnumerable<SignaturePair> GetEnumeratorSignature(
        ImmutableList<LinqGenNode> upstream, IOperation operation)
    {
        if (operation.SemanticModel?.GetSymbolInfo(operation.Syntax).Symbol is ISymbol symbol)
        {
            yield return new(symbol, upstream.Add(new GetEnumeratorNode()));
        }
    }
}
