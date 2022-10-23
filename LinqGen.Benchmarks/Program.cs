// LinqGen.Benchmarks, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Reflection;
using System.Text;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

foreach (var summary in BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args))
{
    SaveSummary(summary);
}

void SaveSummary(Summary summary)
{
    var solutionDir = GetSolutionDirectory();
    if (solutionDir is null)
        return;

    var targetType = GetTargetType(summary);
    if (targetType is null)
        return;

    var title = targetType.Name;

    var resultsPath = Path.Combine(solutionDir, "docs/BenchmarksResults");

    Directory.CreateDirectory(resultsPath);

    var filePath = Path.Combine(resultsPath, $"{title}.md");

    if (File.Exists(filePath))
        File.Delete(filePath);

    using var fileWriter = new StreamWriter(filePath, false, Encoding.UTF8);
    var logger = new StreamLogger(fileWriter);

    logger.WriteLine($"## {title}");
    logger.WriteLine();

    logger.WriteLine("### Source");
    logger.WriteLine($"[{targetType.Name}.cs](../../LinqGen.Benchmarks/Cases/{targetType.Name}.cs)");

    logger.WriteLine();

    logger.WriteLine("### Results:");
    MarkdownExporter.GitHub.ExportToLog(summary, logger);
}

Type? GetTargetType(Summary summary)
{
    return summary.BenchmarksCases.FirstOrDefault()?.Descriptor.Type;
}

string? GetSolutionDirectory()
{
    var dir = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);

    while (!string.IsNullOrEmpty(dir))
    {
        if (Directory.EnumerateFiles(dir, "*.sln", SearchOption.TopDirectoryOnly).Any())
            return dir;

        dir = Path.GetDirectoryName(dir);
    }

    return null;
}

