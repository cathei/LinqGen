## ListWhereSelectSum

### Source
[ListWhereSelectSum.cs](../../LinqGen.Benchmarks/Cases/ListWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |     Mean |    Error |   StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------------- |---------:|---------:|---------:|------:|-------:|----------:|------------:|
|            ForLoop | 11.89 μs | 0.023 μs | 0.021 μs |  0.25 |      - |         - |        0.00 |
|        ForEachLoop | 14.56 μs | 0.045 μs | 0.040 μs |  0.31 |      - |         - |        0.00 |
|               Linq | 47.26 μs | 0.123 μs | 0.115 μs |  1.00 | 0.0610 |     152 B |        1.00 |
|    LinqGenDelegate | 31.25 μs | 0.087 μs | 0.082 μs |  0.66 |      - |         - |        0.00 |
|      LinqGenStruct | 17.82 μs | 0.050 μs | 0.047 μs |  0.38 |      - |         - |        0.00 |
| StructLinqDelegate | 22.64 μs | 0.041 μs | 0.039 μs |  0.48 | 0.0305 |      96 B |        0.63 |
|   StructLinqStruct | 17.61 μs | 0.068 μs | 0.064 μs |  0.37 |      - |         - |        0.00 |
|  HyperLinqDelegate | 17.75 μs | 0.052 μs | 0.046 μs |  0.38 |      - |         - |        0.00 |
|    HyperLinqStruct | 17.54 μs | 0.065 μs | 0.060 μs |  0.37 |      - |         - |        0.00 |
