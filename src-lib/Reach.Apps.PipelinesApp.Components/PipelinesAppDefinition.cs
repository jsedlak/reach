using Reach.Applets;

namespace Reach.Apps.PipelinesApp.Components;

public static class PipelinesAppDefinition
{
    public static AppletDefinition Default => new()
    {
        Name = "Pipelines",
        Description = "Provides support for managing content & data pipelines.",
        Icon = "BookOpen",
        AppletComponentType = typeof(PipelinesEditor).AssemblyQualifiedName!,
        SettingsComponentType = typeof(PipelinesEditorSettings).AssemblyQualifiedName!
    };
}
