using Reach.Pipelines.ServiceModel;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reach.Pipelines
{
    public class PipelineExecutorBuilder : ICanAddPipeline, ICanSetExecutorFactory
    {
        private List<PipelineMap> _pipelines = new List<PipelineMap>();
        private IPipelineExecutorFactory _pipelineExecutorFactory;

        public ICanAddPipeline RegisterFactory<TFactory>(TFactory factory) where TFactory : IPipelineExecutorFactory
        {
            _pipelineExecutorFactory = factory;
            return this;
        }

        public ICanAddStep Register<TPipeline>()
        {
            var newPipeline = new PipelineMap(typeof(TPipeline));
            _pipelines.Add(newPipeline);
            return newPipeline;
        }

        public ICanAddStep Register(Type type)
        {
            var newPipeline = new PipelineMap(type);
            _pipelines.Add(newPipeline);
            return newPipeline;
        }

        private IEnumerable<object> CompilePipelines(Container container)
        {
            foreach (var map in _pipelines)
            {
                object pipeline = null;
                try
                {
                    var steps = map.Steps.Select(m => Activator.CreateInstance(m));
                    pipeline = Activator.CreateInstance(map.Pipeline, new[] { steps });
                }
                catch (Exception ex)
                {
                    // TODO: log this exception
                    throw ex;
                }

                if (pipeline != null)
                {
                    yield return pipeline;
                }
            }
        }

        public IPipelineExecutor Build(Container container)
        {
            var compiledPipelines = CompilePipelines(container);
            return _pipelineExecutorFactory.CreateInstance(container, compiledPipelines);
        }

        
    }
}
