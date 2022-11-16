## ToArrayVsToList

### Source
[ToArrayVsToList.cs](../../LinqGen.Benchmarks/Cases/ToArrayVsToList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|        Method |     Mean |     Error |    StdDev |     Gen0 |     Gen1 |     Gen2 | Allocated |
|-------------- |---------:|----------:|----------:|---------:|---------:|---------:|----------:|
|       ToArray | 6.765 ms | 0.1049 ms | 0.0981 ms | 156.2500 | 156.2500 | 156.2500 |   1.91 MB |
| ToArrayStruct | 5.965 ms | 0.0405 ms | 0.0359 ms | 164.0625 | 164.0625 | 164.0625 |   1.91 MB |
|        ToList | 7.365 ms | 0.0572 ms | 0.0507 ms |  78.1250 |  78.1250 |  78.1250 |   1.91 MB |
|  ToListStruct | 6.612 ms | 0.0636 ms | 0.0531 ms | 140.6250 | 140.6250 | 140.6250 |   1.91 MB |
