## WhereSelectSum

### Source
[WhereSelectSum.cs](../../LinqGen.Benchmarks/Cases/WhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |      Mean |     Error |    StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|------:|-------:|----------:|------------:|
|            ForLoop |  8.005 μs | 0.0731 μs | 0.0571 μs |  0.13 |      - |         - |        0.00 |
|        ForEachLoop | 28.798 μs | 0.1379 μs | 0.1222 μs |  0.47 |      - |      40 B |        0.25 |
|               Linq | 60.743 μs | 0.2635 μs | 0.2201 μs |  1.00 | 0.0610 |     160 B |        1.00 |
|    LinqGenDelegate | 22.592 μs | 0.2693 μs | 0.2519 μs |  0.37 |      - |         - |        0.00 |
|      LinqGenStruct | 17.751 μs | 0.2999 μs | 0.2805 μs |  0.29 |      - |         - |        0.00 |
| StructLinqDelegate | 22.816 μs | 0.2690 μs | 0.2384 μs |  0.38 | 0.0305 |      88 B |        0.55 |
|   StructLinqStruct | 17.395 μs | 0.1016 μs | 0.0950 μs |  0.29 |      - |         - |        0.00 |
|  HyperLinqDelegate | 22.714 μs | 0.1809 μs | 0.1692 μs |  0.37 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.561 μs | 0.0850 μs | 0.0795 μs |  0.29 |      - |         - |        0.00 |
