## ArrayWhereToArray

### Source
[ArrayWhereToArray.cs](../../LinqGen.Benchmarks/Cases/ArrayWhereToArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|             Method |     Mean |     Error |    StdDev | Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated | Alloc Ratio |
|------------------- |---------:|----------:|----------:|------:|--------:|---------:|---------:|---------:|----------:|------------:|
|               Linq | 6.490 ms | 0.1253 ms | 0.1442 ms |  1.00 |    0.00 | 460.9375 | 406.2500 | 398.4375 |   3.91 MB |        1.00 |
|    LinqGenDelegate | 7.001 ms | 0.0215 ms | 0.0179 ms |  1.09 |    0.02 | 125.0000 | 125.0000 | 125.0000 |   1.91 MB |        0.49 |
|      LinqGenStruct | 6.215 ms | 0.0215 ms | 0.0201 ms |  0.96 |    0.02 | 156.2500 | 156.2500 | 156.2500 |   1.91 MB |        0.49 |
| StructLinqDelegate | 6.280 ms | 0.0396 ms | 0.0370 ms |  0.97 |    0.02 | 132.8125 | 132.8125 | 132.8125 |   1.91 MB |        0.49 |
|   StructLinqStruct | 5.280 ms | 0.0262 ms | 0.0218 ms |  0.82 |    0.02 | 164.0625 | 164.0625 | 164.0625 |   1.91 MB |        0.49 |
|  HyperLinqDelegate | 6.114 ms | 0.0425 ms | 0.0397 ms |  0.95 |    0.02 |  93.7500 |  93.7500 |  93.7500 |   1.91 MB |        0.49 |
|    HyperLinqStruct | 5.363 ms | 0.0206 ms | 0.0192 ms |  0.83 |    0.02 | 187.5000 | 187.5000 | 187.5000 |   1.91 MB |        0.49 |
