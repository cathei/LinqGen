# LinqGen ⚡️
[![Nuget](https://img.shields.io/nuget/v/LinqGen)](https://www.nuget.org/packages?q=LinqGen)
[![openupm](https://img.shields.io/npm/v/com.cathei.linqgen?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.cathei.linqgen/)
[![Discord](https://img.shields.io/discord/942240862354702376?color=%235865F2&label=discord&logo=discord&logoColor=%23FFFFFF)](https://discord.gg/kpuRTkpeQC)

## Linq meets Source Generator

LinqGen is project to optimize Linq queries using source generation of user code.

It aims to make allocation-free, specialized Linq queries per your type.

## Install
Install from NuGet, both [LinqGen](https://www.nuget.org/packages/LinqGen) as library and [LinqGen.Generator](https://www.nuget.org/packages/LinqGen.Generator) as incremental source generator.

```xml
<ItemGroup>
    <PackageReference Include="LinqGen" Version="0.3.1" />
    <PackageReference Include="LinqGen.Generator" Version="0.3.1" />
</ItemGroup>
```

For Unity, you can install as git package from Unity Package Manager.
```
https://github.com/cathei/LinqGen.git?path=LinqGen.Unity/Packages/com.cathei.linqgen
```
Or install via OpenUPM.
```
openupm add com.cathei.linqgen
```

### Any questions?

Feel free to make an issue, or ask me directly from [Discord](https://discord.gg/kpuRTkpeQC)!

## Usage
Just add `Gen()` in front of your Linq query.
It will generate code to ensure zero-allocation, may have slightly better performance.
```csharp
using Cathei.LinqGen;
 
int[] array = new int[] { 1, 2, 3, 4, 5 };

int result = array.Gen()
                  .Where(x => x % 2 == 0)
                  .Select(x => x * 2)
                  .Sum();
```

For additional performance boost, use struct functions with `IStructFunction` interface.
```csharp
int result = array.Gen()
                  .Where(new Predicate())
                  .Select(new Selector())
                  .Sum();
```

This is benchmark result for above code. You can see full benchmark results [here](./docs/BenchmarksResults).

|             Method |  Count |     Mean |   Error |  StdDev | Ratio | Allocated | Alloc Ratio |
|------------------- |------- |---------:|--------:|--------:|------:|----------:|------------:|
|            ForLoop | 100000 | 449.8 us | 4.56 us | 4.27 us |  0.50 |         - |       0.000 |
|        ForEachLoop | 100000 | 444.3 us | 1.48 us | 1.39 us |  0.49 |         - |       0.000 |
|               Linq | 100000 | 899.8 us | 5.65 us | 5.01 us |  1.00 |     105 B |       1.000 |
|    LinqGenDelegate | 100000 | 576.2 us | 4.43 us | 4.14 us |  0.64 |       1 B |       0.010 |
|      LinqGenStruct | 100000 | 449.8 us | 4.06 us | 3.60 us |  0.50 |         - |       0.000 |

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
It's because everything `LinqGen.Generator` generates designed to be precede over stub methods on [overload resolution](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/expressions#11782-method-invocations).

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

        foreach (var item in Input.Gen()
                     .Select(new Selector())
                     .Order(new Comparer()))
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

## Supported methods
[List of Linq Operations](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/standard-query-operators-overview)

* ✔️ Supported operation will work even if your .NET version Linq does not support corresponding method.
* ⚡️ Indicates that the operation also support struct function and comparer for additional performance.

### Generations
| Method         | Support             | Note |
|----------------|---------------------|------|
| Empty          | ✔️ Supported        |      |
| Range          | ✔️ Supported        |      |
| Repeat         | ✔️ Supported        |      |
| DefaultIfEmpty | ⚠️ Work-in-progress |      |

### Filtering Operations
| Method | Support          | Note |
|--------|------------------|------|
| Where  | ✔️ Supported ⚡️  |      |
| OfType | ✔️ Supported     |      |

### Projection Operations
| Method     | Support             | Note |
|------------|---------------------|------|
| Select     | ✔️ Supported ⚡️     |      |
| SelectMany | ⚠️ Work-in-progress |      |
| Zip        | ✔️ Supported ⚡️     |      |

### Set Operations
| Method      | Support             | Note |
|-------------|---------------------|------|
| Distinct    | ✔️ Supported ⚡️     |      |
| DistinctBy  | ⚠️ Work-in-progress |      |
| Except      | ⚠️ Work-in-progress |      |
| ExceptBy    | ⚠️ Work-in-progress |      |
| Intersect   | ⚠️ Work-in-progress |      |
| IntersectBy | ⚠️ Work-in-progress |      |
| Union       | ⚠️ Work-in-progress |      |
| UnionBy     | ⚠️ Work-in-progress |      |

### Sorting Operations
| Method            | Support             | Note                                                 |
|-------------------|---------------------|------------------------------------------------------|
| Order             | ✔️ Supported ⚡️     | `OrderBy` that uses element itself as key            |
| OrderDescending   | ✔️ Supported ⚡️     | `OrderByDescending` that uses element itself as key  |
| OrderBy           | ✔️ Supported ⚡️     |                                                      |
| OrderByDescending | ✔️ Supported ⚡️     |                                                      |
| ThenBy            | ✔️ Supported ⚡️     |                                                      |
| ThenByDescending  | ✔️ Supported ⚡️     |                                                      |
| Reverse           | ⚠️ Work-in-progress |                                                      |

### Partitioning Operations
| Method    | Support             | Note |
|-----------|---------------------|------|
| Skip      | ✔️ Supported        |      |
| SkipLast  | ✔️ Supported        |      |
| SkipWhile | ✔️ Supported ⚡️     |      |
| Take      | ✔️ Supported        |      |
| TakeLast  | ✔️ Supported        |      |
| TakeWhile | ✔️ Supported ⚡️     |      |
| Chunk     | ⚠️ Work-in-progress |      |

### Converting Operations
| Method        | Support             | Note                                              |
|---------------|---------------------|---------------------------------------------------|
| AsEnumerable  | ✔️ Supported        | Converts LinqGen enumerable to `IEnumerable`      |
| Cast          | ✔️ Supported        |                                                   |
| Gen           | ✔️ Supported        | Converts `IEnumerable` to LinqGen enumerable      |
| GetEnumerator | ✔️ Supported        | Automatically generated when using with `foreach` |
| OfType        | ✔️ Supported        |                                                   |
| ToArray       | ✔️ Supported        |                                                   |
| ToList        | ✔️ Supported        |                                                   |
| ToDictionary  | ⚠️ Work-in-progress |                                                   |
| ToLookup      | ⚠️ Work-in-progress |                                                   |

### Concatenation Operations
| Method  | Support             | Note |
|---------|---------------------|------|
| Append  | ✔️ Supported         |      |
| Concat  | ✔️ Supported         |      |
| Prepend | ✔️ Supported         |      |

### Grouping Operations
| Method  | Support          | Note |
|---------|------------------|------|
| GroupBy | ✔️ Supported ⚡️  |      |

### Joining Operations
| Method    | Support             | Note |
|-----------|---------------------|------|
| Join      | ⚠️ Work-in-progress |      |
| GroupJoin | ⚠️ Work-in-progress |      |

### Aggregation Evaluations
| Method    | Support             | Note                                            |
|-----------|---------------------|-------------------------------------------------|
| Aggregate | ✔️ Supported ⚡️     |                                                 |
| Count     | ✔️ Supported        |                                                 |
| Max       | ✔️ Supported        |                                                 |
| MaxBy     | ✔️ Supported ⚡️     |                                                 |
| Min       | ✔️ Supported        |                                                 |
| MinBy     | ✔️ Supported ⚡️     |                                                 |
| Sum       | ✔️ Supported        | Supports duck typing with `+` operator overload |
| Average   | ⚠️ Work-in-progress |                                                 |
| LongCount | ⚠️ Work-in-progress |                                                 |

### Element Evaluations
| Method             | Support              | Note |
|--------------------|----------------------|------|
| First              | ✔️ Supported         |      |
| FirstOrDefault     | ✔️ Supported         |      |
| Last               | ✔️ Supported         |      |
| LastOrDefault      | ✔️ Supported         |      |
| Single             | ⚠️ Work-in-progress  |      |
| SingleOrDefault    | ⚠️ Work-in-progress  |      |
| ElementAt          | ⚠️ Work-in-progress  |      |
| ElementAtOrDefault | ⚠️ Work-in-progress  |      |

### Quantifier Evaluations
| Method   | Support             | Note |
|----------|---------------------|------|
| All      | ✔️ Supported ⚡️     |      |
| Any      | ✔️ Supported ⚡️     |      |
| Contains | ⚠️ Work-in-progress |      |

### Equality Evaluations
| Method        | Support              | Note |
|---------------|----------------------|------|
| SequenceEqual | ⚠️ Work-in-progress  |      |

### IStructFunction Utilities
| Method    | Support          | Note                                        |
|-----------|------------------|---------------------------------------------|
| RemoveAll | ✔️ Supported ⚡️  | List extension method with struct predicate |

## Limitations
* Element or key types that used with LinqGen must have at least `internal` accessibility.
* Struct enumerable should implement `IStructEnumerable<,>` interface.
* LinqGen queries should be treated as anonymous type, it cannot be used as return value or instance member. If you have these needs, use `AsEnumerable()` to convert.
* LinqGen may not work well when `[InternalsVisibleTo]` is used while both assemblies are using LinqGen. It can be solved when [this language feature](https://github.com/dotnet/csharplang/issues/6794) is implemented.

## Further readings
* Jon Skeet's [Edulinq](https://codeblog.jonskeet.uk/category/edulinq/), reimplementing Linq-to-objects.
* Article about [alloc-free Linq implementation and limitations](https://blog.devgenius.io/like-regular-linq-but-faster-and-without-allocations-is-it-possible-3d4724632e2a).
