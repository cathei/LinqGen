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
|               **Linq** | **IntWrapper[10000]** | **1,690,038.6 ns** | **32,234.67 ns** | **47,249.16 ns** | **72.2656** | **15.6250** |  **200313 B** |
|    LinqGenDelegate | IntWrapper[10000] | 1,997,096.7 ns | 15,966.81 ns | 14,935.36 ns |       - |       - |       3 B |
|      LinqGenStruct | IntWrapper[10000] | 1,301,348.2 ns |  6,616.22 ns |  6,188.82 ns |       - |       - |       1 B |
| StructLinqDelegate | IntWrapper[10000] | 1,960,173.1 ns |  8,979.13 ns |  8,399.09 ns |       - |       - |      93 B |
|   StructLinqStruct | IntWrapper[10000] | 1,099,255.9 ns |  5,814.90 ns |  5,439.26 ns |       - |       - |       3 B |
|               **Linq** |    **IntWrapper[20]** |     **1,124.3 ns** |      **7.24 ns** |      **6.42 ns** |  **0.3395** |       **-** |     **712 B** |
|    LinqGenDelegate |    IntWrapper[20] |     1,265.3 ns |      3.60 ns |      3.37 ns |       - |       - |         - |
|      LinqGenStruct |    IntWrapper[20] |       705.5 ns |      3.14 ns |      2.94 ns |       - |       - |         - |
| StructLinqDelegate |    IntWrapper[20] |     1,301.7 ns |      9.06 ns |      8.47 ns |  0.0420 |       - |      88 B |
|   StructLinqStruct |    IntWrapper[20] |       662.0 ns |      9.44 ns |      7.88 ns |       - |       - |         - |
|               **Linq** |   **IntWrapper[500]** |    **49,213.8 ns** |    **138.85 ns** |    **129.88 ns** |  **4.8828** |       **-** |   **10312 B** |
|    LinqGenDelegate |   IntWrapper[500] |    63,937.2 ns |    307.07 ns |    287.23 ns |       - |       - |         - |
|      LinqGenStruct |   IntWrapper[500] |    25,119.7 ns |    359.38 ns |    336.16 ns |       - |       - |         - |
| StructLinqDelegate |   IntWrapper[500] |    61,728.7 ns |    291.82 ns |    272.97 ns |       - |       - |      88 B |
|   StructLinqStruct |   IntWrapper[500] |    18,089.1 ns |    349.78 ns |    454.82 ns |       - |       - |         - |
