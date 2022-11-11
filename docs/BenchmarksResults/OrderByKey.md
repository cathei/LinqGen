## OrderByKey

### Source
[OrderByKey.cs](../../LinqGen.Benchmarks/Cases/OrderByKey.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
| Method             |         list |           Mean |       Error |      StdDev |    Gen0 | Allocated |
|--------------------|------------- |---------------:|------------:|------------:|--------:|----------:|
| **Linq**           | **Int32[10000]** | **1,205,538.6 ns** | **5,688.95 ns** | **5,321.45 ns** | **54.6875** |  **120313 B** |
| LinqGenDelegate    | Int32[10000] | 1,184,167.1 ns | 5,570.10 ns | 5,210.27 ns |       - |       1 B |
| LinqGenStruct      | Int32[10000] |   911,479.7 ns | 6,057.89 ns | 5,666.56 ns |       - |       1 B |
| StructLinqDelegate | Int32[10000] | 1,254,528.7 ns | 6,413.88 ns | 5,999.54 ns |       - |     362 B |
| StructLinqStruct   | Int32[10000] |   930,127.1 ns | 4,519.40 ns | 4,006.33 ns |       - |     129 B |
| **Linq**           |    **Int32[20]** |       **695.0 ns** |     **3.87 ns** |     **3.62 ns** |  **0.2632** |     **552 B** |
| LinqGenDelegate    |    Int32[20] |       821.6 ns |     2.99 ns |     2.80 ns |       - |         - |
| LinqGenStruct      |    Int32[20] |       556.8 ns |     1.68 ns |     1.31 ns |       - |         - |
| StructLinqDelegate |    Int32[20] |     1,012.0 ns |     4.77 ns |     4.23 ns |  0.0496 |     104 B |
| StructLinqStruct   |    Int32[20] |       678.2 ns |     3.10 ns |     2.90 ns |       - |         - |
| **Linq**           |   **Int32[500]** |    **32,315.4 ns** |   **191.68 ns** |   **179.30 ns** |  **2.9907** |    **6312 B** |
| LinqGenDelegate    |   Int32[500] |    31,296.0 ns |   313.32 ns |   277.75 ns |       - |         - |
| LinqGenStruct      |   Int32[500] |    12,558.4 ns |    53.66 ns |    50.19 ns |       - |         - |
| StructLinqDelegate |   Int32[500] |    32,883.9 ns |   430.12 ns |   359.17 ns |       - |     104 B |
| StructLinqStruct   |   Int32[500] |    14,519.9 ns |   277.54 ns |   246.03 ns |       - |         - |
