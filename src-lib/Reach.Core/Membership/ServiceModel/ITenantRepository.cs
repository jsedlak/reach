using Reach.Cqrs;
using Reach.Membership.Model;

namespace Reach.Membership.ServiceModel;

public interface ITenantRepository
{
    Task<IEnumerable<Tenant>> GetAllForUser(string userId);

    Task<CommandResponse> Create(Tenant tenant);
}