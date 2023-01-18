// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

#nullable enable

using System;

namespace Cathei.LinqGen.Hidden
{
    /// <summary>
    /// Methods that throws Exception cannot be inlined.
    /// So we will have this class to wrap exception-throwing.
    /// </summary>
    public static class ExceptionUtils
    {
        public static void ThrowInvalidOperation(string? message = null)
        {
            throw new InvalidOperationException(message);
        }
    }
}