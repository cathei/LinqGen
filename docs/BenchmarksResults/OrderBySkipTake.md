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
| Method             |         list |           Mean |       Error |      StdDev |    Gen0 | Allocated |
|--------------------|------------- |---------------:|------------:|------------:|--------:|----------:|
| **Linq**           | **Int32[10000]** |    **97,048.5 ns** |   **524.73 ns** |   **465.16 ns** | **56.5186** |  **120312 B** |
| LinqGenDelegate    | Int32[10000] |    12,678.8 ns |    33.76 ns |    31.58 ns |       - |         - |
| LinqGenStruct      | Int32[10000] |    12,701.9 ns |    51.74 ns |    45.87 ns |       - |         - |
| StructLinqDelegate | Int32[10000] | 1,214,990.4 ns | 8,611.23 ns | 8,054.95 ns |       - |     427 B |
| StructLinqStruct   | Int32[10000] |   910,009.5 ns | 6,675.71 ns | 6,244.46 ns |       - |     145 B |
| **Linq**           |    **Int32[20]** |       **554.1 ns** |     **2.65 ns** |     **2.48 ns** |  **0.2632** |     **552 B** |
| LinqGenDelegate    |    Int32[20] |       129.9 ns |     0.47 ns |     0.39 ns |       - |         - |
| LinqGenStruct      |    Int32[20] |       127.8 ns |     0.72 ns |     0.67 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       737.4 ns |     3.55 ns |     3.32 ns |  0.0725 |     152 B |
| StructLinqStruct   |    Int32[20] |       459.7 ns |     1.15 ns |     1.08 ns |       - |         - |
| **Linq**           |   **Int32[500]** |     **9,052.0 ns** |    **34.82 ns** |    **32.57 ns** |  **3.0060** |    **6312 B** |
| LinqGenDelegate    |   Int32[500] |       850.8 ns |     2.87 ns |     2.68 ns |       - |         - |
| LinqGenStruct      |   Int32[500] |       827.3 ns |     5.69 ns |     5.04 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    29,972.4 ns |   204.66 ns |   159.79 ns |  0.0610 |     152 B |
| StructLinqStruct   |   Int32[500] |    12,167.8 ns |    41.55 ns |    36.83 ns |       - |         - |
