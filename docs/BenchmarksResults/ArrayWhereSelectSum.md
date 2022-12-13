## ArrayWhereSelectSum

### Source
[ArrayWhereSelectSum.cs](../../LinqGen.Benchmarks/Cases/ArrayWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |   Count |            Mean |         Error |        StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|------------------- |-------- |----------------:|--------------:|--------------:|------:|--------:|-------:|----------:|------------:|
|            **ForLoop** |     **100** |        **96.27 ns** |      **0.348 ns** |      **0.326 ns** |  **0.21** |    **0.00** |      **-** |         **-** |        **0.00** |
|        ForEachLoop |     100 |        95.54 ns |      0.493 ns |      0.462 ns |  0.20 |    0.00 |      - |         - |        0.00 |
|               Linq |     100 |       467.42 ns |      8.303 ns |      7.767 ns |  1.00 |    0.00 | 0.0496 |     104 B |        1.00 |
|    LinqGenDelegate |     100 |       228.58 ns |      0.609 ns |      0.570 ns |  0.49 |    0.01 |      - |         - |        0.00 |
|      LinqGenStruct |     100 |       197.85 ns |      0.877 ns |      0.820 ns |  0.42 |    0.01 |      - |         - |        0.00 |
| StructLinqDelegate |     100 |       283.18 ns |      4.302 ns |      3.814 ns |  0.61 |    0.02 | 0.0458 |      96 B |        0.92 |
|   StructLinqStruct |     100 |       213.53 ns |      0.961 ns |      0.899 ns |  0.46 |    0.01 |      - |         - |        0.00 |
|  HyperLinqDelegate |     100 |       226.52 ns |      3.612 ns |      3.202 ns |  0.48 |    0.01 |      - |         - |        0.00 |
|    HyperLinqStruct |     100 |       117.92 ns |      2.237 ns |      2.092 ns |  0.25 |    0.01 |      - |         - |        0.00 |
|                    |         |                 |               |               |       |         |        |           |             |
|            **ForLoop** |   **10000** |    **21,077.66 ns** |    **859.716 ns** |  **2,534.893 ns** |  **0.26** |    **0.02** |      **-** |         **-** |        **0.00** |
|        ForEachLoop |   10000 |    20,719.75 ns |    799.396 ns |  2,357.036 ns |  0.24 |    0.04 |      - |         - |        0.00 |
|               Linq |   10000 |    80,153.40 ns |    430.475 ns |    381.605 ns |  1.00 |    0.00 |      - |     104 B |        1.00 |
|    LinqGenDelegate |   10000 |    51,180.75 ns |    432.631 ns |    383.516 ns |  0.64 |    0.01 |      - |         - |        0.00 |
|      LinqGenStruct |   10000 |    22,681.41 ns |    441.982 ns |  1,187.355 ns |  0.28 |    0.01 |      - |         - |        0.00 |
| StructLinqDelegate |   10000 |    53,189.38 ns |    225.030 ns |    210.493 ns |  0.66 |    0.00 |      - |      96 B |        0.92 |
|   StructLinqStruct |   10000 |    22,151.71 ns |    441.391 ns |    931.043 ns |  0.28 |    0.01 |      - |         - |        0.00 |
|  HyperLinqDelegate |   10000 |    37,890.71 ns |    375.942 ns |    333.263 ns |  0.47 |    0.00 |      - |         - |        0.00 |
|    HyperLinqStruct |   10000 |     9,125.27 ns |     99.677 ns |     93.238 ns |  0.11 |    0.00 |      - |         - |        0.00 |
|                    |         |                 |               |               |       |         |        |           |             |
|            **ForLoop** | **1000000** | **4,631,809.70 ns** | **11,712.565 ns** | **10,955.941 ns** |  **0.51** |    **0.00** |      **-** |       **5 B** |        **0.04** |
|        ForEachLoop | 1000000 | 4,608,446.62 ns |  4,167.696 ns |  3,898.465 ns |  0.51 |    0.00 |      - |       5 B |        0.04 |
|               Linq | 1000000 | 9,042,176.80 ns | 21,710.126 ns | 19,245.461 ns |  1.00 |    0.00 |      - |     114 B |        1.00 |
|    LinqGenDelegate | 1000000 | 5,970,134.45 ns | 12,920.672 ns | 10,789.346 ns |  0.66 |    0.00 |      - |       5 B |        0.04 |
|      LinqGenStruct | 1000000 | 4,654,940.58 ns |  8,283.525 ns |  7,748.414 ns |  0.51 |    0.00 |      - |       5 B |        0.04 |
| StructLinqDelegate | 1000000 | 6,143,315.35 ns | 22,348.238 ns | 20,904.556 ns |  0.68 |    0.00 |      - |     101 B |        0.89 |
|   StructLinqStruct | 1000000 | 4,721,607.00 ns | 43,440.957 ns | 38,509.276 ns |  0.52 |    0.00 |      - |       5 B |        0.04 |
|  HyperLinqDelegate | 1000000 | 5,507,330.08 ns | 13,507.338 ns | 11,279.239 ns |  0.61 |    0.00 |      - |       5 B |        0.04 |
|    HyperLinqStruct | 1000000 | 4,174,158.20 ns | 17,825.167 ns | 16,673.672 ns |  0.46 |    0.00 |      - |       5 B |        0.04 |
