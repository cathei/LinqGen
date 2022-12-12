## OrderByKey

### Source
[OrderByKey.cs](../../LinqGen.Benchmarks/Cases/OrderByKey.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |         list |           Mean |        Error |       StdDev |    Gen0 | Allocated |
|------------------- |------------- |---------------:|-------------:|-------------:|--------:|----------:|
|               **Linq** | **Int32[10000]** | **1,191,612.0 ns** | **14,421.66 ns** | **13,490.03 ns** | **54.6875** |  **120313 B** |
|    LinqGenDelegate | Int32[10000] | 1,171,528.8 ns |  4,515.16 ns |  3,770.36 ns |       - |       1 B |
|      LinqGenStruct | Int32[10000] |   880,688.7 ns |  9,512.98 ns |  8,898.45 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,265,806.3 ns | 10,642.12 ns |  9,954.64 ns |       - |     379 B |
|   StructLinqStruct | Int32[10000] |   921,673.0 ns | 11,279.39 ns | 10,550.75 ns |       - |     129 B |
|               **Linq** |    **Int32[20]** |       **704.1 ns** |      **3.38 ns** |      **3.17 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       811.7 ns |      3.79 ns |      3.54 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       536.5 ns |      1.49 ns |      1.39 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       992.6 ns |      3.93 ns |      3.49 ns |  0.0496 |     104 B |
|   StructLinqStruct |    Int32[20] |       659.3 ns |      3.34 ns |      2.79 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,998.7 ns** |    **257.57 ns** |    **228.33 ns** |  **2.9907** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |    31,907.8 ns |    569.26 ns |    951.11 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |    12,973.4 ns |    258.17 ns |    265.12 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    32,360.1 ns |    271.25 ns |    240.46 ns |       - |     104 B |
|   StructLinqStruct |   Int32[500] |    14,000.2 ns |     95.17 ns |     79.47 ns |       - |         - |
