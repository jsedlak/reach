namespace Reach.Applets;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AppletInitializerAttribute : Attribute
{
    public AppletInitializerAttribute(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Gets or Sets the unique name for the applet which this class initializes
    /// </summary>
    public string Name { get; set; } = null!;
}
