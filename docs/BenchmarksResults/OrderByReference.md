## OrderByReference

### Source
[OrderByReference.cs](../../LinqGen.Benchmarks/Cases/OrderByReference.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |              list |           Mean |        Error |       StdDev |    Gen0 |    Gen1 | Allocated |
|------------------- |------------------ |---------------:|-------------:|-------------:|--------:|--------:|----------:|
|               **Linq** | **IntWrapper[10000]** | **1,610,524.8 ns** |  **6,688.72 ns** |  **5,929.37 ns** | **70.3125** | **15.6250** |  **200313 B** |
|    LinqGenDelegate | IntWrapper[10000] | 1,719,922.0 ns | 33,636.15 ns | 31,463.27 ns |       - |       - |       1 B |
|      LinqGenStruct | IntWrapper[10000] | 1,184,598.5 ns | 11,449.35 ns | 10,709.73 ns |       - |       - |       1 B |
| StructLinqDelegate | IntWrapper[10000] | 1,934,274.3 ns | 16,367.42 ns | 15,310.10 ns |       - |       - |     747 B |
|   StructLinqStruct | IntWrapper[10000] | 1,089,174.2 ns |  9,441.63 ns |  8,369.76 ns |       - |       - |     651 B |
|               **Linq** |    **IntWrapper[20]** |     **1,088.4 ns** |      **3.59 ns** |      **3.18 ns** |  **0.3395** |       **-** |     **712 B** |
|    LinqGenDelegate |    IntWrapper[20] |     1,143.9 ns |      3.48 ns |      3.25 ns |       - |       - |         - |
|      LinqGenStruct |    IntWrapper[20] |       672.3 ns |      2.77 ns |      2.59 ns |       - |       - |         - |
| StructLinqDelegate |    IntWrapper[20] |     1,277.6 ns |      4.41 ns |      4.12 ns |  0.0420 |       - |      88 B |
|   StructLinqStruct |    IntWrapper[20] |       649.7 ns |      3.12 ns |      2.92 ns |       - |       - |         - |
|               **Linq** |   **IntWrapper[500]** |    **47,698.1 ns** |    **264.65 ns** |    **247.55 ns** |  **4.8828** |       **-** |   **10312 B** |
|    LinqGenDelegate |   IntWrapper[500] |    53,996.2 ns |    241.67 ns |    188.68 ns |       - |       - |         - |
|      LinqGenStruct |   IntWrapper[500] |    22,674.5 ns |    388.43 ns |    363.34 ns |       - |       - |         - |
| StructLinqDelegate |   IntWrapper[500] |    60,219.2 ns |    175.45 ns |    164.11 ns |       - |       - |      89 B |
|   StructLinqStruct |   IntWrapper[500] |    17,182.5 ns |    149.17 ns |    139.53 ns |       - |       - |         - |
