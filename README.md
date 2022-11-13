# LinqGen
[![Nuget](https://img.shields.io/nuget/v/LinqGen)](https://www.nuget.org/packages?q=LinqGen)
[![Discord](https://img.shields.io/discord/942240862354702376?color=%235865F2&label=discord&logo=discord&logoColor=%23FFFFFF)](https://discord.gg/htzm856QhA)

## Linq meets Source Generator

LinqGen is project to optimize Linq queries using source generation of user code.

It aims to make allocation-free, specialized Linq queries per your type.

## Install
Install from NuGet, both [LinqGen](https://www.nuget.org/packages/LinqGen) as library and [LinqGen.Generator](https://www.nuget.org/packages/LinqGen.Generator) as source generator.

```xml
    <PackageReference Include="LinqGen" Version="0.0.1-preview" />
    <PackageReference Include="LinqGen.Generator" Version="0.0.1-preview">
        <PrivateAssets>all</PrivateAssets>
        <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
    </PackageReference>
```

For Unity, you can install as Unity package.
```
https://github.com/cathei/LinqGen.git?path=LinqGen.Unity/Packages/com.cathei.linqgen#v0.0.1-preview
```

## Usage
Just add `Specialize()` in front of your Linq query.
It will generate code to ensure zero-allocation slightly better performance.
```csharp
using Cathei.LinqGen;
 
int[] array = new int[] { 1, 2, 3, 4, 5 };

int result = array.Specialize()
                  .Where(x => x % 2 == 0)
                  .Select(x => x * 2)
                  .Sum();
```

For additional performance boost, use struct functions with `IStructFunction` interface.
```csharp
int result = array.Specialize()
                  .Where(new Predicate())
                  .Select(new Selector())
                  .Sum();
```

This is benchmark result for above code. You can see full benchmark results [here](./docs/BenchmarksResults).

|             Method |      Mean |     Error |    StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|------------------- |----------:|----------:|----------:|------:|-------:|----------:|------------:|
|            ForLoop | 11.228 μs | 0.0615 μs | 0.0514 μs |  0.32 |      - |         - |        0.00 |
|        ForEachLoop |  9.577 μs | 0.0285 μs | 0.0267 μs |  0.27 |      - |         - |        0.00 |
|               Linq | 35.388 μs | 0.1858 μs | 0.1647 μs |  1.00 |      - |     104 B |        1.00 |
|    LinqGenDelegate | 22.625 μs | 0.0637 μs | 0.0596 μs |  0.64 |      - |         - |        0.00 |
|      LinqGenStruct | 12.329 μs | 0.0394 μs | 0.0369 μs |  0.35 |      - |         - |        0.00 |

## Why not just use struct Linq implementations?

Because of [this issue](https://github.com/dotnet/runtime/discussions/77192),
struct linq implementations with many generics must do runtime lookup.
Which makes them not much faster than original Linq.

Also, they have to have bunch of type information and tricks for type inference.
Which makes your code hard to read and understand. The error messages or stack trace will be very messy as well.

Using source generation also makes your code friendly for AOT platforms, such as Unity,
which has [maximum generic depth](https://forum.unity.com/threads/il2cpp-max-nested-generic-types.540534/).

Being source generator makes `LinqGen` core library much small than other struct linq implementations, though it may grow as user uses Linq operations.

It's worth to mention that `LinqGen` uses standard .NET enumerator like `List<T>.Enumerator` so it can give same behaviour when the iterating collection changed.

## How does LinqGen work?

LinqGen has two part of assembly, `LinqGen` and `LinqGen.Generator`.
The `LinqGen` assembly contains a stub method and types, which helps you autocomplete and helps generator infer types.

After you write a Linq query with stub methods, then `LinqGen.Generator` runs and replace the stub methods with generated methods.

How is it possible, while modifying user code is not allowed with source generators?
It's because everything `LinqGen.Generator` generates designed to be precede over stub methods on [overload resolution](https://learn.microsoft.com/en-us/dotnet/visual-basic/reference/language-specification/overload-resolution).

## Supported methods (working-in-progress)
### Generations
* Empty
* Range
* Repeat

### Operations
* Select
* Where
* Cast
* OfType
* Skip
* Take
* Distinct
* OrderBy, OrderByDescending
* ThenBy, ThenByDescending

### Evaluations
* First
* FirstOrDefault
* Last
* LastOrDefault
* Count
* Sum
  * Supports duck typing with `+` operator overload
* Min
* Max

### Etc
* Specialize
    * Converts IEnumerable to LinqGen enumerable
* AsEnumerable
    * Converts LinqGen enumerable to IEnumerable

## Limitations
* Element or key types that used with LinqGen must have at least `internal` accessibility.
* LinqGen queries should be treated as anonymous type, it cannot be used as return value or instance member. If you have these needs, use `AsEnumerable()` to convert.

## Further readings
* Jon Skeet's [Edulinq](https://codeblog.jonskeet.uk/category/edulinq/), reimplementing Linq-to-objects.
* Article about [alloc-free Linq implementation and limitations](https://blog.devgenius.io/like-regular-linq-but-faster-and-without-allocations-is-it-possible-3d4724632e2a).
