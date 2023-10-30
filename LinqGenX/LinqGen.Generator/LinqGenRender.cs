using System;
using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

/// <summary>
/// Instruction that actually generates a source.
/// </summary>
public abstract class LinqGenRender : LinqGenInstruction
{
    public readonly IdentifierNameSyntax MethodName;

    protected LinqGenRender(IdentifierNameSyntax methodName)
        : base(ImmutableArray.Create<TypeSyntax>(methodName))
    {
        MethodName = methodName;
    }

    public abstract CompilationUnitSyntax Render(ImmutableList<LinqGenInstruction> instructions, uint id);
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
