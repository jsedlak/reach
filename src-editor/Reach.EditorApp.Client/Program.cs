using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Reach.Apps.ContentApp.Components.Pages;
using Reach.Components.Context;
using Reach.EditorApp.Client.Runtime;
using Reach.EditorApp.Client.Services;
using Reach.Membership.Views;
using Reach.Orchestration;
using Tazor.Components;
using Tazor.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

/* #################################### */
/* BUILD & CONFIGURE THE APPLICATION    */
/* #################################### */

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.AddApplets(
    typeof(ContentEditorPage).Assembly
);

builder.Services.AddScoped<ITenantContext, TenantContext>(sp =>
{
    var repoSvc = sp.GetRequiredService<HttpTenantService>();
    return new TenantContext(
        () => repoSvc.GetTenantsForUserAsync(),
        sp.GetRequiredService<NavigationManager>()
    );
});

// Add our HTTP clients!
builder.Services.AddHttpClient("global", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri("https://localhost:7208/"));
builder.Services.AddHttpClient("graphql", client => client.BaseAddress = new Uri("https://localhost:7208/"));

// Add our repositories & services
builder.Services.AddSingleton<HttpTenantService>();
builder.Services.AddSingleton<HttpRegionService>();

// Add our region generation, we're running local only so use our host
builder.Services.WithPathRegionUrls(builder.HostEnvironment.BaseAddress, "api", "graphql"); // Configure for localhost
// builder.Services.WithPathRegionUrls("https://{region}.reachcms.io/", "api", "graphql");  // Configure for hosted platform (multi-region)

// Add our theming stuff
builder.Services.AddTazor()
    .Build();

/* #################################### */
/* EXECUTE THE APPLICATION              */
/* #################################### */
var app = builder.Build();

// initiate theming
await app.UseTazor();

await app.RunAsync();
