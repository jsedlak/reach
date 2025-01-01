using Orleans.TestingHost;

namespace Reach.Silo.Tests;

public abstract class OrleansTestBase<TConfigurator> : IDisposable
    where TConfigurator : ISiloConfigurator, new()
{
    private readonly TestCluster _cluster;

    protected OrleansTestBase()
    {
        _cluster = new TestClusterBuilder()
            .AddSiloBuilderConfigurator<TConfigurator>()
            .Build();
        
        _cluster.Deploy();
    }

    public void Dispose()
    {
        _cluster.StopAllSilos();
    }

    protected IGrainFactory Grains => _cluster.GrainFactory;

    protected IServiceProvider Services => _cluster.ServiceProvider;
}