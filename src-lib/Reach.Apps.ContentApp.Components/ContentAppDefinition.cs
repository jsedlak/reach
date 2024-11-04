using Reach.Applets;

namespace Reach.Apps.ContentApp.Components;

public static class ContentAppDefinition
{
    public static AppletDefinition Default => new()
    {
        Name = "Content",
        Description = "Provides access to creating and managing the core building blocks of the Reach platform.",
        Icon = "BookOpen",
        AppletComponentType = typeof(ContentEditor).AssemblyQualifiedName!,
        SettingsComponentType = typeof(ContentEditorSettings).AssemblyQualifiedName!
    };
}
