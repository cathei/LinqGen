## ListSelectSum

### Source
[ListSelectSum.cs](../../LinqGen.Benchmarks/Cases/ListSelectSum.cs)

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
|        ForEachLoop |  9.567 μs | 0.0192 μs | 0.0170 μs |  0.17 |      - |         - |        0.00 |
|               Linq | 56.723 μs | 0.7221 μs | 0.6030 μs |  1.00 |      - |      72 B |        1.00 |
|    LinqGenDelegate | 22.072 μs | 0.0181 μs | 0.0160 μs |  0.39 |      - |         - |        0.00 |
|      LinqGenStruct | 13.237 μs | 0.0251 μs | 0.0210 μs |  0.23 |      - |         - |        0.00 |
| StructLinqDelegate | 13.287 μs | 0.0254 μs | 0.0212 μs |  0.23 | 0.0305 |      64 B |        0.89 |
|   StructLinqStruct |  9.623 μs | 0.0210 μs | 0.0196 μs |  0.17 |      - |         - |        0.00 |
|  HyperLinqDelegate | 17.698 μs | 0.0639 μs | 0.0533 μs |  0.31 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.566 μs | 0.2085 μs | 0.1848 μs |  0.31 |      - |         - |        0.00 |
