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
|            ForLoop |  7.887 μs | 0.0088 μs | 0.0082 μs |  0.13 |      - |         - |        0.00 |
|        ForEachLoop | 28.821 μs | 0.0537 μs | 0.0448 μs |  0.47 |      - |      40 B |        0.25 |
|               Linq | 60.864 μs | 0.1599 μs | 0.1417 μs |  1.00 | 0.0610 |     160 B |        1.00 |
|    LinqGenDelegate | 26.503 μs | 0.0822 μs | 0.0728 μs |  0.44 |      - |         - |        0.00 |
|      LinqGenStruct |  8.355 μs | 0.0415 μs | 0.0388 μs |  0.14 |      - |         - |        0.00 |
| StructLinqDelegate | 22.448 μs | 0.0837 μs | 0.0783 μs |  0.37 | 0.0305 |      88 B |        0.55 |
|   StructLinqStruct | 17.376 μs | 0.0139 μs | 0.0123 μs |  0.29 |      - |         - |        0.00 |
|  HyperLinqDelegate | 22.186 μs | 0.0256 μs | 0.0239 μs |  0.36 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.448 μs | 0.1134 μs | 0.1005 μs |  0.29 |      - |         - |        0.00 |
