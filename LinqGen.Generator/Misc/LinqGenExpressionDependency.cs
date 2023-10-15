// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public readonly struct LinqGenExpressionDependency : IEquatable<LinqGenExpressionDependency>
{
    public readonly LinqGenExpression Expression;
    public readonly ImmutableArray<LinqGenExpression> Dependencies;

    /// logic
    /// consider all nested upstreams
    /// consider direct downstream and its all additional nested upstreams

    public LinqGenExpressionDependency(
        in LinqGenExpression expression,
        in ImmutableArray<LinqGenExpression> dependencies)
    {
        Expression = expression;
        Dependencies = dependencies;
    }

    public bool Equals(LinqGenExpressionDependency other)
    {
        return Expression.Equals(other.Expression) &&
               ImmutableArrayComparer<LinqGenExpression>.Default.Equals(Dependencies, other.Dependencies);
    }

    public override bool Equals(object? obj)
    {
        return obj is LinqGenExpressionDependency other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCombine(
            Expression.GetHashCode(),
            ImmutableArrayComparer<LinqGenExpression>.Default.GetHashCode(Dependencies));
    }
}