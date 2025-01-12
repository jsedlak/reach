using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Reach.Platform;
using Tazor.Components;
using Tazor.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add HTTP clients
builder.Services.AddHttpClients(builder.Configuration);

// Add authentication
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
});

// Add reach services
builder.Services.AddPlatformServices();

// Adds Tazor
builder.Services
    .AddTazor()
    .Build();

// Build and run the app
var app =  builder.Build();

await app.UseTazor();
await app.RunAsync();