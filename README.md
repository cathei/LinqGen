# LinqGen

## Linq meets Source Generator

LinqGen is project to optimize Linq queries using source generation of user code.

It aims to make allocation-free, specialized Linq queries per your type.

## Why not just use struct Linq implementations?

Because of [this issue](https://github.com/dotnet/runtime/discussions/77192),
struct linq implementations with many generics must do runtime lookup.
Which makes them not much faster than original Linq.

Also, they have to have bunch of type information and tricks for type inference.
Which makes your code hard to read and understand. The error messages or stack trace will be very messy as well.

Using source generation also makes your code friendly for AOT platforms, such as Unity,
which has [maximum generic depth](https://forum.unity.com/threads/il2cpp-max-nested-generic-types.540534/).

## How does LinqGen work?

LinqGen has two part of assembly, `LinqGen` and `LinqGen.Generator`.
The `LinqGen` assembly contains a stub method and types, which helps you autocomplete and helps generator infer types.

After you write a Linq query with stub methods, then `LinqGen.Generator` runs and replace the stub methods with generated methods.

How is it possible, while modifying user code is not allowed with source generators?
It's because everything `LinqGen.Generator` generates designed to be precede over stub methods on [overload resolution](https://learn.microsoft.com/en-us/dotnet/visual-basic/reference/language-specification/overload-resolution).

## Supported methods (working-in-progress)
### Operations
* Select
* Where
* Cast
* OfType

### Evaluations
* First
* FirstOrDefault
* Sum
  * Supports duck typing with `+` operator overload

### Etc
* Gen
    * Converts IEnumerable to LinqGen enumerable
* AsEnumerable
    * Converts LinqGen enumerable to IEnumerable
