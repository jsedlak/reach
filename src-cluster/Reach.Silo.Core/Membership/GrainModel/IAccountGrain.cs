using Reach.Cqrs;
using Reach.Membership.Commands;

namespace Reach.Silo.Membership.GrainModel;

public interface IAccountGrain : IGrainWithStringKey
{
    Task<CommandResponse> SetSkipOnboardingFlag(SetSkipOnboardingFlagCommand command);
}
