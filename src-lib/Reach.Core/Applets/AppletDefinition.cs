namespace Reach.Applets;

/// <summary>
/// Describes an applet that can be added and interacted 
/// with through the core platform
/// </summary>
public class AppletDefinition
{
    /// <summary>
    /// Gets or Sets a unique identifier representing this applet
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or Sets the name of the applet
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or Sets a brief description of the applet
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the applet's icon 
    /// </summary>
    public string Icon { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the endpoint at which this applet is hosted
    /// </summary>
    public string BaseUrl { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the component type used to render the applet
    /// </summary>
    public string AppletComponentType { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the component type used to render the applet's settings
    /// </summary>
    public string SettingsComponentType { get; set; } = null!;
}
