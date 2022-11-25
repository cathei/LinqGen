﻿## ListWhereToArray

### Source
[ListWhereToArray.cs](../../LinqGen.Benchmarks/Cases/ListWhereToArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |   Count |           Mean |         Error |        StdDev | Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated | Alloc Ratio |
|------------------- |-------- |---------------:|--------------:|--------------:|------:|--------:|---------:|---------:|---------:|----------:|------------:|
|               **Linq** |     **100** |       **452.8 ns** |       **2.88 ns** |       **2.55 ns** |  **1.00** |    **0.00** |   **0.3633** |        **-** |        **-** |     **760 B** |        **1.00** |
|    LinqGenDelegate |     100 |       454.3 ns |       2.31 ns |       2.16 ns |  1.00 |    0.01 |   0.1144 |        - |        - |     240 B |        0.32 |
|      LinqGenStruct |     100 |       354.3 ns |       2.07 ns |       1.93 ns |  0.78 |    0.01 |   0.1144 |        - |        - |     240 B |        0.32 |
| StructLinqDelegate |     100 |       330.5 ns |       1.08 ns |       1.01 ns |  0.73 |    0.00 |   0.1450 |        - |        - |     304 B |        0.40 |
|   StructLinqStruct |     100 |       222.2 ns |       0.66 ns |       0.59 ns |  0.49 |    0.00 |   0.1147 |        - |        - |     240 B |        0.32 |
|  HyperLinqDelegate |     100 |       465.6 ns |       1.89 ns |       1.58 ns |  1.03 |    0.01 |   0.1144 |        - |        - |     240 B |        0.32 |
|    HyperLinqStruct |     100 |       368.1 ns |       1.00 ns |       0.93 ns |  0.81 |    0.01 |   0.1144 |        - |        - |     240 B |        0.32 |
|                    |         |                |               |               |       |         |          |          |          |           |             |
|               **Linq** |   **10000** |    **55,357.4 ns** |     **489.35 ns** |     **457.74 ns** |  **1.00** |    **0.00** |  **25.6348** |        **-** |        **-** |   **53600 B** |        **1.00** |
|    LinqGenDelegate |   10000 |    51,417.6 ns |     442.75 ns |     414.15 ns |  0.93 |    0.01 |   9.5825 |        - |        - |   20160 B |        0.38 |
|      LinqGenStruct |   10000 |    28,054.2 ns |     551.94 ns |     891.28 ns |  0.52 |    0.01 |   9.6130 |        - |        - |   20160 B |        0.38 |
| StructLinqDelegate |   10000 |    54,955.5 ns |     308.29 ns |     288.38 ns |  0.99 |    0.01 |   9.5825 |        - |        - |   20224 B |        0.38 |
|   StructLinqStruct |   10000 |    37,326.3 ns |     736.82 ns |     788.39 ns |  0.67 |    0.02 |   9.5215 |        - |        - |   20160 B |        0.38 |
|  HyperLinqDelegate |   10000 |    51,090.9 ns |     349.11 ns |     326.56 ns |  0.92 |    0.01 |   9.5215 |        - |        - |   20160 B |        0.38 |
|    HyperLinqStruct |   10000 |    36,342.3 ns |     700.56 ns |     885.98 ns |  0.66 |    0.02 |   9.5215 |        - |        - |   20160 B |        0.38 |
|                    |         |                |               |               |       |         |          |          |          |           |             |
|               **Linq** | **1000000** | **6,996,105.2 ns** | **129,072.35 ns** | **120,734.35 ns** |  **1.00** |    **0.00** | **429.6875** | **375.0000** | **367.1875** | **4099853 B** |        **1.00** |
|    LinqGenDelegate | 1000000 | 6,235,831.6 ns |  37,246.40 ns |  33,017.96 ns |  0.89 |    0.02 | 148.4375 | 148.4375 | 148.4375 | 2001006 B |        0.49 |
|      LinqGenStruct | 1000000 | 5,309,786.8 ns |  23,692.04 ns |  22,161.55 ns |  0.76 |    0.01 | 148.4375 | 148.4375 | 148.4375 | 2001005 B |        0.49 |
| StructLinqDelegate | 1000000 | 6,101,019.1 ns |  27,678.72 ns |  25,890.69 ns |  0.87 |    0.01 |  78.1250 |  78.1250 |  78.1250 | 2001307 B |        0.49 |
|   StructLinqStruct | 1000000 | 5,227,711.0 ns |  33,042.48 ns |  29,291.30 ns |  0.75 |    0.01 | 148.4375 | 148.4375 | 148.4375 | 2001058 B |        0.49 |
|  HyperLinqDelegate | 1000000 | 6,056,567.0 ns |  52,183.67 ns |  48,812.63 ns |  0.87 |    0.02 | 171.8750 | 171.8750 | 171.8750 | 2001141 B |        0.49 |
|    HyperLinqStruct | 1000000 | 5,199,972.1 ns |  32,962.96 ns |  30,833.57 ns |  0.74 |    0.01 | 187.5000 | 187.5000 | 187.5000 | 2001162 B |        0.49 |