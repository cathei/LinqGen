## AddVsAddRange

### Source
[AddVsAddRange.cs](../../LinqGen.Benchmarks/Cases/AddVsAddRange.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|           Method |     Mean |     Error |    StdDev | Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated | Alloc Ratio |
|----------------- |---------:|----------:|----------:|------:|--------:|---------:|---------:|---------:|----------:|------------:|
|      ListAddLinq | 8.964 ms | 0.0503 ms | 0.0471 ms |  1.21 |    0.01 | 671.8750 | 609.3750 | 609.3750 |      4 MB |        2.10 |
| ListAddRangeLinq | 8.964 ms | 0.0780 ms | 0.0651 ms |  1.21 |    0.01 | 625.0000 | 578.1250 | 562.5000 |      4 MB |        2.10 |
|       ToListLinq | 6.396 ms | 0.0780 ms | 0.0692 ms |  0.86 |    0.01 | 570.3125 | 515.6250 | 507.8125 |      4 MB |        2.10 |
|          ListAdd | 7.699 ms | 0.1538 ms | 0.3277 ms |  1.00 |    0.05 | 601.5625 | 546.8750 | 539.0625 |      4 MB |        2.10 |
|              Add | 7.401 ms | 0.0462 ms | 0.0432 ms |  1.00 |    0.00 | 148.4375 | 148.4375 | 148.4375 |   1.91 MB |        1.00 |
|         AddRange | 7.273 ms | 0.0448 ms | 0.0419 ms |  0.98 |    0.01 | 148.4375 | 148.4375 | 148.4375 |   1.91 MB |        1.00 |
