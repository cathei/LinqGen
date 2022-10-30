## SelectSum

### Source
[SelectSum.cs](../../LinqGen.Benchmarks/Cases/SelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |      Mean |     Error |    StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|------:|-------:|----------:|------------:|
|            ForLoop |  9.462 μs | 0.0389 μs | 0.0345 μs |  0.24 |      - |         - |        0.00 |
|        ForEachLoop | 25.917 μs | 0.1046 μs | 0.0979 μs |  0.67 |      - |      40 B |        0.45 |
|               Linq | 38.900 μs | 0.0989 μs | 0.0925 μs |  1.00 |      - |      88 B |        1.00 |
|    LinqGenDelegate | 27.016 μs | 0.2227 μs | 0.1859 μs |  0.69 |      - |         - |        0.00 |
|      LinqGenStruct | 11.810 μs | 0.0289 μs | 0.0271 μs |  0.30 |      - |         - |        0.00 |
| StructLinqDelegate | 14.344 μs | 0.0366 μs | 0.0325 μs |  0.37 | 0.0153 |      56 B |        0.64 |
|   StructLinqStruct |  9.472 μs | 0.0418 μs | 0.0391 μs |  0.24 |      - |         - |        0.00 |
|  HyperLinqDelegate | 34.880 μs | 0.2039 μs | 0.1908 μs |  0.90 |      - |         - |        0.00 |
|    HyperLinqStruct | 34.931 μs | 0.0894 μs | 0.0793 μs |  0.90 |      - |         - |        0.00 |
