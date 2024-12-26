using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Reach.Membership;

namespace Reach.Silo.Host.Queries;

/// <summary>
/// Translates header information into injectable data for GraphQL queries
/// </summary>
public sealed class OrganizationHubInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, OperationRequestBuilder requestBuilder, CancellationToken cancellationToken)
    {
        if(context.Request.Headers.TryGetValue(MembershipHttpConstants.OrganizationIdHeader, out var organizationId))
        {
            requestBuilder.SetGlobalState("organizationId", (string)organizationId!);
        }

        if (context.Request.Headers.TryGetValue(MembershipHttpConstants.HubIdHeader, out var hubId))
        {
            requestBuilder.SetGlobalState("hubId", (string)hubId!);
        }

        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}