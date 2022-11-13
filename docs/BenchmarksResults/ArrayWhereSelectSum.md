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
|            ForLoop | 11.228 μs | 0.0615 μs | 0.0514 μs |  0.32 |      - |         - |        0.00 |
|        ForEachLoop |  9.577 μs | 0.0285 μs | 0.0267 μs |  0.27 |      - |         - |        0.00 |
|               Linq | 35.388 μs | 0.1858 μs | 0.1647 μs |  1.00 |      - |     104 B |        1.00 |
|    LinqGenDelegate | 22.625 μs | 0.0637 μs | 0.0596 μs |  0.64 |      - |         - |        0.00 |
|      LinqGenStruct | 12.329 μs | 0.0394 μs | 0.0369 μs |  0.35 |      - |         - |        0.00 |
| StructLinqDelegate | 22.668 μs | 0.0984 μs | 0.0921 μs |  0.64 | 0.0305 |      96 B |        0.92 |
|   StructLinqStruct | 17.631 μs | 0.0597 μs | 0.0558 μs |  0.50 |      - |         - |        0.00 |
|  HyperLinqDelegate | 17.728 μs | 0.0585 μs | 0.0547 μs |  0.50 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.576 μs | 0.0679 μs | 0.0602 μs |  0.50 |      - |         - |        0.00 |
