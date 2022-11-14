## ArrayWhereToArray

### Source
[ArrayWhereToArray.cs](../../LinqGen.Benchmarks/Cases/ArrayWhereToArray.cs)

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
|               Linq | 55.55 μs | 0.773 μs | 0.603 μs |  1.00 |    0.00 | 25.6348 |  52.54 KB |        1.00 |
|    LinqGenDelegate | 62.81 μs | 1.005 μs | 0.839 μs |  1.13 |    0.02 | 24.9023 |  51.58 KB |        0.98 |
|      LinqGenStruct | 30.97 μs | 0.780 μs | 2.262 μs |  0.53 |    0.03 | 24.9939 |  51.91 KB |        0.99 |
| StructLinqDelegate | 52.65 μs | 0.392 μs | 0.367 μs |  0.95 |    0.01 |  9.7046 |  20.01 KB |        0.38 |
|   StructLinqStruct | 44.93 μs | 0.843 μs | 0.828 μs |  0.81 |    0.02 |  9.7046 |  20.06 KB |        0.38 |
|  HyperLinqDelegate | 52.05 μs | 0.843 μs | 0.789 μs |  0.94 |    0.01 |  9.5825 |   19.8 KB |        0.38 |
|    HyperLinqStruct | 34.13 μs | 0.675 μs | 1.393 μs |  0.61 |    0.03 |  9.3384 |  19.32 KB |        0.37 |
