## OrderBySkipTake

### Source
[OrderBySkipTake.cs](../../LinqGen.Benchmarks/Cases/OrderBySkipTake.cs)

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
|               **Linq** | **Int32[10000]** |    **95,558.5 ns** |   **519.52 ns** |   **485.96 ns** | **56.5186** |  **120312 B** |
|    LinqGenDelegate | Int32[10000] |    84,690.8 ns |   388.37 ns |   363.28 ns |       - |         - |
|      LinqGenStruct | Int32[10000] |    43,503.8 ns |   844.23 ns |   829.15 ns |       - |         - |
| StructLinqDelegate | Int32[10000] | 1,205,861.8 ns | 4,091.41 ns | 3,827.11 ns |       - |     154 B |
|   StructLinqStruct | Int32[10000] |   892,996.4 ns | 9,847.03 ns | 9,210.92 ns |       - |       1 B |
|               **Linq** |    **Int32[20]** |       **553.4 ns** |     **3.01 ns** |     **2.67 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       540.9 ns |     1.28 ns |     1.14 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       392.6 ns |     0.93 ns |     0.87 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       724.5 ns |     3.28 ns |     3.07 ns |  0.0725 |     152 B |
|   StructLinqStruct |    Int32[20] |       453.3 ns |     1.14 ns |     1.01 ns |       - |         - |
|               **Linq** |   **Int32[500]** |     **9,022.8 ns** |    **54.56 ns** |    **51.04 ns** |  **3.0060** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |     7,032.4 ns |    83.08 ns |    77.71 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |     3,774.9 ns |    13.76 ns |    12.87 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    29,464.9 ns |   330.48 ns |   309.13 ns |  0.0610 |     152 B |
|   StructLinqStruct |   Int32[500] |    12,062.3 ns |    48.58 ns |    45.44 ns |       - |         - |
