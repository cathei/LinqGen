// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.Operations;

namespace Cathei.LinqGen.Generator;

public static class LinqGenAnalyzer
{
    public static bool ShouldAnalyze(SyntaxNode syntax)
    {
        if (syntax is not InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax })
        {
            // not a method invocation
            return false;
        }

        return true;
    }

    public static ImmutableArray<LinqGenSignature> Analyze(
        SemanticModel model, SyntaxNode syntax, CancellationToken cancellationToken)
    {
        if (syntax is not InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax memberAccessSyntax })
        {
            // not a method invocation
            return ImmutableArray<LinqGenSignature>.Empty;
        }

        var memberSymbolInfo = model.GetSymbolInfo(memberAccessSyntax);

        if (memberSymbolInfo.Symbol is not IMethodSymbol methodSymbol ||
            ParseGenerationMethod(methodSymbol) is not Type nodeType)
        {
            // not a generation method
            return ImmutableArray<LinqGenSignature>.Empty;
        }

        var operation = model.GetOperation(syntax, cancellationToken);

        if (operation is not IInvocationOperation invocationOperation)
        {
            // failed to find operation
            return ImmutableArray<LinqGenSignature>.Empty;
        }

        // Collection phase
        var database = FindSignatures(invocationOperation, nodeType, cancellationToken);

        // Expansion phase
        var builder = ImmutableArray.CreateBuilder<LinqGenSignature>(database.Nodes.Count);

        foreach (var upstream in database.Nodes)
        {
            var node = upstream[upstream.Count - 1];
            var list = node.ExpandToInstructions(upstream, database.Arguments);
            builder.Add(new LinqGenSignature(list));
        }

        return builder.MoveToImmutable();
    }

    private readonly struct SignatureDatabase
    {
        public readonly List<ImmutableList<LinqGenNode>> Nodes = new();
        public readonly Dictionary<ArgumentSyntax, ImmutableList<LinqGenNode>> Arguments = new();

        public SignatureDatabase() {}
    }

    private static SignatureDatabase FindSignatures(
        IInvocationOperation operation,
        Type generationNode,
        CancellationToken cancellationToken)
    {
        var database = new SignatureDatabase();
        var upstream = ImmutableList.Create(
            (LinqGenNode)Activator.CreateInstance(generationNode, operation.TargetMethod));

        // register upstream itself first
        database.Nodes.Add(upstream);

        // Trace result usage
        FindSignaturesFromUsage(database, upstream, operation.Parent, cancellationToken);
        return database;
    }

    private static void FindSignaturesFromUsage(
        in SignatureDatabase database,
        ImmutableList<LinqGenNode> upstream,
        IOperation? operation,
        CancellationToken cancellationToken)
    {
        while (operation != null)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (operation is IVariableDeclaratorOperation varDeclOperation)
            {
                // The operation saved as local variable
                FindSignaturesFromLocal(database, upstream, varDeclOperation, cancellationToken);
                return;
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
                    FindSignaturesFromInvocation(database, upstream, invocationOperation, cancellationToken);
                    return;
                }

                // The operation used as an argument
                // This chain will be analyzed later
                // Map upstream with argument syntax
                database.Arguments.Add((ArgumentSyntax)argumentOperation.Syntax, upstream);

                // Also add get enumerator node
                database.Nodes.Add(upstream.Add(new GetEnumeratorNode()));
                return;
            }

            if (operation.Parent is IForEachLoopOperation forEachOperation &&
                forEachOperation.Collection == operation)
            {
                // The operation is being enumerated
                database.Nodes.Add(upstream.Add(new GetEnumeratorNode()));
                return;
            }

            if (operation.Syntax is StatementSyntax)
            {
                // Current statement is finished, stop looping
                return;
            }

            operation = operation.Parent;
        }
    }

    private static void FindSignaturesFromLocal(
        in SignatureDatabase database,
        ImmutableList<LinqGenNode> upstream,
        IVariableDeclaratorOperation operation,
        CancellationToken cancellationToken)
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

        foreach (var usage in usages)
            FindSignaturesFromUsage(database, upstream, usage, cancellationToken);
    }

    private static void FindSignaturesFromInvocation(
        in SignatureDatabase database,
        ImmutableList<LinqGenNode> upstream,
        IInvocationOperation operation,
        CancellationToken cancellationToken)
    {
        if (ParseOperationMethod(operation.TargetMethod) is Type operationNode)
        {
            // Operation node should have usages
            var instance = (LinqGenNode)Activator.CreateInstance(operationNode, operation.TargetMethod);
            upstream = upstream.Add(instance);

            // Register operation itself first
            database.Nodes.Add(upstream);

            // Trace result usage
            FindSignaturesFromUsage(database, upstream, operation.Parent, cancellationToken);

            // TODO: emit warning if result is not used
        }
        else if (ParseEvaluationMethod(operation.TargetMethod) is Type evaluationNode)
        {
            // Evaluation node is leaf node, no need to find downstream
            var instance = (LinqGenNode)Activator.CreateInstance(evaluationNode, operation.TargetMethod);

            database.Nodes.Add(upstream.Add(instance));

            // TODO: emit warning if result is not used
        }
    }
}
