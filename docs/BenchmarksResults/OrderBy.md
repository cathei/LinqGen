## OrderBy

### Source
[OrderBy.cs](../../LinqGen.Benchmarks/Cases/OrderBy.cs)

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
|               **Linq** | **Int32[10000]** | **1,168,735.1 ns** |  **6,725.90 ns** |  **6,291.41 ns** | **54.6875** |  **120313 B** |
|    LinqGenDelegate | Int32[10000] |   903,215.3 ns | 11,258.88 ns |  9,980.70 ns |       - |       1 B |
|      LinqGenStruct | Int32[10000] |   840,615.0 ns | 16,312.92 ns | 15,259.11 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,212,909.0 ns |  4,534.94 ns |  4,241.98 ns |       - |     379 B |
|   StructLinqStruct | Int32[10000] |   914,346.8 ns | 11,769.90 ns | 11,009.58 ns |       - |     129 B |
|               **Linq** |    **Int32[20]** |       **672.0 ns** |      **3.10 ns** |      **2.75 ns** |  **0.2632** |     **552 B** |
|    LinqGenDelegate |    Int32[20] |       429.1 ns |      0.43 ns |      0.38 ns |       - |         - |
|      LinqGenStruct |    Int32[20] |       336.5 ns |      0.57 ns |      0.53 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       703.4 ns |      1.46 ns |      1.22 ns |  0.0420 |      88 B |
|   StructLinqStruct |    Int32[20] |       435.2 ns |      1.00 ns |      0.93 ns |       - |         - |
|               **Linq** |   **Int32[500]** |    **32,105.5 ns** |    **112.20 ns** |    **104.95 ns** |  **2.9907** |    **6312 B** |
|    LinqGenDelegate |   Int32[500] |    14,518.6 ns |    231.64 ns |    247.86 ns |       - |         - |
|      LinqGenStruct |   Int32[500] |    10,035.2 ns |     45.29 ns |     40.15 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    30,074.9 ns |    334.56 ns |    296.58 ns |       - |      88 B |
|   StructLinqStruct |   Int32[500] |    12,648.0 ns |    127.43 ns |    119.19 ns |       - |         - |
