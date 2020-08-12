using System;

namespace Reach.Pipelines
{
    public class PipelineAttribute : Attribute
    {
        public PipelineAttribute(Type pipelineType, int order = 0)
        {
            Order = order;
            PipelineType = pipelineType;
        }

        public Type PipelineType { get; set; }

        public int Order { get; set; }
    }
}
