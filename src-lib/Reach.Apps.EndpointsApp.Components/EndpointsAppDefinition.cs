using Reach.Applets;

namespace Reach.Apps.EndpointsApp.Components;

public static class EndpointsAppDefinition
{
    public static AppletDefinition Default => new()
    {
        Name = "Endpoints",
        Description = "Provides support for defining data endpoints.",
        Icon = "BookOpen",
        BaseUrl = "endpoints",
        AppletComponentType = typeof(EndpointsEditor).AssemblyQualifiedName!,
        SettingsComponentType = typeof(EndpointsEditorSettings).AssemblyQualifiedName!
    };
}
