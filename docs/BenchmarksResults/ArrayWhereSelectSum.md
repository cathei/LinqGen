## ArrayWhereSelectSum

### Source
[ArrayWhereSelectSum.cs](../../LinqGen.Benchmarks/Cases/ArrayWhereSelectSum.cs)

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
|            ForLoop | 11.303 μs | 0.0208 μs | 0.0195 μs |  0.32 |      - |         - |        0.00 |
|        ForEachLoop |  9.641 μs | 0.0378 μs | 0.0354 μs |  0.27 |      - |         - |        0.00 |
|               Linq | 35.460 μs | 0.0940 μs | 0.0879 μs |  1.00 |      - |     104 B |        1.00 |
|    LinqGenDelegate | 22.561 μs | 0.0957 μs | 0.0895 μs |  0.64 |      - |         - |        0.00 |
|      LinqGenStruct | 12.664 μs | 0.0530 μs | 0.0496 μs |  0.36 |      - |         - |        0.00 |
| StructLinqDelegate | 22.513 μs | 0.0803 μs | 0.0751 μs |  0.63 | 0.0305 |      96 B |        0.92 |
|   StructLinqStruct | 17.479 μs | 0.1234 μs | 0.1154 μs |  0.49 |      - |         - |        0.00 |
|  HyperLinqDelegate | 17.605 μs | 0.0563 μs | 0.0527 μs |  0.50 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.369 μs | 0.0768 μs | 0.0718 μs |  0.49 |      - |         - |        0.00 |
