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
|            ForLoop |  7.884 μs | 0.0131 μs | 0.0109 μs |  0.13 |      - |         - |        0.00 |
|        ForEachLoop | 31.692 μs | 0.2060 μs | 0.1826 μs |  0.53 |      - |      40 B |        0.25 |
|               Linq | 60.155 μs | 0.1376 μs | 0.1220 μs |  1.00 | 0.0610 |     160 B |        1.00 |
|    LinqGenDelegate | 21.777 μs | 0.0715 μs | 0.0634 μs |  0.36 |      - |         - |        0.00 |
|      LinqGenStruct |  6.926 μs | 0.0117 μs | 0.0109 μs |  0.12 |      - |         - |        0.00 |
| StructLinqDelegate | 22.203 μs | 0.1412 μs | 0.1321 μs |  0.37 | 0.0305 |      88 B |        0.55 |
|   StructLinqStruct | 17.551 μs | 0.0960 μs | 0.0898 μs |  0.29 |      - |         - |        0.00 |
|  HyperLinqDelegate | 22.265 μs | 0.1302 μs | 0.1218 μs |  0.37 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.503 μs | 0.1333 μs | 0.1247 μs |  0.29 |      - |         - |        0.00 |
