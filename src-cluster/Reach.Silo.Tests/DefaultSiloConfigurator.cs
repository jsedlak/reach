using Orleans.TestingHost;
using Petl.EventSourcing;
using Reach.Silo.Content.Grains;

namespace Reach.Silo.Tests;

public class DefaultSiloConfigurator : ISiloConfigurator
{
    public virtual void Configure(ISiloBuilder siloBuilder)
    {
        siloBuilder.Services.AddOrleansSerializers();
        siloBuilder.Services.AddMemoryEventSourcing();
        siloBuilder.UseInMemoryReminderService();
        siloBuilder.AddMemoryStreams("StreamProvider");
        siloBuilder.AddMemoryGrainStorageAsDefault();
        siloBuilder.AddMemoryGrainStorage("PubSubStore");
    }
}