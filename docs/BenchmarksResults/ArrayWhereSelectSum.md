## ArrayWhereSelectSum

### Source
[ArrayWhereSelectSum.cs](../../LinqGen.Benchmarks/Cases/ArrayWhereSelectSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|          Method |   Count |     Mean |     Error |    StdDev | Ratio | Allocated | Alloc Ratio |
|---------------- |-------- |---------:|----------:|----------:|------:|----------:|------------:|
|         ForLoop | 1000000 | 4.831 ms | 0.0952 ms | 0.1019 ms |  0.53 |       5 B |        0.04 |
|     ForEachLoop | 1000000 | 4.674 ms | 0.0218 ms | 0.0204 ms |  0.51 |       5 B |        0.04 |
|            Linq | 1000000 | 9.209 ms | 0.0424 ms | 0.0397 ms |  1.00 |     115 B |        1.00 |
| LinqGenDelegate | 1000000 | 6.139 ms | 0.0597 ms | 0.0558 ms |  0.67 |       5 B |        0.04 |
|   LinqGenStruct | 1000000 | 4.757 ms | 0.0219 ms | 0.0195 ms |  0.52 |       5 B |        0.04 |
