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
|             Method |              list |           Mean |       Error |      StdDev |    Gen0 |    Gen1 | Allocated |
|------------------- |------------------ |---------------:|------------:|------------:|--------:|--------:|----------:|
|               **Linq** | **IntWrapper[10000]** | **1,634,070.9 ns** | **5,798.92 ns** | **5,424.31 ns** | **70.3125** | **15.6250** |  **200313 B** |
|    LinqGenDelegate | IntWrapper[10000] | 1,951,424.0 ns | 6,674.89 ns | 6,243.70 ns |       - |       - |       3 B |
|      LinqGenStruct | IntWrapper[10000] | 1,205,376.7 ns | 6,282.11 ns | 5,568.93 ns |       - |       - |       1 B |
| StructLinqDelegate | IntWrapper[10000] | 1,917,701.6 ns | 5,257.16 ns | 4,917.55 ns |       - |       - |      91 B |
|   StructLinqStruct | IntWrapper[10000] | 1,091,084.4 ns | 2,625.96 ns | 2,456.32 ns |       - |       - |       3 B |
|               **Linq** |    **IntWrapper[20]** |     **1,106.2 ns** |     **5.74 ns** |     **5.37 ns** |  **0.3395** |       **-** |     **712 B** |
|    LinqGenDelegate |    IntWrapper[20] |     1,239.4 ns |     7.61 ns |     7.12 ns |       - |       - |         - |
|      LinqGenStruct |    IntWrapper[20] |       692.0 ns |     2.56 ns |     2.27 ns |       - |       - |         - |
| StructLinqDelegate |    IntWrapper[20] |     1,295.4 ns |     2.92 ns |     2.73 ns |  0.0420 |       - |      88 B |
|   StructLinqStruct |    IntWrapper[20] |       657.2 ns |     2.19 ns |     2.05 ns |       - |       - |         - |
|               **Linq** |   **IntWrapper[500]** |    **49,263.4 ns** |   **399.54 ns** |   **354.18 ns** |  **4.8828** |       **-** |   **10312 B** |
|    LinqGenDelegate |   IntWrapper[500] |    62,608.6 ns |   444.64 ns |   371.29 ns |       - |       - |         - |
|      LinqGenStruct |   IntWrapper[500] |    23,825.9 ns |   424.85 ns |   397.41 ns |       - |       - |         - |
| StructLinqDelegate |   IntWrapper[500] |    61,977.4 ns |   197.44 ns |   164.87 ns |       - |       - |      88 B |
|   StructLinqStruct |   IntWrapper[500] |    17,908.7 ns |   161.20 ns |   150.79 ns |       - |       - |         - |
