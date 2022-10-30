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
|             Method |          Mean |      Error |     StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------------- |--------------:|-----------:|-----------:|------:|-------:|----------:|------------:|
|               Linq | 14,355.343 ns | 80.2766 ns | 75.0908 ns | 1.000 | 0.0305 |      88 B |        1.00 |
|    LinqGenDelegate |      2.754 ns |  0.0127 ns |  0.0118 ns | 0.000 |      - |         - |        0.00 |
|      LinqGenStruct |      2.703 ns |  0.0363 ns |  0.0339 ns | 0.000 |      - |         - |        0.00 |
| StructLinqDelegate |     13.843 ns |  0.0292 ns |  0.0273 ns | 0.001 | 0.0268 |      56 B |        0.64 |
|   StructLinqStruct |      3.486 ns |  0.0092 ns |  0.0086 ns | 0.000 |      - |         - |        0.00 |
|  HyperLinqDelegate |      5.627 ns |  0.0293 ns |  0.0274 ns | 0.000 |      - |         - |        0.00 |
|    HyperLinqStruct |      3.199 ns |  0.0798 ns |  0.0747 ns | 0.000 |      - |         - |        0.00 |
