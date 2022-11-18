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
|         ForEach | 50.81 μs | 0.378 μs | 0.353 μs |  0.82 |    0.01 | 31.1890 |   64.3 KB |        1.00 |
|            Linq | 62.20 μs | 0.543 μs | 0.508 μs |  1.00 |    0.00 | 31.1279 |  64.34 KB |        1.00 |
| LinqGenDelegate | 58.50 μs | 0.810 μs | 0.757 μs |  0.94 |    0.02 |  9.5825 |  19.72 KB |        0.31 |
|   LinqGenStruct | 29.64 μs | 0.592 μs | 1.324 μs |  0.49 |    0.02 |  9.6130 |  19.72 KB |        0.31 |
