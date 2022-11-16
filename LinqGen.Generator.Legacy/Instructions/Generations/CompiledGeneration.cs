// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Cathei.LinqGen.Generator
{
    using static SyntaxFactory;
    using static CodeGenUtils;

    /// <summary>
    /// Mock class for already compiled generation.
    /// Evaluation methods can still be added to it.
    /// </summary>
    public sealed class CompiledGeneration : Generation
    {
        public INamedTypeSymbol TypeSymbol { get; }

        public CompiledGeneration(INamedTypeSymbol typeSymbol, int id) : base(generated: true)
        {
            TypeSymbol = typeSymbol;
            ClassName = ParseName(TypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
            IdentifierName = IdentifierName($"Compiled_{id}");

            INamedTypeSymbol stubInterfaceSymbol = default!;

            foreach (var interfaceSymbol in typeSymbol.AllInterfaces)
            {
                if (IsInputStubEnumerable(interfaceSymbol))
                {
                    stubInterfaceSymbol = interfaceSymbol;
                }
                else if (IsCountable(interfaceSymbol))
                {
                    IsCountable = true;
                }
                else if (IsPartition(interfaceSymbol))
                {
                    IsPartition = true;
                }
            }

            TryParseStubInterface(stubInterfaceSymbol, out var elementSymbol, out _);
            OutputElementType = ParseTypeName(elementSymbol);
        }

        public override NameSyntax ClassName { get; }
        public override IdentifierNameSyntax IdentifierName { get; }
        public override TypeSyntax OutputElementType { get; }

        public override bool IsCountable { get; }
        public override bool IsPartition { get; }

        public override SourceText Render()
        {
            return CompiledGenerationTemplate.Render(this);
        }


        // public INamedTypeSymbol TypeSymbol { get; }
        //
        // public CompiledGeneration(INamedTypeSymbol typeSymbol) : base(null)
        // {
        //     TypeSymbol = typeSymbol;
        //     ClassName = ParseName(TypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        //
        //     TryParseStubInterface(typeSymbol, out var elementSymbol, out _);
        //
        //     // SupportGenericElementOutput = elementSymbol is ITypeParameterSymbol;
        //     // OutputElementType = ParseTypeName(elementSymbol
        //     //     .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        // }
        //
        // public IdentifierNameSyntax? IdentifierName { get; private set; }
        //
        // // public override bool SupportGenericElementOutput { get; }
        //
        // // public override TypeSyntax OutputElementType { get; }
        //
        // public override SourceText Render(IdentifierNameSyntax assemblyName, int id)
        // {
        //     IdentifierName = IdentifierName($"Compiled_{id}");
        //     return CompiledGenerationTemplate.Render(assemblyName, this);
        // }
    }
}