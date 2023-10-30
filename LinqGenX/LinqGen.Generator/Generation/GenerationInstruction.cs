using System;
using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public abstract class GenerationInstruction : LinqGenInstruction
{
    public readonly TypeSyntax SourceType;
    public readonly TypeSyntax ElementType;

    public TypeSyntax InterfaceType => GenericName(Identifier("IStub"), TypeArgumentList(ElementType));

    protected GenerationInstruction(TypeSyntax sourceType, TypeSyntax elementType)
        : base(ImmutableArray.Create(sourceType))
    {
        SourceType = sourceType;
        ElementType = elementType;
    }
}

