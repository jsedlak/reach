using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Reach.Components.Context;
using Reach.Platform.ServiceModel;
using Tazor.Components;
using Tazor.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

/* #################################### */
/* BUILD & CONFIGURE THE APPLICATION    */
/* #################################### */

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

//builder.AddApplets(
//    typeof(ContentEditorPage).Assembly
//);

// Add support for our tenant identification and selection
builder.Services.AddCascadingValue(static sp =>
    new CascadingValueSource<TenantContext>(new TenantContext(), false)
);

// Add our HTTP clients!
builder.Services.AddHttpClient("global", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri("https://localhost:7208/"));
builder.Services.AddHttpClient("graphql", client => client.BaseAddress = new Uri("https://localhost:7208/"));

// Add our repositories & services
//builder.Services.AddSingleton<IOrganizationService, HttpOrganizationService>();

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
