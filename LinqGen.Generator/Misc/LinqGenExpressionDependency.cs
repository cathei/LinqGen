// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Immutable;

namespace Cathei.LinqGen.Generator;

public readonly struct LinqGenExpressionDependency : IEquatable<LinqGenExpressionDependency>
{
    public readonly LinqGenExpression Expression;
    public readonly ImmutableArray<LinqGenExpression> Dependencies;

    /// Dependencies of single expression.
    /// Consider all nested upstreams.
    /// Consider direct downstream and its all additional nested upstreams.
    public LinqGenExpressionDependency(
        in LinqGenExpression expression,
        in ImmutableArray<LinqGenExpression> dependencies)
    {
        Expression = expression;
        Dependencies = dependencies;
    }

    public bool Equals(LinqGenExpressionDependency other)
    {
        if (!Expression.Equals(other.Expression))
            return false;

        if (Dependencies.Length != other.Dependencies.Length)
            return false;

        for (int i = 0; i < Dependencies.Length; ++i)
        {
            if (!Dependencies[i].Equals(other.Dependencies[i]))
                return false;
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is LinqGenExpressionDependency other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Expression.GetHashCode();
    }
}
