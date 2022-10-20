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

