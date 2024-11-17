using Microsoft.AspNetCore.Authorization;
using Reach.Security;

namespace Reach.EditorApp.Security;

internal class TenantAuthorizationHandler : AuthorizationHandler<TenantRequiredAttribute>
{
    private readonly ILogger<TenantAuthorizationHandler> _logger;

    public TenantAuthorizationHandler(ILogger<TenantAuthorizationHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TenantRequiredAttribute requirement)
    {
        if (context.Resource is not null && context.Resource is Microsoft.AspNetCore.Http.DefaultHttpContext httpContext)
        {
            _logger.LogInformation($"AUTHORIZATION HANDLER PATH: {httpContext.Request.Path.Value}");
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}