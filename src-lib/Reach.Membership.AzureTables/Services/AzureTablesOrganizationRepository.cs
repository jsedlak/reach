using Azure.Data.Tables;
using Microsoft.Extensions.DependencyInjection;
using Reach.Membership.Model;
using Reach.Membership.ServiceModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Reach.Membership.Services;

public sealed class AzureTablesOrganizationRepository : IOrganizationReadRepository, IOrganizationWriteRepository
{
    public const string OrganizationsTable = "organizations";
    public const string HubsTable = "hubs";

    private TableServiceClient _tableServiceClient;

    public AzureTablesOrganizationRepository([FromKeyedServices("membership-storage")]TableServiceClient tableServiceClient)
    {
        _tableServiceClient = tableServiceClient;
        _tableServiceClient.CreateTableIfNotExists(OrganizationsTable);
        _tableServiceClient.CreateTableIfNotExists(HubsTable);
    }

    public async Task AddHubToOrganization(Hub hub)
    {
        var tableClient = _tableServiceClient.GetTableClient(HubsTable);

        hub.PartitionKey = AzureTableServiceConstants.DefaultPartitionKey;
        hub.RowKey = hub.Id.ToString();

        await tableClient.UpsertEntityAsync(hub);
    }

    public async Task CreateOrganization(Organization organization)
    {
        var tableClient = _tableServiceClient.GetTableClient(OrganizationsTable);

        organization.PartitionKey = AzureTableServiceConstants.DefaultPartitionKey;
        organization.RowKey = organization.Id.ToString();

        await tableClient.UpsertEntityAsync(organization);
    }

    public Task<IEnumerable<Organization>> GetOrganizationsForUser(string userId)
    {
        var tableClient = _tableServiceClient.GetTableClient(OrganizationsTable);
        var results = tableClient.QueryAsync<Organization>(m => m.OwnerIdentifier == userId);

        return Task.FromResult(results.ToBlockingEnumerable());
    }

    public Task<IEnumerable<Hub>> GetHubsForOrganization(Guid organizationId)
    {
        var tableClient = _tableServiceClient.GetTableClient(HubsTable);
        var results = tableClient.QueryAsync<Hub>(m => m.OrganizationId == organizationId);

        return Task.FromResult(results.ToBlockingEnumerable());
    }

    public Task<IEnumerable<Hub>> GetHubsForOrganizations(IEnumerable<Guid> organizationIds)
    {
        var tableClient = _tableServiceClient.GetTableClient(HubsTable);

        // TODO: Rewrite this for performance to not pull back all orgs
        var results = tableClient.QueryAsync<Hub>(m => true).ToBlockingEnumerable(); //.Where(m => organizationIds.Contains(m.Id));

        return Task.FromResult(results);
    }
}