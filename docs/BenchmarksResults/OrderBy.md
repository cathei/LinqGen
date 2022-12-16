## OrderBy

### Source
[OrderBy.cs](../../LinqGen.Benchmarks/Cases/OrderBy.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |         list |           Mean |       Error |      StdDev |    Gen0 | Allocated |
|------------------- |------------- |---------------:|------------:|------------:|--------:|----------:|
|               **Linq** | **Int32[10000]** | **1,198,507.2 ns** | **4,697.87 ns** | **4,394.39 ns** | **54.6875** |  **120313 B** |
|    LinqGenDelegate | Int32[10000] | 1,149,516.3 ns | 3,485.15 ns | 3,089.49 ns |       - |       1 B |
|      LinqGenStruct | Int32[10000] |   849,740.8 ns | 5,003.20 ns | 4,680.00 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,219,230.2 ns | 3,520.15 ns | 3,120.52 ns |       - |      90 B |
|   StructLinqStruct | Int32[10000] |   925,544.4 ns | 8,802.34 ns | 7,803.05 ns |       - |       1 B |
|               **Linq** |    **Int32[20]** |       **678.2 ns** |     **4.16 ns** |     **3.89 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       630.4 ns |     2.51 ns |     2.35 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       383.6 ns |     2.61 ns |     2.32 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       713.7 ns |     5.67 ns |     5.03 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       443.1 ns |     4.10 ns |     3.84 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,886.6 ns** |   **159.65 ns** |   **141.53 ns** |  **2.9907** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |    27,203.3 ns |   361.41 ns |   338.06 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |    11,055.5 ns |   175.66 ns |   155.72 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    28,760.3 ns |   573.99 ns |   563.74 ns |  0.0305 |      88 B |
|   StructLinqStruct |   Int32[500] |    12,921.5 ns |    96.22 ns |    80.35 ns |       - |         - |
