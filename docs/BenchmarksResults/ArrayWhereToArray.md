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
|               Linq | 56.66 μs | 0.983 μs | 1.132 μs |  1.00 |    0.00 | 25.4517 |  52.27 KB |        1.00 |
|    LinqGenDelegate | 55.15 μs | 0.637 μs | 0.596 μs |  0.97 |    0.02 |  9.5215 |  19.54 KB |        0.37 |
|      LinqGenStruct | 26.60 μs | 0.530 μs | 1.309 μs |  0.48 |    0.02 |  9.5215 |  19.52 KB |        0.37 |
| StructLinqDelegate | 51.58 μs | 0.383 μs | 0.359 μs |  0.91 |    0.02 |  9.7046 |  19.99 KB |        0.38 |
|   StructLinqStruct | 38.33 μs | 0.767 μs | 1.238 μs |  0.68 |    0.02 |  9.5215 |  19.57 KB |        0.37 |
|  HyperLinqDelegate | 50.83 μs | 0.497 μs | 0.465 μs |  0.90 |    0.02 |  9.5215 |  19.63 KB |        0.38 |
|    HyperLinqStruct | 34.49 μs | 0.685 μs | 1.047 μs |  0.60 |    0.03 |  9.5825 |  19.71 KB |        0.38 |
