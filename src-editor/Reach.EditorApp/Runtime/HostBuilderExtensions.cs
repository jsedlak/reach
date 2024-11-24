using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Reach.Applets;
using Reach.Security;
using System.Reflection;
using System.Security.Claims;

namespace Reach.EditorApp.Runtime;

/// <summary>
/// Provides methods to extend the functionality of startup
/// </summary>
public static class HostExtensions
{
    /// <summary>
    /// Adds all applets to the platform
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddApplets(this WebApplicationBuilder builder, params Assembly[] assemblies)
    {
        var initializers = new List<IAppletInitializer>();

        foreach(var assembly in assemblies)
        {
            // get all types that implement IAppletInitializer from the assembly and have the custom attribute AppletInitializer
            var types = assembly.GetTypes()
                .Where(t => 
                    t.GetInterfaces().Contains(typeof(IAppletInitializer)) && 
                    t.GetCustomAttribute<AppletInitializerAttribute>() != null
                )
                .DistinctBy(t => t.GetCustomAttribute<AppletInitializerAttribute>()!.Name);

            if (types is null || !types.Any())
            {
                continue;
            }

            foreach(var initializerType in types)
            {
                // TODO: Do we need to check for empty constructor, throw error, or log?
                var initializer = Activator.CreateInstance(initializerType) as IAppletInitializer;
                if (initializer is null)
                {
                    continue;
                }

                // register custom services
                initializer.RegisterServer(builder.Services);

                initializers.Add(initializer);
            }
        }

        return builder;
    }

    /// <summary>
    /// Adds Auth0 authentication
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAuth0WebApp(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuth0WebAppAuthentication(options =>
        {
            options.Domain = builder.Configuration["Auth0:Domain"];
            options.ClientId = builder.Configuration["Auth0:ClientId"];
            options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
            options.OpenIdConnectEvents = new OpenIdConnectEvents
            {
                OnTokenValidated = async context =>
                {
                    var token = context.TokenEndpointResponse.AccessToken;

                    var appIdentity = new ClaimsIdentity();
                    appIdentity.AddClaim(new Claim(CustomClaimTypes.AccessToken, token));

                    context.Principal.AddIdentity(appIdentity);
                }
            };
        })
        .WithAccessToken(options =>
        {
            options.Audience = builder.Configuration["Auth0:Audience"];
        });

        builder.Services.AddCascadingAuthenticationState();
        // builder.Services.AddAuthenticationStateSerialization();
        // builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

        return builder;
    }
}
