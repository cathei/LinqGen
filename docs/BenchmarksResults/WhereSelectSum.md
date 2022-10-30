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
|            ForLoop |  7.927 μs | 0.0221 μs | 0.0184 μs |  0.13 |      - |         - |        0.00 |
|        ForEachLoop | 28.658 μs | 0.1883 μs | 0.1761 μs |  0.47 |      - |      40 B |        0.25 |
|               Linq | 60.596 μs | 0.3195 μs | 0.2989 μs |  1.00 | 0.0610 |     160 B |        1.00 |
|    LinqGenDelegate | 22.550 μs | 0.0769 μs | 0.0719 μs |  0.37 |      - |         - |        0.00 |
|      LinqGenStruct |  8.075 μs | 0.0241 μs | 0.0214 μs |  0.13 |      - |         - |        0.00 |
| StructLinqDelegate | 22.314 μs | 0.1280 μs | 0.1197 μs |  0.37 | 0.0305 |      88 B |        0.55 |
|   StructLinqStruct | 17.419 μs | 0.0883 μs | 0.0826 μs |  0.29 |      - |         - |        0.00 |
|  HyperLinqDelegate | 22.346 μs | 0.1201 μs | 0.1123 μs |  0.37 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.440 μs | 0.0838 μs | 0.0700 μs |  0.29 |      - |         - |        0.00 |
