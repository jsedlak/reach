using Microsoft.AspNetCore.Mvc;
using Petl.EventSourcing;
using Petl.EventSourcing.Providers;
using Reach.Content.Commands.Fields;
using Reach.Silo.Content.Grains;
using Reach.Silo.Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

// add our aspire service defaults
builder.AddServiceDefaults();

// Grab our providers
builder.AddKeyedAzureTableClient("clustering");
builder.AddKeyedAzureBlobClient("grain-state");
builder.AddKeyedAzureTableClient("streaming");
builder.AddMongoDBClient("reach-mongo");
//builder.AddKeyedAzureEventProcessorClient("reach-event-hubs");
//builder.AddKeyedAzureEventHubProducerClient("reach-event-hubs", static settings =>
//{
//    settings.EventHubName = "ReachEvents";
//});
//builder.AddKeyedAzureEventHubConsumerClient("reach-event-hubs", static settings =>
//{
//    settings.EventHubName = "ReachEvents";
//});

// Add Event Sourcing
builder.Services.AddOrleansSerializers();
builder.Services.AddMongoEventSourcing("reach");

var ehConnectionString = builder.Configuration.GetConnectionString("EventHubsConnectionString");

// Add Microsoft Orleans
builder.UseOrleans(o =>
{
    o.AddEventHubStreams("StreamProvider", configurator =>
    {
        configurator.ConfigureEventHub(builder => builder.Configure(options =>
        {
            options.ConfigureEventHubConnection(
                ehConnectionString,
                "ReachEvents",
                "ReachEventsEditorGroup"
            );
        }));
    });

    o.UseDashboard(x => x.HostSelf = true);
});

var app = builder.Build();

app.Map("/dashboard", x => x.UseOrleansDashboard());

app.MapGet("/test", async ([FromServices] IClusterClient cluster) =>
{
    var fieldDefinitionId = Guid.NewGuid();
    var fieldDefinition = cluster.GetGrain<IFieldDefinitionGrain>(fieldDefinitionId);

    var result = await fieldDefinition.Create(new CreateFieldDefinitionCommand(fieldDefinitionId) { Name = "Test", EditorDefinitionId = Guid.NewGuid() });

    return result;
});

app.MapGrainEndpoint<IFieldDefinitionGrain>("field-definitions");

// app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

await app.RunAsync();

