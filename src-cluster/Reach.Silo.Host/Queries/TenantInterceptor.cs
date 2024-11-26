using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Reach.Membership;

namespace Reach.Silo.Host.Queries;

public sealed class TenantInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, OperationRequestBuilder requestBuilder, CancellationToken cancellationToken)
    {
        if(context.Request.Headers.TryGetValue(TenantHttpConstants.TenantIdHeader, out var tenantId))
        {
            requestBuilder.SetGlobalState("tenantId", (string)tenantId!);
        }

        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}