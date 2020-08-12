using Reach.Pipelines.ServiceModel;
using System;
using System.Collections.Generic;

namespace Reach.Pipelines
{
    public sealed class PipelineMap : ICanAddStep
    {
        private readonly IComparer<Type> _comparer = new PipelineMapStepComparer();
        private readonly List<Type> _steps = new List<Type>();

        public PipelineMap(Type pipelineType)
        {
            Pipeline = pipelineType;
        }

        public ICanAddStep WithStep<TStep>()
        {
            return WithStep(typeof(TStep));
        }

        public ICanAddStep WithStep(Type type)
        {
            _steps.Add(type);
            _steps.Sort(_comparer);
            return this;
        }

        public Type Pipeline { get; private set; }

        public IEnumerable<Type> Steps { get { return _steps; } }
    }
}
