using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Reach.Orchestration.ServiceModel;
using Reach.Orchestration.Services;
using Reach.Platform;
using Tazor.Components;
using Tazor.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient("TenancyApi",
      client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
});

// Add reach services
builder.Services.AddScoped<IRegionUrlFormatter, PathBasedRegionUrlFormatter>(sp => new PathBasedRegionUrlFormatter(
    "https://localhost:7119", "https://localhost:7119", "api", "graphql")
);

// Adds Tazor
builder.Services
    .AddTazor()
    .Build();

// Build and run the app
var app =  builder.Build();

await app.UseTazor();
await app.RunAsync();