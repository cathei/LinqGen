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
|             Method |      Mean |     Error |    StdDev | Ratio | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|------:|----------:|------------:|
|            ForLoop |  9.386 μs | 0.0531 μs | 0.0496 μs |  0.24 |         - |        0.00 |
|        ForEachLoop | 25.891 μs | 0.2113 μs | 0.1976 μs |  0.65 |      40 B |        0.45 |
|               Linq | 39.648 μs | 0.4394 μs | 0.4110 μs |  1.00 |      88 B |        1.00 |
|    LinqGenDelegate | 26.956 μs | 0.1401 μs | 0.1310 μs |  0.68 |         - |        0.00 |
|      LinqGenStruct | 11.727 μs | 0.0519 μs | 0.0433 μs |  0.30 |         - |        0.00 |
| StructLinqDelegate | 34.714 μs | 0.1070 μs | 0.0893 μs |  0.88 |      56 B |        0.64 |
|   StructLinqStruct |  9.344 μs | 0.0206 μs | 0.0161 μs |  0.24 |         - |        0.00 |
|  HyperLinqDelegate | 34.544 μs | 0.1349 μs | 0.1196 μs |  0.87 |         - |        0.00 |
|    HyperLinqStruct | 34.512 μs | 0.1205 μs | 0.1127 μs |  0.87 |         - |        0.00 |
