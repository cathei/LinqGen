using System;
using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public abstract class GenerationInstruction : LinqGenInstruction
{
    public readonly TypeSyntax SourceType;
    public readonly TypeSyntax ElementType;

    public TypeSyntax InterfaceType => GenericName(Identifier("IStub"), TypeArgumentList(ElementType));

    protected GenerationInstruction(LinqGenInstruction? upstream, TypeSyntax sourceType, TypeSyntax elementType)
        : base(upstream, ImmutableArray.Create(sourceType))
    {
        SourceType = sourceType;
        ElementType = elementType;
    }
}

