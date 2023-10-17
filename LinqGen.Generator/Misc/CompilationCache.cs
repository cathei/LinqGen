// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Linq;

namespace Cathei.LinqGen.Generator;

public readonly struct CompilationCache : IEquatable<CompilationCache>
{
    public readonly Compilation Original;
    public readonly Compilation Filtered;
    public readonly Dictionary<SyntaxTree, SemanticModel> SemanticModels;

    public CompilationCache(Compilation compilation)
    {
        Original = compilation;
        Filtered = compilation.RemoveSyntaxTrees(
            compilation.SyntaxTrees.Where(x => x.FilePath.StartsWith("LinqGen.Generator")));
        SemanticModels = new();
    }

    public bool Equals(CompilationCache other)
    {
        // SyntaxTrees is ordered array
        return Original.SyntaxTrees.SequenceEqual(other.Original.SyntaxTrees);
    }

    public override int GetHashCode()
    {
        int hash = 0;

        foreach (var tree in Original.SyntaxTrees)
        {
            hash = HashCombine(hash, tree.GetHashCode());
        }

        return hash;
    }

    public SemanticModel GetSemanticModel(SyntaxTree tree)
    {
        if (SemanticModels.TryGetValue(tree, out var model))
            return model;

        model = Filtered.GetSemanticModel(tree);
        SemanticModels.Add(tree, model);
        return model;
    }
}
