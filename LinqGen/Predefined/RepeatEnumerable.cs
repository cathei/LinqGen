// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cathei.LinqGen.Hidden;


// namespace Cathei.LinqGen.Hidden
// {
//     public readonly struct RepeatEnumerable<T> : ICompiledStub<T, RepeatEnumerable<T>>
//     {
//         private readonly T element;
//         private readonly int count;
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public RepeatEnumerable(T element, int count)
//         {
//             this.element = element;
//             this.count = count;
//         }
//
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public Enumerator GetEnumerator() => new Enumerator(this);
//
//         public struct Enumerator : IEnumerator<T>
//         {
//             private T element;
//             private int count;
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public Enumerator(in RepeatEnumerable<T> parent)
//             {
//                 element = parent.element;
//                 count = parent.count;
//             }
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public bool MoveNext()
//             {
//                 return --count >= 0;
//             }
//
//             public T Current
//             {
//                 [MethodImpl(MethodImplOptions.AggressiveInlining)]
//                 get => element;
//             }
//
//             object IEnumerator.Current => Current!;
//
//             public void Reset() => throw new NotImplementedException();
//
//             [MethodImpl(MethodImplOptions.AggressiveInlining)]
//             public void Dispose() { }
//         }
//     }
// }
//
// namespace Cathei.LinqGen
// {
//     public static partial class GenEnumerable
//     {
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         public static RepeatEnumerable<T> Repeat<T>(T element, int count)
//         {
//             return new RepeatEnumerable<T>(element, count);
//         }
//     }
// }