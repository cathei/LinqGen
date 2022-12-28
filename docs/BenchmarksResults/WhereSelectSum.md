## WhereSelectSum

### Source
[WhereSelectSum.cs](../../LinqGen.Benchmarks/Cases/WhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |      Mean |     Error |    StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|------:|-------:|----------:|------------:|
|            ForLoop |  7.936 μs | 0.0455 μs | 0.0404 μs |  0.13 |      - |         - |        0.00 |
|        ForEachLoop | 28.671 μs | 0.2000 μs | 0.1871 μs |  0.47 |      - |      40 B |        0.25 |
|               Linq | 60.390 μs | 0.3188 μs | 0.2982 μs |  1.00 |      - |     160 B |        1.00 |
|    LinqGenDelegate | 22.059 μs | 0.0490 μs | 0.0458 μs |  0.37 |      - |         - |        0.00 |
|      LinqGenStruct |  7.814 μs | 0.0050 μs | 0.0047 μs |  0.13 |      - |         - |        0.00 |
| StructLinqDelegate | 22.224 μs | 0.1979 μs | 0.1851 μs |  0.37 | 0.0305 |      88 B |        0.55 |
|   StructLinqStruct | 17.203 μs | 0.0151 μs | 0.0118 μs |  0.29 |      - |         - |        0.00 |
|  HyperLinqDelegate | 22.065 μs | 0.1664 μs | 0.1557 μs |  0.37 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.330 μs | 0.0777 μs | 0.0726 μs |  0.29 |      - |         - |        0.00 |
