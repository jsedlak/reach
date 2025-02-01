using Reach.Cqrs;

namespace Reach.Silo.Membership.GrainModel;

public interface IHubManagementGrain : IGrainWithStringKey
{
    Task<CommandResponse> Initialize();

    Task<CommandResponse> Upgrade();
}
