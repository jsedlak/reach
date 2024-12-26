using Reach.Cqrs;
using Reach.Membership.Postgres.Data;
using Reach.Membership.Postgres.Model;
using Reach.Membership.ServiceModel;
using Reach.Membership.Views;
using Reach.Orchestration.ServiceModel;

namespace Reach.Membership.Postgres.Services;

public class PostgresOrganizationService : IOrganizationService
{
    private readonly MembershipDbContext _membershipContext;
    private readonly IRegionProvider _regionProvider;

    public PostgresOrganizationService(MembershipDbContext membershipContext, IRegionProvider regionProvider)
    {
        _membershipContext = membershipContext;
        _regionProvider = regionProvider;
    }

    public async Task<CommandResponse> CreateHub(Guid id, Guid organizationId, string name, string slug, string iconUrl, string regionKey)
    {
        var organization = _membershipContext.Organizations.FirstOrDefault(m => m.Id == organizationId);

        if (organization == null)
        {
            // TODO: Add error message
            return new CommandResponse();
        }

        organization.Hubs.Add(new Hub
        {
            Id = id,
            OrganizationId = organizationId,
            Name = name,
            Slug = slug,
            IconUrl = iconUrl,
            OwnerIdentifier = "",
            RegionKey = regionKey
        });

        await _membershipContext.SaveChangesAsync();

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> CreateOrganization(Guid id, string name, string slug, string ownerId)
    {
        _membershipContext.Organizations.Add(new Organization
        {
            Id = id,
            Name = name,
            Slug = slug,
            OwnerIdentifier = ownerId
        });

        await _membershipContext.SaveChangesAsync();

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public Task<IEnumerable<AvailableOrganizationView>> GetOrganizationsForUserAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AvailableOrganizationView>> GetOrganizationsForUserAsync(string userId)
    {
        var regions = await _regionProvider.GetAll();

        var orgs = _membershipContext.Organizations
            .Where(m => m.OwnerIdentifier == userId)
            .Select(m => new AvailableOrganizationView
            {
                Id = m.Id,
                Name = m.Name,
                Slug = m.Slug,
                Hubs = m.Hubs.Select(h => new AvailableHubView
                {
                    Id = h.Id,
                    Name = h.Name,
                    Slug = h.Slug,
                    IconUrl = h.IconUrl,
                    OrganizationId = h.OrganizationId,
                    Region = regions.First(m => m.Key == h.RegionKey)
                }).ToArray()
            });

        return orgs;
    }
}
