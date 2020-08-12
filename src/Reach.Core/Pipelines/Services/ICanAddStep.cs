using Reach.Pipelines.ServiceModel;
using System;

namespace Reach.Pipelines
{
    public interface ICanAddStep
    {
        ICanAddStep WithStep<TStep>();

        ICanAddStep WithStep(Type type);
    }
}
