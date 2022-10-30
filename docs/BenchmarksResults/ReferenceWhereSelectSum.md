## ReferenceWhereSelectSum

### Source
[ReferenceWhereSelectSum.cs](../../LinqGen.Benchmarks/Cases/ReferenceWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |      Mean |    Error |   StdDev | Ratio | Allocated | Alloc Ratio |
|------------------- |----------:|---------:|---------:|------:|----------:|------------:|
|        ForEachLoop |  14.36 μs | 0.073 μs | 0.061 μs |  0.13 |         - |        0.00 |
|               Linq | 110.51 μs | 0.325 μs | 0.304 μs |  1.00 |     152 B |        1.00 |
|    LinqGenDelegate |  30.43 μs | 0.062 μs | 0.055 μs |  0.28 |         - |        0.00 |
|      LinqGenStruct |  17.67 μs | 0.024 μs | 0.020 μs |  0.16 |         - |        0.00 |
| StructLinqDelegate |  51.52 μs | 0.308 μs | 0.257 μs |  0.47 |      96 B |        0.63 |
|   StructLinqStruct |  36.94 μs | 0.034 μs | 0.030 μs |  0.33 |         - |        0.00 |
|  HyperLinqDelegate |  41.83 μs | 0.162 μs | 0.152 μs |  0.38 |         - |        0.00 |
|    HyperLinqStruct |  17.46 μs | 0.051 μs | 0.042 μs |  0.16 |         - |        0.00 |
