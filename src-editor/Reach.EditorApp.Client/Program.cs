using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Reach.EditorApp.Client.Authentication;
using Tazor.Components;
using Tazor.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

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
