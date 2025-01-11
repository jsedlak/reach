using Reach.Cqrs;
using Reach.Membership.Views;

namespace Reach.Silo.Membership.ServiceModel;

public interface IAccountViewWriteRepository
{
    Task<CommandResponse> UpsertSettings(AccountSettingsView settings);
}
