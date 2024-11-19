using Azure.Data.Tables;
using Microsoft.Extensions.DependencyInjection;
using Reach.Cqrs;
using Reach.Membership.Model;
using Reach.Membership.ServiceModel;

namespace Reach.Membership.Services;

public sealed class AzureTablesTenantRepository : ITenantRepository
{
    private TableServiceClient _tableServiceClient;

    public AzureTablesTenantRepository([FromKeyedServices("tenant-storage")]TableServiceClient tableServiceClient)
    {
        _tableServiceClient = tableServiceClient;
        _tableServiceClient.CreateTableIfNotExists("tenants");
    }

    public async Task<CommandResponse> Create(Tenant tenant)
    {   
        var tableClient = _tableServiceClient.GetTableClient("tenants");

        tenant.PartitionKey = "global";
        tenant.RowKey = tenant.Id.ToString();

        await tableClient.UpsertEntityAsync(tenant);
        
        return new CommandResponse {  IsSuccess = true };
    }

    public Task<IEnumerable<Tenant>> GetAllForUser(string userId)
    {
        var tableClient = _tableServiceClient.GetTableClient("tenants");
        var results = tableClient.QueryAsync<Tenant>(tenant => tenant.OwnerIdentifier == userId);
        return Task.FromResult(results.ToBlockingEnumerable());
    }
}
