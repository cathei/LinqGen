## OrderBySkipTake

### Source
[OrderBySkipTake.cs](../../LinqGen.Benchmarks/Cases/OrderBySkipTake.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |         list |           Mean |       Error |      StdDev |    Gen0 | Allocated |
|------------------- |------------- |---------------:|------------:|------------:|--------:|----------:|
|               **Linq** | **Int32[10000]** |    **99,703.6 ns** |   **420.49 ns** |   **372.75 ns** | **56.5186** |  **120312 B** |
|    LinqGenDelegate | Int32[10000] |    12,722.3 ns |    35.87 ns |    33.55 ns |       - |         - |
|      LinqGenStruct | Int32[10000] |    12,838.0 ns |   103.96 ns |    92.16 ns |       - |         - |
| StructLinqDelegate | Int32[10000] | 1,237,376.1 ns | 3,493.01 ns | 2,916.82 ns |       - |     154 B |
|   StructLinqStruct | Int32[10000] |   912,531.5 ns | 3,798.63 ns | 3,553.24 ns |       - |       1 B |
|               **Linq** |    **Int32[20]** |       **558.2 ns** |     **3.13 ns** |     **2.93 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       124.0 ns |     0.70 ns |     0.66 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       126.4 ns |     0.30 ns |     0.27 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       741.8 ns |     3.42 ns |     3.20 ns |  0.0725 |     152 B |
|   StructLinqStruct |    Int32[20] |       464.7 ns |     2.03 ns |     1.90 ns |       - |         - |
|               **Linq** |   **Int32[500]** |     **9,308.8 ns** |    **39.72 ns** |    **37.16 ns** |  **3.0060** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |       852.5 ns |     2.77 ns |     2.59 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |       832.8 ns |     3.14 ns |     2.94 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    30,382.7 ns |   356.41 ns |   315.95 ns |  0.0610 |     152 B |
|   StructLinqStruct |   Int32[500] |    12,686.7 ns |    70.18 ns |    62.21 ns |       - |         - |
