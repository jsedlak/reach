using Reach.Cqrs;
using Reach.Membership.Model;
using Reach.Membership.ServiceModel;
using Reach.Membership.Views;
using Reach.Orchestration.ServiceModel;

namespace Reach.Membership.Services;

public class AzureTablesOrganizationService : IOrganizationService
{
    private readonly IOrganizationReadRepository _organizationReadRepository;
    private readonly IOrganizationWriteRepository _organizationWriteRepository;
    private readonly IAccountResolver _accountResolver;
    private readonly IRegionProvider _regionProvider;

    public AzureTablesOrganizationService(
        IAccountResolver accountResolver,
        IRegionProvider regionProvider,
        IOrganizationReadRepository organizationReadRepository,
        IOrganizationWriteRepository organizationWriteRepository)
    {
        _regionProvider = regionProvider;
        _organizationReadRepository = organizationReadRepository;
        _organizationWriteRepository = organizationWriteRepository;
        _accountResolver = accountResolver;
    }

    public async Task<CommandResponse> CreateOrganization(Guid id, string name, string slug, string ownerId)
    {
        await _organizationWriteRepository.CreateOrganization(new Organization
        {
            Id = id,
            Name = name,
            Slug = slug,
            OwnerIdentifier = ownerId,
            PartitionKey = AzureTableServiceConstants.DefaultPartitionKey,
            RowKey = id.ToString()
        });

        return new CommandResponse { IsSuccess = true };
    }

    public async Task<CommandResponse> CreateHub(Guid id, Guid organizationId, string name, string slug, string iconUrl, string regionKey)
    {
        await _organizationWriteRepository.AddHubToOrganization(new Hub
        {
            Id = id,
            OrganizationId = organizationId,
            Name = name,
            Slug = slug,
            IconUrl = iconUrl,
            RegionKey = regionKey,
            PartitionKey = AzureTableServiceConstants.DefaultPartitionKey,
            RowKey = id.ToString()
        });

        return new CommandResponse { IsSuccess = true };
    }

    public async Task<IEnumerable<AvailableOrganizationView>> GetOrganizationsForUserAsync()
    {
        var userId = await _accountResolver.GetCurrentAccountAsync();

        return await GetOrganizationsForUserAsync(userId);
    }

    public async Task<IEnumerable<AvailableOrganizationView>> GetOrganizationsForUserAsync(string userId)
    {
        var organizations = (await _organizationReadRepository.GetOrganizationsForUser(userId)).ToArray();
        
        var hubs = (await _organizationReadRepository.GetHubsForOrganizations(organizations.Select(m => m.Id).ToList()))
            .ToArray()
            .Where(m => organizations.Any(o => o.Id == m.OrganizationId));

        var regions = await _regionProvider.GetAll();

        return organizations.Select(org =>
        {
            var hubViews = hubs
                .Where(m => m.OrganizationId == org.Id)
                .Select(hub => new AvailableHubView
                {
                    Id = hub.Id,
                    OrganizationId = hub.OrganizationId,
                    Name = hub.Name,
                    Slug = hub.Slug,
                    IconUrl = hub.IconUrl,
                    Region = regions.First(m => m.Key == hub.RegionKey)
                });

            return new AvailableOrganizationView
            {
                Id = org.Id,
                Name = org.Name,
                Slug = org.Slug,
                Hubs = hubViews.ToArray()
            };
        });
    }
}
