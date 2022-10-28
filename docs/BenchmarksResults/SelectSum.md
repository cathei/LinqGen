## SelectSum

### Source
[SelectSum.cs](../../LinqGen.Benchmarks/Cases/SelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |      Mean |     Error |    StdDev | Ratio | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|------:|----------:|------------:|
|            ForLoop |  9.469 ms | 0.0237 ms | 0.0221 ms |  0.24 |      10 B |        0.07 |
|        ForEachLoop | 25.941 ms | 0.3002 ms | 0.2661 ms |  0.65 |      61 B |        0.44 |
|               Linq | 40.141 ms | 0.6941 ms | 0.6493 ms |  1.00 |     140 B |        1.00 |
|    LinqGenDelegate | 20.205 ms | 0.1205 ms | 0.1127 ms |  0.50 |      21 B |        0.15 |
|      LinqGenStruct | 12.380 ms | 0.1771 ms | 0.1478 ms |  0.31 |      10 B |        0.07 |
| StructLinqDelegate | 14.448 ms | 0.0542 ms | 0.0480 ms |  0.36 |      66 B |        0.47 |
|   StructLinqStruct |  9.587 ms | 0.0302 ms | 0.0268 ms |  0.24 |      10 B |        0.07 |
|  HyperLinqDelegate | 35.040 ms | 0.0997 ms | 0.0779 ms |  0.87 |      45 B |        0.32 |
|    HyperLinqStruct | 34.753 ms | 0.1456 ms | 0.1290 ms |  0.86 |      45 B |        0.32 |
