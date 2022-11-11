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
|             Method |              list |           Mean |       Error |      StdDev |    Gen0 |    Gen1 | Allocated |
|------------------- |------------------ |---------------:|------------:|------------:|--------:|--------:|----------:|
|               **Linq** | **IntWrapper[10000]** | **1,639,812.2 ns** | **8,257.42 ns** | **7,724.00 ns** | **72.2656** | **15.6250** |  **200313 B** |
|    LinqGenDelegate | IntWrapper[10000] | 1,632,092.7 ns | 5,234.22 ns | 4,896.10 ns |       - |       - |       1 B |
|      LinqGenStruct | IntWrapper[10000] | 1,058,379.7 ns | 4,656.43 ns | 4,355.62 ns |       - |       - |       1 B |
| StructLinqDelegate | IntWrapper[10000] | 1,932,938.8 ns | 6,977.17 ns | 6,185.08 ns |       - |       - |     731 B |
|   StructLinqStruct | IntWrapper[10000] | 1,103,452.4 ns | 9,678.47 ns | 9,053.25 ns |       - |       - |     643 B |
|               **Linq** |    **IntWrapper[20]** |     **1,120.8 ns** |     **9.96 ns** |     **8.83 ns** |  **0.3395** |       **-** |     **712 B** |
|    LinqGenDelegate |    IntWrapper[20] |     1,191.3 ns |     4.60 ns |     4.08 ns |       - |       - |         - |
|      LinqGenStruct |    IntWrapper[20] |       720.6 ns |     5.08 ns |     4.51 ns |       - |       - |         - |
| StructLinqDelegate |    IntWrapper[20] |     1,299.2 ns |     5.00 ns |     4.68 ns |  0.0420 |       - |      88 B |
|   StructLinqStruct |    IntWrapper[20] |       658.0 ns |     1.73 ns |     1.62 ns |       - |       - |         - |
|               **Linq** |   **IntWrapper[500]** |    **49,011.5 ns** |   **278.78 ns** |   **260.77 ns** |  **4.8828** |       **-** |   **10312 B** |
|    LinqGenDelegate |   IntWrapper[500] |    50,697.2 ns |   296.82 ns |   277.65 ns |       - |       - |         - |
|      LinqGenStruct |   IntWrapper[500] |    15,584.5 ns |   176.49 ns |   165.09 ns |       - |       - |         - |
| StructLinqDelegate |   IntWrapper[500] |    61,454.7 ns |   273.34 ns |   255.68 ns |       - |       - |      89 B |
|   StructLinqStruct |   IntWrapper[500] |    17,757.3 ns |   104.14 ns |    97.41 ns |       - |       - |         - |
