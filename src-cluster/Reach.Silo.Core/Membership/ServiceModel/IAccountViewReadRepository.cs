using Reach.Membership.Views;

namespace Reach.Silo.Membership.ServiceModel;

public interface IAccountViewReadRepository
{
    Task<AccountSettingsView?> GetSettings(string accountId);
}
