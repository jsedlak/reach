namespace Reach.Applets;

/// <inheritdoc />
public class AppletContext : IAppletContext
{
    /// <inheritdoc />
    public event EventHandler<AppletDefinition>? AppletChanged;

    /// <inheritdoc />
    public Task SetAppletAsync(Guid appletId)
    {
        var newApplet = Applets.FirstOrDefault(m => m.Id == appletId);
        
        if (newApplet != null)
        {
            CurrentApplet = newApplet;
            AppletChanged?.Invoke(this, newApplet);
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public IEnumerable<AppletDefinition> Applets { get; set; } = [];

    /// <inheritdoc />
    public AppletDefinition CurrentApplet { get; set; } = null!;
}
