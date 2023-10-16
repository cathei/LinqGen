// LinqGen.Tests.Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Immutable;
using System.Reflection;
using Cathei.LinqGen;
using Cathei.LinqGen.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

Assembly FindAssembly(string name)
{
    return AppDomain.CurrentDomain
        .GetAssemblies()
        .First(x => x.GetName().Name == name);
}

void ReportErrors(ImmutableArray<Diagnostic> diagnostics)
{
    foreach (var diagnostic in diagnostics)
    {
        Console.WriteLine(diagnostic.ToString());
    }
}

GeneratorDriver driver = CSharpGeneratorDriver.Create(
    new [] { new LinqGenIncrementalGenerator().AsSourceGenerator() }, null, null, null,
    new GeneratorDriverOptions(IncrementalGeneratorOutputKind.None, true));

var systemAssembly = typeof(int).Assembly;
var systemRuntimeAssembly = FindAssembly("System.Runtime");
var linqGenAssembly = typeof(Gen).Assembly;

SyntaxTree synTree1 = CSharpSyntaxTree.ParseText("""
using Cathei.LinqGen;
using System.Collections.Generic;

public static class MyClass1
{
    public static void MyFunc1()
    {
        List<int> myList = new List<int> { 1, 2, 3, 4, 5 };

        var query = myList.Gen().Select(x => x * 2);

        //foreach (var x in query) { }
    }
}
""", CSharpParseOptions.Default, "MyClass1.cs");

MetadataReference[] asmRefs =
{
    MetadataReference.CreateFromFile(systemAssembly.Location),
    MetadataReference.CreateFromFile(systemRuntimeAssembly.Location),
    MetadataReference.CreateFromFile(linqGenAssembly.Location)
};

Compilation comp = CSharpCompilation.Create(
    "LinqGen.Test.Incremental", new [] { synTree1 },
    asmRefs,
    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

ReportErrors(comp.GetDiagnostics());

// Run the generators once to set up the baseline cached results.
driver = driver.RunGeneratorsAndUpdateCompilation(comp, out comp, out _);

SyntaxTree synTree2 = CSharpSyntaxTree.ParseText("""
using Cathei.LinqGen;

public static class MyClass2
{
    public static void MyFunc2()
    {
        int[] myArray = new int[] { 5, 4, 3, 2, 1 };

        var query = myArray.Gen().Where(x => x % 2 == 0);

        //foreach (var x in query) { }
    }
}
""", CSharpParseOptions.Default, "MyClass2.cs");

comp = comp.AddSyntaxTrees(synTree2);

ReportErrors(comp.GetDiagnostics());

driver = driver.RunGeneratorsAndUpdateCompilation(comp, out comp, out _);

GeneratorDriverRunResult runResult = driver.GetRunResult();

foreach (var result in runResult.Results)
{
    foreach (var pair in result.TrackedSteps)
    {
        Console.WriteLine(pair.Key);
    }

    foreach (var pair in result.TrackedOutputSteps)
    {
        Console.WriteLine(pair.Key);
    }
}

