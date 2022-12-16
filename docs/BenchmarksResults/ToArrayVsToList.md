## ToArrayVsToList

### Source
[ToArrayVsToList.cs](../../LinqGen.Benchmarks/Cases/ToArrayVsToList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.2 (6.0.222.6406), Arm64 RyuJIT AdvSIMD


```
|        Method |     Mean |     Error |    StdDev |     Gen0 |     Gen1 |     Gen2 | Allocated |
|-------------- |---------:|----------:|----------:|---------:|---------:|---------:|----------:|
|       ToArray | 6.374 ms | 0.0329 ms | 0.0308 ms | 218.7500 | 218.7500 | 218.7500 |   1.91 MB |
| ToArrayStruct | 5.545 ms | 0.0451 ms | 0.0422 ms | 226.5625 | 226.5625 | 226.5625 |   1.91 MB |
|        ToList | 6.395 ms | 0.0237 ms | 0.0222 ms | 218.7500 | 218.7500 | 218.7500 |   1.91 MB |
|  ToListStruct | 5.534 ms | 0.0626 ms | 0.0555 ms | 226.5625 | 226.5625 | 226.5625 |   1.91 MB |
