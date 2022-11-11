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
| Method             |         list |           Mean |        Error |       StdDev |    Gen0 | Allocated |
|--------------------|------------- |---------------:|-------------:|-------------:|--------:|----------:|
| **Linq**           | **Int32[10000]** | **1,197,724.0 ns** |  **7,322.39 ns** |  **6,849.37 ns** | **54.6875** |  **120313 B** |
| LinqGenDelegate    | Int32[10000] | 1,198,317.5 ns |  7,596.37 ns |  6,343.32 ns |       - |       1 B |
| LinqGenStruct      | Int32[10000] |   946,092.4 ns |  8,801.56 ns |  8,232.98 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,244,328.7 ns | 16,867.98 ns | 15,778.32 ns |       - |     346 B |
| StructLinqStruct   | Int32[10000] |   920,378.7 ns |  3,644.38 ns |  3,408.96 ns |       - |     129 B |
| **Linq**           |    **Int32[20]** |       **678.6 ns** |      **2.82 ns** |      **2.50 ns** |  **0.2632** |     **552 B** |
| LinqGenDelegate    |    Int32[20] |       811.6 ns |      3.13 ns |      2.93 ns |       - |         - |
| LinqGenStruct      |    Int32[20] |       555.4 ns |      1.70 ns |      1.59 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |       721.3 ns |      2.38 ns |      2.22 ns |  0.0420 |      88 B |
| StructLinqStruct   |    Int32[20] |       440.9 ns |      1.20 ns |      1.07 ns |       - |         - |
| **Linq**           |   **Int32[500]** |    **33,101.2 ns** |    **157.49 ns** |    **139.61 ns** |  **2.9907** |    **6312 B** |
| LinqGenDelegate    |   Int32[500] |    31,271.8 ns |    373.63 ns |    311.99 ns |       - |         - |
| LinqGenStruct      |   Int32[500] |    12,894.4 ns |    131.26 ns |    122.78 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    28,561.1 ns |    245.24 ns |    229.39 ns |  0.0305 |      88 B |
| StructLinqStruct   |   Int32[500] |    12,923.7 ns |     76.37 ns |     71.44 ns |       - |         - |
