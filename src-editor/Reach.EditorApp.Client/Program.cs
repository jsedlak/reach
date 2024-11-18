using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Reach.EditorApp.Client.Runtime;
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
    typeof(Reach.Apps.ContentApp.Components.ContentEditor).Assembly
);

Console.WriteLine(builder.Configuration.GetDebugView());

// Add our HTTP clients!
builder.Services.AddHttpClient("api", client => client.BaseAddress = new Uri("https://localhost:7208/"));
builder.Services.AddHttpClient("graphql", client => client.BaseAddress = new Uri("https://localhost:7208/"));

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
