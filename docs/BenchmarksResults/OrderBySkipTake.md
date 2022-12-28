## OrderBySkipTake

### Source
[OrderBySkipTake.cs](../../LinqGen.Benchmarks/Cases/OrderBySkipTake.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |         list |           Mean |       Error |      StdDev |    Gen0 | Allocated |
|------------------- |------------- |---------------:|------------:|------------:|--------:|----------:|
|               **Linq** | **Int32[10000]** |    **96,561.3 ns** | **1,912.35 ns** | **2,552.93 ns** | **56.5186** |  **120312 B** |
|    LinqGenDelegate | Int32[10000] |    51,118.6 ns | 1,021.22 ns | 1,815.22 ns |       - |         - |
|      LinqGenStruct | Int32[10000] |    37,018.3 ns |   729.02 ns |   997.89 ns |       - |         - |
| StructLinqDelegate | Int32[10000] | 1,196,052.1 ns | 1,696.46 ns | 1,416.62 ns |       - |     427 B |
|   StructLinqStruct | Int32[10000] |   890,707.5 ns | 2,708.43 ns | 2,533.47 ns |       - |     145 B |
|               **Linq** |    **Int32[20]** |       **538.0 ns** |     **1.13 ns** |     **1.06 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       345.5 ns |     0.36 ns |     0.33 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       306.8 ns |     0.33 ns |     0.31 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       747.8 ns |     6.69 ns |     6.26 ns |  0.0725 |     152 B |
|   StructLinqStruct |    Int32[20] |       461.0 ns |     5.00 ns |     4.68 ns |       - |         - |
|               **Linq** |   **Int32[500]** |     **8,944.6 ns** |    **91.77 ns** |    **85.84 ns** |  **3.0060** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |     3,599.4 ns |    23.70 ns |    22.17 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |     2,752.9 ns |    18.44 ns |    17.25 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    29,902.0 ns |   596.70 ns |   928.99 ns |  0.0610 |     152 B |
|   StructLinqStruct |   Int32[500] |    12,750.6 ns |   253.62 ns |   450.81 ns |       - |         - |
