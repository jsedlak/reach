using Microsoft.Extensions.DependencyInjection;

namespace Reach.Applets;

/// <summary>
/// Initializes an applet into the ecosystem.
/// </summary>
public interface IAppletInitializer
{
    AppletDefinition CreateDefinition();

    void RegisterServer(IServiceCollection services);

    void RegisterClient(IServiceCollection services);
}