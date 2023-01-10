## ArrayWhereSelectSum

### Source
[ArrayWhereSelectSum.cs](../../LinqGen.Benchmarks/Cases/ArrayWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.1 (7.0.122.56804), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.1 (7.0.122.56804), Arm64 RyuJIT AdvSIMD


```
|             Method |   Count |            Mean |         Error |        StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------- |-------- |----------------:|--------------:|--------------:|------:|--------:|-------:|----------:|------------:|
|            **ForLoop** |     **100** |        **74.89 ns** |      **0.842 ns** |      **0.746 ns** |  **0.18** |    **0.00** |      **-** |         **-** |        **0.00** |
|        ForEachLoop |     100 |        75.34 ns |      0.703 ns |      0.624 ns |  0.18 |    0.00 |      - |         - |        0.00 |
|               Linq |     100 |       409.21 ns |      7.808 ns |      6.922 ns |  1.00 |    0.00 | 0.0162 |     104 B |        1.00 |
|    LinqGenDelegate |     100 |       216.04 ns |      2.300 ns |      2.151 ns |  0.53 |    0.01 |      - |         - |        0.00 |
|      LinqGenStruct |     100 |        66.28 ns |      0.306 ns |      0.255 ns |  0.16 |    0.00 |      - |         - |        0.00 |
| StructLinqDelegate |     100 |       279.12 ns |      0.974 ns |      0.911 ns |  0.68 |    0.01 | 0.0153 |      96 B |        0.92 |
|   StructLinqStruct |     100 |       210.17 ns |      1.344 ns |      1.257 ns |  0.51 |    0.01 |      - |         - |        0.00 |
|  HyperLinqDelegate |     100 |       224.65 ns |      1.015 ns |      0.949 ns |  0.55 |    0.01 |      - |         - |        0.00 |
|    HyperLinqStruct |     100 |       111.28 ns |      0.335 ns |      0.313 ns |  0.27 |    0.00 |      - |         - |        0.00 |
|                    |         |                 |               |               |       |         |        |           |             |
|            **ForLoop** |   **10000** |    **17,195.40 ns** |    **556.051 ns** |  **1,639.528 ns** |  **0.23** |    **0.02** |      **-** |         **-** |        **0.00** |
|        ForEachLoop |   10000 |    15,215.47 ns |    641.500 ns |  1,881.409 ns |  0.22 |    0.02 |      - |         - |        0.00 |
|               Linq |   10000 |    71,356.94 ns |    385.645 ns |    301.086 ns |  1.00 |    0.00 |      - |     104 B |        1.00 |
|    LinqGenDelegate |   10000 |    42,694.14 ns |    829.846 ns |  1,340.046 ns |  0.58 |    0.02 |      - |         - |        0.00 |
|      LinqGenStruct |   10000 |     8,121.78 ns |    320.852 ns |    941.004 ns |  0.12 |    0.02 |      - |         - |        0.00 |
| StructLinqDelegate |   10000 |    49,117.05 ns |    556.645 ns |    493.451 ns |  0.69 |    0.01 |      - |      96 B |        0.92 |
|   StructLinqStruct |   10000 |    19,504.98 ns |    384.972 ns |    907.424 ns |  0.28 |    0.02 |      - |         - |        0.00 |
|  HyperLinqDelegate |   10000 |    31,356.81 ns |    381.750 ns |    357.090 ns |  0.44 |    0.00 |      - |         - |        0.00 |
|    HyperLinqStruct |   10000 |     9,021.44 ns |     93.385 ns |     82.784 ns |  0.13 |    0.00 |      - |         - |        0.00 |
|                    |         |                 |               |               |       |         |        |           |             |
|            **ForLoop** | **1000000** | **3,504,226.11 ns** | **39,907.852 ns** | **37,329.830 ns** |  **0.44** |    **0.01** |      **-** |       **3 B** |        **0.03** |
|        ForEachLoop | 1000000 | 3,300,136.93 ns | 20,085.343 ns | 18,787.843 ns |  0.42 |    0.00 |      - |       3 B |        0.03 |
|               Linq | 1000000 | 7,891,118.63 ns | 46,395.306 ns | 38,742.182 ns |  1.00 |    0.00 |      - |     117 B |        1.00 |
|    LinqGenDelegate | 1000000 | 4,765,647.72 ns | 40,324.554 ns | 37,719.613 ns |  0.60 |    0.00 |      - |       6 B |        0.05 |
|      LinqGenStruct | 1000000 | 3,327,080.28 ns | 11,085.657 ns | 10,369.530 ns |  0.42 |    0.00 |      - |       3 B |        0.03 |
| StructLinqDelegate | 1000000 | 5,648,235.21 ns | 24,730.542 ns | 21,922.981 ns |  0.72 |    0.00 |      - |     102 B |        0.87 |
|   StructLinqStruct | 1000000 | 3,579,690.29 ns | 16,552.035 ns | 14,672.948 ns |  0.45 |    0.00 |      - |       3 B |        0.03 |
|  HyperLinqDelegate | 1000000 | 4,418,747.19 ns | 15,823.498 ns | 14,801.310 ns |  0.56 |    0.00 |      - |       6 B |        0.05 |
|    HyperLinqStruct | 1000000 | 2,985,805.73 ns | 31,109.770 ns | 29,100.098 ns |  0.38 |    0.00 |      - |       3 B |        0.03 |
