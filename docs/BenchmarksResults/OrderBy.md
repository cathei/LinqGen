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
|               **Linq** | **Int32[10000]** | **1,188,104.8 ns** | **5,794.67 ns** | **5,420.34 ns** | **54.6875** |  **120313 B** |
|    LinqGenDelegate | Int32[10000] | 1,120,008.6 ns | 7,007.96 ns | 6,555.25 ns |       - |       1 B |
|      LinqGenStruct | Int32[10000] |   813,058.2 ns | 5,122.06 ns | 4,791.18 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,221,807.6 ns | 5,816.16 ns | 5,440.44 ns |       - |      90 B |
|   StructLinqStruct | Int32[10000] |   906,275.8 ns | 5,717.33 ns | 5,347.99 ns |       - |       1 B |
|               **Linq** |    **Int32[20]** |       **675.0 ns** |     **2.12 ns** |     **1.98 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       606.1 ns |     2.13 ns |     1.89 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       338.4 ns |     2.24 ns |     1.99 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       702.6 ns |     2.62 ns |     2.45 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       434.5 ns |     1.71 ns |     1.60 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,754.4 ns** |   **207.70 ns** |   **194.29 ns** |  **2.9907** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |    29,479.3 ns |   341.48 ns |   319.42 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |     9,005.9 ns |    38.13 ns |    35.67 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    28,213.1 ns |   249.59 ns |   221.25 ns |  0.0305 |      88 B |
|   StructLinqStruct |   Int32[500] |    13,035.7 ns |   176.19 ns |   164.81 ns |       - |         - |
