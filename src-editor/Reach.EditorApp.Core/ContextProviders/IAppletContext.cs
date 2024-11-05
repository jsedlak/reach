using Reach.Applets;

namespace Reach.EditorApp.ContextProviders;

/// <summary>
/// Provides a basis for maintaining contextual data about the state
/// of which applet is being used.
/// </summary>
public interface IAppletContext
{
    /// <summary>
    /// Handles when the current applet has changed.
    /// </summary>
    event EventHandler<AppletDefinition> AppletChanged;

    /// <summary>
    /// Sets the current applet based on the provided id
    /// </summary>
    /// <param name="appletId"></param>
    /// <returns></returns>
    Task SetAppletAsync(Guid appletId);

    /// <summary>
    /// Gets or Sets the set of applets that are available
    /// </summary>
    IEnumerable<AppletDefinition> Applets { get; set; }

    /// <summary>
    /// Gets or Sets the current applet
    /// </summary>
    AppletDefinition CurrentApplet { get; set; }
}