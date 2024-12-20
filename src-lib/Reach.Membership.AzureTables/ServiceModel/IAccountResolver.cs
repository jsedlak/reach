namespace Reach.Membership.ServiceModel;

public interface IAccountResolver
{
    Task<string> GetCurrentAccountAsync();
}
