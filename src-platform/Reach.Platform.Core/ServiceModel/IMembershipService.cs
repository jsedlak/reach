using Reach.Cqrs;
using Reach.Membership.Commands;
using Reach.Membership.Views;

namespace Reach.Platform.ServiceModel;

public interface IMembershipService
{
    Task<AccountSettingsView> GetAccountSettings();

    Task<CommandResponse> SetSkipOnboardingFlag(SetSkipOnboardingFlagCommand command);
}
