using Microsoft.Extensions.Logging;
using Reach.Membership.Events;
using Reach.Silo.Content.Grains;
using Reach.Silo.Membership.GrainModel;
using Reach.Silo.Membership.ServiceModel;

namespace Reach.Silo.Membership.Grains;

[ImplicitStreamSubscription(MembershipGrainConstants.Account_EventStream)]
internal class AccountViewGrain : SubscribedViewGrain<BaseAccountEvent>, IAccountViewGrain
{
    private readonly IAccountViewReadRepository _accountViewReadRepository;
    private readonly IAccountViewWriteRepository _accountViewWriteRepository;

    public AccountViewGrain(
        IAccountViewReadRepository accountViewReadRepository,
        IAccountViewWriteRepository accountViewWriteRepository,
        ILogger<AccountViewGrain> logger
    ) : base(logger)
    {
        _accountViewReadRepository = accountViewReadRepository;
        _accountViewWriteRepository = accountViewWriteRepository;
    }

    public async Task Handle(SkipOnboardingFlagSetEvent @event)
    {
        var result = await _accountViewReadRepository.GetSettings(@event.AccountId);

        if(result is null)
        {
            result = new()
            {
                AccountId = @event.AccountId
            };
        }

        result.SkipsOnboarding = @event.SkipOnboarding;

        await _accountViewWriteRepository.UpsertSettings(result);
    }
}
