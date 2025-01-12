using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Reach.Platform.Services;

namespace Reach.Platform;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var apiHost = configuration.GetSection("Api").GetValue<string>("Host") ?? "https://silo";

        services.AddScoped<CustomAuthorizationMessageHandler>(sp =>
            new CustomAuthorizationMessageHandler(
                [apiHost], 
                sp.GetRequiredService<IAccessTokenProvider>(),
                sp.GetRequiredService<NavigationManager>()
            )
        );

        services.AddHttpClient("api",
            client => client.BaseAddress = new Uri(apiHost)
        ).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

        services.AddHttpClient("graphql",
            client => client.BaseAddress = new Uri(apiHost)
        ).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

        return services;
    }
}