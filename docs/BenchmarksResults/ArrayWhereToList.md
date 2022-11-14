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
|               Linq | 62.31 μs | 0.631 μs | 0.590 μs |  1.00 |    0.00 | 31.1279 |  64.34 KB |        1.00 |
|    LinqGenDelegate | 55.54 μs | 0.487 μs | 0.455 μs |  0.89 |    0.01 | 31.1890 |   64.3 KB |        1.00 |
|      LinqGenStruct | 28.90 μs | 0.641 μs | 1.880 μs |  0.45 |    0.03 | 31.2195 |   64.3 KB |        1.00 |
| StructLinqDelegate | 56.46 μs | 0.239 μs | 0.224 μs |  0.91 |    0.01 |  9.5825 |  19.69 KB |        0.31 |
|   StructLinqStruct | 42.01 μs | 0.838 μs | 1.991 μs |  0.64 |    0.01 |  9.5825 |   19.7 KB |        0.31 |
|  HyperLinqDelegate | 52.43 μs | 0.565 μs | 0.529 μs |  0.84 |    0.01 |  9.7046 |  19.87 KB |        0.31 |
|    HyperLinqStruct | 35.71 μs | 0.732 μs | 2.159 μs |  0.58 |    0.02 |  9.7046 |  19.88 KB |        0.31 |
