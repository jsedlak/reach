using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reach.Pipelines
{
    internal sealed class PipelineMapStepComparer : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            if (x == null) { return 1; }
            if (y == null) { return -1; }

            var left = x.GetCustomAttribute<PipelineAttribute>()?.Order ?? 0;
            var right = x.GetCustomAttribute<PipelineAttribute>()?.Order ?? 0;

            return left.CompareTo(right);
        }
    }
}
