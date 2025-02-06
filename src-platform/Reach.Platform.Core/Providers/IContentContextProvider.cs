using Reach.Content.Views;

namespace Reach.Platform.Providers;

public interface IContentContextProvider : IContextProvider
{
    IEnumerable<ComponentDefinitionView> ComponentDefinitions { get; }

    IEnumerable<EditorDefinitionView> EditorDefinitions { get; }

    IEnumerable<FieldDefinitionView> FieldDefinitions { get; }
}