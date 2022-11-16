## ArrayWhereToList

### Source
[ArrayWhereToList.cs](../../LinqGen.Benchmarks/Cases/ArrayWhereToList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|          Method |     Mean |    Error |   StdDev | Ratio | RatioSD |    Gen0 | Allocated | Alloc Ratio |
|---------------- |---------:|---------:|---------:|------:|--------:|--------:|----------:|------------:|
|         ForEach | 44.65 μs | 0.340 μs | 0.318 μs |  0.71 |    0.00 | 31.1890 |   64.3 KB |        1.00 |
|            Linq | 63.10 μs | 0.297 μs | 0.278 μs |  1.00 |    0.00 | 31.1279 |  64.34 KB |        1.00 |
| LinqGenDelegate | 61.68 μs | 0.471 μs | 0.441 μs |  0.98 |    0.01 |  9.5825 |  19.68 KB |        0.31 |
|   LinqGenStruct | 46.69 μs | 0.933 μs | 1.037 μs |  0.74 |    0.02 |  9.5825 |  19.68 KB |        0.31 |
