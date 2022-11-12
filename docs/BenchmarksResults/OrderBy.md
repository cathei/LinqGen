## OrderBy

### Source
[OrderBy.cs](../../LinqGen.Benchmarks/Cases/OrderBy.cs)

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
|               **Linq** | **Int32[10000]** | **1,195,641.7 ns** | **4,531.55 ns** | **4,017.10 ns** | **54.6875** |  **120313 B** |
|    LinqGenDelegate | Int32[10000] | 1,185,401.3 ns | 6,457.14 ns | 6,040.01 ns |       - |       1 B |
|      LinqGenStruct | Int32[10000] |   915,971.7 ns | 9,205.47 ns | 8,610.81 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,229,346.8 ns | 7,959.54 ns | 7,445.36 ns |       - |     346 B |
|   StructLinqStruct | Int32[10000] |   928,049.5 ns | 9,537.44 ns | 8,921.33 ns |       - |     129 B |
|               **Linq** |    **Int32[20]** |       **692.0 ns** |     **6.22 ns** |     **5.52 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       718.4 ns |     3.84 ns |     3.60 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       499.0 ns |     2.24 ns |     1.99 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       727.1 ns |    14.27 ns |    25.00 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       443.1 ns |     0.77 ns |     0.68 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **33,672.4 ns** |   **340.26 ns** |   **301.64 ns** |  **2.9907** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |    31,467.5 ns |   438.56 ns |   410.23 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |    13,062.6 ns |    68.76 ns |    60.96 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    28,420.1 ns |   206.14 ns |   192.82 ns |  0.0305 |      88 B |
|   StructLinqStruct |   Int32[500] |    13,001.3 ns |    69.49 ns |    65.00 ns |       - |         - |
