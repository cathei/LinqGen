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
|             Method |      Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
|        ForEachLoop |  9.614 μs | 0.0411 μs | 0.0364 μs |  0.17 |    0.00 |      - |         - |        0.00 |
|               Linq | 56.849 μs | 0.4304 μs | 0.4026 μs |  1.00 |    0.00 |      - |      72 B |        1.00 |
|    LinqGenDelegate | 24.416 μs | 0.6068 μs | 1.7509 μs |  0.43 |    0.04 |      - |         - |        0.00 |
|      LinqGenStruct | 11.170 μs | 0.0067 μs | 0.0063 μs |  0.20 |    0.00 |      - |         - |        0.00 |
| StructLinqDelegate | 13.193 μs | 0.0066 μs | 0.0062 μs |  0.23 |    0.00 | 0.0305 |      64 B |        0.89 |
|   StructLinqStruct |  9.551 μs | 0.0222 μs | 0.0208 μs |  0.17 |    0.00 |      - |         - |        0.00 |
|  HyperLinqDelegate | 17.493 μs | 0.0422 μs | 0.0395 μs |  0.31 |    0.00 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.288 μs | 0.0539 μs | 0.0505 μs |  0.30 |    0.00 |      - |         - |        0.00 |
