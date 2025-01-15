using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Petl.EventSourcing;
using Petl.EventSourcing.Providers;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Host.Extensions;
using Reach.Silo.Host.Queries;
using Reach.Silo;
using Reach.Silo.Content.ServiceModel;
using Reach.Silo.Content.Services;

var builder = WebApplication.CreateBuilder(args);

// add our aspire service defaults
builder.Services.AddCors();
builder.AddServiceDefaults();

// Add Authentication/Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = $"{builder.Configuration["Auth0:Domain"]}";
        c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}"
        };
    });

// Routing services
builder.Services.AddControllers();

// Grab our providers
builder.AddKeyedAzureTableClient("clustering");
builder.AddKeyedAzureBlobClient("grain-state");
builder.AddKeyedAzureTableClient("streaming");
builder.AddMongoDBClient("reach-mongo");

// Add multi-tenancy support!
// TODO: Figure out how we can pass the cluster endpoint dynamically
builder.AddPostgresMembership("membership-database");

// Add our view repositories
builder.Services.AddMongoRepositories("reach");
builder.Services.AddScoped<IEditorViewReadRepository, StaticEditorViewReadRepository>();

// Add Event Sourcing
builder.Services.AddOrleansSerializers();
builder.Services.AddMongoEventSourcing("reach");

// var ehConnectionString = builder.Configuration.GetConnectionString("EventHubsConnectionString");

// Add our GraphQL
builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType(q => q.Name("Query"))
    .AddType<EditorDefinitionQueries>()
    .AddType<FieldDefinitionQueries>()
    .AddType<ComponentDefinitionQueries>()
    .AddType<ComponentQueries>()
    .AddType<RendererDefinitionQueries>()
    .AddType<OrganizationQueries>()
    .AddType<EditorQueries>()
    .AddHttpRequestInterceptor<OrganizationHubInterceptor>();

// Add Microsoft Orleans with Dashboard
builder.UseOrleans(o =>
{
    // o.UseDashboard(x => x.HostSelf = true);
});

var app = builder.Build();

// Add Auth middleware
app.UseAuthentication();
app.UseAuthorization();

// Grant access to the dashboard, we use the self-host option until the port stuff can be figured out
app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// app.Map("/dashboard", x => x.UseOrleansDashboard());

// Map our grain endpoints using their interfaces
app.MapGrainEndpoint<IFieldDefinitionGrain>("field-definitions");
app.MapGrainEndpoint<IEditorDefinitionGrain>("editor-definitions");
app.MapGrainEndpoint<IComponentDefinitionGrain>("component-definitions");
app.MapGrainEndpoint<IComponentGrain>("components");
app.MapGraphQL();
    //.RequireCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
    //.RequireAuthorization();

// Map controllers
app.MapControllers();

// configure storing guids as strings
// TODO: Remove for prod performance
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

await app.RunAsync();

