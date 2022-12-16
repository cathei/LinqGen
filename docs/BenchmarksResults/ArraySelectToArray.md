﻿## ArraySelectToArray

### Source
[ArraySelectToArray.cs](../../LinqGen.Benchmarks/Cases/ArraySelectToArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |   Count |            Mean |         Error |        StdDev |          Median | Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated | Alloc Ratio |
|------------------- |-------- |----------------:|--------------:|--------------:|----------------:|------:|--------:|---------:|---------:|---------:|----------:|------------:|
|               **Linq** |     **100** |       **209.08 ns** |      **0.849 ns** |      **0.753 ns** |       **209.08 ns** |  **1.00** |    **0.00** |   **0.2255** |        **-** |        **-** |     **472 B** |        **1.00** |
|    LinqGenDelegate |     100 |       346.70 ns |      2.979 ns |      2.326 ns |       347.02 ns |  1.66 |    0.01 |   0.2027 |        - |        - |     424 B |        0.90 |
|      LinqGenStruct |     100 |       242.80 ns |      1.499 ns |      1.403 ns |       242.84 ns |  1.16 |    0.01 |   0.2027 |        - |        - |     424 B |        0.90 |
| StructLinqDelegate |     100 |       188.95 ns |      1.086 ns |      0.907 ns |       189.19 ns |  0.90 |    0.01 |   0.2332 |        - |        - |     488 B |        1.03 |
|   StructLinqStruct |     100 |       133.64 ns |      0.668 ns |      0.625 ns |       133.69 ns |  0.64 |    0.00 |   0.2027 |        - |        - |     424 B |        0.90 |
|  HyperLinqDelegate |     100 |       165.05 ns |      1.186 ns |      1.109 ns |       165.06 ns |  0.79 |    0.01 |   0.2027 |        - |        - |     424 B |        0.90 |
|    HyperLinqStruct |     100 |        90.29 ns |      1.789 ns |      2.197 ns |        89.50 ns |  0.44 |    0.01 |   0.2027 |        - |        - |     424 B |        0.90 |
|                    |         |                 |               |               |                 |       |         |          |          |          |           |             |
|               **Linq** |   **10000** |    **14,875.08 ns** |    **112.167 ns** |    **104.921 ns** |    **14,879.36 ns** |  **1.00** |    **0.00** |  **18.8599** |        **-** |        **-** |   **40072 B** |        **1.00** |
|    LinqGenDelegate |   10000 |    25,392.28 ns |    181.279 ns |    151.376 ns |    25,387.92 ns |  1.70 |    0.01 |  18.8599 |        - |        - |   40024 B |        1.00 |
|      LinqGenStruct |   10000 |    13,012.96 ns |    114.216 ns |    101.249 ns |    12,986.16 ns |  0.87 |    0.01 |  18.8599 |        - |        - |   40024 B |        1.00 |
| StructLinqDelegate |   10000 |    15,217.20 ns |    120.857 ns |    107.137 ns |    15,198.11 ns |  1.02 |    0.01 |  18.8599 |        - |        - |   40088 B |        1.00 |
|   StructLinqStruct |   10000 |     8,898.67 ns |     98.978 ns |     92.584 ns |     8,895.96 ns |  0.60 |    0.01 |  18.8599 |        - |        - |   40024 B |        1.00 |
|  HyperLinqDelegate |   10000 |    14,820.10 ns |     57.128 ns |     50.642 ns |    14,829.18 ns |  1.00 |    0.01 |  18.8599 |        - |        - |   40024 B |        1.00 |
|    HyperLinqStruct |   10000 |     7,869.10 ns |     43.230 ns |     40.438 ns |     7,868.94 ns |  0.53 |    0.01 |  18.8599 |        - |        - |   40024 B |        1.00 |
|                    |         |                 |               |               |                 |       |         |          |          |          |           |             |
|               **Linq** | **1000000** | **1,679,397.30 ns** | **32,799.598 ns** | **39,045.588 ns** | **1,676,840.62 ns** |  **1.00** |    **0.00** | **117.1875** | **117.1875** | **117.1875** | **4000198 B** |        **1.00** |
|    LinqGenDelegate | 1000000 | 3,006,251.81 ns | 44,880.723 ns | 41,981.456 ns | 2,998,421.06 ns |  1.80 |    0.06 | 179.6875 | 179.6875 | 179.6875 | 4000144 B |        1.00 |
|      LinqGenStruct | 1000000 | 1,748,168.46 ns | 34,776.098 ns | 37,210.035 ns | 1,746,142.25 ns |  1.05 |    0.04 | 179.6875 | 179.6875 | 179.6875 | 4000143 B |        1.00 |
| StructLinqDelegate | 1000000 | 1,720,865.56 ns | 34,255.933 ns | 35,178.322 ns | 1,717,945.80 ns |  1.03 |    0.03 | 113.2813 | 113.2813 | 113.2813 | 4000226 B |        1.00 |
|   StructLinqStruct | 1000000 | 1,031,713.25 ns | 19,782.910 ns | 19,429.463 ns | 1,030,354.90 ns |  0.62 |    0.02 | 267.5781 | 267.5781 | 267.5781 | 4000197 B |        1.00 |
|  HyperLinqDelegate | 1000000 | 1,691,122.16 ns | 32,665.605 ns | 44,713.049 ns | 1,695,468.55 ns |  1.00 |    0.02 | 121.0938 | 121.0938 | 121.0938 | 4000105 B |        1.00 |
|    HyperLinqStruct | 1000000 |   972,956.75 ns | 27,555.295 ns | 80,380.088 ns |   949,792.54 ns |  0.54 |    0.02 | 176.7578 | 176.7578 | 176.7578 | 4000140 B |        1.00 |