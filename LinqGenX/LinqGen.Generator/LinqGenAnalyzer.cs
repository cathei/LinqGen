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

    public static ImmutableArray<LinqGenRender> Analyze(
        SemanticModel model, SyntaxNode syntax, CancellationToken cancellationToken)
    {
        if (syntax is not InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax memberAccessSyntax })
        {
            // not a method invocation
            return ImmutableArray<LinqGenRender>.Empty;
        }

        var memberSymbolInfo = model.GetSymbolInfo(memberAccessSyntax);

        if (memberSymbolInfo.Symbol is not IMethodSymbol methodSymbol ||
            ParseGenerationMethod(methodSymbol) is not NodeConstructor nodeConstructor)
        {
            // not a generation method
            return ImmutableArray<LinqGenRender>.Empty;
        }

        var operation = model.GetOperation(syntax, cancellationToken);

        if (operation is not IInvocationOperation invocationOperation)
        {
            // failed to find operation
            return ImmutableArray<LinqGenRender>.Empty;
        }

        // Collection phase
        var database = FindSignatures(invocationOperation, nodeConstructor, cancellationToken);

        // Expansion phase
        var builder = ImmutableArray.CreateBuilder<LinqGenRender>(database.Nodes.Count);

        foreach (var node in database.Nodes)
        {
            var render = node.ExpandToRender(database.Arguments);
            builder.Add(render);
        }

        return builder.MoveToImmutable();
    }

    private readonly struct SignatureDatabase
    {
        public readonly List<LinqGenNode> Nodes = new();
        public readonly Dictionary<ArgumentSyntax, LinqGenNode> Arguments = new();

        public SignatureDatabase() {}
    }

    private static SignatureDatabase FindSignatures(
        IInvocationOperation operation,
        NodeConstructor nodeConstructor,
        CancellationToken cancellationToken)
    {
        var database = new SignatureDatabase();
        var upstream = nodeConstructor(null, operation.TargetMethod);

        // register upstream itself first
        database.Nodes.Add(upstream);

        // Trace result usage
        FindSignaturesFromUsage(database, upstream, operation.Parent, cancellationToken);
        return database;
    }

    private static void FindSignaturesFromUsage(
        in SignatureDatabase database,
        LinqGenNode upstream,
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
                database.Nodes.Add(new GetEnumeratorNode(upstream));
                return;
            }

            if (operation.Parent is IForEachLoopOperation forEachOperation &&
                forEachOperation.Collection == operation)
            {
                // The operation is being enumerated
                database.Nodes.Add(new GetEnumeratorNode(upstream));
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
        LinqGenNode upstream,
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
        LinqGenNode upstream,
        IInvocationOperation operation,
        CancellationToken cancellationToken)
    {
        if (ParseOperationMethod(operation.TargetMethod) is NodeConstructor operationNodeCtor)
        {
            // Operation node should have usages
            var node = operationNodeCtor(upstream, operation.TargetMethod);

            // Register operation itself first
            database.Nodes.Add(node);

            // Trace result usage
            FindSignaturesFromUsage(database, node, operation.Parent, cancellationToken);

            // TODO: emit warning if result is not used
        }
        else if (ParseEvaluationMethod(operation.TargetMethod) is NodeConstructor evaluationNodeCtor)
        {
            // Evaluation node is leaf node, no need to find downstream
            var node = evaluationNodeCtor(upstream, operation.TargetMethod);

            database.Nodes.Add(node);

            // TODO: emit warning if result is not used
        }
    }
}
