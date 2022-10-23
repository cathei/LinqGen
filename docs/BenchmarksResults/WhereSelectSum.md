## WhereSelectSum

### Source
[WhereSelectSum.cs](../../LinqGen.Benchmarks/Cases/WhereSelectSum.cs)

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
|            ForLoop |  7.905 μs | 0.0124 μs | 0.0103 μs |  0.13 |      - |         - |        0.00 |
|        ForEachLoop | 28.764 μs | 0.1480 μs | 0.1312 μs |  0.47 |      - |      40 B |        0.25 |
|               Linq | 60.714 μs | 0.1555 μs | 0.1454 μs |  1.00 | 0.0610 |     160 B |        1.00 |
|    LinqGenDelegate | 24.401 μs | 0.0682 μs | 0.0604 μs |  0.40 |      - |         - |        0.00 |
|      LinqGenStruct |  7.150 μs | 0.0106 μs | 0.0099 μs |  0.12 |      - |         - |        0.00 |
| StructLinqDelegate | 22.413 μs | 0.0770 μs | 0.0720 μs |  0.37 | 0.0305 |      88 B |        0.55 |
|   StructLinqStruct | 17.526 μs | 0.0427 μs | 0.0379 μs |  0.29 |      - |         - |        0.00 |
|  HyperLinqDelegate | 22.300 μs | 0.0803 μs | 0.0712 μs |  0.37 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.361 μs | 0.0293 μs | 0.0260 μs |  0.29 |      - |         - |        0.00 |
