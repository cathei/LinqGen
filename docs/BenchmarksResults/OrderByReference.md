## OrderByReference

### Source
[OrderByReference.cs](../../LinqGen.Benchmarks/Cases/OrderByReference.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |              list |           Mean |        Error |       StdDev |    Gen0 |    Gen1 | Allocated |
|------------------- |------------------ |---------------:|-------------:|-------------:|--------:|--------:|----------:|
|               **Linq** | **IntWrapper[10000]** | **1,609,214.6 ns** |  **3,768.51 ns** |  **3,525.07 ns** | **72.2656** | **15.6250** |  **200313 B** |
|    LinqGenDelegate | IntWrapper[10000] | 1,610,117.4 ns | 10,711.64 ns | 10,019.68 ns |       - |       - |       1 B |
|      LinqGenStruct | IntWrapper[10000] |   977,495.0 ns |  5,088.45 ns |  4,510.78 ns |       - |       - |       1 B |
| StructLinqDelegate | IntWrapper[10000] | 1,945,021.5 ns |  5,021.11 ns |  4,696.75 ns |       - |       - |      93 B |
|   StructLinqStruct | IntWrapper[10000] | 1,098,937.6 ns |  4,375.65 ns |  4,092.98 ns |       - |       - |       3 B |
|               **Linq** |    **IntWrapper[20]** |     **1,104.0 ns** |      **3.67 ns** |      **3.43 ns** |  **0.3395** |       **-** |     **712 B** |
|    LinqGenDelegate |    IntWrapper[20] |       970.2 ns |      3.50 ns |      3.11 ns |       - |       - |         - |
|      LinqGenStruct |    IntWrapper[20] |       494.3 ns |      2.05 ns |      1.82 ns |       - |       - |         - |
| StructLinqDelegate |    IntWrapper[20] |     1,293.5 ns |      3.63 ns |      3.40 ns |  0.0420 |       - |      88 B |
|   StructLinqStruct |    IntWrapper[20] |       657.5 ns |      2.61 ns |      2.44 ns |       - |       - |         - |
|               **Linq** |   **IntWrapper[500]** |    **48,702.8 ns** |    **122.41 ns** |    **108.52 ns** |  **4.8828** |       **-** |   **10312 B** |
|    LinqGenDelegate |   IntWrapper[500] |    47,305.0 ns |    293.69 ns |    260.35 ns |       - |       - |         - |
|      LinqGenStruct |   IntWrapper[500] |    12,696.7 ns |     66.18 ns |     58.67 ns |       - |       - |         - |
| StructLinqDelegate |   IntWrapper[500] |    61,190.6 ns |    317.18 ns |    296.69 ns |       - |       - |      88 B |
|   StructLinqStruct |   IntWrapper[500] |    17,582.8 ns |    104.84 ns |     81.85 ns |       - |       - |         - |
