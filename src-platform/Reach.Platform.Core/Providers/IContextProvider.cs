namespace Reach.Platform.Providers;

public interface IContextProvider
{
    Task Refresh(Guid organizationId, Guid hubId);
    
    event EventHandler? ContextChanged;
}