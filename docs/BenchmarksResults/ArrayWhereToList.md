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
|             Method |     Mean |    Error |   StdDev | Ratio | RatioSD |    Gen0 | Allocated | Alloc Ratio |
|------------------- |---------:|---------:|---------:|------:|--------:|--------:|----------:|------------:|
|               Linq | 61.06 μs | 0.424 μs | 0.397 μs |  1.00 |    0.00 | 31.1279 |  64.34 KB |        1.00 |
|    LinqGenDelegate | 65.96 μs | 0.402 μs | 0.336 μs |  1.08 |    0.01 |  9.6436 |  19.92 KB |        0.31 |
|      LinqGenStruct | 32.88 μs | 0.657 μs | 1.842 μs |  0.54 |    0.03 |  9.5215 |   19.6 KB |        0.30 |
| StructLinqDelegate | 59.39 μs | 0.416 μs | 0.369 μs |  0.97 |    0.01 |  9.5825 |  19.77 KB |        0.31 |
|   StructLinqStruct | 39.95 μs | 0.789 μs | 0.908 μs |  0.65 |    0.02 |  9.4604 |  19.47 KB |        0.30 |
|  HyperLinqDelegate | 50.44 μs | 0.436 μs | 0.408 μs |  0.83 |    0.01 |  9.5825 |  19.65 KB |        0.31 |
|    HyperLinqStruct | 32.98 μs | 0.645 μs | 1.004 μs |  0.54 |    0.02 |  9.7046 |  19.95 KB |        0.31 |
