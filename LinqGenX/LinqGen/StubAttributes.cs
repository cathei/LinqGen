// LinqGen, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2023

using System;
using System.Collections;
using System.Collections.Generic;
using Cathei.LinqGen.Hidden;

namespace Cathei.LinqGen.Hidden
{
    public abstract class LinqGenStubAttribute : Attribute
    {
        public readonly string Node;

        protected LinqGenStubAttribute(string node)
        {
            Node = node;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class LinqGenGenerationAttribute : LinqGenStubAttribute
    {
        public LinqGenGenerationAttribute(string node) : base(node) { }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class LinqGenOperationAttribute : LinqGenStubAttribute
    {
        public LinqGenOperationAttribute(string node) : base(node) { }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class LinqGenEvaluationAttribute : LinqGenStubAttribute
    {
        public LinqGenEvaluationAttribute(string node) : base(node) { }
    }
}