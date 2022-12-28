## OrderByKey

### Source
[OrderByKey.cs](../../LinqGen.Benchmarks/Cases/OrderByKey.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |         list |           Mean |        Error |       StdDev |    Gen0 | Allocated |
|------------------- |------------- |---------------:|-------------:|-------------:|--------:|----------:|
|               **Linq** | **Int32[10000]** | **1,174,256.0 ns** |  **5,664.90 ns** |  **5,298.95 ns** | **54.6875** |  **120313 B** |
|    LinqGenDelegate | Int32[10000] |   923,065.1 ns |  4,829.80 ns |  4,281.50 ns |       - |       1 B |
|      LinqGenStruct | Int32[10000] |   810,178.1 ns |  4,870.33 ns |  4,555.71 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,236,121.8 ns |  6,164.20 ns |  5,464.40 ns |       - |     366 B |
|   StructLinqStruct | Int32[10000] |   919,429.8 ns | 11,207.62 ns | 10,483.62 ns |       - |     137 B |
|               **Linq** |    **Int32[20]** |       **696.9 ns** |      **3.79 ns** |      **3.36 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       530.0 ns |      4.06 ns |      3.80 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       425.3 ns |      2.52 ns |      2.23 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |     1,024.5 ns |      6.06 ns |      5.67 ns |  0.0496 |     104 B |
|   StructLinqStruct |    Int32[20] |       676.3 ns |      6.37 ns |      5.96 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,019.6 ns** |    **198.98 ns** |    **166.16 ns** |  **2.9907** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |    16,570.8 ns |    330.28 ns |    659.60 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |     9,876.7 ns |    191.53 ns |    204.94 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    34,044.1 ns |    520.82 ns |    487.17 ns |       - |     104 B |
|   StructLinqStruct |   Int32[500] |    14,558.4 ns |    279.20 ns |    531.20 ns |       - |         - |
