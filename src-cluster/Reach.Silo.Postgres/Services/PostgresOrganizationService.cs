using Microsoft.EntityFrameworkCore;
using Reach.Cqrs;
using Reach.Membership.Views;
using Reach.Silo.Data;
using Reach.Silo.Membership.ServiceModel;
using Reach.Silo.Model;

namespace Reach.Silo.Services;

internal class PostgresOrganizationService : IOrganizationService
{
    private readonly MembershipDbContext _membershipContext;

    public PostgresOrganizationService(MembershipDbContext membershipContext)
    {
        _membershipContext = membershipContext;

        _membershipContext.Database.EnsureCreated();
    }

    public async Task<CommandResponse> CreateHub(Guid id, Guid organizationId, string name, string slug, string iconUrl)
    {
        var organization = _membershipContext.Organizations.FirstOrDefault(m => m.Id == organizationId);

        if (organization == null)
        {
            // TODO: Add error message
            return new CommandResponse();
        }

        _membershipContext.Hubs.Add(new Hub
        {
            Id = id,
            OrganizationId = organizationId,
            Name = name,
            Slug = slug,
            IconUrl = iconUrl,
            OwnerIdentifier = ""
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
        var orgs = _membershipContext.Organizations
            .Where(m => m.OwnerIdentifier == userId)
            .Include(organization => organization.Hubs)
            .ToList()
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
                    OrganizationId = h.OrganizationId
                }).ToArray()
            });

        return orgs;
    }
}
