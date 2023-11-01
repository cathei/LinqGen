using System;
using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

/// <summary>
/// Instruction that actually generates a source.
/// </summary>
public abstract class LinqGenRender : LinqGenOperation
{
    public readonly IdentifierNameSyntax MethodName;

    protected LinqGenRender(LinqGenInstruction upstream, IdentifierNameSyntax methodName)
        : base(upstream, ImmutableArray.Create<TypeSyntax>(methodName))
    {
        MethodName = methodName;
    }

    public abstract CompilationUnitSyntax Render();
}

// public class RenderMemberOperationInstruction : LinqGenRender
// {
//     public RenderMemberOperationInstruction(IdentifierNameSyntax methodName) : base(methodName)
//     {
//
//     }
// }

// public class RenderMemberEvaluationInstruction : RenderInstruction
// {
//
// }
//
// public class RenderExtensionEvaluationInstruction : RenderInstruction
// {
//
// }
