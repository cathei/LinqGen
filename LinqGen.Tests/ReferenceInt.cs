// LinqGen.Tests, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Cathei.LinqGen;

namespace Cathei.LinqGen.Tests;

public class ReferenceInt : IEquatable<ReferenceInt>, IComparable<ReferenceInt>
{
    private readonly int _value;

    public ReferenceInt(int value)
    {
        _value = value;
    }

    public static ReferenceInt operator +(ReferenceInt? x, ReferenceInt? y)
    {
        return new((x?._value ?? 0) + (y?._value ?? 0));
    }

    public static ReferenceInt operator -(ReferenceInt? x, ReferenceInt? y)
    {
        return new((x?._value ?? 0) - (y?._value ?? 0));
    }

    public static ReferenceInt operator *(ReferenceInt? x, ReferenceInt? y)
    {
        return new((x?._value ?? 0) * (y?._value ?? 0));
    }

    public static ReferenceInt operator /(ReferenceInt? x, ReferenceInt? y)
    {
        return new((x?._value ?? 0) / (y?._value ?? 0));
    }

    public static ReferenceInt operator %(ReferenceInt? x, ReferenceInt? y)
    {
        return new((x?._value ?? 0) % (y?._value ?? 0));
    }

    public static ReferenceInt operator -(ReferenceInt x)
    {
        return new(-x._value);
    }

    public static bool operator ==(ReferenceInt? x, ReferenceInt? y)
    {
        return x?._value == y?._value;
    }

    public static bool operator !=(ReferenceInt? x, ReferenceInt? y)
    {
        return !(x == y);
    }

    public static implicit operator ReferenceInt(int value)
    {
        return new(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is ReferenceInt other && Equals(other);
    }
    public bool Equals(ReferenceInt? other)
    {
        if (other is null)
            return false;
        return _value == other._value;
    }

    public int CompareTo(ReferenceInt? other)
    {
        if (other is null)
            return 1;
        return _value.CompareTo(other._value);
    }

    public override int GetHashCode()
    {
        return _value;
    }

    public override string ToString()
    {
        return $"RefInt {_value}";
    }
}
