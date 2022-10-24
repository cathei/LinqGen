## WhereSelectSum

### Source
[WhereSelectSum.cs](../../LinqGen.Benchmarks/Cases/WhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2130/21H1/May2021Update)
AMD Ryzen 5 3600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT AVX2


```
|             Method |      Mean |     Error |    StdDev | Ratio | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|------:|----------:|------------:|
|            ForLoop |  4.191 μs | 0.0739 μs | 0.0986 μs |  0.06 |         - |        0.00 |
|        ForEachLoop | 41.442 μs | 0.4150 μs | 0.3882 μs |  0.55 |      40 B |        0.25 |
|               Linq | 75.555 μs | 0.5871 μs | 0.5492 μs |  1.00 |     160 B |        1.00 |
|    LinqGenDelegate | 25.131 μs | 0.1873 μs | 0.1661 μs |  0.33 |         - |        0.00 |
|      LinqGenStruct |  7.588 μs | 0.0777 μs | 0.0689 μs |  0.10 |         - |        0.00 |
| StructLinqDelegate | 25.022 μs | 0.1210 μs | 0.1010 μs |  0.33 |      88 B |        0.55 |
|   StructLinqStruct | 13.610 μs | 0.0316 μs | 0.0296 μs |  0.18 |         - |        0.00 |
|  HyperLinqDelegate | 26.306 μs | 0.1449 μs | 0.1356 μs |  0.35 |         - |        0.00 |
|    HyperLinqStruct | 13.622 μs | 0.0400 μs | 0.0374 μs |  0.18 |         - |        0.00 |
