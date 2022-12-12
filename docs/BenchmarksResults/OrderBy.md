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
|             Method |         list |           Mean |        Error |       StdDev |    Gen0 | Allocated |
|------------------- |------------- |---------------:|-------------:|-------------:|--------:|----------:|
|               **Linq** | **Int32[10000]** | **1,192,619.9 ns** |  **9,390.82 ns** |  **8,784.18 ns** | **54.6875** |  **120313 B** |
|    LinqGenDelegate | Int32[10000] | 1,177,624.2 ns | 22,307.86 ns | 18,628.08 ns |       - |       1 B |
|      LinqGenStruct | Int32[10000] |   879,087.5 ns | 16,537.04 ns | 15,468.76 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,228,406.5 ns |  5,772.77 ns |  5,117.41 ns |       - |     379 B |
|   StructLinqStruct | Int32[10000] |   918,504.7 ns |  6,867.98 ns |  6,424.31 ns |       - |     129 B |
|               **Linq** |    **Int32[20]** |       **678.2 ns** |      **2.15 ns** |      **1.90 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       692.0 ns |      4.64 ns |      4.34 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       443.4 ns |      3.04 ns |      2.84 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       707.0 ns |      3.47 ns |      3.25 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       433.4 ns |      1.79 ns |      1.67 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,688.1 ns** |    **181.74 ns** |    **170.00 ns** |  **2.9907** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |    28,622.8 ns |    532.66 ns |    472.19 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |    12,131.3 ns |    138.97 ns |    123.19 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    28,010.0 ns |    158.75 ns |    148.50 ns |  0.0305 |      88 B |
|   StructLinqStruct |   Int32[500] |    12,614.6 ns |     58.44 ns |     45.62 ns |       - |         - |
