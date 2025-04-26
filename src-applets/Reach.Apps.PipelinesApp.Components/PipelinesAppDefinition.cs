using Microsoft.Extensions.DependencyInjection;
using Reach.Applets;

namespace Reach.Apps.PipelinesApp.Components;

[AppletInitializer("Pipelines")]
public class PipelinesAppDefinition : IAppletInitializer
{
    public static AppletDefinition Default => new()
    {
        Name = "Pipelines",
        Description = "Provides support for managing content & data pipelines.",
        Icon = "BookOpen",
        BaseUrl = "pipelines",
        AppletComponentType = typeof(PipelinesEditor).AssemblyQualifiedName!,
        SettingsComponentType = typeof(PipelinesEditorSettings).AssemblyQualifiedName!
    };


    public AppletDefinition CreateDefinition() => Default;

    public void RegisterServices(IServiceCollection services)
    {
    }
}
