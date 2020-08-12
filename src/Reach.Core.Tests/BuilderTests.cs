using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reach.DataModel;
using Reach.Diagnostics;
using Reach.Pipelines;
using Reach.Pipelines.DataModel;
using Reach.Pipelines.ServiceModel;
using Reach.Pipelines.Services;
using SimpleInjector;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Reach.Core.Tests
{
    [TestClass]
    public class BuilderTests
    {
        private static Container GetContainer()
        {
            var container = new Container();
            container.Register<ILog, TraceLog>();
            return container;
        }

        [TestMethod]
        public async Task CanHandleMemoryConfig()
        {
            var builder = new PipelineExecutorBuilder()
                .UseMemoryPipelines();

            builder
                .Register<BasicPipeline>()
                .WithStep<BasicPipelineStep>();

            var executor = builder.Build(GetContainer());

            var result = await executor.ExecuteAsync(new BasicRequest { });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Events);
            Assert.IsTrue(result.Events.Count() == 1);
        }

        [TestMethod]
        public async Task CanAutoBuild()
        {
            var builder = new PipelineExecutorBuilder()
                .UseMemoryPipelines()
                .RegisterAssemblies(new[] { Assembly.GetExecutingAssembly() });

            var executor = builder.Build(GetContainer());

            var result = await executor.ExecuteAsync(new BasicRequest { });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Events);
            Assert.IsTrue(result.Events.Count() == 1);
        }

    }

    public class BasicRequest
    {
    }

    public class CreateTodoTask
    {
        public string Title { get; set; }
    }

    public class BasicPipeline : SteppedPipeline<BasicRequest>
    {
        public BasicPipeline(IEnumerable<object> steps) 
            : base(steps)
        {
        }
    }

    [Pipeline(typeof(IPipeline<BasicRequest>))]
    public class BasicPipelineStep : IPipelineStep<BasicRequest>, IPipelineStep<CreateTodoTask>
    {
        public Task<PipelineResult> ExecuteAsync(PipelineExecutionContext<CreateTodoTask> context)
        {
            throw new System.NotImplementedException();
        }

        public Task<PipelineResult> ExecuteAsync(PipelineExecutionContext<BasicRequest> context)
        {
            Trace.WriteLine("COMPLETED BASIC PIPELINE STEP");
            return Task.FromResult(
                PipelineResult.Success(new[] { new BasicEventHappened(1) })
            );
        }
    }

    public class BasicEventHappened : Event
    {
        public BasicEventHappened(int count)
        {
            RecordsCompleted = count;
        }

        public int RecordsCompleted { get; set; }
    }
}
