using Reach.Cqrs;
using Reach.Membership.Commands;
using Reach.Membership.Events;
using Reach.Silo.Content.Grains;
using Reach.Silo.Membership.GrainModel;
using Reach.Silo.Membership.Model;

namespace Reach.Silo.Membership.Grains;

internal class AccountGrain : StreamingEventSourcedGrain<Account, BaseAccountEvent>,
    IAccountGrain
{
    public AccountGrain() 
        : base(MembershipGrainConstants.Account_EventStream)
    {
    }

    public async Task<CommandResponse> SetSkipOnboardingFlag(SetSkipOnboardingFlagCommand command)
    {
        await Raise(new SkipOnboardingFlagSetEvent(this.GetPrimaryKeyString())
        {
            SkipOnboarding = command.SkipOnboarding
        });

        return CommandResponse.Success();
    }
}
