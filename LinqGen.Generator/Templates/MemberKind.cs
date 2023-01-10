// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;

namespace Cathei.LinqGen.Generator;

[Flags]
public enum MemberKind
{
    Enumerable = 1,
    Enumerator = 2,
    Both = Enumerable// | Enumerator
}