## OrderByReference

### Source
[OrderByReference.cs](../../LinqGen.Benchmarks/Cases/OrderByReference.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |              list |           Mean |        Error |       StdDev |    Gen0 |    Gen1 | Allocated |
|------------------- |------------------ |---------------:|-------------:|-------------:|--------:|--------:|----------:|
|               **Linq** | **IntWrapper[10000]** | **1,652,057.4 ns** |  **4,419.30 ns** |  **3,917.60 ns** | **72.2656** | **15.6250** |  **200313 B** |
|    LinqGenDelegate | IntWrapper[10000] | 1,605,568.0 ns |  5,681.86 ns |  5,036.82 ns |       - |       - |       1 B |
|      LinqGenStruct | IntWrapper[10000] | 1,018,464.4 ns | 14,756.04 ns | 13,802.81 ns |       - |       - |       1 B |
| StructLinqDelegate | IntWrapper[10000] | 1,936,958.4 ns |  8,222.54 ns |  7,691.37 ns |       - |       - |      91 B |
|   StructLinqStruct | IntWrapper[10000] | 1,110,104.9 ns |  3,769.44 ns |  3,525.94 ns |       - |       - |       3 B |
|               **Linq** |    **IntWrapper[20]** |     **1,113.5 ns** |      **7.12 ns** |      **6.66 ns** |  **0.3395** |       **-** |     **712 B** |
|    LinqGenDelegate |    IntWrapper[20] |     1,014.3 ns |      3.82 ns |      3.57 ns |       - |       - |         - |
|      LinqGenStruct |    IntWrapper[20] |       596.2 ns |      2.74 ns |      2.28 ns |       - |       - |         - |
| StructLinqDelegate |    IntWrapper[20] |     1,317.2 ns |      4.46 ns |      4.17 ns |  0.0420 |       - |      88 B |
|   StructLinqStruct |    IntWrapper[20] |       665.1 ns |      3.54 ns |      3.31 ns |       - |       - |         - |
|               **Linq** |   **IntWrapper[500]** |    **49,361.9 ns** |    **169.62 ns** |    **158.66 ns** |  **4.8828** |       **-** |   **10312 B** |
|    LinqGenDelegate |   IntWrapper[500] |    46,730.5 ns |    217.83 ns |    203.76 ns |       - |       - |         - |
|      LinqGenStruct |   IntWrapper[500] |    14,469.6 ns |     64.62 ns |     53.96 ns |       - |       - |         - |
| StructLinqDelegate |   IntWrapper[500] |    63,166.7 ns |    363.86 ns |    340.35 ns |       - |       - |      88 B |
|   StructLinqStruct |   IntWrapper[500] |    17,886.4 ns |    155.12 ns |    137.51 ns |       - |       - |         - |
