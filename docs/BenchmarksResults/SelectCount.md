## SelectCount

### Source
[SelectCount.cs](../../LinqGen.Benchmarks/Cases/SelectCount.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
| Method             |          Mean |      Error |     StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|--------------------|--------------:|-----------:|-----------:|------:|-------:|----------:|------------:|
| Linq               | 14,156.453 ns | 24.7850 ns | 20.6966 ns | 1.000 | 0.0305 |      88 B |        1.00 |
| LinqGenDelegate    |  7,678.963 ns | 10.2209 ns |  7.9798 ns | 0.542 |      - |         - |        0.00 |
| LinqGenStruct      |  7,843.080 ns | 35.0955 ns | 32.8284 ns | 0.554 |      - |         - |        0.00 |
| StructLinqDelegate |     13.926 ns |  0.1093 ns |  0.1022 ns | 0.001 | 0.0268 |      56 B |        0.64 |
| StructLinqStruct   |      3.469 ns |  0.0168 ns |  0.0157 ns | 0.000 |      - |         - |        0.00 |
| HyperLinqDelegate  |      5.577 ns |  0.0294 ns |  0.0275 ns | 0.000 |      - |         - |        0.00 |
| HyperLinqStruct    |      3.089 ns |  0.0661 ns |  0.0649 ns | 0.000 |      - |         - |        0.00 |
