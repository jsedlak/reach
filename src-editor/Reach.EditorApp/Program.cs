using Reach.EditorApp.Components;
using Reach.EditorApp.Runtime;
using Tazor.Components;
using Microsoft.AspNetCore.Authorization;
using Reach.EditorApp.Security;
using Reach.Orchestration;
using Reach.Orchestration.Model;
using Reach.Membership.ServiceModel;
using Reach.Membership.Services;
using Reach.EditorApp.Services;
using Reach.Apps.ContentApp.Components.Pages;
using Microsoft.AspNetCore.Components;
using Reach.Components.Context;
using Reach.EditorApp.ServiceModel;

var builder = WebApplication.CreateBuilder(args);

/* #################################### */
/* BUILD THE APPLICATION                */
/* #################################### */

// Add our aspire hook
builder.AddServiceDefaults();
builder.Services.AddHttpForwarderWithServiceDiscovery();

// grab storage provider
//builder.AddAzureTableClient("tenant-storage", settings => settings.)
builder.AddKeyedAzureTableClient("tenant-storage");

// Add multi-tenancy support!
// TODO: Figure out how we can pass the cluster endpoint dynamically
builder.Services.WithInMemoryRegions(new Region { Id = Guid.Empty, Name = "Global", Key = "global" });
builder.Services.WithPathRegionUrls("https://localhost:7208/", "api", "graphql");
builder.Services.AddScoped<ITenantRepository, AzureTablesTenantRepository>();

// Add our HTTP clients!
// TODO: Move to Service Defaults
builder.Services.AddHttpClient("global", client => client.BaseAddress = new Uri("https://reach-silo/")).AddServiceDiscovery();
builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri("https://reach-silo/")).AddServiceDiscovery();
builder.Services.AddHttpClient("graphql", client => client.BaseAddress = new Uri("https://reach-silo/")).AddServiceDiscovery();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(options => options.SerializeAllClaims = true);

builder.Services.AddControllers();

// Configure authentication
builder.AddAuth0WebApp();

// Configure authorization
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, TenantAuthorizationHandler>();

// Add support for our tenant identification and selection
builder.Services.AddCascadingValue(static sp => 
    new CascadingValueSource<TenantContext>(new TenantContext(), false)
);

// Add our repositories
builder.Services.AddScoped<ITenantService, RepositoryTenantService>();
builder.Services.AddScoped<IRegionService, ProviderRegionService>();

// Add our cascading contexts
builder.AddApplets(
    typeof(ContentEditorPage).Assembly
);

// Add our theming stuff
builder.Services.AddTazorServer().Build();

Console.WriteLine(builder.Configuration.GetDebugView());

/* #################################### */
/* EXECUTE THE APPLICATION              */
/* #################################### */
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Add our auth0 callback endpoints 
app.UseAuth0();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Reach.EditorApp.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(Reach.Apps.ContentApp.Components._Imports).Assembly);

app.MapControllers();

// TODO: Formalize into an extension (MapClusterProxy)
app.MapForwarder("/api", "https://reach-silo/api").RequireAuthorization();
app.MapForwarder("/graphql", "https://reach-silo/graphql").RequireAuthorization();

app.Run();
