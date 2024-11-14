using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Reach.EditorApp.Client.Authentication;
using Reach.EditorApp.Client.Runtime;
using Tazor.Components;
using Tazor.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.AddApplets(
    typeof(Reach.Apps.ContentApp.Components.ContentEditor).Assembly
);

// Add our HTTP clients!
// TODO: Move to Service Defaults
builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri("https://localhost:7208/"));
builder.Services.AddHttpClient("graphql", client => client.BaseAddress = new Uri("https://localhost:7208/"));

// Add our theming stuff
builder.Services.AddTazor()
    .Build();

/* ################################# */
/* BUILD AND EXECUTE THE APPLICATION */
/* ################################# */
var app = builder.Build();

// initiate theming
await app.UseTazor();

await app.RunAsync();
