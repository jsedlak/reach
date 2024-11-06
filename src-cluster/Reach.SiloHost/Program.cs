using Microsoft.AspNetCore.Mvc;
using Reach.Content.Commands.Fields;
using Reach.Silo.Content.Grains;

var builder = WebApplication.CreateBuilder(args);

// add our aspire service defaults
builder.AddServiceDefaults();

// Grab our providers
builder.AddKeyedAzureTableClient("clustering");
builder.AddKeyedAzureBlobClient("grain-state");

builder.UseOrleans(o => o.UseDashboard(x => x.HostSelf = true));

var app = builder.Build();

app.Map("/dashboard", x => x.UseOrleansDashboard());

app.MapGet("/test", async ([FromServices] IClusterClient cluster) =>
{
    var fieldDefinitionId = Guid.NewGuid();
    var fieldDefinition = cluster.GetGrain<IFieldDefinitionGrain>(fieldDefinitionId);

    var result = await fieldDefinition.Create(new CreateFieldDefinitionCommand(fieldDefinitionId));

    return result;
});
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

