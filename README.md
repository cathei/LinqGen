# LinqGen
[![Nuget](https://img.shields.io/nuget/v/LinqGen)](https://www.nuget.org/packages?q=LinqGen)
[![Discord](https://img.shields.io/discord/942240862354702376?color=%235865F2&label=discord&logo=discord&logoColor=%23FFFFFF)](https://discord.gg/kpuRTkpeQC)

## Linq meets Source Generator

LinqGen is project to optimize Linq queries using source generation of user code.

It aims to make allocation-free, specialized Linq queries per your type.

## Install
Install from NuGet, both [LinqGen](https://www.nuget.org/packages/LinqGen) as library and [LinqGen.Generator](https://www.nuget.org/packages/LinqGen.Generator) as source generator.

```xml
    <PackageReference Include="LinqGen" Version="0.0.4-preview" />
    <PackageReference Include="LinqGen.Generator" Version="0.0.4-preview" />
```

For Unity, you can install as Unity package.
```
https://github.com/cathei/LinqGen.git?path=LinqGen.Unity/Packages/com.cathei.linqgen#v0.0.4-preview
```

### Any questions?

Feel free to make an issue, or ask me directly from [Discord](https://discord.gg/kpuRTkpeQC)!

## Usage
Just add `Specialize()` in front of your Linq query.
It will generate code to ensure zero-allocation, may have slightly better performance.
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

|          Method |   Count |     Mean |     Error |    StdDev | Ratio | Allocated | Alloc Ratio |
|---------------- |-------- |---------:|----------:|----------:|------:|----------:|------------:|
|         ForLoop | 1000000 | 4.831 ms | 0.0952 ms | 0.1019 ms |  0.53 |       5 B |        0.04 |
|     ForEachLoop | 1000000 | 4.674 ms | 0.0218 ms | 0.0204 ms |  0.51 |       5 B |        0.04 |
|            Linq | 1000000 | 9.209 ms | 0.0424 ms | 0.0397 ms |  1.00 |     115 B |        1.00 |
| LinqGenDelegate | 1000000 | 6.139 ms | 0.0597 ms | 0.0558 ms |  0.67 |       5 B |        0.04 |
|   LinqGenStruct | 1000000 | 4.757 ms | 0.0219 ms | 0.0195 ms |  0.52 |       5 B |        0.04 |

## Why not just use struct Linq implementations?

Because of [this issue](https://github.com/dotnet/runtime/discussions/77192),
struct linq implementations with many generics must do runtime lookup.
Which makes them not much faster than original Linq.

Also, they have to have bunch of type information and tricks for type inference.
Which makes your code hard to read and understand. The error messages or stack trace will be very messy as well.

Using source generation also makes your code friendly for AOT platforms, such as Unity,
which has [maximum generic depth](https://forum.unity.com/threads/il2cpp-max-nested-generic-types.540534/).

Being source generator makes `LinqGen` core library much small than other struct linq implementations, though it may grow as user uses Linq operations.

## How does LinqGen work?

LinqGen has two part of assembly, `LinqGen` and `LinqGen.Generator`.
The `LinqGen` assembly contains a stub method and types, which helps you autocomplete and helps generator infer types.

After you write a Linq query with stub methods, then `LinqGen.Generator` runs and replace the stub methods with generated methods.

How is it possible, while modifying user code is not allowed with source generators?
It's because everything `LinqGen.Generator` generates designed to be precede over stub methods on [overload resolution](https://learn.microsoft.com/en-us/dotnet/visual-basic/reference/language-specification/overload-resolution).

## Does LinqGen works with Unity Burst compiler?

**Yes!** LinqGen is aiming to support Unity Burst compiler. Below code is sample of using LinqGen in Burst-compiled job system.

```csharp
[BurstCompile(CompileSynchronously = true)]
public struct LinqGenSampleJob : IJob
{
    [ReadOnly]
    public NativeArray<int> Input;

    [WriteOnly]
    public NativeArray<int> Output;

    public void Execute()
    {
        int index = 0;

        foreach (var item in Input.Specialize()
                                  .Select(new Selector())
                                  .OrderBy(new Comparer()))
        {
            Output[index++] = item;
        }
    }
}

public struct Selector : IStructFunction<int, int>
{
    public int Invoke(int arg) => arg * 10;
}

public struct Comparer : IComparer<int>
{
    public int Compare(int x, int y) => x - y;
}
```

### Current limitation with Burst (to-be-fixed)
* Only `NativeArray<T>` and `NativeSlice<T>` is supported for struct enumeration. 

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
* LinqGen may not work well when `[InternalsVisibleTo]` is used while both assemblies are using LinqGen. It can be solved when [this language feature](https://github.com/dotnet/csharplang/issues/6794) is implemented.

## Further readings
* Jon Skeet's [Edulinq](https://codeblog.jonskeet.uk/category/edulinq/), reimplementing Linq-to-objects.
* Article about [alloc-free Linq implementation and limitations](https://blog.devgenius.io/like-regular-linq-but-faster-and-without-allocations-is-it-possible-3d4724632e2a).
