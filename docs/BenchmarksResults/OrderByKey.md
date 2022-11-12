## OrderByKey

### Source
[OrderByKey.cs](../../LinqGen.Benchmarks/Cases/OrderByKey.cs)

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
|               **Linq** | **Int32[10000]** | **1,216,939.9 ns** | **6,007.92 ns** | **5,016.89 ns** | **54.6875** |  **120313 B** |
|    LinqGenDelegate | Int32[10000] | 1,218,050.1 ns | 2,846.98 ns | 2,663.07 ns |       - |       1 B |
|      LinqGenStruct | Int32[10000] |   934,624.4 ns | 4,845.56 ns | 4,532.54 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,282,138.1 ns | 6,842.41 ns | 6,065.62 ns |       - |     106 B |
|   StructLinqStruct | Int32[10000] |   946,597.5 ns | 5,423.64 ns | 5,073.27 ns |       - |       1 B |
|               **Linq** |    **Int32[20]** |       **712.2 ns** |     **5.60 ns** |     **5.24 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       832.7 ns |     3.06 ns |     2.86 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       554.5 ns |     1.13 ns |     1.00 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |     1,017.8 ns |     3.01 ns |     2.81 ns |  0.0496 |     104 B |
|   StructLinqStruct |    Int32[20] |       670.5 ns |     1.32 ns |     1.10 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,859.0 ns** |   **255.00 ns** |   **226.05 ns** |  **2.9907** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |    32,973.3 ns |   647.05 ns |   664.47 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |    13,509.3 ns |   116.04 ns |   108.55 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    33,951.0 ns |   297.76 ns |   278.52 ns |       - |     104 B |
|   StructLinqStruct |   Int32[500] |    14,725.6 ns |   146.23 ns |   129.63 ns |       - |         - |
