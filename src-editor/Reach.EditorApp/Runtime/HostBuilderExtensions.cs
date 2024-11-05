using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Applets;
using Reach.EditorApp.Authentication;
using Reach.EditorApp.ContextProviders;
using Reach.Security;
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
    public static WebApplicationBuilder AddApplets(this WebApplicationBuilder builder)
    {
        IEnumerable<AppletDefinition> applets = [
            Apps.ContentApp.Components.ContentAppDefinition.Default,
            Apps.PipelinesApp.Components.PipelinesAppDefinition.Default,
            Apps.EndpointsApp.Components.EndpointsAppDefinition.Default
        ];

        var ctx = new AppletContext
        {
            Applets = applets
        };

        builder.Services.AddCascadingValue<AppletContext>(sp => ctx);

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
        builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

        return builder;
    }
}
