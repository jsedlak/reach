using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Reach.Platform;
using Reach.Platform.ServiceModel;
using Reach.Platform.Services;
using Tazor.Components;
using Tazor.Wasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add HTTP clients
var apiHost = builder.Configuration.GetSection("Api").GetValue<string>("Host") ?? "https://silo";

builder.Services.AddScoped<CustomAuthorizationMessageHandler>(sp =>
    new CustomAuthorizationMessageHandler(
        [apiHost], 
        sp.GetRequiredService<IAccessTokenProvider>(),
        sp.GetRequiredService<NavigationManager>()
    )
);

builder.Services.AddHttpClient("api",
      client => client.BaseAddress = new Uri(apiHost)
    ).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddHttpClient("graphql",
      client => client.BaseAddress = new Uri(apiHost)
    ).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

// Add authentication
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
});

// Add reach services
builder.Services.AddScoped<IMembershipService, HttpMembershipService>();
builder.Services.AddScoped<IOrganizationService, HttpOrganizationService>();

// Adds Tazor
builder.Services
    .AddTazor()
    .Build();

// Build and run the app
var app =  builder.Build();

await app.UseTazor();
await app.RunAsync();