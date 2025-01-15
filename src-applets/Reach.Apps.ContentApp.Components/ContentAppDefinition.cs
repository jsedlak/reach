using Microsoft.Extensions.DependencyInjection;
using Reach.Applets;
using Reach.Apps.ContentApp.Components.Pages;
using Reach.Platform.Services;

namespace Reach.Apps.ContentApp.Components;

[AppletInitializer("Content")]
public class ContentAppDefinition : IAppletInitializer
{
    public static AppletDefinition Default => new()
    {
        Name = "Content",
        Description = "Provides access to creating and managing the core building blocks of the Reach platform.",
        Icon = "BookOpen",
        BaseUrl = "content",
        AppletComponentType = typeof(ContentEditorPage).AssemblyQualifiedName!,
        SettingsComponentType = typeof(ContentEditorSettings).AssemblyQualifiedName!
    };

    public AppletDefinition CreateDefinition() => Default;

    public void RegisterServices(IServiceCollection services)
    {
    }
}
