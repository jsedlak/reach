using Reach.Pipelines.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reach.Pipelines
{
    public static class PipelineExecutorBuilderExtensions
    {
        public static ICanAddPipeline RegisterAssemblies(this ICanAddPipeline builder, IEnumerable<Assembly> assemblies = null)
        {
            if (assemblies == null)
            {
                assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            var basePipelineType = typeof(IPipeline<>);
            var pipelines = assemblies
                .SelectMany(m => 
                    m.GetTypes().Where(t => 
                        t.GetInterfaces().Any(i => i.IsGenericType && basePipelineType.IsAssignableFrom(i.GetGenericTypeDefinition()))
                    )
                );

            foreach (var pipelineType in pipelines)
            {
                var pipelineInterfaceTypes = pipelineType.GetInterfaces().Where(m => m.IsGenericType && basePipelineType.IsAssignableFrom(m.GetGenericTypeDefinition()));

                foreach (var interfaceType in pipelineInterfaceTypes)
                {
                    var map = builder.Register(pipelineType);

                    var matchingSteps = assemblies.SelectMany(m => m.GetTypes().Where(t =>
                    {
                        var attr = t.GetCustomAttribute<PipelineAttribute>();
                        return attr != null && interfaceType.IsAssignableFrom(attr.PipelineType);
                    }));

                    foreach(var step in matchingSteps)
                    {
                        map.WithStep(step);
                    }
                }
            }

            return builder;
        }
    }
}
