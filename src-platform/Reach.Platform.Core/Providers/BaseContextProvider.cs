namespace Reach.Platform.Providers;

internal abstract class BaseContextProvider : IContextProvider
{
    public abstract Task Refresh(Guid organizationId, Guid hubId);
    
    protected virtual void OnContextChanged()
    {
        ContextChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? ContextChanged;
}