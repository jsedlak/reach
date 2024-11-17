using Microsoft.Extensions.DependencyInjection;
using Reach.Applets;
using Reach.Apps.ContentApp.Services;

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
        AppletComponentType = typeof(ContentEditor).AssemblyQualifiedName!,
        SettingsComponentType = typeof(ContentEditorSettings).AssemblyQualifiedName!
    };

    public AppletDefinition CreateDefinition() => Default;

    public void RegisterServer(IServiceCollection services)
    {
        services.AddTransient<EditorDefinitionService>();
        services.AddTransient<FieldDefinitionService>();
    }

    public void RegisterClient(IServiceCollection services)
    {
        services.AddTransient<EditorDefinitionService>();
        services.AddTransient<FieldDefinitionService>();
    }
}
