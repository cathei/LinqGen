// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Linq;

namespace Cathei.LinqGen.Generator;

public abstract class PredefinedGeneration : Generation
{
    protected PredefinedGeneration(in LinqGenExpression expression, uint id) : base(expression, id)
    {
    }

    public override bool SupportPartition => true;

    protected override ParameterListSyntax GetExtensionMethodParameters()
    {
        var parameters = GetParameters(true);

        parameters = parameters.Prepend(
            Parameter(IdentifierName("GenerationStub"), Identifier("stub")).WithModifiers(ThisTokenList));

        return ParameterList(parameters);
    }
}