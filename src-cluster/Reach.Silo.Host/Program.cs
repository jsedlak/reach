using Petl.EventSourcing;
using Petl.EventSourcing.Providers;
using Reach.Silo.Content.Grains;
using Reach.Silo.Host.Extensions;
using Reach.Silo.Host.Queries;

var builder = WebApplication.CreateBuilder(args);

// add our aspire service defaults
builder.Services.AddCors();
builder.AddServiceDefaults();

// Grab our providers
builder.AddKeyedAzureTableClient("clustering");
builder.AddKeyedAzureBlobClient("grain-state");
builder.AddKeyedAzureTableClient("streaming");
builder.AddMongoDBClient("reach-mongo");

// Add our view repositories
builder.AddRepositories();

// Add Event Sourcing
builder.Services.AddOrleansSerializers();
builder.Services.AddMongoEventSourcing("reach");

var ehConnectionString = builder.Configuration.GetConnectionString("EventHubsConnectionString");

// Add our GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddType<EditorDefinitionQueries>()
    .AddType<FieldDefinitionQueries>();

// Add Microsoft Orleans with Dashboard
builder.UseOrleans(o =>
{
    o.UseDashboard(x => x.HostSelf = true);
});

var app = builder.Build();

// Grant access to the dashboard, we use the self-host option until the port stuff can be figured out
app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.Map("/dashboard", x => x.UseOrleansDashboard());

// Map our grain endpoints using their interfaces
app.MapGrainEndpoint<IFieldDefinitionGrain>("field-definitions");
app.MapGrainEndpoint<IEditorDefinitionGrain>("editor-definitions");
app.MapGraphQL().RequireCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

await app.RunAsync();

