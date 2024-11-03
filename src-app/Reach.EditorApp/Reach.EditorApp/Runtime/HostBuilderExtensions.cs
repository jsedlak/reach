using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.EditorApp.Authentication;
using Reach.Security;
using System.Security.Claims;

namespace Reach.EditorApp.Runtime;

public static class HostExtensions
{
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
